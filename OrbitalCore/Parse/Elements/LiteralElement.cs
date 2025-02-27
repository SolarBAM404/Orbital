using OrbitalCore.Parse.Visitors;

namespace OrbitalCore.Parse.Elements;

public class LiteralElement<T>(object value) : IOrbitalElement<T>
{
    public object Value { get; }
    
    public T Accept(IOrbitalVisitor<T> visitor)
    {
        return visitor.VisitLiteral(this);
    }
}