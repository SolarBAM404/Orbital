using OrbitalCore.Lex;
using OrbitalCore.Parse.Nodes;
using OrbitalCore.Parse.Nodes.Abstract;
using OrbitalCore.Parse.Nodes.BasicTypes;
using OrbitalCore.Parse.Nodes.BuiltInFunctions;
using OrbitalCore.Parse.Nodes.Expressions;
using OrbitalCore.Parse.Nodes.Statements;

namespace OrbitalCore.Parse;

public class Parser(List<Token> tokens)
{
    // Entry point for parsing a list of tokens into a queue of abstract syntax tree nodes
    public static Queue<AbstractTreeNode> Parse(List<Token> tokens)
    {
        Parser parser = new Parser(tokens);
        return parser.Parse();
    }

    private List<Token> _tokens = tokens;
    private int _position = 0;
    private Queue<AbstractTreeNode> _output = new();

    public static Scope CurrentScope { get; set; } = new();
    
    public static Dictionary<string, VariableNode> GlobalVariables { get; set; }= new();

    // Main parsing loop that processes tokens until the end is reached
    public Queue<AbstractTreeNode> Parse()
    {     
        while (!IsEnd())
        {
            AbstractTreeNode node = ParseExpression();
            if (node is not EmptyNode)
            {
                _output.Enqueue(node);
            }
        }
        
        return _output;
    }

    // Parses an expression based on operator precedence
    private AbstractTreeNode ParseExpression(int precedence = 0)
    {
        AbstractTreeNode left = ParsePrimary();
        
        if (left is OperatorNode operatorNode)
        {
            if (operatorNode.Value is TokenTypes.Probe or TokenTypes.Orbit)
            {
                return ParseStatement(operatorNode);
            }
        }

        while (true)
        {
            Token operatorToken = CurrentToken();
            int tokenPrecedence = Precedence(operatorToken);

            if (tokenPrecedence < precedence)
            {
                break;
            }
            
            if (operatorToken.TokenType == TokenTypes.Assignment)
            {
                if (left is not VariableNode variableNode)
                {
                    throw new Exception("Left side of assignment must be a variable");
                }
                
                Advance();
                AbstractTreeNode right = ParseExpression(tokenPrecedence + 1);
                
                return new AssignmentNode(variableNode.Name, right, CurrentScope);
            }
            else if (IsComparisonOperator(operatorToken.TokenType))
            {
                Advance();
                AbstractTreeNode right = ParseExpression(tokenPrecedence + 1);
                left = new ComparisonBinaryExpressionNode(left, right, operatorToken.TokenType);
            }
            else if (IsLogicalOperator(operatorToken.TokenType))
            {
                Advance();
                AbstractTreeNode right = ParseExpression(tokenPrecedence + 1);
                left = new LogicalBinaryExpressionNode(left, right, operatorToken.TokenType);
            }
            else if (IsMathematicalOperator(operatorToken.TokenType))
            {
                Advance();
                AbstractTreeNode right = ParseMathematicalExpression(tokenPrecedence + 1);
                
                AbstractTreeNode? leftNode = left;
                
                if (left is StringNode || right is StringNode)
                {
                    if (operatorToken.TokenType == TokenTypes.Gain)
                    {
                        left = new StringConcatenationNode(left, right);
                    }
                    else
                    {
                        throw new Exception("Invalid operation on strings");
                    }
                }
                else
                {
                    left = new MathematicalBinaryExpressionNode(left, right, operatorToken.TokenType);
                }
            }
            else
            {
                break;
            }
        }

        return left;
    }
    
    // Parses a mathematical expression based on operator precedence
    private AbstractTreeNode ParseMathematicalExpression(int precedence = 0)
    {
        AbstractTreeNode left = ParsePrimary();

        while (true)
        {
            Token operatorToken = CurrentToken();
            int tokenPrecedence = Precedence(operatorToken);

            if (tokenPrecedence < precedence || !IsMathematicalOperator(operatorToken.TokenType))
            {
                break;
            }
            
            Advance();
            AbstractTreeNode right = ParseMathematicalExpression(tokenPrecedence + 1);
            
            if (left is StringNode || right is StringNode)
            {
                if (operatorToken.TokenType == TokenTypes.Gain)
                {
                    left = new StringConcatenationNode(left, right);
                    return left;
                }
                else
                {
                    throw new Exception("Invalid operation on strings");
                }
            }
            
            left = new MathematicalBinaryExpressionNode(left, right, operatorToken.TokenType);
        }

        return left;
    }

    // Checks if the token is a logical operator
    private bool IsLogicalOperator(TokenTypes operatorTokenTokenType)
    {
        return operatorTokenTokenType switch
        {
            TokenTypes.Stable or TokenTypes.Path => true,
            _ => false
        };
    }

    // Checks if the token is a comparison operator
    private bool IsComparisonOperator(TokenTypes tokenType)
    {
        return tokenType switch
        {
            TokenTypes.Align or TokenTypes.Disrupt or TokenTypes.Above or TokenTypes.Below or TokenTypes.Safe or TokenTypes.Unsafe => true,
            _ => false
        };
    }

    // Checks if the token is a mathematical operator
    private bool IsMathematicalOperator(TokenTypes tokenType)
    {
        return tokenType switch
        {
            TokenTypes.Gain or TokenTypes.Drain or TokenTypes.Amplify or TokenTypes.Disperse => true,
            _ => false
        };
    }

    // Parses a primary expression (number, string, boolean, identifier, or parenthesized expression)
    private AbstractTreeNode ParsePrimary()
    {
        Token current = CurrentToken();
        Advance();

        switch (current.TokenType)
        {
            case TokenTypes.Number:
                return new NumberNode(current.Value);
            case TokenTypes.String:
                return new StringNode(current.Value);
            case TokenTypes.Boolean:
                return new BooleanNode(current.Value);
            case TokenTypes.Identifier:
                if (CheckForFunction())
                {
                    return ParseFunction();
                }
                
                VariableNode variableNode = new (current.Value, CurrentScope);
                if (CurrentScope.Exists(current.Value))
                {
                    return variableNode;
                }
                
                CurrentScope.DefineVariable(current.Value, null);
                // _output.Enqueue(variableNode);
                return variableNode;
            case TokenTypes.LeftParenthesis:
                AbstractTreeNode expression = ParseExpression();
                if (CurrentToken().TokenType != TokenTypes.RightParenthesis)
                {
                    throw new Exception("Expected ')'");
                }
                Advance();
                return expression;
            case TokenTypes.RightParenthesis:
                return ParsePrimary();
            case TokenTypes.LeftBrace:
                Queue<AbstractTreeNode> body = new();
                while (CurrentToken().TokenType != TokenTypes.RightBrace)
                {
                    AbstractTreeNode node = ParseExpression();
                    body.Enqueue(node);
                }
                Advance();
                return new BodyNode(body);
            case TokenTypes.SemiColon:
                return EmptyNode.Instance;
            case TokenTypes.EoF:
                return EndOfFileNode.Instance;
            case TokenTypes.Align:
            case TokenTypes.Disrupt:
            case TokenTypes.Above:
            case TokenTypes.Below:
            case TokenTypes.Safe:
            case TokenTypes.Unsafe:
            case TokenTypes.Stable:
            case TokenTypes.Path:
            case TokenTypes.Gain:
            case TokenTypes.Drain:
            case TokenTypes.Amplify:
            case TokenTypes.Disperse:
            case TokenTypes.Probe:
            case TokenTypes.Scan:
            case TokenTypes.Orbit:
                return new OperatorNode(current.TokenType);
            default:
                throw new Exception("Unexpected token: " + current.TokenType);
        }
    }

    // Parses a function call
    private AbstractFunctionNode ParseFunction()
    {
        Token functionToken = PreviousToken();
        string? functionName = functionToken.Value;

        if (CurrentToken().TokenType != TokenTypes.LeftParenthesis)
        {
            throw new Exception("Expected '('");
        }
        Advance(); // Skip '('

        List<AbstractTreeNode> arguments = new();
        while (CurrentToken().TokenType != TokenTypes.RightParenthesis)
        {
            AbstractTreeNode argument = ParseExpression();
            arguments.Add(argument);

            if (CurrentToken().TokenType == TokenTypes.Comma)
            {
                Advance();
            }
        }

        if (CurrentToken().TokenType != TokenTypes.RightParenthesis)
        {
            throw new Exception("Expected ')'");
        }

        AbstractFunctionNode abstractFunctionNode = BuiltInFunctions.GetFunction(functionName, arguments);
        return abstractFunctionNode;
    }
    
    private AbstractTreeNode ParseStatement(OperatorNode operatorNode)
    {
        return operatorNode.Value switch
        {
            TokenTypes.Probe => ParseIfElseStatement(),
            TokenTypes.Orbit => ParseWhileStatement(),
            _ => throw new Exception("Unexpected token: " + operatorNode.Value)
        };
    }

    public IfThenElseStatement ParseIfElseStatement()
    {
        
        if (CurrentToken().TokenType != TokenTypes.LeftParenthesis)
        {
            throw new Exception("Expected '('");
        }
        Advance(); // Consume the '(' token
        
        AbstractTreeNode condition = ParseExpression();
        
        if (CurrentToken().TokenType != TokenTypes.RightParenthesis)
        {
            throw new Exception("Expected ')'");
        }
        Advance(); // Consume the ')' token
        
        if (CurrentToken().TokenType != TokenTypes.LeftBrace)
        {
            throw new Exception("Expected '{'");
        }
        Queue<AbstractTreeNode> ifBody = new();
        Scope parentScope = CurrentScope;
        CurrentScope = new(parentScope);
        Advance(); // Consume the '{' token
        while (CurrentToken().TokenType != TokenTypes.RightBrace)
        {
            AbstractTreeNode node = ParseExpression();
            ifBody.Enqueue(node);
        }
        Advance(); // Consume the '}' token
        CurrentScope = parentScope;
        
        if (CurrentToken().TokenType != TokenTypes.Scan)
        {
            return new IfThenElseStatement(condition, new BodyNode(ifBody));
        }
        
        Advance(); // Consume the 'scan' token
        
        if (CurrentToken().TokenType != TokenTypes.LeftBrace)
        {
            throw new Exception("Expected '{'");
        }
        
        Queue<AbstractTreeNode> elseBody = new();
        CurrentScope = new(parentScope);
        Advance(); // Consume the '{' token
        while (CurrentToken().TokenType != TokenTypes.RightBrace)
        {
            AbstractTreeNode node = ParseExpression();
            elseBody.Enqueue(node);
        }
        Advance(); // Consume the '}' token
        CurrentScope = parentScope;
        
        return new IfThenElseStatement(condition, new BodyNode(ifBody), new BodyNode(elseBody));
    }

    private WhileStatementNode ParseWhileStatement()
    {
        if (CurrentToken().TokenType != TokenTypes.LeftParenthesis)
        {
            throw new Exception("Expected '('");
        }
        Advance(); // Consume the '(' token
        
        AbstractTreeNode condition = ParseExpression();
        
        if (CurrentToken().TokenType != TokenTypes.RightParenthesis)
        {
            throw new Exception("Expected ')'");
        }
        Advance(); // Consume the ')' token
        
        if (CurrentToken().TokenType != TokenTypes.LeftBrace)
        {
            throw new Exception("Expected '{'");
        }
        Queue<AbstractTreeNode> body = new();
        Scope parentScope = CurrentScope;
        CurrentScope = new(parentScope);
        Advance(); // Consume the '{' token
        while (CurrentToken().TokenType != TokenTypes.RightBrace)
        {
            AbstractTreeNode node = ParseExpression();
            body.Enqueue(node);
        }
        Advance(); // Consume the '}' token
        CurrentScope = parentScope;
        
        return new WhileStatementNode(condition, new BodyNode(body));
    }

    // Checks if the current token is the start of a function call
    public bool CheckForFunction()
    {
        return CurrentToken().TokenType == TokenTypes.LeftParenthesis;
    }

    // Advances the token position by one
    private void Advance()
    {
        _position++;
    }

    // Returns the current token
    private Token CurrentToken()
    {
        return IsEnd() ? new Token(TokenTypes.EoF, "") : _tokens[_position];
    }

    // Returns the previous token
    private Token PreviousToken()
    {
        return _position == 0 ? _tokens[0] : _tokens[_position - 1];
    }

    // Peeks at the next token without advancing the position
    private Token Peek()
    {
        if (IsEnd())
        {
            return new Token(TokenTypes.EoF, "");
        }
        return _tokens[_position + 1];
    }

    // Checks if the end of the token list has been reached
    private bool IsEnd()
    {
        return _position >= _tokens.Count;
    }

    // Determines the precedence of a token based on its type
    private int Precedence(Token token)
    {
        Array enumValues = typeof(TokenTypes).GetEnumValues();
        foreach (TokenTypes type in enumValues)
        {
            if (type == token.TokenType)
            {
                return (int)type;
            }
        }
        return -1;
    }
}