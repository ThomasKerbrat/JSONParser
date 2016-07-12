using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TK.JSONParser.Tokens
{
    public class Tokenizer
    {
        private string input;

        public Tokenizer(string input)
        {
            this.input = input;
        }

        public Token GetNextToken()
        {
            throw new NotImplementedException();
        }
    }
}
