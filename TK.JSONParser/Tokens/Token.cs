using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TK.JSONParser.Tokens
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


        public object Type { get; private set; }
        public object Value { get; private set; }
    }
}
