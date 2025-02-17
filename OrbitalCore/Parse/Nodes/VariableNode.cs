using OrbitalCore.Parse.Nodes.Abstract;
using OrbitalCore.Parse.Nodes.Expressions;

namespace OrbitalCore.Parse.Nodes;

public class VariableNode(string currentValue, Scope currentScope) : AbstractTreeNode
{

    public string Name { get; set; } = currentValue;
    public Scope Scope { get; set; } = currentScope;
    public object? Value { get; set; } = null;

    public override object? Evaluate()
    {
        if (Value is AbstractTreeNode value)
        {
            for (int i = 0; i < 10; i++)
            {
                object o = value.Evaluate();
                if (o is AbstractTreeNode nodeV)
                {
                    o = nodeV.Evaluate();
                }
                else
                {
                    Scope.SetVariable(Name, o);
                    break;
                }
            }
            Value = Scope.GetVariable(Name); 
            return Value;
        }
        
        object? variable = Scope.GetVariable(Name);

        if (variable == null) return null;
        
        if (variable is AbstractTreeNode node)
        {
            variable = node.Evaluate();
        }
        
        if (Value != null)
        {
            // variable = Value;
            Scope.SetVariable(Name, variable);
        }
        else
        {
            throw new Exception($"Variable {Name} is not defined");
        }

        return variable;
    }
    
    public bool CheckIfVariableExists()
    {
        return Scope.GetVariable(Name) != null;
    }
}