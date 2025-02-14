using OrbitalCore.Parser.Nodes.BasicTypes;
using OrbitalCore.Tokens;

namespace OrbitalCore.Parser.Nodes.Expressions;

public class MathematicalBinaryExpressionNode(AbstractTreeNode expression1, AbstractTreeNode expression2, TokenTypes @operator) : AbstractExpressionNode(expression1, expression2)
{
    public TokenTypes Operator { get; set; } = @operator;
    
    public override object Evaluate()
    {
        if (Expression1 is not NumberNode || Expression2 is not NumberNode)
        {
            throw new Exception("Both expressions must be numbers");
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