using OrbitalCore.Lex;
using OrbitalCore.Parse;

namespace OrbitalCore;

public class Orbital
{
    public static void Run(string source)
    {
        Scanner scanner = new Scanner(source);
        List<Token?> tokens = scanner.ScanTokens();

        Parser parser = new Parser(tokens);
        List<IOrbitalElement> orbitalElements = parser.Parse();

        Interpreter interpreter = new Interpreter();
        interpreter.Interpret(orbitalElements);

    }
}