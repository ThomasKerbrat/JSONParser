using TK.JSONParser.Visitors;

namespace TK.JSONParser.Parsing
{
    public interface INode
    {
        T Accept<T>(IVisitor<T> visitor);
    }
}