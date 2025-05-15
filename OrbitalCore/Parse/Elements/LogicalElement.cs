using OrbitalCore.Lex;
using OrbitalCore.Parse.Visitors;

namespace OrbitalCore.Parse.Elements;

public class LogicalElement(IOrbitalElement left, Token? @operator, IOrbitalElement right) : IOrbitalElement
{
    public IOrbitalElement Left { get; set; } = left;
    public Token? Operator { get; set; } = @operator;
    public IOrbitalElement Right { get; set; } = right;
    
    public object? Accept(IOrbitalVisitor visitor)
    {
        return visitor.VisitLogical(this);
    }
}