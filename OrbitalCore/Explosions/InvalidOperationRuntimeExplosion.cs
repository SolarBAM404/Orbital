using OrbitalCore.Parse;

namespace OrbitalCore.Explosions;

public class InvalidOperationRuntimeExplosion(IOrbitalElement token, string message) : RuntimeExplosion(token, message)
{
}