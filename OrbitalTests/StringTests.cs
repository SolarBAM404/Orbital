using OrbitalCore;

namespace OrbitalTests;

[TestFixture]
public class StringTests
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
    public void StringTests_BaseString()
    {
        string code = "uplink(\"Hello, World!\");";
        Orbital.Run(code);
        Assert.That(GetOutput(), Is.EqualTo("Hello, World!"));
    }

    [Test]
    public void StringTests_EmptyString()
    {
        string code = "uplink(\"\");";
        Orbital.Run(code);
        Assert.That(GetOutput(), Is.EqualTo(""));
    }

    [Test]
    public void StringTests_StringWithNumber()
    {
        string code = "uplink(\"123\");";
        Orbital.Run(code);
        Assert.That(GetOutput(), Is.EqualTo("123"));
    }

    [Test]
    public void StringTests_StringWithSpecialCharacters()
    {
        string code = "uplink(\"!@#$%^&*()\");";
        Orbital.Run(code);
        Assert.That(GetOutput(), Is.EqualTo("!@#$%^&*()"));
    }

    [Test]
    public void StringTests_ConcatenatedStrings()
    {
        string code = "uplink(\"Hello, \" gain \"World!\");";
        Orbital.Run(code);
        Assert.That(GetOutput(), Is.EqualTo("Hello, World!"));
    }

    [Test]
    public void StringTests_ComparisonOperators()
    {
        string code = "uplink(\"Hello, World!\" align \"Hello, World!\");";
        Orbital.Run(code);
        Assert.That(GetOutput(), Is.EqualTo("True"));
    }

    [Test]
    public void StringTests_FalseComparisonOperators()
    {
        string code = "uplink(\"Hello, World!\" align \"Hello, World\");";
        Orbital.Run(code);
        Assert.That(GetOutput(), Is.EqualTo("False"));
    }

    [Test]
    public void StringTests_ConcatenatedStringsAndComparisonOperators()
    {
        string code = "uplink(\"Hello, \" gain \"World!\" align \"Hello, World!\");";
        Orbital.Run(code);
        Assert.That(GetOutput(), Is.EqualTo("True"));
    }

    [Test]
    public void StringTests_Phase1()
    {
        string code = "uplink(\"hello\" gain \"world\");";
        Orbital.Run(code);
        Assert.That(GetOutput(), Is.EqualTo("helloworld"));
    }

    [Test]
    public void StringTests_Phase2()
    {
        string code = "uplink(\"foo\" gain \"bar\" align \"foobar\");";
        Orbital.Run(code);
        Assert.That(GetOutput(), Is.EqualTo("True"));
    }

    [Test]
    public void StringTests_Phase3()
    {
        string code = "uplink(\"10 corgis\" disrupt \"10\" gain \"corgis\");";
        Orbital.Run(code);
        Assert.That(GetOutput(), Is.EqualTo("True"));
    }

    [Test]
    public void StringTests_Phase4()
    {
        string code = "uplink(1 gain \"0\");";
        Orbital.Run(code);
        Assert.That(GetOutput(), Is.EqualTo("10"));
    }

    [Test]
    public void StringTests_Phase5()
    {
        string code = "uplink(\"false\" align void);";
        Orbital.Run(code);
        Assert.That(GetOutput(), Is.EqualTo("False"));
    }
}