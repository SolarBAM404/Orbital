using OrbitalCore.Parse.Nodes.Abstract;

namespace OrbitalCore.Parse.Nodes.BasicTypes;

public class BooleanNode(object? value) : AbstractValueNode(value)
{
    public new bool Value => bool.Parse((string)base.Value);
    
    public override object? Evaluate()
    {
        return Value;
    }
}