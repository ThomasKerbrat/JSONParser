using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TK.JSONParser.Parsing.Values;
using TK.JSONParser.Visitors;

namespace TK.JSONParser.Parsing.Expressions
{
    public class MemberExpression : IExpression
    {
        public MemberExpression(string memberName, IValueExpression value)
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
