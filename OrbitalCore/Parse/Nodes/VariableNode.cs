using OrbitalCore.Parse.Nodes.Abstract;
using OrbitalCore.Parse.Nodes.Expressions;

namespace OrbitalCore.Parse.Nodes;

public class VariableNode(string name, Scope currentScope) : AbstractTreeNode
{
    public string Name { get; set; } = name;

    public override object? Evaluate()
    {
        return currentScope.GetVariable(Name) ?? throw new Exception($"Variable {Name} not found");
    }
}