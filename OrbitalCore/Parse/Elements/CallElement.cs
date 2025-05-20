using OrbitalCore.Lex;
using OrbitalCore.Parse.Visitors;

namespace OrbitalCore.Parse.Elements;

public class CallElement(IOrbitalElement callee, Token? paren, List<IOrbitalElement> arguments)
    : IOrbitalElement
{
    public IOrbitalElement Callee { get; } = callee;
    public Token? Paren { get; } = paren;
    public List<IOrbitalElement> Arguments { get; } = arguments;

    public object? Accept(IOrbitalVisitor visitor)
    {
        return visitor.VisitCall(this);
    }
}