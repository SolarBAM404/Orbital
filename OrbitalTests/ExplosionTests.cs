using OrbitalCore;
using OrbitalCore.Explosions;

namespace OrbitalTests;

[TestFixture]
public class ExplosionTests
{
    private StringWriter _stringWriter;

    [SetUp]
    public void Setup()
    {
        _stringWriter = new StringWriter();
        Console.SetOut(_stringWriter);
    }

    [TearDown]
    public void TearDown()
    {
        Console.SetOut(Console.Out);
        _stringWriter.Dispose();
    }

    [Test]
    public void ExplosionError_InvalidAddition_ThrowsException()
    {
        string code = "5 gain void;";
        Assert.Throws<InvalidOperationRuntimeExplosion>(() => Orbital.Run(code));
    }
    
    [Test]
    public void ExplosionError_InvalidSubtraction_ThrowsException()
    {
        string code = "5 drain \"Hello\";";
        Assert.Throws<InvalidOperationRuntimeExplosion>(() => Orbital.Run(code));
    }
    
    [Test]
    public void ExplosionError_InvalidMultiplication_ThrowsException()
    {
        string code = "5 amplify \"Hello\";";
        Assert.Throws<InvalidOperationRuntimeExplosion>(() => Orbital.Run(code));
    }
    
    [Test]
    public void ExplosionError_InvalidDivision_ThrowsException()
    {
        string code = "5 disperse \"Hello\";";
        Assert.Throws<InvalidOperationRuntimeExplosion>(() => Orbital.Run(code));
    }

    [Test]
    public void ExplosionError_OutsideOfScope_ThrowsException()
    {
        string code = "probe(signal) { x = 5; } uplink(x);";
        Assert.Throws<InvalidVariableExplosion>(() => Orbital.Run(code));
    }
}