using OrbitalCore.Lex;
using OrbitalCore.Parse.Nodes.Abstract;

namespace OrbitalCore.Parse.Nodes.Expressions;

public class ComparisonBinaryExpressionNode(AbstractTreeNode expression1, AbstractTreeNode expression2, TokenTypes @operator) : AbstractExpressionNode(expression1, expression2)
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
        
        switch (Operator)
        {
            case TokenTypes.Align:
                return Expression1.Evaluate().Equals(Expression2.Evaluate());
            case TokenTypes.Disrupt:
                return !Expression1.Evaluate().Equals(Expression2.Evaluate());
            case TokenTypes.Above:
                return Expression1.Evaluate() is double da1 && Expression2.Evaluate() is double da2 && da1 > da2;
            case TokenTypes.Below:
                return Expression1.Evaluate() is double db1 && Expression2.Evaluate() is double db2 && db1 < db2;
            case TokenTypes.Safe:
                return Expression1.Evaluate() is double ds1 && Expression2.Evaluate() is double ds2 && ds1 >= ds2;
            case TokenTypes.Unsafe:
                return Expression1.Evaluate() is double du1 && Expression2.Evaluate() is double du2 && du1 <= du2;
            default:
                throw new Exception("Invalid operator");
        }   
    }
}