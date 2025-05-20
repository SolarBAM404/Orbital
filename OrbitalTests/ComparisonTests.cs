using OrbitalCore;

namespace OrbitalTests;

[TestFixture]
public class ComparisonTests
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
    public void Comparison_Equal()
    {
        // Evaluate the following code:
        // 1 == 1
        string code = "uplink(1 align 1);";
        Orbital.Run(code);
        Assert.That(GetOutput(), Is.EqualTo("True"));
    }

    [Test]
    public void Comparison_NotEqual()
    {
        // Evaluate the following code:
        // 1 != 2
        string code = "uplink(1 disrupt 2);";
        Orbital.Run(code);
        Assert.That(GetOutput(), Is.EqualTo("True"));
    }

    [Test]
    public void Comparison_GreaterThan()
    {
        // Evaluate the following code:
        // 2 > 1
        string code = "uplink(2 above 1);";
        Orbital.Run(code);
        Assert.That(GetOutput(), Is.EqualTo("True"));
    }

    [Test]
    public void Comparison_LessThan()
    {
        // Evaluate the following code:
        // 1 < 2
        string code = "uplink(1 below 2);";
        Orbital.Run(code);
        Assert.That(GetOutput(), Is.EqualTo("True"));
    }

    [Test]
    public void Comparison_GreaterThanOrEqual()
    {
        // Evaluate the following code:
        // 2 >= 1
        string code = "uplink(2 safe 1);";
        Orbital.Run(code);
        Assert.That(GetOutput(), Is.EqualTo("True"));
    }

    [Test]
    public void Comparison_LessThanOrEqual()
    {
        // Evaluate the following code:
        // 1 <= 2
        string code = "uplink(1 unsafe 2);";
        Orbital.Run(code);
        Assert.That(GetOutput(), Is.EqualTo("True"));
    }
    
    [Test]
    public void Comparison_Negate()
    {
        // Evaluate the following code:
        // !true
        string code = "uplink(negate(signal));";
        Orbital.Run(code);
        Assert.That(GetOutput(), Is.EqualTo("False"));
    }

    [Test]
    public void Comparison_NegateWithExtra()
    {
        // Evaluate the following code:
        // !true == false
        string code = "uplink(negate(signal) align void);";
        Orbital.Run(code);
        Assert.That(GetOutput(), Is.EqualTo("True"));
    }

    [Test]
    public void Comparison_NegateWithExtra2()
    {
        // Evaluate the following code:
        // !(true != false)
        string code = "uplink(negate(signal disrupt void));";
        Orbital.Run(code);
        Assert.That(GetOutput(), Is.EqualTo("False"));
    }

    [Test]
    public void Comparison_Phrase1()
    {
        // Evaluate the following code:
        // true == true
        string code = "uplink(signal align signal);";
        Orbital.Run(code);
        Assert.That(GetOutput(), Is.EqualTo("True"));
    }

    [Test]
    public void Comparison_Phrase2()
    {
        // Evaluate the following code:
        // true != false
        string code = "uplink(signal disrupt void);";
        Orbital.Run(code);
        Assert.That(GetOutput(), Is.EqualTo("True"));
    }

    [Test]
    public void Comparison_Phrase3()
    {
        // Evaluate the following code:
        // (5 < 10)
        string code = "uplink(5 below 10);";
        Orbital.Run(code);
        Assert.That(GetOutput(), Is.EqualTo("True"));
    }

    [Test]
    public void Comparison_Phrase4()
    {
        // Evaluate the following code:
        // !(5 - 4 > 3 * 2 == !false)
        string code = "uplink(negate((5 drain 4 above 3 amplify 2) align negate(void)));";
        Orbital.Run(code);
        Assert.That(GetOutput(), Is.EqualTo("True"));
    }

    [Test]
    public void Comparison_Phrase5()
    {
        // Evaluate the following code:
        // true and true
        string code = "uplink(signal stable signal);";
        Orbital.Run(code);
        Assert.That(GetOutput(), Is.EqualTo("True"));
    }

    [Test]
    public void Comparison_Phrase6()
    {
        // Evaluate the following code:
        // false or true
        string code = "uplink(void path signal);";
        Orbital.Run(code);
        Assert.That(GetOutput(), Is.EqualTo("True"));
    }

    [Test]
    public void Comparison_Phrase7()
    {
        // Evaluate the following code:
        // (0 < 1) or false
        string code = "uplink((0 below 1) path void);";
        Orbital.Run(code);
        Assert.That(GetOutput(), Is.EqualTo("True"));
    }

    [Test]
    public void Comparison_Phrase8()
    {
        // Evaluate the following code:
        // false or false
        string code = "uplink(void path void);";
        Orbital.Run(code);
        Assert.That(GetOutput(), Is.EqualTo("False"));
    }
}