using OrbitalCore;
using OrbitalCore.Parse.Nodes;
using OrbitalCore.Parse.Nodes.BasicTypes;
using OrbitalCore.Parse.Nodes.Expressions;

namespace OrbitalTests;

[TestFixture]
public class VariableTests
{
    
    [Test]
    public void VariableTests_IntegerVariable()
    {
        string code = "x = 1;";
        List<object?> results = Evaluator.EvaluateAndExecute(code);
        Assert.Multiple(() =>
        {
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0], Is.InstanceOf(typeof(NumberNode)));
            Assert.That(((NumberNode)results[0]!).Value, Is.EqualTo(1));
        });
    }
    
    [Test]
    public void VariableTests_FloatVariable()
    {
        string code = "x = 1.5;";
        List<object?> results = Evaluator.EvaluateAndExecute(code);
        Assert.Multiple(() =>
        {
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0], Is.InstanceOf(typeof(NumberNode)));
            Assert.That(((NumberNode)results[0]!).Value, Is.EqualTo(1.5));
        });
    }
    
    [Test]
    public void VariableTests_StringVariable()
    {
        string code = "x = \"Hello, World!\";";
        List<object?> results = Evaluator.EvaluateAndExecute(code);
        Assert.Multiple(() =>
        {
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0], Is.InstanceOf(typeof(StringNode)));
            Assert.That(((StringNode)results[0]!).Value, Is.EqualTo("Hello, World!"));
        });
    }
    
    [Test]
    public void VariableTests_VariableAssignment()
    {
        string code = "x = 1; y = x;";
        List<object?> results = Evaluator.EvaluateAndExecute(code);
        Assert.Multiple(() =>
        {
            Assert.That(results.Count, Is.EqualTo(2));
            Assert.That(results[0], Is.InstanceOf(typeof(NumberNode)));
            Assert.That(((NumberNode)results[0]!).Value, Is.EqualTo(1));
            Assert.That(results[1], Is.InstanceOf(typeof(NumberNode)));
            Assert.That(((NumberNode)results[1]!).Value, Is.EqualTo(1));
        });
    }
    
    [Test]
    public void VariableTests_VariableReassignment()
    {
        string code = "x = 1; x = 2;";
        List<object?> results = Evaluator.EvaluateAndExecute(code);
        Assert.Multiple(() =>
        {
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0], Is.InstanceOf(typeof(NumberNode)));
            Assert.That(((NumberNode)results[0]!).Evaluate(), Is.EqualTo(2));
        });
    }
    
    [Test]
    public void VariableTests_VariableAssignmentWithExpression()
    {
        string code = "x = 1; y = x gain 1;";
        List<object?> results = Evaluator.EvaluateAndExecute(code);
        Assert.Multiple(() =>
        {
            Assert.That(results.Count, Is.EqualTo(2));
            Assert.That(results[0], Is.InstanceOf(typeof(NumberNode)));
            Assert.That(((NumberNode)results[0]!).Evaluate(), Is.EqualTo(1));
            Assert.That(results[1], Is.InstanceOf(typeof(MathematicalBinaryExpressionNode)));
            Assert.That(((MathematicalBinaryExpressionNode)results[1]!).Evaluate(), Is.EqualTo(2));
        });
    }

    [Test]
    public void VariableTests_VariableComparison()
    {
        string code = "x = 1; y = 1; x align y;";
        List<object?> results = Evaluator.EvaluateAndExecute(code);
        Assert.Multiple(() =>
        {
            Assert.That(results.Count, Is.EqualTo(3));
            Assert.That(results[0], Is.InstanceOf(typeof(NumberNode)));
            Assert.That(((NumberNode)results[0]!).Evaluate(), Is.EqualTo(1));
            Assert.That(results[1], Is.InstanceOf(typeof(NumberNode)));
            Assert.That(((NumberNode)results[1]!).Evaluate(), Is.EqualTo(1));
            Assert.That(results[2], Is.EqualTo(true));
        });
    }

    [Test]
    public void VariableTests_VariableComparisonWithString()
    {
        string code = "x = \"Hello, World!\"; y = \"Hello, World!\"; x align y;";
        List<object?> results = Evaluator.EvaluateAndExecute(code);
        Assert.Multiple(() =>
        {
            Assert.That(results.Count, Is.EqualTo(3));
            Assert.That(results[0], Is.InstanceOf(typeof(StringNode)));
            Assert.That(((StringNode)results[0]!).Evaluate(), Is.EqualTo("Hello, World!"));
            Assert.That(results[1], Is.InstanceOf(typeof(StringNode)));
            Assert.That(((StringNode)results[1]!).Evaluate(), Is.EqualTo("Hello, World!"));
            Assert.That(results[2], Is.EqualTo(true));
        });
    }
    
}