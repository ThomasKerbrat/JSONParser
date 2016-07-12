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
        Integer,
        Fraction,
        Exponent,
        True,
        False,
        Null,

        Error,
        End
    }
}
