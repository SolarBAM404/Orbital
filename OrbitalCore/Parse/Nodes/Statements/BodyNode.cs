using OrbitalCore.Parse.Nodes.Abstract;

namespace OrbitalCore.Parse.Nodes.Statements;

public class BodyNode : AbstractTreeNode
{
    private Queue<AbstractTreeNode> Statements { get; set; } = new();
    
    public BodyNode(Queue<AbstractTreeNode> statements)
    {
        Statements = statements;
    }
    
    public override object? Evaluate()
    {
        foreach (AbstractTreeNode statement in Statements)
        {
            statement.Evaluate();
        }
        return null;
    }
}