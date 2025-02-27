using System.Linq.Expressions;
using OrbitalCore.Objects;

namespace OrbitalCore.Lex;

public class Scanner
{
    public static Dictionary<string, TokenTypes> Keywords;

    static Scanner()
    {
        Keywords = new Dictionary<string, TokenTypes>
        {
            { "signal", TokenTypes.Signal },
            { "void", TokenTypes.Void },
            { "probe", TokenTypes.Probe },
            { "scan", TokenTypes.Scan },
            { "orbit", TokenTypes.Orbit },
            { "nova", TokenTypes.Nova },
            { "land", TokenTypes.Land },
        };
    }
    
    public int start = 0;
    public int current = 0;
    public int line = 1;
    
    private string _source;
    
    public Scanner(string source)
    {
        _source = source;
    }
    
    public List<Token> ScanTokens()
    {
        List<Token> tokens = new List<Token>();
        
        while (!IsAtEnd())
        {
            start = current;
            ScanToken(tokens);
        }
        
        tokens.Add(new Token(TokenTypes.EoF, null, null, line));
        return tokens;
    }
    
    private void ScanToken(List<Token> tokens)
    {
        char character = Advance();
        switch (character)
        {
            // Symbols
            case ('('):
            {
                AddToken(TokenTypes.LeftParenthesis, null, tokens);
                break;
            }
            case (')'):
            {
                AddToken(TokenTypes.RightParenthesis, null, tokens);
                break;
            }
            case ('{'):
            {
                AddToken(TokenTypes.LeftBrace, null, tokens);
                break;
            }
            case ('}'):
            {
                AddToken(TokenTypes.RightBrace, null, tokens);
                break;
            }
            case (';'):
            {
                AddToken(TokenTypes.SemiColon, null, tokens);
                break;
            }
            case ('='):
            {
                AddToken(TokenTypes.Assignment, null, tokens);
                break;
            }
            case ('"'):
                ScanString(tokens);
                break;

            // Ingore whitespace
            case (' '):
            case ('\r'):
            case ('\t'):
            {
                break;
            }
            
            // New line
            case ('\n'):
                line++;
                break;
            
            default:

                if (IsDigit(character))
                {
                    ScanNumber(tokens);
                }
                else if (IsAlpha(character))
                {
                    ScanIdentifier(tokens);
                }
                else
                {
                    throw new Exception("Unexpected character: " + character);
                }
                
                break;
                
        }
    }
    
    private void ScanIdentifier(List<Token> tokens)
    {
        while (IsAlpha(Peek()) || IsDigit(Peek()))
        {
            Advance();
        }
        
        string text = _source.Substring(start, current - start);
        
        TokenTypes type = TokenTypes.Identifier;
        if (Keywords.TryGetValue(text, out var keyword))
        {
            type = keyword;
        }
        
        AddToken(type, null, tokens);
    }

    private void ScanNumber(List<Token> tokens)
    {
        while (IsDigit(Peek()))
        {
            Advance();
        }
        
        if (Peek() == '.' && IsDigit(PeekNext()))
        {
            Advance();
            while (IsDigit(Peek()))
            {
                Advance();
            }
        }
        
        string numberString = _source.Substring(start, current - start);
        double number = double.Parse(numberString);
        
        AddToken(TokenTypes.Number, number, tokens);
    }
    
    public void ScanString(List<Token> tokens)
    {
        MatchUntil('"');
        
        if (IsAtEnd())
        {
            throw new Exception("Unterminated string.");
        }
        
        Advance();
        
        string value = _source.Substring(start + 1, current - start - 2);
        AddToken(TokenTypes.String, value, tokens);
    }
    
    private void AddToken(TokenTypes type, object? literal, List<Token> tokens)
    {
        string text = _source.Substring(start, current - start);
        tokens.Add(new Token(type, text, literal, line));
    }
    
    private bool IsAtEnd()
    {
        return current >= _source.Length;
    }
    
    private char Advance()
    {
        current++;
        return _source[current - 1];
    }
    
    private bool Match(char expected)
    {
        if (IsAtEnd())
        {
            return false;
        }
        
        if (_source[current] != expected)
        {
            return false;
        }
        
        char c = _source[current];
        return c == expected;
    }
    
    private int MatchUntil(char expected)
    {
        int count = 0;
        while (!IsAtEnd() && _source[current] != expected)
        {
            count++;
            current++;
        }
        
        return count;
    }
    
    private int MatchUntil(Func<char, bool> predicate)
    {
        int count = 0;
        while (!IsAtEnd() && predicate(_source[current]))
        {
            count++;
            current++;
        }
        
        return count;
    }
    
    private bool IsAlpha(char c)
    {
        if (c is >= 'a' and <= 'z' or >= 'A' and <= 'Z')
        {
            return true;
        }

        return false;
    }
    
    private bool IsDigit(char c)
    {
        if (c is >= '0' and <= '9')
        {
            return true;
        }

        return false;
    }
    
    private bool IsAlphaNumeric(char c)
    {
        return IsAlpha(c) || IsDigit(c);
    }
    
    private char Peek()
    {
        if (IsAtEnd())
        {
            return '\0';
        }
        
        return _source[current];
    }
    
    private char PeekNext()
    {
        if (current + 1 >= _source.Length)
        {
            return '\0';
        }
        
        return _source[current + 1];
    }
    
    
}