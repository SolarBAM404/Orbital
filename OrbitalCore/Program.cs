// See https://aka.ms/new-console-template for more information

using OrbitalCore;

if (args.Length == 0)
{
    string input = "";
    while (input != "exit")
    {
        Console.Write("> ");
        input = Console.ReadLine();
        if (input == "exit")
        {
            break;
        }
        // List<object?> results = Evaluator.EvaluateAndExecute(input);
    }

    return;
}

// if path is provided, read the file and execute the code

string path = args[0];
string code = File.ReadAllText(path);
// List<object?> fileResults = Evaluator.EvaluateAndExecute(code);