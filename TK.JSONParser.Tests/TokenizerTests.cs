using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TK.JSONParser.Tokens;

namespace TK.JSONParser.Tests
{
    [TestFixture]
    class TokenizerTests
    {
        [Test]
        public void empty() { }

        #region Util Methods

        private void assert_token_type_and_value(string input, TokenType expected, bool skipType = false, bool skipValue = false)
        {
            Tokenizer tokenizer = new Tokenizer(input);

            Token token = tokenizer.GetNextToken();

            if (!skipType) Assert.That(token.Type, Is.EqualTo(expected));
            if (!skipValue) Assert.That(token.Value, Is.EqualTo(input));
        }

        private void assert_token_type(string input, TokenType expected)
            => assert_token_type_and_value(input, expected, skipValue: true);

        private void assert_token_value(string input, TokenType expected)
            => assert_token_type_and_value(input, expected, skipType: true);

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
            => assert_token_type_and_value(input, expected);

        [TestCase(@"""string""", TokenType.String)]
        [TestCase(@"""a string with spaces""", TokenType.String)]
        [TestCase(@"""a string with { curlies }""", TokenType.String)]
        [TestCase(@"""a string with [ brackets ]""", TokenType.String)]
        [TestCase(@"""a string with 123.456e789 numbers""", TokenType.String)]
        [TestCase("0", TokenType.Number)]
        [TestCase("123", TokenType.Number)]
        [TestCase("123.456", TokenType.Number)]
        [TestCase("123.456e789", TokenType.Number)]
        [TestCase("123.456e-789", TokenType.Number)]
        [TestCase("123.456e+789", TokenType.Number)]
        [TestCase("123.456E789", TokenType.Number)]
        [TestCase("123.456E-789", TokenType.Number)]
        [TestCase("123.456E+789", TokenType.Number)]
        public void tokenizer_should_support_complex_tokens(string input, TokenType expected)
            => assert_token_type_and_value(input, expected: expected);

        [TestCase(" ", TokenType.End)]
        [TestCase("  {", TokenType.OpenCurlyBrace)]
        public void tokenizer_should_ignore_whitespaces(string input, TokenType expected)
            => assert_token_type(input, expected);
    }
}
