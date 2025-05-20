using OrbitalCore.Parse.Visitors;

namespace OrbitalCore.Parse.Elements;

public class EoFElement : IOrbitalElement
{
    public object? Accept(IOrbitalVisitor visitor)
    {
        visitor.VisitEof();
        return null;
    }
}