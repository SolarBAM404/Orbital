using OrbitalCore.Parse;
using OrbitalCore.Parse.Elements;

namespace OrbitalCore.Explosions;

public class InvalidVariableExplosion(IOrbitalElement element, string message) : RuntimeExplosion(element, message)
{
    public override string ToString()
    {
        if (Element is VariableElement variableElement)
        {
            return $"Invalid variable '{variableElement.Name}' at {variableElement.Name.Line}. \n{message}";
        }

        return message;
    }
}