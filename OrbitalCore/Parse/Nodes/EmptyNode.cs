using OrbitalCore.Parse.Nodes.Abstract;

namespace OrbitalCore.Parse.Nodes;

public class EmptyNode : AbstractTreeNode
{
    public static EmptyNode Instance { get; } = new();
    
    public override object? Evaluate()
    {
        return null;
    }
}