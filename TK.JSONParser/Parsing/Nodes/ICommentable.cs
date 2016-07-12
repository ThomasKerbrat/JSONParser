namespace TK.JSONParser.Parsing.Nodes
{
    public interface ICommentable
    {
        CommentNode Comment { get; }

        void AddComment(string comment);
    }
}
