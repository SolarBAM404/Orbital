namespace OrbitalCore.Tokens;

public class Tokeniser(string raw)
{
    private string _raw = raw;
    private List<Token> _tokens = [];
    private int _index = 0;

    public List<Token> GetTokens()
    {
        while (_index < _raw.Length)
        {
            var current = _raw[_index];
            if (char.IsWhiteSpace(current))
            {
                _index++;
                continue;
            }
            
            if (current == '"')
            {
                var str = ReadString();
                _tokens.Add(new Token(TokenTypes.String, str));
                continue;
            }
            
            if (char.IsDigit(current))
            {
                var number = ReadWhile(char.IsDigit);
                _tokens.Add(new Token(TokenTypes.Number, number));
                continue;
            }
            
            if (char.IsLetter(current))
            {
                var identifier = ReadWhile(char.IsLetter);

                TokenTypes? token = GetToken(identifier);
                if (token != null)
                {
                    _tokens.Add(new Token((TokenTypes)token, identifier));
                    continue;
                }
                
                _tokens.Add(new Token(TokenTypes.Identifier, identifier));
                continue;
            }

            switch (current)
            {
                case '{':
                    _tokens.Add(new Token(TokenTypes.LeftBrace, "{"));
                    _index++;
                    break;
                case '}':
                    _tokens.Add(new Token(TokenTypes.RightBrace, "}"));
                    _index++;
                    break;
                case '(':
                    _tokens.Add(new Token(TokenTypes.LeftParenthesis, "("));
                    _index++;
                    break;
                case ')':
                    _tokens.Add(new Token(TokenTypes.RightParenthesis, ")"));
                    _index++;
                    break;
                case ';':
                    _tokens.Add(new Token(TokenTypes.SemiColon, ";"));
                    _index++;
                    break;
                case '=':
                    _tokens.Add(new Token(TokenTypes.Assignment, "="));
                    _index++;
                    break;
            }
            
        }
        return _tokens;
    }
    
    private string ReadWhile(Func<char, bool> predicate)
    {
        var result = "";
        while (_index < _raw.Length && predicate(_raw[_index]))
        {
            result += _raw[_index];
            _index++;
        }
        return result;
    }
    
    private string ReadString()
    {
        var result = "";
        _index++;
        while (_index < _raw.Length && _raw[_index] != '"')
        {
            result += _raw[_index];
            _index++;
        }
        _index++;
        return result;
    }

    private TokenTypes? GetToken(String value)
    {
        return value switch
        {
            "true" => TokenTypes.Boolean,
            "false" => TokenTypes.Boolean,
            "gain" => TokenTypes.Gain,
            "drain" => TokenTypes.Drain,
            "amplify" => TokenTypes.Amplify,
            "disperse" => TokenTypes.Disperse,
            "stable" => TokenTypes.Stable,
            "path" => TokenTypes.Path,
            "negate" => TokenTypes.Negate,
            "align" => TokenTypes.Align,
            "disrupt" => TokenTypes.Disrupt,
            "above" => TokenTypes.Above,
            "below" => TokenTypes.Below,
            "safe" => TokenTypes.Safe,
            "unsafe" => TokenTypes.Unsafe,
            _ => null
        };
    }
}