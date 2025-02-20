// using OrbitalCore;
//
// namespace OrbitalTests;
//
// [TestFixture]
// public class MathsTests
// {
//     
//     [Test]
//     public void Maths_Addition_ReturnsCorrectValue()
//     {
//         // Evaluate the following code:
//         // 1 - 2
//         string code = "1 drain 2;";
//         List<object?> results = Evaluator.EvaluateAndExecute(code);
//         Assert.That(results, Is.EquivalentTo(new List<object> { -1.0 }));
//     }
//
//     [Test]
//     public void Maths_AdditionSubtraction_ReturnsCorrectValue()
//     {
//         // Evaluate the following code:
//         // 2.5 + 2.5 - 1.25
//         string code = "2.5 gain 2.5 drain 1.25;";
//         List<object?> results = Evaluator.EvaluateAndExecute(code);
//         Assert.That(results, Is.EquivalentTo(new List<object> { 3.75 }));
//         
//     }
//
//     [Test]
//     public void Maths_Parenthesis_ReturnsCorrectValue()
//     {
//         // Evaluate the following code:
//         // (10 * 2) / 6 
//         string code = "(10 amplify 2) disperse 6;";
//         List<object?> results = Evaluator.EvaluateAndExecute(code);
//         Assert.That(results, Is.EquivalentTo(new List<object> { 3.3333333333333335 }));
//     }
//
//     [Test]
//     public void Maths_Bodmas_ReturnsCorrectValue()
//     {
//         // Evaluate the following code:
//         // 8.5 / (2 * 9) - -3
//         string code = "8.5 disperse (2 amplify 9) drain -3;";
//         List<object?> results = Evaluator.EvaluateAndExecute(code);
//         Assert.That(results, Is.EquivalentTo(new List<object> { 3.4722222222222223d }));
//     }
//
// }