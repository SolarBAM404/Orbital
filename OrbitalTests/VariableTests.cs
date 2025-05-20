using OrbitalCore;
using OrbitalCore.Explosions;

namespace OrbitalTests;
[TestFixture]
public class VariableTests
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

    private string[] GetOutput()
    {
        return _stringWriter.ToString().Trim().Split(Environment.NewLine);
    }

    [Test]
    public void VariableTests_IntegerVariable()
    {
        string code = "x = 1; uplink(x);";
        Orbital.Run(code);
        Assert.That(GetOutput()[0], Is.EqualTo("1"));
    }

    [Test]
    public void VariableTests_FloatVariable()
    {
        string code = "x = 1.5; uplink(x);";
        Orbital.Run(code);
        Assert.That(GetOutput()[0], Is.EqualTo("1.5"));
    }

    [Test]
    public void VariableTests_StringVariable()
    {
        string code = "x = \"Hello, World!\"; uplink(x);";
        Orbital.Run(code);
        Assert.That(GetOutput()[0], Is.EqualTo("Hello, World!"));
    }

    [Test]
    public void VariableTests_VariableAssignment()
    {
        string code = "x = 1; y = x; uplink(y);";
        Orbital.Run(code);
        Assert.That(GetOutput()[0], Is.EqualTo("1"));
    }

    [Test]
    public void VariableTests_VariableReassignment()
    {
        string code = "x = 1; x = 2; uplink(x);";
        Orbital.Run(code);
        Assert.That(GetOutput()[0], Is.EqualTo("2"));
    }

    [Test]
    public void VariableTests_VariableAssignmentWithExpression()
    {
        string code = "x = 1; y = x gain 1; uplink(y);";
        Orbital.Run(code);
        Assert.That(GetOutput()[0], Is.EqualTo("2"));
    }

    [Test]
    public void VariableTests_VariableComparison()
    {
        string code = "x = 1; y = 1; uplink(x align y);";
        Orbital.Run(code);
        Assert.That(GetOutput()[0], Is.EqualTo("True"));
    }

    [Test]
    public void VariableTests_VariableComparisonWithString()
    {
        string code = "x = \"Hello, World!\"; y = \"Hello, World!\"; uplink(x align y);";
        Orbital.Run(code);
        Assert.That(GetOutput()[0], Is.EqualTo("True"));
    }

    [Test]
    public void VariableTests_Phase1()
    {
        string code = "quickMaths = 10; quickMaths = quickMaths gain 2; uplink(quickMaths);";
        Orbital.Run(code);
        Assert.That(GetOutput()[0], Is.EqualTo("12"));
    }

    [Test]
    public void VariableTests_Phase2()
    {
        string code = "floatTest = 1.0; floatTest = floatTest gain 5.5; uplink(floatTest);";
        Orbital.Run(code);
        Assert.That(GetOutput()[0], Is.EqualTo("6.5"));
    }

    [Test]
    public void VariableTests_Phase3()
    {
        string code = "stringCatTest = \"10 corgis\"; stringCatTest = stringCatTest gain 5 gain \" more corgis\"; uplink(stringCatTest);";
        Orbital.Run(code);
        Assert.That(GetOutput()[0], Is.EqualTo("10 corgis5 more corgis"));
    }

    [Test]
    public void VariableTests_Phase4()
    {
        string code = "errorTest = 5; uplink(errorTest gain false);";
        Assert.Throws<InvalidOperationRuntimeExplosion>(() => Orbital.Run(code));
    }

    [Test]
    public void VariableTests_LocalVariable_Phase1()
    {
        string code = "x = 1; uplink(x);";
        Orbital.Run(code);
        Assert.That(GetOutput()[0], Is.EqualTo("1"));
    }

    [Test]
    public void VariableTests_LocalVariable_Phase2()
    {
        string code = "x = 1; uplink(x gain 1); uplink(x);";
        Orbital.Run(code);
        var output = GetOutput();
        Assert.That(output[0], Is.EqualTo("2"));
        Assert.That(output[1], Is.EqualTo("1"));
    }

    [Test]
    public void VariableTests_LocalVariable_Phase3()
    {
        string code = "x = 1; probe (x align 1) {y = 2; uplink(y);}; uplink(x); uplink(y);";
        Orbital.Run(code);
        var output = GetOutput();
        Assert.That(output[0], Is.EqualTo("2"));
        Assert.That(output[1], Is.EqualTo("1"));
        Assert.That(output.Length, Is.EqualTo(2));
    }
}