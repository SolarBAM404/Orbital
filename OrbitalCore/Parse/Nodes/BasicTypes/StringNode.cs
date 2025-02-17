using OrbitalCore.Parse.Nodes.Abstract;

namespace OrbitalCore.Parse.Nodes.BasicTypes;

public class StringNode(object? value) : AbstractValueNode(value)
{
    public override string? Evaluate()
    {
        return (string?)Value;
    }
}