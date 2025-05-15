using OrbitalCore.Parse.Visitors;

namespace OrbitalCore.Parse.Elements;

public class ExpressionBlockElement(List<LiteralElement> elements) : IOrbitalElement
{
    public List<LiteralElement> Elements { get; } = elements;
    
    public object? Accept(IOrbitalVisitor visitor)
    {
        visitor.VisitExpressionBlock(this);
        return null;
    }
}