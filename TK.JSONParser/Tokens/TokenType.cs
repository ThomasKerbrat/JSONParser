namespace TK.JSONParser.Tokens
{
    public enum TokenType
    {
        OpenBracket,
        CloseBracket,

        OpenCurlyBrace,
        CloseCurlyBrace,

        Colon,
        Comma,

        String,
        Number,
        True,
        False,
        Null,

        Error
    }
}
