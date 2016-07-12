namespace TK.JSONParser.Parsing.Nodes
{
    public interface IConstantNode<T> : INode
    {
        T Value { get; }
    }
}
