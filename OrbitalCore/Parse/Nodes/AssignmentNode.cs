using OrbitalCore.Parse.Nodes.Abstract;

namespace OrbitalCore.Parse.Nodes;

public class AssignmentNode(string name, AbstractTreeNode expression, Scope scope)
    : AbstractTreeNode
{
    public string VariableName => name;
    public AbstractTreeNode Expression { get; set; } = expression;
    public Scope Scope { get; set; } = scope;

    public override object? Evaluate()
    {
        Scope.SetVariable(VariableName, Expression.Evaluate());
        return null;
    }
}