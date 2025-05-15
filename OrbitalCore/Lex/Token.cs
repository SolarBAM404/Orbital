namespace OrbitalCore.Lex;

public class Token(TokenTypes tokenType, string? value, object? literal, int line)
{
    public TokenTypes TokenType { get; } = tokenType;
    public string? Value { get; } = value;
    public object? Literal { get; set; } = literal;
    public int Line { get; set; } = 0;

    public override bool Equals(object? obj)
    {
        if (obj is not Token other)
            return false;

        return TokenType == other.TokenType && string.Equals(Value, other.Value) && Equals(Literal, other.Literal);
    }
}