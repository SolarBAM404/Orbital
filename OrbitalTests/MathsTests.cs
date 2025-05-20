using OrbitalCore;

namespace OrbitalTests;

[TestFixture]
public class MathsTests
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
    public void Maths_Addition_ReturnsCorrectValue()
    {
        // Evaluate the following code:
        // 1 - 2
        string code = "uplink(1 drain 2);";
        Orbital.Run(code);
        // check if the output to the console is correct (1 - 2 = -1)
        Assert.That(GetOutput(), Is.EqualTo("-1"));
    }

    [Test]
    public void Maths_AdditionSubtraction_ReturnsCorrectValue()
    {
        // Evaluate the following code:
        // 2.5 + 2.5 - 1.25
        string code = "uplink(2.5 gain 2.5 drain 1.25);";
        
        Orbital.Run(code);
        Assert.That(GetOutput(), Is.EquivalentTo("3.75"));
    }
    
    [Test]
    public void Maths_Parenthesis_ReturnsCorrectValue()
    {
        // Evaluate the following code:
        // (10 * 2) / 6
        string code = "uplink((10 amplify 2) disperse 6);";
        Orbital.Run(code);
        Assert.That(GetOutput(), Is.EqualTo("3.3333333333333335"));
    }

    [Test]
    public void Maths_Bodmas_ReturnsCorrectValue()
    {
        // Evaluate the following code:
        // 8.5 / (2 * 9) - -3
        string code = "uplink(8.5 disperse (2 amplify 9) drain -3);";
        Orbital.Run(code);
        Assert.That(GetOutput(), Is.EqualTo("3.4722222222222223"));
    }
    
    [Test]
    public void Maths_ComplexExpression_ReturnsCorrectValue()
    {
        // Evaluate the following code:
        // 1 + 2 * 3 - 4 / 5
        string code = "uplink(1 gain 2 amplify 3 drain 4 disperse 5);";
        Orbital.Run(code);
        Assert.That(GetOutput(), Is.EqualTo("6.2"));
    }

}