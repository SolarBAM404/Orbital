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

        Run(input);
    }

    return;
}

// if path is provided, read the file and execute the code

string path = args[0];

if (!File.Exists(path))
{
    Console.WriteLine($"File not found: {path}");
    return;
}

if (Path.GetExtension(path) != ".orbital")
{
    Console.WriteLine($"Invalid file extension: {path}");
    return;
}

string code = File.ReadAllText(path);
Run(code);

void Run(string code)
{
    try
    {
        Orbital.Run(code);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"{ex.GetType()} - {ex.Message}");
    }
}