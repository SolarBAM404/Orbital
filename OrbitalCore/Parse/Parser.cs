using OrbitalCore.Lex;

namespace OrbitalCore.Parse;

public class Parser
{
    private List<Token> _tokens;
    
    public Parser(List<Token> tokens)
    {
        _tokens = tokens;
    }
}