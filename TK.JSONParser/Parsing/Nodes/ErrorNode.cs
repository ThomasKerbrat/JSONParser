using System;
using TK.JSONParser.Visitors;

namespace TK.JSONParser.Parsing.Nodes
{
    public class ErrorNode : INode
    {
        private string message;

        public ErrorNode(string message)
        {
            this.message = message;
        }

        public override string ToString()
            => message;

        public T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
