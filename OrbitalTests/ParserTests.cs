using OrbitalCore.Parser;
using OrbitalCore.Parser.Nodes.BasicTypes;
using OrbitalCore.Parser.Nodes.Expressions;
using OrbitalCore.Tokens;

namespace OrbitalTests;

[TestFixture]
public class ParserTests
{
    [Test]
    public void Parse_SimpleAddition_ReturnsCorrectTree()
    {
        // Arrange
        var tokens = new List<Token>
        {
            new Token(TokenTypes.Number, "2"),
            new Token(TokenTypes.Gain, "+"),
            new Token(TokenTypes.Number, "3"),
            new Token(TokenTypes.SemiColon, ";")
        };
        var parser = new Parser(tokens);

        // Act
        var result = parser.Parse();

        // Assert
        Assert.That(result, Is.InstanceOf<MathematicalBinaryExpressionNode>());
        var binaryNode = (MathematicalBinaryExpressionNode)result;
        Assert.That(binaryNode.Operator, Is.EqualTo(TokenTypes.Gain));
        Assert.That(binaryNode.Expression1, Is.InstanceOf<NumberNode>());
        Assert.That(binaryNode.Expression2, Is.InstanceOf<NumberNode>());
        Assert.That(((NumberNode)binaryNode.Expression1).Value, Is.EqualTo(2.0));
        Assert.That(((NumberNode)binaryNode.Expression2).Value, Is.EqualTo(3.0));
    }

    [Test]
    public void Parse_MultiplicationAndAddition_ReturnsCorrectTree()
    {
        // Arrange
        var tokens = new List<Token>
        {
            new Token(TokenTypes.Number, "2"),
            new Token(TokenTypes.Amplify, "*"),
            new Token(TokenTypes.Number, "3"),
            new Token(TokenTypes.Gain, "+"),
            new Token(TokenTypes.Number, "4"),
            new Token(TokenTypes.SemiColon, ";")
        };
        var parser = new Parser(tokens);

        // Act
        var result = parser.Parse();

        // Assert
        Assert.That(result, Is.InstanceOf<MathematicalBinaryExpressionNode>());
        var additionNode = (MathematicalBinaryExpressionNode)result;
        Assert.That(additionNode.Operator, Is.EqualTo(TokenTypes.Gain));
        Assert.That(additionNode.Expression1, Is.InstanceOf<MathematicalBinaryExpressionNode>());
        Assert.That(additionNode.Expression2, Is.InstanceOf<NumberNode>());
        Assert.That(((NumberNode)additionNode.Expression2).Value, Is.EqualTo(4.0));

        var multiplicationNode = (MathematicalBinaryExpressionNode)additionNode.Expression1;
        Assert.That(multiplicationNode.Operator, Is.EqualTo(TokenTypes.Amplify));
        Assert.That(multiplicationNode.Expression1, Is.InstanceOf<NumberNode>());
        Assert.That(multiplicationNode.Expression2, Is.InstanceOf<NumberNode>());
        Assert.That(((NumberNode)multiplicationNode.Expression1).Value, Is.EqualTo(2.0));
        Assert.That(((NumberNode)multiplicationNode.Expression2).Value, Is.EqualTo(3.0));
    }

    [Test]
    public void Parse_ParenthesizedExpression_ReturnsCorrectTree()
    {
        // Arrange
        var tokens = new List<Token>
        {
            new Token(TokenTypes.LeftParenthesis, "("),
            new Token(TokenTypes.Number, "2"),
            new Token(TokenTypes.Gain, "+"),
            new Token(TokenTypes.Number, "3"),
            new Token(TokenTypes.RightParenthesis, ")"),
            new Token(TokenTypes.Amplify, "*"),
            new Token(TokenTypes.Number, "4"),
            new Token(TokenTypes.SemiColon, ";")
        };
        var parser = new Parser(tokens);

        // Act
        var result = parser.Parse();

        // Assert
        Assert.That(result, Is.InstanceOf<MathematicalBinaryExpressionNode>());
        var multiplicationNode = (MathematicalBinaryExpressionNode)result;
        Assert.That(multiplicationNode.Operator, Is.EqualTo(TokenTypes.Amplify));
        Assert.That(multiplicationNode.Expression1, Is.InstanceOf<MathematicalBinaryExpressionNode>());
        Assert.That(multiplicationNode.Expression2, Is.InstanceOf<NumberNode>());
        Assert.That(((NumberNode)multiplicationNode.Expression2).Value, Is.EqualTo(4.0));

        var additionNode = (MathematicalBinaryExpressionNode)multiplicationNode.Expression1;
        Assert.That(additionNode.Operator, Is.EqualTo(TokenTypes.Gain));
        Assert.That(additionNode.Expression1, Is.InstanceOf<NumberNode>());
        Assert.That(additionNode.Expression2, Is.InstanceOf<NumberNode>());
        Assert.That(((NumberNode)additionNode.Expression1).Value, Is.EqualTo(2.0));
        Assert.That(((NumberNode)additionNode.Expression2).Value, Is.EqualTo(3.0));
    }
}