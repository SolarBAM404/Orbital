using OrbitalCore.Parse.Nodes.Abstract;

namespace OrbitalCore.Parse.Nodes.BasicTypes;

public class NumberNode(object? value) : AbstractValueNode(value)
{
    public new double Value => double.Parse((string)base.Value);
    
    public override object? Evaluate()
    {
        return Value;
    }
}