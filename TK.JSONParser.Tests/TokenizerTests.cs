using NUnit.Framework;
using TK.JSONParser.Tokens;

namespace TK.JSONParser.Tests
{
    [TestFixture]
    class TokenizerTests
    {
        #region Helper Methods

        private void assert_token(string input, TokenType expectedType, string expectedValue, bool assertValue = true)
        {
            Tokenizer tokenizer = new Tokenizer(input);
            Token token = tokenizer.GetNextToken();
            Assert.That(token.Type, Is.EqualTo(expectedType));
            if (assertValue) Assert.That(token.Value, Is.EqualTo(expectedValue));
        }

        private void assert_token_type(string input, TokenType expectedType)
            => assert_token(input, expectedType, string.Empty, false);

        #endregion

        [TestCase("", TokenType.End)]
        [TestCase("{", TokenType.OpenCurlyBrace)]
        [TestCase("}", TokenType.CloseCurlyBrace)]
        [TestCase("[", TokenType.OpenBracket)]
        [TestCase("]", TokenType.CloseBracket)]
        [TestCase(":", TokenType.Colon)]
        [TestCase(",", TokenType.Comma)]
        [TestCase("-", TokenType.Minus)]
        [TestCase("+", TokenType.Plus)]
        [TestCase("true", TokenType.True)]
        [TestCase("false", TokenType.False)]
        [TestCase("null", TokenType.Null)]
        public void tokenizer_should_support_simple_tokens(string input, TokenType expected)
            => assert_token(input, expected, input);

        [TestCase("\"a string\"", TokenType.String, "a string")]
        [TestCase("   \" \\\" \"   ", TokenType.String, " \" ")]
        [TestCase(@""" \\ """, TokenType.String, @" \ ")]
        [TestCase(@""" \/ """, TokenType.String, @" / ")]
        //[TestCase(@""" \u0043\u0023 """, TokenType.String, " C# ")] // Not supported yet.
        public void tokenizer_should_support_strings(string input, TokenType expectedType, string expectedValue)
            => assert_token(input, expectedType, expectedValue);

        [TestCase("0", TokenType.Integer, "0")]
        [TestCase("123", TokenType.Integer, "123")]
        [TestCase("0123", TokenType.Error, "01")]
        //[TestCase(".456", TokenType.Fraction, "456")] // Not supported yet.
        //[TestCase("e789", TokenType.Exponent, "789")] // Not supported yet.
        //[TestCase("e-789", TokenType.Exponent, "")] // Not supported yet.
        //[TestCase("e+789", TokenType.Exponent, "")] // Not supported yet.
        //[TestCase("E789", TokenType.Exponent, "789")] // Not supported yet.
        //[TestCase("E-789", TokenType.Exponent, "")] // Not supported yet.
        //[TestCase("E+789", TokenType.Exponent, "")] // Not supported yet.
        public void tokenizer_should_support_numbers(string input, TokenType expectedType, string expectedValue)
            => assert_token(input, expectedType, expectedValue);

        [TestCase(" ", TokenType.End)]
        [TestCase("  {", TokenType.OpenCurlyBrace)]
        public void tokenizer_should_ignore_whitespaces(string input, TokenType expected)
            => assert_token_type(input, expected);

        [TestCase(" // this is a comment ", TokenType.Comment, " this is a comment ")]
        public void tokenizer_should_support_comments(string input, TokenType expectedType, string expectedValue)
            => assert_token(input, expectedType, expectedValue);

        [Test]
        public void tokenizer_should_parse_a_json_object()
        {
            var input = "{ \"prop\": 123, \"prop2\": \"value\" }";
            var tokenizer = new Tokenizer(input);

            Token token;

            token = tokenizer.GetNextToken();
            Assert.That(token.Type, Is.EqualTo(TokenType.OpenCurlyBrace));

            token = tokenizer.GetNextToken();
            Assert.That(token.Type, Is.EqualTo(TokenType.String));
            Assert.That(token.Value, Is.EqualTo("prop"));

            token = tokenizer.GetNextToken();
            Assert.That(token.Type, Is.EqualTo(TokenType.Colon));

            token = tokenizer.GetNextToken();
            Assert.That(token.Type, Is.EqualTo(TokenType.Integer));
            Assert.That(token.Value, Is.EqualTo("123"));

            token = tokenizer.GetNextToken();
            Assert.That(token.Type, Is.EqualTo(TokenType.Comma));

            token = tokenizer.GetNextToken();
            Assert.That(token.Type, Is.EqualTo(TokenType.String));
            Assert.That(token.Value, Is.EqualTo("prop2"));

            token = tokenizer.GetNextToken();
            Assert.That(token.Type, Is.EqualTo(TokenType.Colon));

            token = tokenizer.GetNextToken();
            Assert.That(token.Type, Is.EqualTo(TokenType.String));
            Assert.That(token.Value, Is.EqualTo("value"));

            token = tokenizer.GetNextToken();
            Assert.That(token.Type, Is.EqualTo(TokenType.CloseCurlyBrace));

            token = tokenizer.GetNextToken();
            Assert.That(token.Type, Is.EqualTo(TokenType.End));
        }
    }
}
