using OrbitalCore.Parse.Nodes.Abstract;

namespace OrbitalCore.Parse.Nodes.BuiltInFunctions;

public class NegateCmdNode(List<AbstractTreeNode> arguments) : AbstractFunctionNode("negate", arguments)
{
    public override object? Evaluate()
    {
        if (Arguments.Count != 1)
        {
            throw new Exception("NegateCmdNode requires exactly 1 argument");
        }

        var value = Arguments[0].Evaluate();
        if (value is double d)
        {
            return -d;
        }
        else if (value is bool b)
        {
            return !b;
        }
        else
        {
            throw new Exception("NegateCmdNode requires a number or boolean argument");
        }
    }
}