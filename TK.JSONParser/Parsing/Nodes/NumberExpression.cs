using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TK.JSONParser.Visitors;

namespace TK.JSONParser.Parsing.Nodes
{
    public class NumberExpression : INode, IValueExpression
    {
        private int value;

        public NumberExpression(int value)
        {
            this.value = value;
        }

        public int Value
            => value;

        public T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
