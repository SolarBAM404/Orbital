using OrbitalCore.Parse.Elements;

namespace OrbitalCore.Parse.Visitors;

public interface IOrbitalVisitor<T>
{
    T VisitLogical(LogicalElement<T> element);
    T VisitLiteral(LiteralElement<T> element);
}