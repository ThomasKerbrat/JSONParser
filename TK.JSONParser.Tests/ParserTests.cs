using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TK.JSONParser.Parsing;
using TK.JSONParser.Parsing.Nodes;

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

            INode expression = parser.ParseJSON();

            Assert.That(expression, Is.TypeOf<ObjectNode>());
            Assert.That(((ObjectNode)expression).Items.Count, Is.EqualTo(0));
        }

        [Test]
        public void parser_should_parse_object_with_members()
        {
            var input = "{ \"prop1\": 123, \"prop2\": \"value\" }";
            var parser = new Parser(input);

            INode expression = parser.ParseJSON();

            Assert.That(expression, Is.TypeOf<ObjectNode>());
            Assert.That(((ObjectNode)expression).Items.Count, Is.EqualTo(0));
        }

        [Test]
        public void parser_should_parse_empty_array()
        {
            var input = "[]";
            var parser = new Parser(input);

            INode expression = parser.ParseJSON();

            Assert.That(expression, Is.TypeOf<ArrayNode>());
            Assert.That(((ArrayNode)expression).Items.Count, Is.EqualTo(0));
        }

        [Test]
        public void parser_should_parse_array_with_elements()
        {
            var input = @"[ 123, ""value"" ]";
            var parser = new Parser(input);

            INode expression = parser.ParseJSON();

            Assert.That(expression, Is.TypeOf<ArrayNode>());
            Assert.That(((ArrayNode)expression).Items.Count, Is.EqualTo(0));
        }

        [TestCase("123", typeof(NumberNode))]
        [TestCase("\"string\"", typeof(StringNode))]
        public void parser_should_parse_values(string input, Type expected)
        {
            var parser = new Parser(input);
            INode expression = parser.ParseJSON();
            Assert.That(expression, Is.TypeOf(expected));
        }
    }
}
