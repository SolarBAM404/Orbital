using OrbitalCore.Parse;

namespace OrbitalCore.Explosions;

public abstract class RuntimeExplosion(IOrbitalElement element, string message) : Exception(message)
{
    public IOrbitalElement Element { get; } = element;
}