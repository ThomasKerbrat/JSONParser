using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TK.JSONParser.Parsing.Nodes;
using TK.JSONParser.Visitors;

namespace TK.JSONParser.Parsing.Nodes
{
    public class ObjectNode : INode, IValueExpression
    {
        private Dictionary<string, MemberNode> members;

        public ObjectNode()
        {
            this.members = new Dictionary<string, MemberNode>();
        }

        public IReadOnlyDictionary<string, MemberNode> Members
            => members;

        internal bool AddMember(MemberNode expression)
        {
            throw new NotImplementedException();
        }

        public T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
