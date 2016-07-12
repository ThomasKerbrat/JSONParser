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
        Minus,
        Plus,

        String,
        Number,
        True,
        False,
        Null,

        Error,
        End
    }
}
