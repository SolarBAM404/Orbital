using OrbitalCore;
        using OrbitalCore.Exceptions;
        using OrbitalCore.Parse;
        using OrbitalCore.Parse.Nodes;
        using OrbitalCore.Parse.Nodes.BasicTypes;
        using OrbitalCore.Parse.Nodes.Expressions;
        
        namespace OrbitalTests;
        
        [TestFixture]
        public class VariableTests
        {
            [SetUp]
            public void SetUp()
            {
                Parser.CurrentScope = new Scope();
            }
        
            [Test]
            public void VariableTests_IntegerVariable()
            {
                string code = "x = 1;";
                List<object?> results = Evaluator.EvaluateAndExecute(code);
                Assert.Multiple(() =>
                {
                    Assert.That((double)Parser.CurrentScope.GetVariable("x")!, Is.EqualTo(1));
                });
            }
        
            [Test]
            public void VariableTests_FloatVariable()
            {
                string code = "x = 1.5;";
                List<object?> results = Evaluator.EvaluateAndExecute(code);
                Assert.Multiple(() =>
                {
                    Assert.That(Parser.CurrentScope.GetVariable("x"), Is.EqualTo(1.5d));
                });
            }
        
            [Test]
            public void VariableTests_StringVariable()
            {
                string code = "x = \"Hello, World!\";";
                List<object?> results = Evaluator.EvaluateAndExecute(code);
                Assert.Multiple(() =>
                {
                    Assert.That(Parser.CurrentScope.GetVariable("x"), Is.InstanceOf(typeof(string)));
                    Assert.That(Parser.CurrentScope.GetVariable("x"), Is.EqualTo("Hello, World!"));
                });
            }
        
            [Test]
            public void VariableTests_VariableAssignment()
            {
                string code = "x = 1; y = x;";
                List<object?> results = Evaluator.EvaluateAndExecute(code);
                Assert.Multiple(() =>
                {
                    Assert.That(Parser.CurrentScope.GetVariable("x"), Is.InstanceOf(typeof(Double)));
                    Assert.That((double)Parser.CurrentScope.GetVariable("x")!, Is.EqualTo(1));
                    Assert.That(Parser.CurrentScope.GetVariable("y"), Is.InstanceOf(typeof(Double)));
                    Assert.That((double)Parser.CurrentScope.GetVariable("y")!, Is.EqualTo(1));
                });
            }
        
            [Test]
            public void VariableTests_VariableReassignment()
            {
                string code = "x = 1; x = 2;";
                List<object?> results = Evaluator.EvaluateAndExecute(code);
                Assert.Multiple(() =>
                {
                    Assert.That(Parser.CurrentScope.GetVariable("x"), Is.InstanceOf(typeof(Double)));
                    Assert.That((double)Parser.CurrentScope.GetVariable("x")!, Is.EqualTo(2));
                });
            }
        
            [Test]
            public void VariableTests_VariableAssignmentWithExpression()
            {
                string code = "x = 1; y = x gain 1;";
                List<object?> results = Evaluator.EvaluateAndExecute(code);
                Assert.Multiple(() =>
                {
                    Assert.That((double)Parser.CurrentScope.GetVariable("x")!, Is.EqualTo(1));
                    Assert.That((double)Parser.CurrentScope.GetVariable("y")!, Is.EqualTo(2));
                });
            }
        
            [Test]
            public void VariableTests_VariableComparison()
            {
                string code = "x = 1; y = 1; x align y;";
                List<object?> results = Evaluator.EvaluateAndExecute(code);
                Assert.Multiple(() =>
                {
                    Assert.That((double)Parser.CurrentScope.GetVariable("x")!, Is.EqualTo(1));
                    Assert.That((double)Parser.CurrentScope.GetVariable("y")!, Is.EqualTo(1));
                    Assert.That(results[0], Is.EqualTo(true));
                });
            }
        
            [Test]
            public void VariableTests_VariableComparisonWithString()
            {
                string code = "x = \"Hello, World!\"; y = \"Hello, World!\"; x align y;";
                List<object?> results = Evaluator.EvaluateAndExecute(code);
                Assert.Multiple(() =>
                {
                    Assert.That((string)Parser.CurrentScope.GetVariable("x")!, Is.EqualTo("Hello, World!"));
                    Assert.That((string)Parser.CurrentScope.GetVariable("y")!, Is.EqualTo("Hello, World!"));
                    Assert.That(results[0], Is.EqualTo(true));
                });
            }
        
            [Test]
            public void VariableTests_Phase1()
            {
                string code = "quickMaths = 10; quickMaths = quickMaths gain 2; uplink(quickMaths);";
                List<object?> results = Evaluator.EvaluateAndExecute(code);
                Assert.Multiple(() =>
                {
                    Assert.That((double)Parser.CurrentScope.GetVariable("quickMaths")!, Is.EqualTo(12));
                });
            }
        
            [Test]
            public void VariableTests_Phase2()
            {
                string code = "floatTest = 1.0; floatTest = floatTest gain 5.5; uplink(floatTest);";
                List<object?> results = Evaluator.EvaluateAndExecute(code);
                Assert.Multiple(() =>
                {
                    Assert.That((double)Parser.CurrentScope.GetVariable("floatTest")!, Is.EqualTo(6.5));
                });
            }
        
            [Test]
            public void VariableTests_Phase3()
            {
                string code = "stringCatTest = \"10 corgis\"; stringCatTest = stringCatTest gain \"5\" gain \" more corgis\"; uplink(stringCatTest);";
                List<object?> results = Evaluator.EvaluateAndExecute(code);
                Assert.Multiple(() =>
                {
                    Assert.That((string)Parser.CurrentScope.GetVariable("stringCatTest")!, Is.EqualTo("10 corgis5 more corgis"));
                });
            }
        
            [Test]
            public void VariableTests_Phase4()
            {
                string code = "errorTest = 5; errorTest = errorTest gain false; uplink(errorTest);";
                Assert.Multiple(() =>
                {
                    Assert.Throws<CastErrorException>(() => Evaluator.EvaluateAndExecute(code));
                });
            }
        }