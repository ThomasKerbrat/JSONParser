using System.Collections.Generic;

namespace TK.JSONParser.Parsing.Nodes
{
    public interface IStructNode<T> : INode
    {
        IReadOnlyList<T> Items { get; }
    }
}
