using TK.JSONParser.Parsing.Expressions;
using TK.JSONParser.Parsing.Values;

namespace TK.JSONParser.Visitors
{
    public interface IVisitor<T>
    {
        T Visit(ArrayExpression expression);

        T Visit(ObjectExpression expression);

        T Visit(MemberExpression expression);

        T Visit(NumberExpression expression);

        T Visit(StringExpression expression);

        T Visit(ErrorExpression expression);
    }
}