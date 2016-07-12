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

            ObjectNode @object = (ObjectNode)expression;
            Assert.That(@object.Items.Count, Is.EqualTo(2));

            Assert.That(@object.Items[0].Key.Value, Is.EqualTo("prop1"));
            Assert.That(@object.Items[0].Value, Is.TypeOf<NumberNode>());
            Assert.That(((NumberNode)@object.Items[0].Value).Value, Is.EqualTo(123));

            Assert.That(@object.Items[1].Key.Value, Is.EqualTo("prop2"));
            Assert.That(@object.Items[1].Value, Is.TypeOf<StringNode>());
            Assert.That(((StringNode)@object.Items[1].Value).Value, Is.EqualTo("value"));
        }

        [Test]
        public void parser_should_not_parse_object_with_same_members()
        {
            var input = "{ \"prop1\": 123, \"prop1\": \"value\" }";
            var parser = new Parser(input);

            INode expression = parser.ParseJSON();

            Assert.That(expression, Is.TypeOf<ErrorNode>());
            Assert.That(((ErrorNode)expression).ToString(), Is.EqualTo("Member \"prop1\" already present in object."));
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

            ArrayNode array = (ArrayNode)expression;
            Assert.That(array.Items.Count, Is.EqualTo(2));

            Assert.That(array.Items[0], Is.TypeOf<NumberNode>());
            Assert.That(((NumberNode)array.Items[0]).Value, Is.EqualTo(123));

            Assert.That(array.Items[1], Is.TypeOf<StringNode>());
            Assert.That(((StringNode)array.Items[1]).Value, Is.EqualTo("value"));
        }

        [TestCase("{ \"prop\": 123, \"prop2\": [ 456, \"789\" ] }")]
        public void parser_should_not_parse_to_error(string input)
        {
            var parser = new Parser(input);
            INode expression = parser.ParseJSON();
            Assert.That(expression, Is.Not.TypeOf<ErrorNode>());

        }
    }
}
