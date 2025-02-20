// using OrbitalCore;
// using OrbitalCore.Parse;
// using OrbitalCore.Parse.Nodes;
//
// namespace OrbitalTests;
//
// [TestFixture]
// public class StatementTests
// {
//     [SetUp]
//     public void SetUp()
//     {
//         Parser.GlobalVariables = new Dictionary<string, VariableNode>();
//     }
//     
//     [Test]
//     public void StatementTests_BasicIfStatement()
//     {
//         string code = "x = 1; probe(x align 1) { x = x gain 1; };";
//         List<object?> results = Evaluator.EvaluateAndExecute(code); // should return < < 2.0d > >
//         Assert.Multiple(() =>
//         {
//             Assert.That(Parser.CurrentScope.GetVariable("x"), Is.EqualTo(2.0d));
//         });
//     }
//     
//     [Test]
//     public void StatementTests_IfElseStatement_IfSide()
//     {
//         string code = "x = 2; probe(x align 2) { x = x gain 1; } scan { x = x drain 1; };";
//         List<object?> results = Evaluator.EvaluateAndExecute(code); // should return  < 3.0d > 
//         Assert.Multiple(() =>
//         {
//             Assert.That(Parser.CurrentScope.GetVariable("x"), Is.EqualTo(3.0d));
//         });
//     }
//     
//     [Test]
//     public void StatementTests_IfElseStatement_ElseSide()
//     {
//         string code = "x = 1; probe(x align 2) { x = x gain 1; } scan { x = x drain 1; };";
//         List<object?> results = Evaluator.EvaluateAndExecute(code); // should return < < 0.0d > >
//         Assert.Multiple(() =>
//         {
//             Assert.That(Parser.CurrentScope.GetVariable("x"), Is.EqualTo(0.0d));
//         });
//     }
//     
//     [Test]
//     public void StatementTests_IfElseStatement_NestedIf()
//     {
//         string code = "x = 1; probe(x align 1) { probe(x align 1) { x = x gain 1; } scan { x = x drain 1; }; };";
//         List<object?> results = Evaluator.EvaluateAndExecute(code); // should return < < 2.0d > >
//         Assert.Multiple(() =>
//         {
//             Assert.That(Parser.CurrentScope.GetVariable("x"), Is.EqualTo(2.0d));
//         });
//     }
//     
//     [Test]
//     public void StatementTests_IfElseStatement_NestedIfElse()
//     {
//         string code = "x = 1; probe(x below 2) { probe(x below 1) { x = x gain 1; } scan { x = x drain 1; }; };";
//         List<object?> results = Evaluator.EvaluateAndExecute(code); // should return < < 0.0d > >
//         Assert.Multiple(() =>
//         {
//             Assert.That(Parser.CurrentScope.GetVariable("x"), Is.EqualTo(0.0d));
//         });
//     }
//     
//     [Test]
//     public void StatementTests_IfElseStatement_NestedIf_ScanSide()
//     {
//         string code = "x = 1; probe(x align 2) { probe(x align 1) { x = x gain 1; } } " +
//                       "scan { x = x drain 1; probe ( x align 1) { x = x gain 2; } scan { x = x drain 1; }; };";
//         List<object?> results = Evaluator.EvaluateAndExecute(code); // should return < < 1.0d > >
//         Assert.Multiple(() =>
//         {
//             Assert.That(Parser.CurrentScope.GetVariable("x"), Is.EqualTo(-1.0d));
//         });
//     }
//     
//     [Test]
//     public void StatementTests_WhileStatement()
//     {
//         string code = "x = 1; orbit (x below 5) { x = x gain 1; };";
//         List<object?> results = Evaluator.EvaluateAndExecute(code); // should return < < 5.0d > >
//         Assert.Multiple(() =>
//         {
//             Assert.That(Parser.CurrentScope.GetVariable("x"), Is.EqualTo(5.0d));
//         });
//     }
//     
//     [Test]
//     public void StatementTests_WhileStatement_NestedWhile()
//     {
//         string code = "x = 1; orbit (x below 5) { orbit (x below 3) { x = x gain 1; }; x = x gain 2; };";
//         List<object?> results = Evaluator.EvaluateAndExecute(code); // should return < < 3.0d > >
//         Assert.Multiple(() =>
//         {
//             Assert.That(Parser.CurrentScope.GetVariable("x"), Is.EqualTo(5.0d));
//         });
//     }
//     
//     [Test]
//     public void StatementTests_WhileStatement_IfStatement()
//     {
//         string code = "x = 1; orbit (x below 5) { probe(x align 3) { x = x gain 2; } scan { x = x gain 1; }; };";
//         List<object?> results = Evaluator.EvaluateAndExecute(code); // should return < < 3.0d > >
//         Assert.Multiple(() =>
//         {
//             Assert.That(Parser.CurrentScope.GetVariable("x"), Is.EqualTo(5.0d));
//         });
//     }
//     
// }