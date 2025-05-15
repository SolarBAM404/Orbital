using OrbitalCore;

namespace OrbitalTests;

[TestFixture]
public class StatementTests
{
    private StringWriter _stringWriter;

    [SetUp]
    public void Setup()
    {
        // Redirect console output to a StringWriter
        _stringWriter = new StringWriter();
        Console.SetOut(_stringWriter);
    }

    [TearDown]
    public void TearDown()
    {
        // Reset console output
        Console.SetOut(Console.Out);
        _stringWriter.Dispose();
    }

    private string GetOutput()
    {
        return _stringWriter.ToString().Trim();
    }

    [Test]
    public void Statement_BasicIfStatement()
    {
        string code = "x = 1; probe(x align 1) { x = x gain 1; }; uplink(x);";
        Orbital.Run(code);
        Assert.That(GetOutput(), Is.EqualTo("2"));
    }

    [Test]
    public void Statement_IfElseStatement_IfSide()
    {
        string code = "x = 2; probe(x align 2) { x = x gain 1; } scan { x = x drain 1; }; uplink(x);";
        Orbital.Run(code);
        Assert.That(GetOutput(), Is.EqualTo("3"));
    }

    [Test]
    public void Statement_IfElseStatement_ElseSide()
    {
        string code = "x = 1; probe(x align 2) { x = x gain 1; } scan { x = x drain 1; }; uplink(x);";
        Orbital.Run(code);
        Assert.That(GetOutput(), Is.EqualTo("0"));
    }

    [Test]
    public void Statement_IfElseStatement_NestedIf()
    {
        string code = "x = 1; probe(x align 1) { probe(x align 1) { x = x gain 1; } scan { x = x drain 1; }; }; uplink(x);";
        Orbital.Run(code);
        Assert.That(GetOutput(), Is.EqualTo("2"));
    }

    [Test]
    public void Statement_IfElseStatement_NestedIfElse()
    {
        string code = "x = 1; probe(x below 2) { probe(x below 1) { x = x gain 1; } scan { x = x drain 1; }; }; uplink(x);";
        Orbital.Run(code);
        Assert.That(GetOutput(), Is.EqualTo("0"));
    }

    [Test]
    public void Statement_IfElseStatement_NestedIf_ScanSide()
    {
        string code = "x = 1; probe(x align 2) { probe(x align 1) { x = x gain 1; } } " +
                      "scan { x = x drain 1; probe ( x align 1) { x = x gain 2; } scan { x = x drain 1; }; }; uplink(x);";
        Orbital.Run(code);
        Assert.That(GetOutput(), Is.EqualTo("-1"));
    }

    [Test]
    public void Statement_WhileStatement()
    {
        string code = "x = 1; orbit (x below 5) { x = x gain 1; }; uplink(x);";
        Orbital.Run(code);
        Assert.That(GetOutput(), Is.EqualTo("5"));
    }

    [Test]
    public void Statement_WhileStatement_NestedWhile()
    {
        string code = "x = 1; orbit (x below 5) { orbit (x below 3) { x = x gain 1; }; x = x gain 2; }; uplink(x);";
        Orbital.Run(code);
        Assert.That(GetOutput(), Is.EqualTo("5"));
    }

    [Test]
    public void Statement_WhileStatement_IfStatement()
    {
        string code = "x = 1; orbit (x below 5) { probe(x align 3) { x = x gain 2; } scan { x = x gain 1; }; }; uplink(x);";
        Orbital.Run(code);
        Assert.That(GetOutput(), Is.EqualTo("5"));
    }
}