using OrbitalCore.Lex;
using OrbitalCore.Parse.Visitors;

namespace OrbitalCore.Parse.Elements;

public class VariableElement(Token name) : IOrbitalElement
{
    public Token Name { get; } = name;

    public Token? Value { get; set; } = null;

    public object? Accept(IOrbitalVisitor visitor)
    {
        return visitor.VisitVariable(this);
    }

    public override bool Equals(object? obj)
    {
        if (obj is VariableElement variableElement)
        {
            return Name.Equals(variableElement.Name);
        }
        return false;
    }
}