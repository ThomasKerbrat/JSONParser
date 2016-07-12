using System;
using TK.JSONParser.Visitors;

namespace TK.JSONParser.Parsing.Nodes
{
    public class CommentNode : INode, IConstantNode<string>
    {
        private string comment;

        public CommentNode(string comment)
        {
            this.comment = comment;
        }

        public string Value
            => comment;

        public T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}