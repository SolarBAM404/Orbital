using OrbitalCore.Parse.Nodes.Abstract;

namespace OrbitalCore.Parse.Nodes.Expressions;

public class StringConcatenationNode(AbstractTreeNode expression1, AbstractTreeNode expression2) : AbstractExpressionNode(expression1, expression2)
{
    public override object? Evaluate()
    {
        object? value1 = Expression1.Evaluate();
        object? value2 = Expression2.Evaluate();
        
        if (value1 is string || value2 is string )
        {
            return $"{value1}{value2}";
        }
        else
        {
            throw new InvalidOperationException("Cannot concatenate non-string values");
        }
    }
}