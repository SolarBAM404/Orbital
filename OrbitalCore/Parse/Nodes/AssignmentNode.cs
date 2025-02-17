using OrbitalCore.Parse.Nodes.Abstract;

namespace OrbitalCore.Parse.Nodes;

public class AssignmentNode(VariableNode variable, AbstractTreeNode expression, Scope scope)
    : AbstractTreeNode
{
    public VariableNode Variable { get; set; } = variable;
    public AbstractTreeNode Expression { get; set; } = expression;
    public Scope Scope { get; set; } = scope;

    public override object? Evaluate()
    {
        Variable.Value = Expression.Evaluate();
        
        if (Variable.Value is AbstractTreeNode value)
        {
            Variable.Value = value.Evaluate();
        }
        
        Scope.SetVariable(Variable.Name, Variable.Value);
        return null;
    }
}