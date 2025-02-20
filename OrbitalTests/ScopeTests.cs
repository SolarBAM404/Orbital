// using NUnit.Framework;
// using OrbitalCore.Parse;
//
// namespace OrbitalTests;
//
// [TestFixture]
// public class ScopeTests
// {
//     [Test]
//     public void DefineAndRetrieveVariable()
//     {
//         Scope scope = new Scope();
//         scope.DefineVariable("x", 42);
//         Assert.That(scope.GetVariable("x"), Is.EqualTo(42));
//     }
//
//     [Test]
//     public void OverrideVariableInNestedScope()
//     {
//         Scope parentScope = new Scope();
//         parentScope.DefineVariable("x", 42);
//
//         Scope childScope = new Scope(parentScope);
//         childScope.DefineVariable("x", 100);
//
//         Assert.Multiple(() =>
//         {
//             Assert.That(childScope.GetVariable("x"), Is.EqualTo(100));
//             Assert.That(parentScope.GetVariable("x"), Is.EqualTo(42));
//         });
//     }
//
//     [Test]
//     public void RetrieveVariableFromParentScope()
//     {
//         Scope parentScope = new Scope();
//         parentScope.DefineVariable("x", 42);
//
//         Scope childScope = new Scope(parentScope);
//         Assert.That(childScope.GetVariable("x"), Is.EqualTo(42));
//     }
//
//     [Test]
//     public void SetVariableInParentScope()
//     {
//         Scope parentScope = new Scope();
//         parentScope.DefineVariable("x", 42);
//
//         var childScope = new Scope(parentScope);
//         childScope.SetVariable("x", 100);
//
//         Assert.That(childScope.GetVariable("x"), Is.EqualTo(100));
//         Assert.That(parentScope.GetVariable("x"), Is.EqualTo(100));
//     }
//
//     [Test]
//     public void SetVariableInCurrentScope()
//     {
//         var scope = new Scope();
//         scope.DefineVariable("x", 42);
//         scope.SetVariable("x", 100);
//
//         Assert.That(scope.GetVariable("x"), Is.EqualTo(100));
//     }
//     
//     [Test]
//     public void CheckIfVariableExistsOutsideScope()
//     {
//         Scope parentScope = new Scope();
//         Scope childScope = new Scope(parentScope);
//         
//         childScope.DefineVariable("x", 42);
//         Assert.That(parentScope.Exists("x"), Is.False);
//         Assert.That(childScope.Exists("x"), Is.True);
//     } 
// }