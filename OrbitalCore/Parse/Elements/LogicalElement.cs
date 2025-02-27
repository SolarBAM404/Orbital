using OrbitalCore.Lex;
using OrbitalCore.Parse.Visitors;

namespace OrbitalCore.Parse.Elements;

public class LogicalElement<T>(IOrbitalElement<T> left, Token @operator, IOrbitalElement<T> right) : IOrbitalElement<T>
{
    public IOrbitalElement<T> Left { get; set; } = left;
    public Token Operator { get; set; } = @operator;
    public IOrbitalElement<T> Right { get; set; } = right;
    
    public T Accept(IOrbitalVisitor<T> visitor)
    {
        return visitor.VisitLogical(this);
    }
}