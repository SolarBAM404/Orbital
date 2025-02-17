using OrbitalCore.Parse.Nodes.Abstract;

namespace OrbitalCore.Parse.Nodes.Statements;

public class WhileStatementNode(AbstractTreeNode condition, AbstractTreeNode body) : AbstractTreeNode
{
    
    public AbstractTreeNode Condition { get; set; } = condition;
    public AbstractTreeNode Body { get; set; } = body;

    public override object? Evaluate()
    {
        while ((bool)Condition.Evaluate())
        {
            Body.Evaluate();
        }
        return null;
    }
}