using OrbitalCore.Lex;
using OrbitalCore.Parse.Nodes.Abstract;
using OrbitalCore.Parse.Nodes.BasicTypes;

namespace OrbitalCore.Parse.Nodes.Expressions;

public class MathematicalBinaryExpressionNode(AbstractTreeNode expression1, AbstractTreeNode expression2, TokenTypes @operator) : AbstractExpressionNode(expression1, expression2)
{
    public TokenTypes Operator { get; set; } = @operator;
    
    public override object? Evaluate()
    {
        if (Expression1 is VariableNode variableNode1)
        {
            Expression1 = (AbstractTreeNode)variableNode1.Evaluate();
        }
        
        if (Expression2 is VariableNode variableNode2)
        {
            Expression2 = (AbstractTreeNode)variableNode2.Evaluate();
        }
        
        if (Expression1 is not (NumberNode or MathematicalBinaryExpressionNode) || Expression2 is not (NumberNode or MathematicalBinaryExpressionNode))
        {
            throw new Exception($"Both expressions must be numbers or expression nodes: expression1: {Expression1.GetType().Name}, expression2: {Expression2.GetType().Name}");
        }

        return Operator switch
        {
            TokenTypes.Gain => (double)Expression1.Evaluate() + (double)Expression2.Evaluate(),
            TokenTypes.Drain => (double)Expression1.Evaluate() - (double)Expression2.Evaluate(),
            TokenTypes.Amplify => (double)Expression1.Evaluate() * (double)Expression2.Evaluate(),
            TokenTypes.Disperse => (double)Expression1.Evaluate() / (double)Expression2.Evaluate(),
            _ => throw new Exception("Invalid operator")
        };
    }
}