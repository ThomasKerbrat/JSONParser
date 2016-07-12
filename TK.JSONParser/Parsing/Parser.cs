using System;
using TK.JSONParser.Parsing.Nodes;
using TK.JSONParser.Tokens;

namespace TK.JSONParser.Parsing
{
    public class Parser
    {
        private readonly Tokenizer tokenizer;

        public Parser(string input)
        {
            tokenizer = new Tokenizer(input);
            tokenizer.GetNextToken();
        }

        public INode ParseJSON()
        {
            INode json;

            if (tokenizer.MatchToken(TokenType.OpenBracket))
                json = ParseArray();
            else if (tokenizer.MatchToken(TokenType.OpenCurlyBrace))
                json = ParseObject();
            else
                json = CreateUnexpectedErrorExpression();

            return json;
        }

        #region Grammar Methods

        INode ParseArray()
        {
            if (tokenizer.MatchToken(TokenType.CloseBracket))
                return new ArrayNode();

            throw new NotImplementedException();
        }

        INode ParseObject()
        {
            if (tokenizer.MatchToken(TokenType.CloseCurlyBrace))
                return new ObjectNode();

            ObjectNode @object = new ObjectNode();
            Token memberName;

            while (tokenizer.MatchToken(TokenType.String, out memberName))
            {
                INode expression = ParseMember(memberName.Value);

                if (expression is ErrorNode)
                    return expression;

                KeyValueNode member = (KeyValueNode)expression;
                if (!@object.AddKeyValueNode(member))
                    return new ErrorNode(string.Format("Member \"{0}\" already present in object.", member.Key));

                tokenizer.MatchToken(TokenType.Comma);
            }

            return @object;
        }

        INode ParseMember(string name)
        {
            // Match the colon.
            if (!tokenizer.MatchToken(TokenType.Colon))
                return CreateErrorExpression(":");

            // Match the value.
            INode value = ParseValue();
            if (value is ErrorNode)
                return value;

            return new KeyValueNode(name, value);
        }

        INode ParseValue()
        {
            Token token;
            if (tokenizer.MatchToken(TokenType.Integer, out token))
            {
                int value;
                if (int.TryParse(token.Value, out value))
                    return new NumberNode(value);
                else
                    return new ErrorNode("Can not parse number");
            }
            else if (tokenizer.MatchToken(TokenType.String, out token))
            {
                return new StringNode(token.Value);
            }
            else if (tokenizer.MatchToken(TokenType.OpenCurlyBrace))
            {
                return ParseObject();
            }
            else if (tokenizer.MatchToken(TokenType.OpenBracket))
            {
                return ParseArray();
            }
            else
            {
                return CreateUnexpectedErrorExpression();
            }
        }

        #endregion

        #region Error Helpers

        /// <summary>
        /// Creates a new <see cref="ErrorNode"/> saying the <see cref="Parser"/> is expecting
        /// the given <paramref name="expected"/> but it found <see cref="Tokenizer.CurrentToken"/>.
        /// </summary>
        /// <param name="expected">The string expected by the <see cref="Parser"/>.</param>
        /// <returns>A new <see cref="ErrorNode"/>.</returns>
        ErrorNode CreateErrorExpression(string expected)
            => new ErrorNode(string.Format(
                "Expected <{0}>, but <{1}> found.", expected, tokenizer.CurrentToken.Value));

        /// <summary>
        /// Creates a new <see cref="ErrorNode"/> with the <see cref="Tokenizer.CurrentToken"/> as the unexpected value.
        /// </summary>
        /// <returns>A new <see cref="ErrorNode"/>.</returns>
        ErrorNode CreateUnexpectedErrorExpression()
            => new ErrorNode(string.Format(
                "Unexpected token <{0}>.", tokenizer.CurrentToken.Value));

        #endregion

    }
}
