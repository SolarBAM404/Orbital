namespace OrbitalCore.Parse.Nodes.Abstract;

public abstract class AbstractValueNode(object? value) : AbstractTreeNode
{
    public object? Value { get; set; } = value;
    
    public override object? Evaluate()
    {
        return Value;
    }
}