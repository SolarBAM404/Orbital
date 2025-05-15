using OrbitalCore.Parse.Visitors;

namespace OrbitalCore.Parse.Elements;

public class BlockElement : IOrbitalElement
{
    public List<IOrbitalElement> Statements { get; }

    public BlockElement(List<IOrbitalElement> statements)
    {
        Statements = statements;
    }

    public object? Accept(IOrbitalVisitor visitor)
    {
        return visitor.VisitBlock(this);
    }
}
