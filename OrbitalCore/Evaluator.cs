using OrbitalCore.Lex;
using OrbitalCore.Parse;
using OrbitalCore.Parse.Nodes;
using OrbitalCore.Parse.Nodes.Abstract;

namespace OrbitalCore;

public static class Evaluator
{
    
    public static Queue<AbstractTreeNode> Evaluate(string code)
    {
        List<Token> tokens = Tokeniser.Tokenise(code);
        Queue<AbstractTreeNode> nodes = Parser.Parse(tokens);
        return nodes;
    }
    
    public static List<object?> EvaluateAndExecute(string code)
    {
        Queue<AbstractTreeNode> nodes = Evaluate(code);
        return Execute(nodes);
    } 
    
    public static List<object?> Execute(Queue<AbstractTreeNode> nodes)
    {
        List<object?> results = new();
        while (nodes.Count > 0)
        {
            AbstractTreeNode node = nodes.Dequeue();
            
            if (node is (EndOfFileNode or EmptyNode))
            {
                continue;
            }
            
            results.Add(node.Evaluate());
        }
        return results;
    }
    
}