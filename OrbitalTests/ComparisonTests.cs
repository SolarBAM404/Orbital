// using OrbitalCore;
//
// namespace OrbitalTests;
//
// [TestFixture]
// public class ComparisonTests
// {
//     
//     [Test]
//     public void Comparison_Equal()
//     {
//         // Evaluate the following code:
//         // 1 == 1
//         string code = "1 align 1;";
//         List<object?> results = Evaluator.EvaluateAndExecute(code);
//         Assert.That(results, Is.EquivalentTo(new List<object> { true }));
//     }
//     
//     [Test]
//     public void Comparison_NotEqual()
//     {
//         // Evaluate the following code:
//         // 1 != 2
//         string code = "1 disrupt 2;";
//         List<object?> results = Evaluator.EvaluateAndExecute(code);
//         Assert.That(results, Is.EquivalentTo(new List<object> { true }));
//     }
//     
//     [Test]
//     public void Comparison_GreaterThan()
//     {
//         // Evaluate the following code:
//         // 2 > 1
//         string code = "2 above 1;";
//         List<object?> results = Evaluator.EvaluateAndExecute(code);
//         Assert.That(results, Is.EquivalentTo(new List<object> { true }));
//     }
//     
//     [Test]
//     public void Comparison_LessThan()
//     {
//         // Evaluate the following code:
//         // 1 < 2
//         string code = "1 below 2;";
//         List<object?> results = Evaluator.EvaluateAndExecute(code);
//         Assert.That(results, Is.EquivalentTo(new List<object> { true }));
//     }
//     
//     [Test]
//     public void Comparison_GreaterThanOrEqual()
//     {
//         // Evaluate the following code:
//         // 2 >= 1
//         string code = "2 safe 1;";
//         List<object?> results = Evaluator.EvaluateAndExecute(code);
//         Assert.That(results, Is.EquivalentTo(new List<object> { true }));
//     }
//     
//     [Test]
//     public void Comparison_LessThanOrEqual()
//     {
//         // Evaluate the following code:
//         // 1 <= 2
//         string code = "1 unsafe 2;";
//         List<object?> results = Evaluator.EvaluateAndExecute(code);
//         Assert.That(results, Is.EquivalentTo(new List<object> { true }));
//     }
//
//     [Test]
//     public void Comparison_Phrase1()
//     {
//         // Evaluate the following code:
//         // true == true
//         string code = "true align true;";
//         List<object?> results = Evaluator.EvaluateAndExecute(code);
//         Assert.That(results, Is.EquivalentTo(new List<object> { true }));
//     }
//     
//     [Test]
//     public void Comparison_Phrase2()
//     {
//         // Evaluate the following code:
//         // true != false
//         string code = "false disrupt true;";
//         List<object?> results = Evaluator.EvaluateAndExecute(code);
//         Assert.That(results, Is.EquivalentTo(new List<object> { true }));
//     }
//     
//     [Test]
//     public void Comparison_Phrase3()
//     {
//         // Evaluate the following code:
//         // (5 < 10)
//         string code = "(5 below 10);";
//         List<object?> results = Evaluator.EvaluateAndExecute(code);
//         Assert.That(results, Is.EquivalentTo(new List<object> { true }));
//     }
//     
//     [Test]
//     public void Comparison_Phrase4()
//     {
//         // Evaluate the following code:
//         // !(5 - 4 > 3 * 2 == !false) 
//         string code = "negate(5 drain 4 above 3 amplify 2 align negate(false));";
//         List<object?> results = Evaluator.EvaluateAndExecute(code);
//         Assert.That(results, Is.EquivalentTo(new List<object> { true }));
//     }
//     
//     [Test]
//     public void Comparison_Phrase5()
//     {
//         // Evaluate the following code:
//         // true and true
//         string code = "true stable true;";
//         List<object?> results = Evaluator.EvaluateAndExecute(code);
//         Assert.That(results, Is.EquivalentTo(new List<object> { true }));
//     }
//     
//     [Test]
//     public void Comparison_Phrase6()
//     {
//         // Evaluate the following code:
//         // false or true
//         string code = "false stable true;";
//         List<object?> results = Evaluator.EvaluateAndExecute(code);
//         Assert.That(results, Is.EquivalentTo(new List<object> { false }));
//     }
//     
//     [Test]
//     public void Comparison_Phrase7()
//     {
//         // Evaluate the following code:
//         // (0 < 1) or false
//         string code = "(0 below 1) stable false;";
//         List<object?> results = Evaluator.EvaluateAndExecute(code);
//         Assert.That(results, Is.EquivalentTo(new List<object> { false }));
//     }
//     
//     [Test]
//     public void Comparison_Phrase8()
//     {
//         // Evaluate the following code:
//         // false or false
//         string code = "false path false;";
//         List<object?> results = Evaluator.EvaluateAndExecute(code);
//         Assert.That(results, Is.EquivalentTo(new List<object> { false }));
//     }
//     
// }