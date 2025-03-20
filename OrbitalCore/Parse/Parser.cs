using OrbitalCore.Lex;
using OrbitalCore.Parse.Elements;

namespace OrbitalCore.Parse;

public class Parser
{
    private List<Token> _tokens;
    
    public Parser(List<Token> tokens)
    {
        _tokens = tokens;
    }
    
    public List<IOrbitalElement<object>> Parse()
    {
        List<IOrbitalElement<object>> elements = new();
        return elements;
    }
    
    private IOrbitalElement<object> ParseElement()
    {
        return new LiteralElement<object>(null);
    }
}