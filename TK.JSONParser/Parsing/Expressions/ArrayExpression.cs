using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TK.JSONParser.Parsing.Values;
using TK.JSONParser.Visitors;

namespace TK.JSONParser.Parsing.Expressions
{
    public class ArrayExpression : IExpression, IValueExpression
    {
        private List<IValueExpression> elements;

        public ArrayExpression()
        {
            elements = new List<IValueExpression>();
        }

        public IReadOnlyList<IValueExpression> Elements
            => elements;

        public T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
