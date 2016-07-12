using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TK.JSONParser.Parsing;
using TK.JSONParser.Parsing.Expressions;
using TK.JSONParser.Parsing.Values;

namespace TK.JSONParser.Tests
{
    [TestFixture]
    public class ParserTests
    {
        [Test]
        public void parser_should_parse_empty_object()
        {
            var input = "{}";
            var parser = new Parser(input);

            IExpression expression = parser.ParseJSON();

            Assert.That(expression, Is.TypeOf<ObjectExpression>());
            Assert.That(((ObjectExpression)expression).Members.Count, Is.EqualTo(0));
        }

        [Test]
        public void parser_should_parse_object_with_members()
        {
            var input = @"{ ""prop1"": 123, ""prop2"": ""value"" }";
            var parser = new Parser(input);

            IExpression expression = parser.ParseJSON();

            Assert.That(expression, Is.TypeOf<ObjectExpression>());
            Assert.That(((ObjectExpression)expression).Members.Count, Is.EqualTo(0));
        }

        [Test]
        public void parser_should_parse_empty_array()
        {
            var input = "[]";
            var parser = new Parser(input);

            IExpression expression = parser.ParseJSON();

            Assert.That(expression, Is.TypeOf<ArrayExpression>());
            Assert.That(((ArrayExpression)expression).Elements.Count, Is.EqualTo(0));
        }

        [Test]
        public void parser_should_parse_array_with_elements()
        {
            var input = @"[ 123, ""value"" ]";
            var parser = new Parser(input);

            IExpression expression = parser.ParseJSON();

            Assert.That(expression, Is.TypeOf<ArrayExpression>());
            Assert.That(((ArrayExpression)expression).Elements.Count, Is.EqualTo(0));
        }

        [TestCase("123", typeof(NumberExpression))]
        [TestCase("\"string\"", typeof(StringExpression))]
        public void parser_should_parse_values(string input, Type expected)
        {
            var parser = new Parser(input);
            IExpression expression = parser.ParseJSON();
            Assert.That(expression, Is.TypeOf(expected));
        }
    }
}
