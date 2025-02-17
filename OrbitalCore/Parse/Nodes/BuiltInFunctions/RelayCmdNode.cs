using OrbitalCore.Parse.Nodes.Abstract;
using OrbitalCore.Parse.Nodes.BasicTypes;

namespace OrbitalCore.Parse.Nodes.BuiltInFunctions;

public class RelayCmdNode(List<AbstractTreeNode> arguments) : AbstractFunctionNode("relay", arguments)
{
    public override object? Evaluate()
    {
        if (Arguments.Count != 0)
        {
            throw new Exception("Relay function does not take any arguments");
        }

        string? readLine = Console.ReadLine();
        StringNode? input = new(readLine);
        return input.Evaluate();
    }
}