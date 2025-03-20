namespace OrbitalCore.Lex;

public class Token(TokenTypes tokenType, string? value, object? literal, int line)
{
    public TokenTypes TokenType { get; } = tokenType;
    public string? Value { get; } = value;
    public object? Literal { get; set; } = literal;
    public int Line { get; set; } = 0;
}