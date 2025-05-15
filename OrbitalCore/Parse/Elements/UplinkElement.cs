using OrbitalCore.Parse.Visitors;

namespace OrbitalCore.Parse.Elements;

public class UplinkElement(IOrbitalElement? value) : IOrbitalElement
{
    
    public IOrbitalElement? Value { get; } = value;

    public object? Accept(IOrbitalVisitor visitor)
    {
        visitor.VisitUplink(this);
        return null;
    }
}