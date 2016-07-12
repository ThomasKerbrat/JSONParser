using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TK.JSONParser.Parsing;
using TK.JSONParser.Parsing.Nodes;

namespace TK.JSONParser.Visitors
{
    public class InlineVisitor : IVisitor<string>
    {
        public string Visit(ObjectNode expression)
        {
            throw new NotImplementedException();
        }

        public string Visit(NumberNode expression)
        {
            throw new NotImplementedException();
        }

        public string Visit(KeyValueNode node)
        {
            throw new NotImplementedException();
        }

        public string Visit(StringNode expression)
        {
            throw new NotImplementedException();
        }

        public string Visit(ArrayNode expression)
        {
            throw new NotImplementedException();
        }

        public string Visit(ErrorNode expression)
        {
            throw new NotImplementedException();
        }
    }
}
