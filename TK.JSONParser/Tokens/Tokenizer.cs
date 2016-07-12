using System;
using System.Text;

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

        /// <summary>
        /// Tells if the current char is a number as defined by <see cref="char.IsNumber(char)"/>.
        /// </summary>
        public bool IsInteger
            => char.IsNumber(Peek());

        /// <summary>
        /// Tells if the current char is a fraction.
        /// </summary>
        public bool IsFraction
            => Peek() == '.';

        /// <summary>
        /// Tells if the current char is an exponent.
        /// </summary>
        public bool IsExponent
            => Peek() == 'e' || Peek() == 'E';

        /// <summary>
        /// Tells if the current char is a double quote, which indicate a start or an end of a string literal.
        /// </summary>
        public bool IsString
            => Peek() == '"';

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
            else if (Peek() == '-') result = HandleSimpleToken(TokenType.Minus);
            else if (Peek() == '+') result = HandleSimpleToken(TokenType.Plus);
            else if (Peek() == '/') result = HandleComments();
            else if (IsInteger) result = HandleInteger();
            //else if (IsFraction) result = HandleFraction();
            //else if (IsExponent) result = HandleExponent();
            else if (IsIdentifier) result = HandleIdentifier();
            else if (IsString) result = HandleString();
            else result = new Token(TokenType.Error, Peek());

            return CurrentToken = result;
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

        /// <summary>
        /// Creates a new <see cref="Token"/> for the matched number.
        /// </summary>
        /// <returns>A new <see cref="Token"/>.</returns>
        private Token HandleInteger()
        {
            Token result = null;

            if (Peek() == '0')
            {
                Forward();
                if (!IsEnd() && IsInteger) result = new Token(TokenType.Error, "0" + Peek());
                else result = new Token(TokenType.Integer, '0');
            }
            else
            {
                var sb = new StringBuilder();
                while (!IsEnd() && IsInteger)
                {
                    sb.Append(Peek());
                    Forward();
                }
                result = new Token(TokenType.Integer, sb.ToString());
            }

            return result;
        }

        /// <summary>
        /// Creates a new <see cref="Token"/> for the matched number.
        /// </summary>
        /// <returns>A new <see cref="Token"/>.</returns>
        private Token HandleString()
        {
            var sb = new StringBuilder();

            Forward();
            while (!IsEnd() && !IsString)
            {
                if (Peek() == '\\')
                {
                    Forward();
                    if (Peek() == '\\') sb.Append('\\');
                    else if (Peek() == '/') sb.Append('/');
                    else if (Peek() == '"') sb.Append('"');
                    // TODO: Implement the other escape strings :)
                }
                else
                {
                    sb.Append(Peek());
                }
                Forward();
            }

            // Match closing double quote.
            Forward();

            return new Token(TokenType.String, sb.ToString());
        }

        /// <summary>
        /// Create a new <see cref="Token"/> that contains the comment value.
        /// </summary>
        /// <returns>A new <see cref="Token"/>.</returns>
        private Token HandleComments()
        {
            Forward();
            switch (Peek())
            {
                case '/': return HandleInLineComment();
                case '*': return HandleMultiLineComment();
                default: return new Token(TokenType.Error, Peek());
            }
        }

        /// <summary>
        /// Handle single line comments.
        /// </summary>
        /// <returns>A new <see cref="Token"/>.</returns>
        private Token HandleInLineComment()
        {
            var sb = new StringBuilder();

            Forward();
            while (!IsEnd() && Peek() != '\r' && Peek() != '\n')
            {
                sb.Append(Read());
            }

            return new Token(TokenType.Comment, sb.ToString());
        }

        private Token HandleMultiLineComment()
        {
            var sb = new StringBuilder();

            Forward();
            while (!IsEnd() && Peek() != '*' && Peek(1) != '/')
            {
                sb.Append(Read());
            }

            Forward();
            Forward();

            return new Token(TokenType.Comment, sb.ToString());
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

        #region Token Matchers

        /// <summary>
        /// Calls <see cref="MatchToken(TokenType, out Token)"/> but discard the Token value assigned in the method.
        /// </summary>
        /// <param name="type">Type to match against.</param>
        /// <returns>Returns if the <see cref="CurrentToken"/> type has matched with the provided Token type.</returns>
        public bool MatchToken(TokenType type, Token token = null)
            => MatchToken(type, out token);

        /// <summary>
        /// Match the <see cref="CurrentToken"/> type to any <see cref="TokenType"/> provided.
        /// </summary>
        /// <param name="type">Type to match against.</param>
        /// <param name="token">Variable that will be assigned during the match.</param>
        /// <returns>Returns if the <see cref="CurrentToken"/> type has matched with the provided Token type.</returns>
        public bool MatchToken(TokenType type, out Token token)
            => MatchToken(_type => _type == type, out token);

        /// <summary>
        /// Match the <see cref="CurrentToken"/> type to any <see cref="TokenType"/> with the provided predicate.
        /// </summary>
        /// <param name="predicate">A predicate that will determine if the <see cref="TokenType"/> has matched.</param>
        /// <param name="token">Variable that will be assigned during the match.</param>
        /// <returns>Returns if the <see cref="CurrentToken"/> type has matched according to the provided predicate.</returns>
        public bool MatchToken(Func<TokenType, bool> predicate, out Token token)
        {
            if (predicate(CurrentToken.Type))
            {
                token = CurrentToken;
                GetNextToken();
                return true;
            }

            token = null;
            return false;
        }

        #endregion
    }
}
