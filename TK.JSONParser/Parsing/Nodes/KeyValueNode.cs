using System;
using TK.JSONParser.Visitors;

namespace TK.JSONParser.Parsing.Nodes
{
    public class KeyValueNode : INode
    {
        public KeyValueNode(string key, INode value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; }

        public INode Value { get; }

        public T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}