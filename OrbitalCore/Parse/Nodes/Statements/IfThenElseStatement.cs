using OrbitalCore.Parse.Nodes.Abstract;

namespace OrbitalCore.Parse.Nodes.Statements;

public class IfThenElseStatement : AbstractTreeNode
{
    public IfThenElseStatement(AbstractTreeNode condition, BodyNode thenBody, BodyNode elseBody = null!)
    {
        Condition = condition;
        ThenBody = thenBody;
        ElseBody = elseBody;
    }
    
    private AbstractTreeNode Condition { get; set; }
    private BodyNode ThenBody { get; set; }
    private BodyNode? ElseBody { get; set; } = null!;
    
    public override object? Evaluate()
    {
        if ((bool)Condition.Evaluate())
        {
            ThenBody.Evaluate();
        }
        else
        {
            if (ElseBody != null)
            {
                ElseBody.Evaluate();
            }
        }
        return null;
    }
}