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
        public string Visit(ArrayNode node)
        {
            var sb = new StringBuilder("[");

            foreach (var item in node.Items)
            {
                sb.Append(" ");
                sb.Append(item.Accept(this));
                sb.Append(",");
            }

            if (sb.Length > 1)
                sb.Replace(',', ' ', sb.Length - 1, 1);
            sb.Append("]");

            return sb.ToString();
        }

        public string Visit(ObjectNode node)
        {
            var sb = new StringBuilder("{");

            foreach (var item in node.Items)
            {
                sb.Append(" ");
                sb.Append(item.Accept(this));
                sb.Append(",");
            }

            if (sb.Length > 1)
                sb.Replace(',', ' ', sb.Length - 1, 1);
            sb.Append("}");

            return sb.ToString();
        }

        public string Visit(KeyValueNode node)
        {
            return string.Format("{0} : {1}", node.Key.Accept(this), node.Value.Accept(this));
        }

        public string Visit(NumberNode node)
        {
            return node.Value.ToString();
        }

        public string Visit(StringNode node)
        {
            return string.Format("\"{0}\"", node.Value);
        }

        public string Visit(ErrorNode node)
        {
            return node.ToString();
        }
    }
}
