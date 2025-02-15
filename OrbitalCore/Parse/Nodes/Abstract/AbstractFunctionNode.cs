namespace OrbitalCore.Parse.Nodes.Abstract;

public abstract class AbstractFunctionNode(string name, List<AbstractTreeNode> arguments) : AbstractTreeNode
{
    public string Name { get; } = name;
    public List<AbstractTreeNode> Arguments { get; } = arguments;
}