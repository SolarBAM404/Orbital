using OrbitalCore.Parse.Visitors;

namespace OrbitalCore.Parse.Elements;

public class OrbitElement(
    IOrbitalElement condition = null!,
    IOrbitalElement? body = null
) : IOrbitalElement
{
    public IOrbitalElement Condition { get; set; } = condition;
    public IOrbitalElement? Body { get; set; } = body;

    public object? Accept(IOrbitalVisitor visitor)
    {
        return visitor.VisitOrbit(this);
    }
}
