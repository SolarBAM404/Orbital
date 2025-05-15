using OrbitalCore.Lex;
using OrbitalCore.Parse;
using OrbitalCore.Parse.Elements;
using OrbitalCore.Parse.Visitors;

namespace OrbitalCore;

public class Interpreter : IOrbitalVisitor
{
    
    private Scope _scope = new();
    
    public void Interpret(List<IOrbitalElement> elements)
    {
        foreach (IOrbitalElement element in elements)
        {
            element.Accept(this);
        }
    }

    public object VisitLogical(LogicalElement element)
    {
        object left = element.Left.Accept(this);
        object right = element.Right.Accept(this);

        double? lDouble = left is double ld ? ld : null;
        double? rDouble = right is double rd ? rd : null;

        string? lString = left is string ls ? ls : null;
        string? rString = right is string rs ? rs : null;

        bool? lBool = left is bool lb ? lb : null;
        bool? rBool = right is bool rb ? rb : null;

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
            case TokenTypes.Path:
                if (left != null && right != null )
                {
                    return (bool)lBool || (bool)rBool;
                }
                throw new InvalidOperationException("Cannot execute path on non-boolean values");
            case TokenTypes.Stable:
                if (lBool != null && rBool != null)
                {
                    return (bool)lBool && (bool)rBool;
                }
                
                throw new InvalidOperationException("Cannot execute stale path on non-boolean values");
            case TokenTypes.Align: 
                if (lDouble != null && rDouble != null)
                {
                    return lDouble == rDouble;
                } else if (lString != null && rString != null)
                {
                    return lString == rString;
                } else if (lBool != null && rBool != null)
                {
                    return lBool == rBool;
                }
                else if (left == null && right == null)
                {
                    return true;
                }
                
                return false;
            
            case TokenTypes.Disrupt:
                if (lDouble != null && rDouble != null)
                {
                    return lDouble != rDouble;
                } else if (lString != null && rString != null)
                {
                    return lString != rString;
                } else if (lBool != null && rBool != null)
                {
                    return lBool != rBool;
                }
                else if (left == null && right == null)
                {
                    return false;
                }

                return true;
            case TokenTypes.Above:
                if (lDouble != null && rDouble != null)
                {
                    return lDouble > rDouble;
                }
                else if (lString != null && rString != null)
                {
                    return lString.CompareTo(rString) > 0;
                }
                return false;
            case TokenTypes.Below:
                if (lDouble != null && rDouble != null)
                {
                    return lDouble < rDouble;
                }
                else if (lString != null && rString != null)
                {
                    return lString.CompareTo(rString) < 0;
                }
                return false;
            case TokenTypes.Safe:
                if (lDouble != null && rDouble != null)
                {
                    return lDouble >= rDouble;
                }
                else if (lString != null && rString != null)
                {
                    return lString.CompareTo(rString) >= 0;
                }
                return false;
            case TokenTypes.Unsafe:
                if (lDouble != null && rDouble != null)
                {
                    return lDouble <= rDouble;
                }
                else if (lString != null && rString != null)
                {
                    return lString.CompareTo(rString) <= 0;
                }

                return false;
            default:
                throw new ParserRuntimeExplosion(element, "Invalid logical operation");
        }
    }

    public object? VisitLiteral(LiteralElement element)
    {
        return element.Value;
    }

    public void VisitUplink(UplinkElement element)
    {
        object? value = Evaluate(element.Value);
        if (value is LiteralElement literalElement)
        {
            value = Evaluate(literalElement);
        }
        Console.Out.WriteLine(value);
    }
    
    public void VisitExpressionBlock(ExpressionBlockElement element)
    {
        return;
    }
    
    public object? VisitBlock(BlockElement element)
    {
        Scope newScope = new();
        Scope(newScope); // Push a new scope
        try
        {
            Interpret(element.Statements);
        }
        finally
        {
            _scope = _scope.Parent!; // Pop the scope
        }
        return null;
    }
    
    public void VisitEof()
    {
        return;
    }

    public object? VisitUnary(UnaryElement unaryElement)
    {
        throw new NotImplementedException();
    }

    public object? VisitGroup(GroupElement groupElement)
    {
        return Evaluate(groupElement.Element);
    }
    
    public object? VisitVariable(VariableElement variableElement)
    {
        if (variableElement.Name == null)
        {
            throw new InvalidOperationRuntimeExplosion(variableElement, "Variable name is null");
        }

        if (Scope()[variableElement] != null)
        {
            return Scope()[variableElement];
        }
        else
        {
            Scope().Add(variableElement, null);
        }

        return null;
    }

    public void VisitAssignment(AssignElement variableStatementElement)
    {
        object? value = Evaluate(variableStatementElement.Value);
    
        if (Scope().Contains(variableStatementElement.VariableElement))
        {
            Scope().Update(variableStatementElement.VariableElement, value);
        }
        else
        {
            Scope().Add(variableStatementElement.VariableElement, value);
        }
    }

    public object? VisitNegate(NegateElement negateElement)
    {
        object? value = Evaluate(negateElement.Element);
        if (value is bool b)
        {
            return !b;
        }
        else if (value is double d)
        {
            return -d;
        }
        else if (value is string s)
        {
            return Stringy(value);
        }

        throw new InvalidOperationRuntimeExplosion(negateElement, $"Cannot negate {value?.GetType()}");
    }

    public object? VisitProbe(ProbeElement probeElement)
    {
        // Evaluate the main condition
        object? conditionResult = Evaluate(probeElement.IfCondition);
    
        if (conditionResult is bool condition && condition)
        {
            // Execute the "if" branch
            return Evaluate(probeElement.IfThenCondition);
        }
    
        // Check for "else if" conditions
        for (int i = 0; i < probeElement.ElseIfConditions.Count; i++)
        {
            object? elseIfConditionResult = Evaluate(probeElement.ElseIfConditions[i]);
            if (elseIfConditionResult is bool elseIfCondition && elseIfCondition)
            {
                return Evaluate(probeElement.ElseIfThenConditions[i]);
            }
        }

        return probeElement is { ElseCondition: not null, ElseThenCondition: not null } 
            ? Evaluate(probeElement.ElseThenCondition) : null; // Null = No branch executed
    }
    
    public object? VisitOrbit(OrbitElement orbitElement)
    {
        object? condictionResult = Evaluate(orbitElement.Condition);

        if (orbitElement.Body == null) throw new InvalidOperationRuntimeExplosion(orbitElement, "Orbit body is null");
        while (condictionResult is bool condition && condition)
        {
            // Execute the body
            Evaluate(orbitElement.Body);

            // Re-evaluate the condition
            condictionResult = Evaluate(orbitElement.Condition);
        }
        return null;
    }

    private object? Evaluate(IOrbitalElement element)
    {
        return element.Accept(this);
    }

    private string Stringy(object? value)
    {
        if (value == null)
        {
            return "null";
        }
        string? stringy = value.ToString();

        return stringy ?? "null";
    }
    
    private void Scope(Scope scope)
    {
        scope.Parent = _scope;
        _scope = scope;
    }
    
    private Scope Scope()
    {
        return _scope;
    }
}
