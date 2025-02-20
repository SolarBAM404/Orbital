// using OrbitalCore;
//
// namespace OrbitalTests;
//
// [TestFixture]
// public class StringTests
// {
//     [Test]
//     public void StringTests_BaseString()
//     {
//         string code = "\"Hello, World!\"";
//         List<object?> results = Evaluator.EvaluateAndExecute(code);
//         Assert.Multiple(() =>
//         {
//             Assert.That(results.Count, Is.EqualTo(1));
//             Assert.That(results[0], Is.EqualTo("Hello, World!"));
//         });
//     }
//     
//     [Test]
//     public void StringTests_EmptyString()
//     {
//         string code = "\"\"";
//         List<object?> results = Evaluator.EvaluateAndExecute(code);
//         Assert.Multiple(() =>
//         {
//             Assert.That(results.Count, Is.EqualTo(1));
//             Assert.That(results[0], Is.EqualTo(""));
//         });
//     }
//     
//     [Test]
//     public void StringTests_StringWithNumber()
//     {
//         string code = "\"123\"";
//         List<object?> results = Evaluator.EvaluateAndExecute(code);
//         Assert.Multiple(() =>
//         {
//             Assert.That(results.Count, Is.EqualTo(1));
//             Assert.That(results[0], Is.EqualTo("123"));
//         });
//     }
//
//     [Test]
//     public void StringTests_StringWithSpecialCharacters()
//     {
//         string code = "\"!@#$%^&*()\"";
//         List<object?> results = Evaluator.EvaluateAndExecute(code);
//         Assert.Multiple(() =>
//         {
//             Assert.That(results.Count, Is.EqualTo(1));
//             Assert.That(results[0], Is.EqualTo("!@#$%^&*()"));
//         });
//     }
//     
//     [Test]
//     public void StringTests_ConcatenatedStrings()
//     {
//         string code = "\"Hello, \" gain \"World!\"";
//         List<object?> results = Evaluator.EvaluateAndExecute(code);
//         Assert.Multiple(() =>
//         {
//             Assert.That(results.Count, Is.EqualTo(1));
//             Assert.That(results[0], Is.EqualTo("Hello, World!"));
//         });
//     }
//     
//     [Test]
//     public void StringTests_ComparisonOperators()
//     {
//         string code = "\"Hello, World!\" align \"Hello, World!\"";
//         List<object?> results = Evaluator.EvaluateAndExecute(code);
//         Assert.Multiple(() =>
//         {
//             Assert.That(results.Count, Is.EqualTo(1));
//             Assert.That(results[0], Is.EqualTo(true));
//         });
//     }
//     
//     [Test]
//     public void StringTests_FalseComparisonOperators()
//     {
//         string code = "\"Hello, World!\" align \"Hello, World\"";
//         List<object?> results = Evaluator.EvaluateAndExecute(code);
//         Assert.Multiple(() =>
//         {
//             Assert.That(results.Count, Is.EqualTo(1));
//             Assert.That(results[0], Is.EqualTo(false));
//         });
//     }
//     
//     [Test]
//     public void StringTests_ConcatenatedStringsAndComparisonOperators()
//     {
//         string code = "\"Hello, \" gain \"World!\" align \"Hello, World!\"";
//         List<object?> results = Evaluator.EvaluateAndExecute(code);
//         Assert.Multiple(() =>
//         {
//             Assert.That(results.Count, Is.EqualTo(1));
//             Assert.That(results[0], Is.EqualTo(true));
//         });
//     }
//
//     [Test]
//     public void StringTests_Phase1()
//     {
//         string code = "\"hello\" gain \"world\"";
//         List<object?> results = Evaluator.EvaluateAndExecute(code);
//         Assert.Multiple(() =>
//         {
//             Assert.That(results.Count, Is.EqualTo(1));
//             Assert.That(results[0], Is.EqualTo("helloworld"));
//         });
//     }
//     
//     [Test]
//     public void StringTests_Phase2()
//     {
//         string code = "\"foo\" gain \"bar\" align \"foobar\"";
//         List<object?> results = Evaluator.EvaluateAndExecute(code);
//         Assert.Multiple(() =>
//         {
//             Assert.That(results.Count, Is.EqualTo(1));
//             Assert.That(results[0], Is.EqualTo(true));
//         });
//     }
//     
//     [Test]
//     public void StringTests_Phase3()
//     {
//         string code = "\"10 corgis\" disrupt \"10\" gain \"corgis\"";
//         List<object?> results = Evaluator.EvaluateAndExecute(code);
//         Assert.Multiple(() =>
//         {
//             Assert.That(results.Count, Is.EqualTo(1));
//             Assert.That(results[0], Is.EqualTo(true));
//         });
//     }
//     
//     [Test]
//     public void StringTests_Phase4()
//     {
//         string code = "1 gain \"0\"";
//         List<object?> results = Evaluator.EvaluateAndExecute(code);
//         Assert.Multiple(() =>
//         {
//             Assert.That(results.Count, Is.EqualTo(1));
//             Assert.That(results[0], Is.EqualTo("10"));
//         });
//     }
//     
//     [Test]
//     public void StringTests_Phase5()
//     {
//         string code = "\"false\" align false";
//         List<object?> results = Evaluator.EvaluateAndExecute(code);
//         Assert.Multiple(() =>
//         {
//             Assert.That(results.Count, Is.EqualTo(1));
//             Assert.That(results[0], Is.EqualTo(false));
//         });
//     }
// }