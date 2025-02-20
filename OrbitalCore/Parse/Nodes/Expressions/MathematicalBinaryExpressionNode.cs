using OrbitalCore.Exceptions;
using OrbitalCore.Lex;
using OrbitalCore.Parse.Nodes.Abstract;
using OrbitalCore.Parse.Nodes.BasicTypes;

namespace OrbitalCore.Parse.Nodes.Expressions;

public class MathematicalBinaryExpressionNode(AbstractTreeNode expression1, AbstractTreeNode expression2, TokenTypes @operator) : AbstractExpressionNode(expression1, expression2)
{
    public TokenTypes Operator { get; set; } = @operator;
    
    public override object? Evaluate()
    {

        double expression1Value;
        double expression2Value;
        
        switch (Expression1)
        {
            case VariableNode variableNode1:
                object? variable1Obj = variableNode1.Evaluate();
                if (variable1Obj is double variable1)
                {
                    expression1Value = variable1;
                }
                else
                {
                    throw new CastErrorException(typeof(double), variable1Obj.GetType());
                }
                break;
            case NumberNode numberNode1:
                expression1Value = (double)numberNode1.Value;
                break;
            default:
            {
                object expression1 = Expression1.Evaluate();
                try { expression1Value = (double)expression1; }
                catch { throw new Exception("Invalid expression"); }

                break;
            }
        }

        switch (Expression2)
        {
            case VariableNode variableNode2:
                object? variable2Obj = variableNode2.Evaluate();
                if (variable2Obj is double variable2)
                {
                    expression2Value = variable2;
                }
                else
                {
                    throw new CastErrorException(typeof(double), variable2Obj.GetType());
                }
                break;
            case NumberNode numberNode2:
                expression2Value = (double)numberNode2.Value;
                break;
            default:
            {
                object expression2 = Expression2.Evaluate();
                try
                {
                    expression2Value = (double)expression2;
                }
                catch (InvalidCastException castException)
                {
                    throw new CastErrorException(typeof(double), expression2.GetType()); 
                }
                catch
                {
                    throw new Exception("Invalid expression");
                }

                break;
            }
        }

        return Operator switch
        {
            TokenTypes.Gain => expression1Value + expression2Value,
            TokenTypes.Drain => expression1Value - expression2Value,
            TokenTypes.Amplify => expression1Value * expression2Value,
            TokenTypes.Disperse => expression1Value / expression2Value,
            _ => throw new Exception("Invalid operator")
        };
    }
}