using OrbitalCore.Parse.Nodes.Abstract;

namespace OrbitalCore.Parse.Nodes.BuiltInFunctions;

public class UplinkCmdNode(List<AbstractTreeNode> arguments) : AbstractFunctionNode("uplink", arguments)
{
    public override object? Evaluate()
    {
        foreach (var argument in Arguments)
        {
            Console.WriteLine(argument.Evaluate());
        }
        return null;
    }
}