using OrbitalCore.Parse.Visitors;

namespace OrbitalCore.Parse.Elements;

public class NegateElement(IOrbitalElement expression) : IOrbitalElement
{
    
    public IOrbitalElement Element { get; set; } = expression!;
    
    public object? Accept(IOrbitalVisitor visitor)
    {
        return visitor.VisitNegate(this);
    }
}