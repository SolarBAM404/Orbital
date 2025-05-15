using OrbitalCore.Lex;

namespace OrbitalTests;

[TestFixture]
public class ScannerTests
{
    [Test]
    public void Test_ScanTokens_Single()
    {
        const string source = "void";
        Scanner scanner = new Scanner(source);
        List<Token?> tokens = scanner.ScanTokens();
        Assert.Multiple(() =>
        {
            Assert.That(tokens, Has.Count.EqualTo(2));
            Assert.That(tokens[0], Is.InstanceOf<Token>());
            Assert.That(tokens[0].TokenType, Is.EqualTo(TokenTypes.Void));
            Assert.That(tokens[1].TokenType, Is.EqualTo(TokenTypes.EoF));
        });
    }

    [Test]
    public void Test_ScanTokens_Multiple()
    {
        const string source = "x = 5; \n y = 10;";
        Scanner scanner = new Scanner(source);
        List<Token?> tokens = scanner.ScanTokens();
        Assert.Multiple(() =>
        {
            Assert.That(tokens, Has.Count.EqualTo(9));
            Assert.That(tokens[0].TokenType, Is.EqualTo(TokenTypes.Identifier));
            Assert.That(tokens[1].TokenType, Is.EqualTo(TokenTypes.Assignment));
            Assert.That(tokens[2].TokenType, Is.EqualTo(TokenTypes.Number));
            Assert.That(tokens[2].Literal, Is.EqualTo(5));
            Assert.That(tokens[3].TokenType, Is.EqualTo(TokenTypes.SemiColon));
            Assert.That(tokens[4].TokenType, Is.EqualTo(TokenTypes.Identifier));
            Assert.That(tokens[5].TokenType, Is.EqualTo(TokenTypes.Assignment));
            Assert.That(tokens[6].TokenType, Is.EqualTo(TokenTypes.Number));
            Assert.That(tokens[6].Literal, Is.EqualTo(10));
            Assert.That(tokens[7].TokenType, Is.EqualTo(TokenTypes.SemiColon));
            Assert.That(tokens[8].TokenType, Is.EqualTo(TokenTypes.EoF));
        });
    }

    [Test]
    public void Test_ScanTokens_StringTest()
    {
        const string source = "\"Hello, World!\"";
        Scanner scanner = new Scanner(source);
        List<Token?> tokens = scanner.ScanTokens();
        Assert.Multiple(() =>
        {
            Assert.That(tokens, Has.Count.EqualTo(2));
            Assert.That(tokens[0].TokenType, Is.EqualTo(TokenTypes.String));
            Assert.That(tokens[0].Literal, Is.EqualTo("Hello, World!"));
            Assert.That(tokens[1].TokenType, Is.EqualTo(TokenTypes.EoF));
        });
    }

}
