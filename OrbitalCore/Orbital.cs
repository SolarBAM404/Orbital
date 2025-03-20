using OrbitalCore.Lex;
using OrbitalCore.Parse;

namespace OrbitalCore;

public class Orbital
{
    public static void Run(string code)
    {
        // Lexical Analysis
        Scanner lexer = new Scanner(code);
        List<Token> tokens = lexer.ScanTokens();

        // Syntactic Analysis
        Parser parser = new Parser(tokens);
        List<IOrbitalElement<object>> elements = parser.Parse();

        // Semantic Analysis
        Interpreter interpreter = new Interpreter();
        foreach (IOrbitalElement<object> element in elements)
        {
            element.Accept(interpreter);
        }
    }
}