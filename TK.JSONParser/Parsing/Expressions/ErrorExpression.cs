using System;
using TK.JSONParser.Visitors;

namespace TK.JSONParser.Parsing.Expressions
{
    public class ErrorExpression : IExpression
    {
        private string message;

        public ErrorExpression(string message)
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
