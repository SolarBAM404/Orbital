using OrbitalCore.Parse.Visitors;

namespace OrbitalCore.Parse.Elements;

public class VariableStatementElement(VariableElement variableElement, IOrbitalElement? value) : IOrbitalElement
{
    public VariableElement Variable { get; } = variableElement;
    public IOrbitalElement? Value { get; } = value;

    public object? Accept(IOrbitalVisitor visitor)
    {
        // visitor.VisitVariableStatement(this);
        return null;
    }
}