using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TK.JSONParser.Parsing.Values;
using TK.JSONParser.Visitors;

namespace TK.JSONParser.Parsing.Expressions
{
    public class ObjectExpression : IExpression, IValueExpression
    {
        private Dictionary<string, MemberExpression> members;

        public ObjectExpression()
        {
            this.members = new Dictionary<string, MemberExpression>();
        }

        public IReadOnlyDictionary<string, MemberExpression> Members
            => members;

        internal bool AddMember(MemberExpression expression)
        {
            throw new NotImplementedException();
        }

        public T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
