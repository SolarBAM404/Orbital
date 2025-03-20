using OrbitalCore.Lex;
using OrbitalCore.Parse;
using OrbitalCore.Parse.Elements;
using OrbitalCore.Parse.Visitors;

namespace OrbitalCore;

public class Interpreter : IOrbitalVisitor
{

    public void Interpret(List<IOrbitalElement> elements)
    {
        
    }

    public object VisitLogical(LogicalElement element)
    {
        object left = element.Left.Accept(this);
        object right = element.Left.Accept(this);

        double? lDouble = left is double ld ? ld : null;
        double? rDouble = right is double rd ? rd : null;

        string? lString = left is string ls ? ls : null;
        string? rString = right is string rs ? rs : null;

        switch (element.Operator.TokenType)
        {
            case TokenTypes.Gain:
                if (lString != null)
                {
                    return lString + right;
                }

                if (rString != null)
                {
                    return left + rString;
                }

                if (lDouble != null && rDouble != null)
                {
                    return lDouble + rDouble;
                }

                throw new InvalidOperationRuntimeExplosion(element, $"Cannot execute gain on {left?.GetType()} and {right?.GetType()}");
            
            case TokenTypes.Drain:
                if (lDouble != null && rDouble != null)
                {
                    return lDouble - rDouble;
                }

                throw new InvalidOperationRuntimeExplosion(element, $"Cannot execute drain on {left?.GetType()} and {right?.GetType()}");
            
            case TokenTypes.Amplify:
                if (lDouble != null && rDouble != null)
                {
                    return lDouble * rDouble;
                }

                throw new InvalidOperationRuntimeExplosion(element, $"Cannot execute amplify on {left?.GetType()} and {right?.GetType()}");
            
            case TokenTypes.Disperse:
                if (lDouble != null && rDouble != null)
                {
                    return lDouble / rDouble;
                }

                throw new InvalidOperationRuntimeExplosion(element, $"Cannot execute disperse on {left?.GetType()} and {right?.GetType()}");
            
            default:
                throw new ParserRuntimeExplosion(element, "Invalid logical operation");
        }
    }

    public object VisitLiteral(LiteralElement element)
    {
        return element.Value;
    }
}