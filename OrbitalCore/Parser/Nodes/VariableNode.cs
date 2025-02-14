namespace OrbitalCore.Parser.Nodes;

public class VariableNode(string name, object? value = null) : AbstractTreeNode
{

    public string Name { get; set; } = name;
    public object? Value { get; set; } = value;

    public override object Evaluate()
    {
        throw new NotImplementedException();
    }
}