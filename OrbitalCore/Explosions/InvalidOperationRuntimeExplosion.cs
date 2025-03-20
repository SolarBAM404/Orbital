using OrbitalCore.Lex;
using OrbitalCore.Parse;

namespace OrbitalCore;

public class InvalidOperationRuntimeExplosion(IOrbitalElement token, string message) : RuntimeExplosion(token, message)
{
    public IOrbitalElement Element { get; } = token;
}