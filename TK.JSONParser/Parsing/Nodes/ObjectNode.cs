using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TK.JSONParser.Visitors;

namespace TK.JSONParser.Parsing.Nodes
{
    public class ObjectNode : IStructNode<KeyValueNode>
    {
        private List<KeyValueNode> items;
        private List<string> existingKeys;

        public ObjectNode()
        {
            items = new List<KeyValueNode>();
        }

        public IReadOnlyList<KeyValueNode> Items
            => items;

        public bool AddItem(KeyValueNode node)
        {
            if (existingKeys == null)
                existingKeys = new List<string>();
            else if (existingKeys.Contains(node.Key.Value))
                return false;

            items.Add(node);
            existingKeys.Add(node.Key.Value);
            return true;
        }

        public T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
