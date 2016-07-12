using System;
using TK.JSONParser.Visitors;

namespace TK.JSONParser.Parsing.Nodes
{
    public class StringNode : IConstantNode<string>
    {
        private string value;

        public StringNode(string value)
        {
            this.value = value;
        }

        public string Value
            => value;

        public T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}