using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TK.JSONParser.Parsing;
using TK.JSONParser.Tokens;
using TK.JSONParser.Visitors;

namespace TK.JSONParser.Tests.Visitors
{
    [TestFixture]
    public class InlineVisitorTests
    {
        [Test]
        public void empty() { }

        [TestCase(@"[]", @"[]")]
        [TestCase(@"[123]", @"[ 123 ]")]
        [TestCase(@"[123,456]", @"[ 123, 456 ]")]
        [TestCase(@"{}", @"{}")]
        [TestCase(@"{""p"":""123""}", @"{ ""p"" : ""123"" }")]
        [TestCase(@"{""p"":""123"",""p2"":""456""}", @"{ ""p"" : ""123"", ""p2"" : ""456"" }")]
        public void visitor_should_render_contant_values(string input, string expected)
        {
            var parser = new Parser(input);
            INode node = parser.ParseJSON();
            var visitor = new InlineVisitor();

            var result = visitor.Visit(node);

            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
