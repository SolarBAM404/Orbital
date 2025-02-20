// using OrbitalCore.Lex;
// using OrbitalCore.Parse;
// using OrbitalCore.Parse.Nodes.Abstract;
// using OrbitalCore.Parse.Nodes.BasicTypes;
// using OrbitalCore.Parse.Nodes.Expressions;
//
// namespace OrbitalTests;
//
// [TestFixture]
// public class ParserTests
// {
//     [Test]
//     public void Parse_SimpleAddition_ReturnsCorrectTree()
//     {
//         // Arrange
//         var tokens = new List<Token>
//         {
//             new Token(TokenTypes.Number, "2"),
//             new Token(TokenTypes.Gain, "+"),
//             new Token(TokenTypes.Number, "3"),
//             new Token(TokenTypes.SemiColon, ";")
//         };
//         var parser = new Parser(tokens);
//
//         // Act
//         var result = parser.Parse();
//         AbstractTreeNode node = result.Dequeue();
//
//         // Assert
//         Assert.That(node, Is.InstanceOf<MathematicalBinaryExpressionNode>());
//         var binaryNode = (MathematicalBinaryExpressionNode)node;
//         Assert.That(binaryNode.Operator, Is.EqualTo(TokenTypes.Gain));
//         Assert.That(binaryNode.Expression1, Is.InstanceOf<NumberNode>());
//         Assert.That(binaryNode.Expression2, Is.InstanceOf<NumberNode>());
//         Assert.That(((NumberNode)binaryNode.Expression1).Value, Is.EqualTo(2.0));
//         Assert.That(((NumberNode)binaryNode.Expression2).Value, Is.EqualTo(3.0));
//     }
//
//     [Test]
//     public void Parse_MultiplicationAndAddition_ReturnsCorrectTree()
//     {
//         // Arrange
//         var tokens = new List<Token>
//         {
//             new Token(TokenTypes.Number, "2"),
//             new Token(TokenTypes.Amplify, "*"),
//             new Token(TokenTypes.Number, "3"),
//             new Token(TokenTypes.Gain, "+"),
//             new Token(TokenTypes.Number, "4"),
//             new Token(TokenTypes.SemiColon, ";")
//         };
//         var parser = new Parser(tokens);
//
//         // Act
//         Queue<AbstractTreeNode> result = parser.Parse();
//         AbstractTreeNode node = result.Dequeue();
//
//         // Assert
//         Assert.That(node, Is.InstanceOf<MathematicalBinaryExpressionNode>());
//         var additionNode = (MathematicalBinaryExpressionNode)node;
//         Assert.That(additionNode.Operator, Is.EqualTo(TokenTypes.Gain));
//         Assert.That(additionNode.Expression1, Is.InstanceOf<MathematicalBinaryExpressionNode>());
//         Assert.That(additionNode.Expression2, Is.InstanceOf<NumberNode>());
//         Assert.That(((NumberNode)additionNode.Expression2).Value, Is.EqualTo(4.0));
//
//         var multiplicationNode = (MathematicalBinaryExpressionNode)additionNode.Expression1;
//         Assert.That(multiplicationNode.Operator, Is.EqualTo(TokenTypes.Amplify));
//         Assert.That(multiplicationNode.Expression1, Is.InstanceOf<NumberNode>());
//         Assert.That(multiplicationNode.Expression2, Is.InstanceOf<NumberNode>());
//         Assert.That(((NumberNode)multiplicationNode.Expression1).Value, Is.EqualTo(2.0));
//         Assert.That(((NumberNode)multiplicationNode.Expression2).Value, Is.EqualTo(3.0));
//     }
//
//     [Test]
//     public void Parse_ParenthesizedExpression_ReturnsCorrectTree()
//     {
//         // Arrange
//         List<Token> tokens = new List<Token>
//         {
//             new Token(TokenTypes.LeftParenthesis, "("),
//             new Token(TokenTypes.Number, "2"),
//             new Token(TokenTypes.Gain, "+"),
//             new Token(TokenTypes.Number, "3"),
//             new Token(TokenTypes.RightParenthesis, ")"),
//             new Token(TokenTypes.Amplify, "*"),
//             new Token(TokenTypes.Number, "4"),
//             new Token(TokenTypes.SemiColon, ";")
//         };
//         Parser parser = new Parser(tokens);
//
//         // Act
//         Queue<AbstractTreeNode> result = parser.Parse();
//         AbstractTreeNode node = result.Dequeue();
//         
//         // Assert
//         Assert.That(node, Is.InstanceOf<MathematicalBinaryExpressionNode>());
//         MathematicalBinaryExpressionNode multiplicationNode = (MathematicalBinaryExpressionNode)node;
//         Assert.Multiple(() =>
//         {
//             Assert.That(multiplicationNode.Operator, Is.EqualTo(TokenTypes.Amplify));
//             Assert.That(multiplicationNode.Expression1, Is.InstanceOf<MathematicalBinaryExpressionNode>());
//             Assert.That(multiplicationNode.Expression2, Is.InstanceOf<NumberNode>());
//             Assert.That(((NumberNode)multiplicationNode.Expression2).Value, Is.EqualTo(4.0));
//         });
//
//         MathematicalBinaryExpressionNode additionNode = (MathematicalBinaryExpressionNode)multiplicationNode.Expression1;
//         Assert.Multiple(() =>
//         {
//             Assert.That(additionNode.Operator, Is.EqualTo(TokenTypes.Gain));
//             Assert.That(additionNode.Expression1, Is.InstanceOf<NumberNode>());
//             Assert.That(additionNode.Expression2, Is.InstanceOf<NumberNode>());
//             Assert.That(((NumberNode)additionNode.Expression1).Value, Is.EqualTo(2.0));
//             Assert.That(((NumberNode)additionNode.Expression2).Value, Is.EqualTo(3.0));
//         });
//
//         object? o = multiplicationNode.Evaluate();
//         Assert.That(o, Is.EqualTo(20.0));
//     }
//
//     [Test]
//     public void Parse_Boolean()
//     {
//         // Arrange
//         List<Token> tokens = new List<Token>
//         {
//             new Token(TokenTypes.Boolean, "true"),
//             new Token(TokenTypes.SemiColon, ";")
//         };
//         Parser parser = new Parser(tokens);
//
//         // Act
//         Queue<AbstractTreeNode> result = parser.Parse();
//         AbstractTreeNode node = result.Dequeue();
//
//         // Assert
//         Assert.That(node, Is.InstanceOf<BooleanNode>());
//         BooleanNode booleanNode = (BooleanNode)node;
//         Assert.That(booleanNode.Value, Is.True);
//     }
// }