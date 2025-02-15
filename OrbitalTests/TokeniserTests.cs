using OrbitalCore.Lex;

namespace OrbitalTests;

[TestFixture]
public class TokeniserTests
{
    [Test]
    public void Tokeniser_GetTokens_WhenGivenEmptyString_ReturnsEmptyList()
    {
        List<Token> tokens = new Tokeniser("").GetTokens();
        Assert.That(tokens.Count, Is.EqualTo(0));
    }
    
    [Test]
    public void Tokeniser_GetTokens_WhenGivenWhitespace_ReturnsEmptyList()
    {
        List<Token> tokens = new Tokeniser("   ").GetTokens();
        Assert.That(tokens.Count, Is.EqualTo(0));
    }

    [Test]
    public void Tokeniser_GetTokens_WhenGivenString_ReturnsStringToken()
    {
        List<Token> tokens = new Tokeniser("\"Hello, World!\"").GetTokens();
        Assert.That(tokens, Has.Count.EqualTo(1));
        Assert.Multiple(() =>
        {
            Assert.That(tokens[0].TokenType, Is.EqualTo(TokenTypes.String));
            Assert.That(tokens[0].Value, Is.EqualTo("Hello, World!"));
        });
    }

    [Test]
    public void Tokeniser_GetTokens_WhenGivenNumber_ReturnsNumberToken()
    {
        List<Token> tokens = new Tokeniser("123").GetTokens();
        Assert.That(tokens, Has.Count.EqualTo(1));
        Assert.Multiple(() =>
        {
            Assert.That(tokens[0].TokenType, Is.EqualTo(TokenTypes.Number));
            Assert.That(tokens[0].Value, Is.EqualTo("123"));
        });
    }
    
    [Test]
    public void Tokeniser_GetTokens_WhenGivenKeyword_ReturnsBooleanToken()
    {
        List<Token> tokens = new Tokeniser("true").GetTokens();
        Assert.That(tokens, Has.Count.EqualTo(1));
        Assert.Multiple(() =>
        {
            Assert.That(tokens[0].TokenType, Is.EqualTo(TokenTypes.Boolean));
            Assert.That(tokens[0].Value, Is.EqualTo("true"));
        });
    }

    [Test]
    public void Tokeniser_GetTokens_WhenGivenIdentifier()
    {
        List<Token> tokens = new Tokeniser("test").GetTokens();
        Assert.That(tokens, Has.Count.EqualTo(1));
        Assert.Multiple(() =>
        {
            Assert.That(tokens[0].TokenType, Is.EqualTo(TokenTypes.Identifier));
            Assert.That(tokens[0].Value, Is.EqualTo("test"));
        });
    }

    [Test]
    public void Tokeniser_GetTokens_WhenGivenString()
    {
        List<Token> tokens = new Tokeniser("\"test\"").GetTokens();
        Assert.That(tokens, Has.Count.EqualTo(1));
        Assert.Multiple(() =>
        {
            Assert.That(tokens[0].TokenType, Is.EqualTo(TokenTypes.String));
            Assert.That(tokens[0].Value, Is.EqualTo("test"));
        });
    }
}