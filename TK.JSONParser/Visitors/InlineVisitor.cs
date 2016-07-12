using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TK.JSONParser.Parsing;
using TK.JSONParser.Parsing.Expressions;
using TK.JSONParser.Parsing.Values;

namespace TK.JSONParser.Visitors
{
    public class InlineVisitor : IVisitor<string>
    {
        public string Visit(ObjectExpression expression)
        {
            throw new NotImplementedException();
        }

        public string Visit(NumberExpression expression)
        {
            throw new NotImplementedException();
        }

        public string Visit(StringExpression expression)
        {
            throw new NotImplementedException();
        }

        public string Visit(MemberExpression expression)
        {
            throw new NotImplementedException();
        }

        public string Visit(ArrayExpression expression)
        {
            throw new NotImplementedException();
        }

        public string Visit(ErrorExpression expression)
        {
            throw new NotImplementedException();
        }
    }
}
