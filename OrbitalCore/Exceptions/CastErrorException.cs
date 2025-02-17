namespace OrbitalCore.Exceptions;

public class CastErrorException(Type from, Type to) : AbstractException
{
    public Type FromType { get; } = from;
    public Type ToType { get; } = to;
}