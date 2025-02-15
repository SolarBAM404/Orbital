using OrbitalCore.Lex;
using OrbitalCore.Parse.Nodes.Abstract;

namespace OrbitalCore.Parse.Nodes.Expressions;

public class LogicalBinaryExpressionNode(AbstractTreeNode expression1, AbstractTreeNode expression2, TokenTypes @operator) : AbstractExpressionNode(expression1, expression2)
{
    public TokenTypes Operator { get; set; } = @operator;

    public override object? Evaluate()
    {
        switch (Operator)
        {
            case TokenTypes.Stable:
                return (bool)Expression1.Evaluate() && (bool)Expression2.Evaluate();
            case TokenTypes.Path:
                return (bool)Expression1.Evaluate() || (bool)Expression2.Evaluate();
            default:
                throw new Exception("Invalid operator for logical expression");
        }
    }
}