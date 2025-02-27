using OrbitalCore.Parse.Elements;
using OrbitalCore.Parse.Visitors;

namespace OrbitalCore;

public class Interpreter : IOrbitalVisitor<object>
{
    public object VisitLogical(LogicalElement<object> element)
    {
        object left = element.Left.Accept(this);
        return left;
    }

    public object VisitLiteral(LiteralElement<object> element)
    {
        return element.Value;
    }
}