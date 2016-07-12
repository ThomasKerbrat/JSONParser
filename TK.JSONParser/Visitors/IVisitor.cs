using TK.JSONParser.Parsing.Nodes;
using TK.JSONParser.Parsing.Values;

namespace TK.JSONParser.Visitors
{
    public interface IVisitor<T>
    {
        T Visit(ArrayNode expression);

        T Visit(ObjectNode expression);

        T Visit(MemberNode expression);

        T Visit(NumberExpression expression);

        T Visit(StringExpression expression);

        T Visit(ErrorNode expression);
    }
}