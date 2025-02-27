using OrbitalCore.Parse.Visitors;

namespace OrbitalCore.Parse;

public interface IOrbitalElement
{
    object? Accept(IOrbitalVisitor visitor);
}