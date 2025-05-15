using OrbitalCore.Lex;
using OrbitalCore.Parse.Visitors;

namespace OrbitalCore.Parse.Elements;

public class UnaryElement(Token? op, IOrbitalElement right) : IOrbitalElement
{
    public Token? Operator { get; } = op;
    public IOrbitalElement Right { get; } = right;

    public object? Accept(IOrbitalVisitor visitor)
    {
        return visitor.VisitUnary(this);
    }
}