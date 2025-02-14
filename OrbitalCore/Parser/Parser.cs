using OrbitalCore.Parser.Nodes;
using OrbitalCore.Parser.Nodes.BasicTypes;
using OrbitalCore.Parser.Nodes.Expressions;
using OrbitalCore.Tokens;

namespace OrbitalCore.Parser;

public class Parser(List<Token> tokens)
{
    private List<Token> _tokens = tokens;
    private int _position = 0;
    
    private Queue<AbstractTreeNode> _output = new();
    
    public AbstractTreeNode Parse()
    {
        return ParseExpression();
    }

    private AbstractTreeNode ParseExpression()
    {
        AbstractTreeNode left = ParsePrimary();

        while (CurrentToken().TokenType is not (TokenTypes.SemiColon or TokenTypes.EoF))
        {
            Token operatorToken = CurrentToken();
            Advance();
            AbstractTreeNode right = ParsePrimary();

            if (Precedence(operatorToken) < Precedence(Peek()))
            {
                return left;
            }

            switch (operatorToken.TokenType)
            {
                case TokenTypes.Gain:
                case TokenTypes.Drain:
                case TokenTypes.Amplify:
                case TokenTypes.Disperse:
                    left = new MathematicalBinaryExpressionNode(left, right, operatorToken.TokenType);
                    break;
                default:
                    throw new Exception("Unexpected token");
            }
        }

        return left;
    }
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
                return CheckForFunction() ? ParseFunction() : new VariableNode(current.Value);
                break;
            case TokenTypes.LeftParenthesis:
                AbstractTreeNode expression = ParseExpression();
                if (CurrentToken().TokenType != TokenTypes.RightParenthesis)
                {
                    throw new Exception("Expected ')'");
                }
                Advance();
                return expression;
            default:
                throw new Exception("Unexpected token");
        }
    }

    private AbstractTreeNode ParseFunction()
    {
        throw new NotImplementedException();
    }
    
    public bool CheckForFunction()
    {
        return Peek().TokenType == TokenTypes.LeftParenthesis;
    }

    private void Advance()
    {
        _position++;
    }
    
    private Token CurrentToken()
    {
        return _tokens[_position];
    }
    
    private Token Peek()
    {
        if (IsEnd())
        {
            return new Token(TokenTypes.EoF, "");
        }
        return _tokens[_position + 1];
    }
    
    private bool IsEnd()
    {
        return _position >= _tokens.Count;
    }
    
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