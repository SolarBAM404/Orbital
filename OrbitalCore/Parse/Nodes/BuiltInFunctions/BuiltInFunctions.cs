using OrbitalCore.Parse.Nodes.Abstract;

namespace OrbitalCore.Parse.Nodes.BuiltInFunctions;

public class BuiltInFunctions
{
    public static Dictionary<string?, Func<List<AbstractTreeNode>, object>> Functions { get; } = new()
    {
        { "uplink", args => new UplinkCmdNode(args) },
        { "negate", args => new NegateCmdNode(args) },
        { "relay", args => new RelayCmdNode(args) }
    };
    
    public static AbstractFunctionNode GetFunction(string? name, List<AbstractTreeNode> args)
    {
        if (!Functions.ContainsKey(name))
        {
            throw new Exception($"Function '{name}' not found.");
        }
        
        return (AbstractFunctionNode)Functions[name].Invoke(args);
    }
}