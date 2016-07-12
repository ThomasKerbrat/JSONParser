using System.Collections.Generic;

namespace TK.JSONParser.Parsing.Nodes
{
    public interface IStructNode : INode
    {
        IReadOnlyCollection<INode> Items { get; }
    }
}
