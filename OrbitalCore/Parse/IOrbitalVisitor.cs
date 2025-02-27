using OrbitalCore.Parse.Elements;

namespace OrbitalCore.Parse.Visitors;

public interface IOrbitalVisitor
{
    object? VisitLogical(LogicalElement element);
    object? VisitLiteral(LiteralElement element);
}