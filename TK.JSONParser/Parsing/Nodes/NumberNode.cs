using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TK.JSONParser.Visitors;

namespace TK.JSONParser.Parsing.Nodes
{
    public class NumberNode : IConstantNode<int>
    {
        private int value;

        public NumberNode(int value)
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
