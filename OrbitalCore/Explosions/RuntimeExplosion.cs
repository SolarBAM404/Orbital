using OrbitalCore.Parse;

namespace OrbitalCore;

public abstract class RuntimeExplosion(IOrbitalElement element, string message) : Exception(message)
{
    public IOrbitalElement Element { get; } = element;
}