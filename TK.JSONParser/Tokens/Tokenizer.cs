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
        private int position;

        public Tokenizer(string input)
        {
            this.input = input;
        }

        public Token CurrentToken { get; private set; }

        #region Characters matchers

        /// <summary>
        /// Tells if the <see cref="position"/> plus the offset is greater or equal than the lenght of the input.
        /// </summary>
        /// <param name="offset">Default to zero.</param>
        public bool IsEnd(int offset = 0)
            => position + offset >= input.Length;

        /// <summary>
        /// Tells if the current char is a whitespace as defined by <see cref="char.IsWhiteSpace(char)"/>.
        /// </summary>
        public bool IsWhiteSpace
            => char.IsWhiteSpace(Peek());

        /// <summary>
        /// Tells if the current char is a letter as defined by <see cref="char.IsLetter(char)"/>.
        /// </summary>
        public bool IsIdentifier
            => char.IsLetter(Peek());

        #endregion

        public Token GetNextToken()
        {
            Token result = null;

            while (!IsEnd() && (IsWhiteSpace))
            {
                if (IsWhiteSpace) HandleWhiteSpace();
            }

            if (IsEnd()) return CurrentToken = new Token(TokenType.End);

            if (Peek() == '{') result = HandleSimpleToken(TokenType.OpenCurlyBrace);
            else if (Peek() == '}') result = HandleSimpleToken(TokenType.CloseCurlyBrace);
            else if (Peek() == '[') result = HandleSimpleToken(TokenType.OpenBracket);
            else if (Peek() == ']') result = HandleSimpleToken(TokenType.CloseBracket);
            else if (Peek() == ':') result = HandleSimpleToken(TokenType.Colon);
            else if (Peek() == ',') result = HandleSimpleToken(TokenType.Comma);
            else if (IsIdentifier) result = HandleIdentifier();

            return result;
        }

        #region Handle Methods

        /// <summary>
        /// Moves forwards while the current character is a whitespace.
        /// </summary>
        private void HandleWhiteSpace()
        {
            while (!IsEnd() && IsWhiteSpace)
            {
                Forward();
            }
        }

        /// <summary>
        /// Creates a new <see cref="Token"/> and moves forward.
        /// </summary>
        /// <param name="type">The <see cref="TokenType"/> of the new Token.</param>
        /// <returns>A new <see cref="Token"/>.</returns>
        private Token HandleSimpleToken(TokenType type)
        {
            Forward();
            return new Token(type, Peek(-1));
        }

        /// <summary>
        /// Creates a new Token for the matched identifier.
        /// </summary>
        /// <returns>A new <see cref="Token"/>.</returns>
        private Token HandleIdentifier()
        {
            var sb = new StringBuilder();
            Token result = null;

            while (!IsEnd() && (IsIdentifier))
            {
                sb.Append(Peek());
                Forward();
            }

            string identifier = sb.ToString();
            if (identifier == "true") result = new Token(TokenType.True, identifier);
            else if (identifier == "false") result = new Token(TokenType.False, identifier);
            else if (identifier == "null") result = new Token(TokenType.Null, identifier);
            else result = new Token(TokenType.Error, string.Format("Unexpected Token <{0}>.", identifier));

            return result;
        }

        #endregion

        #region Move methods

        /// <summary>
        /// Returns the character at the current position and moves forward.
        /// </summary>
        /// <returns>The <see cref="char"/> at current <see cref="position"/>.</returns>
        private char Read() => input[position++];

        /// <summary>
        /// Returns the character at the current position with the given offset.
        /// </summary>
        /// <param name="offset">Default to zero.</param>
        /// <returns>The <see cref="char"/> at current <see cref="position"/>.</returns>
        private char Peek(int offset = 0) => input[position + offset];

        /// <summary>
        /// Increments the <see cref="position"/> by 1 (one).
        /// </summary>
        private void Forward() => position++;

        #endregion
    }
}
