using TK.JSONParser.Visitors;

namespace TK.JSONParser.Parsing
{
    public interface IExpression
    {
        T Accept<T>(IVisitor<T> visitor);
    }
}