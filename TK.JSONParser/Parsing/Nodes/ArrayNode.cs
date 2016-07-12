using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TK.JSONParser.Visitors;

namespace TK.JSONParser.Parsing.Nodes
{
    public class ArrayNode : IStructNode<INode>
    {
        private List<INode> items;

        public ArrayNode()
        {
            items = new List<INode>();
        }

        public IReadOnlyList<INode> Items
            => items;

        public bool AddItem(INode node)
        {
            items.Add(node);
            return true;
        }

        public T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
