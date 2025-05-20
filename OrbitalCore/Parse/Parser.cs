using OrbitalCore.Lex;
using OrbitalCore.Parse.Elements;

namespace OrbitalCore.Parse;

public class Parser
{
    private List<Token?> _tokens;
    private int _current = 0;
    
    public Parser(List<Token?> tokens)
    {
        _tokens = tokens;
    }

    public List<IOrbitalElement> Parse()
    {
        List<IOrbitalElement> orbitalElements = new();
        
        while (!IsAtEnd())
        {
            orbitalElements.Add(ParseElement());
        }
        return orbitalElements;
    }
    
    private IOrbitalElement ParseElement()
    {
        try
        {
            return ParseStatement();
        }
        catch (Exception e)
        {
            if (Match(TokenTypes.SemiColon))
            {
                return new EoFElement();
            }
            throw;
        }
    }

    private IOrbitalElement ParseStatement()
    {
        if (Match(TokenTypes.Uplink))
        {
            return ParseUplink();
        }
        if (Match(TokenTypes.Negate))
        {
            return ParseNegate();
        }
        if (Match(TokenTypes.Probe))
        {
            return ParseIfStatement();
        }
        if (Match(TokenTypes.Scan))
        {
            return ParseWhileStatement();
        }

        if (Match(TokenTypes.Orbit))
        {
            return ParseWhileStatement();
        }
    
        return ParseAssignment();
    }

    private IOrbitalElement ParseNegate()
    {
        IOrbitalElement expression = ParseElement();
        return new NegateElement(expression);
    }

    private IOrbitalElement ParseUplink()
    {
        IOrbitalElement expression = ParseElement();
        Consume(TokenTypes.SemiColon, $"Expected ';' after expression. Line: {PeekPrevious().Line}");
        return new UplinkElement(expression);
    }
    
    private IOrbitalElement ParseIfStatement()
    {
        Consume(TokenTypes.LeftParenthesis, "Expected '(' after 'if'.");
        IOrbitalElement ifCondition = ParseElement();
        Consume(TokenTypes.RightParenthesis, "Expected ')' after condition.");

        IOrbitalElement ifThenCondition = Match(TokenTypes.LeftBrace) ? ParseBlock() : ParseStatement();

        List<IOrbitalElement> elseIfConditions = new();
        List<IOrbitalElement> elseIfThenConditions = new();

        while (Match(TokenTypes.ProbeScan))
        {
            Consume(TokenTypes.LeftParenthesis, "Expected '(' after 'else if'.");
            elseIfConditions.Add(ParseElement());
            Consume(TokenTypes.RightParenthesis, "Expected ')' after condition.");
            elseIfThenConditions.Add(ParseStatement());
        }

        IOrbitalElement? elseCondition = null;
        IOrbitalElement? elseThenCondition = null;

        if (Match(TokenTypes.Scan))
        {
            elseCondition = new LiteralElement(true); // Represents the "else" condition.
            elseThenCondition = ParseStatement();
        }

        return new ProbeElement(
            ifCondition,
            ifCondition,
            elseIfConditions,
            elseCondition,
            ifThenCondition,
            elseIfThenConditions,
            elseThenCondition
        );
    }

    
    private IOrbitalElement ParseWhileStatement()
    {
        Consume(TokenTypes.LeftParenthesis, "Expected '(' after 'while'.");
        IOrbitalElement condition = ParseElement();
        Consume(TokenTypes.RightParenthesis, "Expected ')' after condition.");
    
        IOrbitalElement body = ParseStatement();
    
        return new OrbitElement(condition, body);
    }

    private IOrbitalElement ParseAssignment()
    {
        IOrbitalElement element = ParseOrStatement();

        if (Match(TokenTypes.Assignment))
        {
            Token? equals = PeekPrevious();
            IOrbitalElement value = ParseAssignment();
            
            if (element is VariableElement variableElement)
            {
                return new AssignElement(variableElement, value);
            }
            
            throw new Exception("Expected variable on left side of assignment.");
        }
        
        return element;
    }

    private IOrbitalElement ParseOrStatement()
    {
        IOrbitalElement element = ParseAndStatement(); 
        
        while (Match(TokenTypes.Path))
        {
            Token? @operator = PeekPrevious();
            IOrbitalElement right = ParseAndStatement();
            element = new LogicalElement(element, @operator, right);
        }
        
        return element;
    }
    
    private IOrbitalElement ParseAndStatement()
    {
        IOrbitalElement element = ParseEquality();

        while (Match(TokenTypes.Stable))
        {
            Token? @operator = PeekPrevious();
            IOrbitalElement right = ParseElement();
            element = new LogicalElement(element, @operator, right);
        }
        
        return element;
    }
    
    private IOrbitalElement ParseEquality()
    {
        IOrbitalElement element = ParseComparison();
        
        while (Match(TokenTypes.Align, TokenTypes.Disrupt))
        {
            Token? @operator = PeekPrevious();
            IOrbitalElement right = ParseElement();
            element = new LogicalElement(element, @operator, right);
        }
        
        return element;
    }
    
    private IOrbitalElement ParseComparison()
    {
        IOrbitalElement element = ParseAddition();
        
        while (Match(TokenTypes.Above, TokenTypes.Safe, TokenTypes.Below, TokenTypes.Unsafe))
        {
            Token? @operator = PeekPrevious();
            IOrbitalElement right = ParseElement();
            element = new LogicalElement(element, @operator, right);
        }
        
        return element;
    }

    private IOrbitalElement ParseMultiplication()
    {
        IOrbitalElement element = Call();
        
        while (Match(TokenTypes.Amplify, TokenTypes.Disperse))
        {
            Token? @operator = PeekPrevious();
            IOrbitalElement right = Call();
            element = new LogicalElement(element, @operator, right);
        }
        
        return element;
    }

    private IOrbitalElement ParseAddition()
    {
        IOrbitalElement element = ParseMultiplication();
        
        while (Match(TokenTypes.Gain, TokenTypes.Drain))
        {
            Token? @operator = PeekPrevious();
            IOrbitalElement right = ParseMultiplication();
            element = new LogicalElement(element, @operator, right);
        }
        
        return element;
    }

    private IOrbitalElement Call()
    {
        IOrbitalElement element = ParsePrimary();
        
        while (true)
        {
            if (Match(TokenTypes.LeftParenthesis))
            {
                element = finishCall(element);
            }
            else
            {
                break;
            }
        }
        
        return element;
    }
    
    private IOrbitalElement ParsePrimary()
    {
        if (Match(TokenTypes.Signal))
        {
            return new LiteralElement(true);
        }

        if (Match(TokenTypes.Void))
        {
            return new LiteralElement(false);
        }

        if (Match(TokenTypes.Null))
        {
            return new LiteralElement(null);
        }
        
        if (Match(TokenTypes.Number, TokenTypes.String))
        {
            return new LiteralElement(PeekPrevious().Literal);
        }
        
        if (Match(TokenTypes.Identifier))
        {
            return new VariableElement(PeekPrevious());
        }
        
        if (Match(TokenTypes.LeftParenthesis))
        {
            IOrbitalElement expression = ParseElement();
            Consume(TokenTypes.RightParenthesis, $"Expected ')' after expression. Line: {PeekPrevious().Line}");
            return new GroupElement(expression);
        }

        if (Match(TokenTypes.LeftBrace))
        {
            return ParseBlock();
        }
        
        throw new NotImplementedException($"Token not implemented yet, {Peek().TokenType}");
    }

    private IOrbitalElement finishCall(IOrbitalElement expression)
    {
        List<IOrbitalElement> elements = new List<IOrbitalElement>();
        if (!Check(TokenTypes.RightParenthesis))
        {
            do
            {
                elements.Add(ParseElement());
            } while (Match(TokenTypes.Comma));
        }
        
        Consume(TokenTypes.RightParenthesis, "Expected ')' after arguments.");
        Token paren = PeekPrevious();
        
        return new CallElement(expression, paren, elements);
    }
    
    private IOrbitalElement ParseBlock()
    {
        List<IOrbitalElement> statements = new();

        // Consume the opening brace
        // Consume(TokenTypes.LeftBrace, "Expected '{' to start a block.");

        // Parse statements until the closing brace
        while (!Check(TokenTypes.RightBrace) && !IsAtEnd())
        {
            if (Check(TokenTypes.SemiColon))
            {
                Advance();
                continue;
            }
            statements.Add(ParseStatement());
        }

        // Consume the closing brace
        Consume(TokenTypes.RightBrace, "Expected '}' to close the block.");

        return new BlockElement(statements);
    }
    
    private Token? Advance()
    {
        if (!IsAtEnd())
        {
            _current++;
        }

        return PeekPrevious();
    }
    
    private Token? Peek()
    {
        return _tokens[_current];
    }
    
    private Token? PeekPrevious()
    {
        return _tokens[_current - 1];
    }
    
    private Token Consume(TokenTypes type, string errorMessage)
    {
        if (Check(type))
        {
            return Advance();
        }
        
        throw new Exception(errorMessage);
    }
    
    private bool Match(params TokenTypes[] types)
    {
        foreach (TokenTypes type in types)
        {
            if (!Check(type)) continue;
            
            Advance();
            return true;
        }
        return false;
    }
    
    private bool Check(TokenTypes type)
    {
        if (IsAtEnd())
        {
            return false;
        }
        return Peek().TokenType == type;
    }
    
    private bool IsAtEnd()
    {
        return Peek().TokenType == TokenTypes.EoF;
    }
}
