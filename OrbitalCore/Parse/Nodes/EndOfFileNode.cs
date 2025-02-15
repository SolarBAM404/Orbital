using OrbitalCore.Parse.Nodes.Abstract;

namespace OrbitalCore.Parse.Nodes;

public class EndOfFileNode : AbstractTreeNode
{
    public static EndOfFileNode Instance { get; } = new();
    
    public override object? Evaluate()
    {
        return null;
    }
}