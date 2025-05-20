using OrbitalCore.Parse.Elements;

namespace OrbitalCore.Parse.Visitors;

public interface IOrbitalVisitor
{
    object? VisitLogical(LogicalElement element);
    object? VisitLiteral(LiteralElement element);
    
    void VisitUplink(UplinkElement element);
    
    void VisitExpressionBlock(ExpressionBlockElement element);
    object? VisitBlock(BlockElement element);
    
    void VisitEof();
    object? VisitGroup(GroupElement groupElement);
    object? VisitVariable(VariableElement variableElement);
    void VisitAssignment(AssignElement variableStatementElement);
    object? VisitNegate(NegateElement negateElement);
    object? VisitProbe(ProbeElement probeElement);
    object? VisitOrbit(OrbitElement orbitElement);
    object? VisitCall(CallElement callElement);
}