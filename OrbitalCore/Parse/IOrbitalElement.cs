using OrbitalCore.Parse.Visitors;

namespace OrbitalCore.Parse;

public interface IOrbitalElement<T>
{
    T Accept(IOrbitalVisitor<T> visitor);
}