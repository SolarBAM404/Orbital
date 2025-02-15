using OrbitalCore.Parse.Nodes.Abstract;

namespace OrbitalCore.Parse.Nodes;

public class VariableNode(string? name, object? value = null) : AbstractTreeNode
{

    public string? Name { get; set; } = name;
    public object? Value { get; set; } = value;

    public override object? Evaluate()
    {
        if (Value is VariableNode variableNode)
        {
            return variableNode.Evaluate();
        }
        return Value;
    }
}