using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TK.JSONParser.Visitors;

namespace TK.JSONParser.Parsing.Nodes
{
    public class MemberNode : INode
    {
        public MemberNode(string memberName, IValueExpression value)
        {
            Name = memberName;
            Value = value;
        }

        public string Name { get; }

        public IValueExpression Value { get; set; }

        public T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
