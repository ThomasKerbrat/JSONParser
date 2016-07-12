namespace TK.JSONParser.Tokenizer
{
    public class Token
    {
        public Token(TokenType token, string value)
        {
            Type = token;
            Value = value;
        }

        public Token(TokenType token)
            : this(token, string.Empty)
        { }

        public Token(TokenType token, char value)
            : this(token, value.ToString())
        { }


        public TokenType Type { get; private set; }
        public string Value { get; private set; }
    }
}
