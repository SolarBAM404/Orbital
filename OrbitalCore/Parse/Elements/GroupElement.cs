using OrbitalCore.Parse.Visitors;

namespace OrbitalCore.Parse.Elements;

public class GroupElement(IOrbitalElement element) : IOrbitalElement
{
    public IOrbitalElement Element { get; } = element;

    public object? Accept(IOrbitalVisitor visitor)
    {
        return visitor.VisitGroup(this);
    }
    
    
}