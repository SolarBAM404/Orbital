namespace OrbitalCore.Parse.Nodes.Abstract;

public abstract class AbstractExpressionNode(AbstractTreeNode expression1, AbstractTreeNode expression2)
    : AbstractTreeNode
{
    public AbstractTreeNode Expression1 { get; set; } = expression1;
    public AbstractTreeNode Expression2 { get; set; } = expression2;
}