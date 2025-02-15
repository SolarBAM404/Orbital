namespace OrbitalCore.Lex;

public class Token(TokenTypes tokenType, string? value)
{
    public TokenTypes TokenType { get; } = tokenType;
    public string? Value { get; } = value;
}