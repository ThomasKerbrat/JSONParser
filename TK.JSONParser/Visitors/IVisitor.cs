using TK.JSONParser.Parsing;
using TK.JSONParser.Parsing.Nodes;

namespace TK.JSONParser.Visitors
{
    public interface IVisitor<T>
    {
        T Visit(ArrayNode node);

        T Visit(ObjectNode node);

        T Visit(KeyValueNode node);

        T Visit(NumberNode node);

        T Visit(StringNode node);

        T Visit(ErrorNode node);
    }

    public static class IVisitorExtentions
    {
        public static T Visit<T>(this IVisitor<T> @this, INode node)
            => node.Accept(@this);
    }
}