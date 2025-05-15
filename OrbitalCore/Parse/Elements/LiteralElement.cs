using OrbitalCore.Parse.Visitors;

namespace OrbitalCore.Parse.Elements;

public class LiteralElement(object? value) : IOrbitalElement
{
    public object? Value { get; } = value;
    
    public object? Accept(IOrbitalVisitor visitor)
    {
        return visitor.VisitLiteral(this);
    }
}