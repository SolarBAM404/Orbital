using OrbitalCore;
using OrbitalCore.Parse;
using OrbitalCore.Parse.Nodes;
using OrbitalCore.Parse.Nodes.Abstract;
using OrbitalCore.Parse.Nodes.BasicTypes;
using OrbitalCore.Parse.Nodes.BuiltInFunctions;

namespace OrbitalTests;

[TestFixture]
public class FunctionalTests
{
    
    [SetUp]
    public void SetUp()
    {
        Parser.GlobalVariables = new Dictionary<string, VariableNode>();
    }
    
    [Test]
    public void FunctionalTests_UplinkTest()
    {
        string code = "x = 1; y = 2; z = x gain y; uplink(z);";
        List<object?> results = Evaluator.EvaluateAndExecute(code);
        Assert.Multiple(() =>
        {
            Assert.That(Parser.GlobalVariables["z"].Evaluate(), Is.EqualTo(3.0d));
        });
    }

    [Test]
    public void FunctionalTest_BasicRelayTest()
    {
        // Arrange
        var input = "test input";
        using var stringReader = new StringReader(input);
        Console.SetIn(stringReader);

        var relayCmdNode = new RelayCmdNode(new List<AbstractTreeNode>());

        // Act
        var result = relayCmdNode.Evaluate();

        // Assert
        Assert.That(result, Is.EqualTo(input));
    }

    [Test]
    public void FunctionalTest_RelayTest()
    {
        string code = "x = relay();";
        string reply = "hello";
        using StringReader stringReader = new(reply);
        Console.SetIn(stringReader);
        List<object?> results = Evaluator.EvaluateAndExecute(code);
        Assert.Multiple(() =>
        {
            Assert.That(Parser.GlobalVariables["x"].Evaluate(), Is.EqualTo(reply));
        });
    }
    
}