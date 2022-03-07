namespace Tokens
{
    public class TokenGrouper : ITokenVisitor
    {
        public List<AbstractToken> tokens = new List<AbstractToken>();
        public AbstractToken? lastNonWhitespace = null;
        public int numTokens = 0;
        public int tokenIndex = 0;
        public bool madeGrouping = false;

        public TokenGrouper(IEnumerable<AbstractToken> tokens)
        {
            this.tokens = tokens.ToList();
            numTokens = this.tokens.Count;
        }

        public static bool IsType<T>(AbstractToken token) where T : AbstractToken
        {
            return TokenTypeVisitor<T>.IsType<T>(token);
        }

        public T? TryGet<T>(AbstractToken token) where T : AbstractToken
        {
            return TokenTypeVisitor<T>.TryGet<T>(token);
        }

        public bool AttemptGroupingPass()
        {
            madeGrouping = false;

            // the variable 'tokenIndex' will change within the visit methods on a successful grouping
            for (tokenIndex = 0; tokenIndex < tokens.Count; tokenIndex++)
            {
                tokens[tokenIndex].Accept(this);
            }

            // the variable 'madeGrouping' will change within the visit methods on a successful grouping
            return madeGrouping;
        }

        public void Visit(NumberToken token)
        {
            lastNonWhitespace = token;
        }

        public void Visit(OpToken token)
        {
            try
            {
                if (token.rawText != "-") return;
                if (numTokens < 2 || tokenIndex >= numTokens-1) return;
                
                Console.Write("--->");
                Console.WriteLine(token);

                NumberToken? maybeNumericPost = TryGet<NumberToken>(tokens[tokenIndex + 1]);
                if (lastNonWhitespace == null && maybeNumericPost != null)
                {
                    // TODO combine with number
                    tokens.RemoveAt(tokenIndex);
                    maybeNumericPost.index -= 1;
                    maybeNumericPost.rawText = "-" + maybeNumericPost.rawText;
                    madeGrouping = true;

                    return;
                }

                // This second one doesn't work due to the whitespace in the way...
                NumberToken? maybeNumberPre = lastNonWhitespace != null ? TryGet<NumberToken>(lastNonWhitespace) : null;
                Console.Write("----");
                Console.WriteLine(lastNonWhitespace);
                Console.Write("----");
                Console.WriteLine(maybeNumberPre);
                if ((maybeNumericPost != null) && (maybeNumberPre == null))
                {
                    // TODO combine with number
                    tokens.RemoveAt(tokenIndex);
                    maybeNumericPost.index -= 1;
                    maybeNumericPost.rawText = "-" + maybeNumericPost.rawText;
                    madeGrouping = true;

                    return;
                }
            }
            finally
            {
                lastNonWhitespace = token;
            }
        }

        public void Visit(WhitespaceToken token)
        {   
        }

        public void Visit(VariableToken token)
        {
            lastNonWhitespace = token;
        }
    }



}
