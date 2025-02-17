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
            
            if (node is (EndOfFileNode or EmptyNode or null))
            {
                continue;
            }
            
            object? result = node.Evaluate();
            
            if (result is not null)
            {
                results.Add(result);
            }
        }
        return results;
    }
    
}