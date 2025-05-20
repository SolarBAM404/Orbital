using OrbitalCore.Parse.Elements;
using OrbitalCore.Parse.Visitors;

namespace OrbitalCore.Parse;

public class AssignElement(VariableElement variableElement, IOrbitalElement value) : IOrbitalElement
{
    public VariableElement VariableElement { get; } = variableElement;
    public IOrbitalElement Value { get; } = value;

    public object? Accept(IOrbitalVisitor visitor)
    {
        visitor.VisitAssignment(this);
        return null;
    }
}