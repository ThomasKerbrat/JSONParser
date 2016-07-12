using System;
using TK.JSONParser.Visitors;

namespace TK.JSONParser.Parsing.Values
{
    public class StringExpression : IExpression, IValueExpression
    {
        private string value;

        public StringExpression(string value)
        {
            this.value = value;
        }

        public T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}