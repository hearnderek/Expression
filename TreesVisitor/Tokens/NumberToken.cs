using System.Text.RegularExpressions;

namespace Tokens
{
    /// <summary>
    /// /^-?\d+/
    /// </summary>
    public class NumberToken : AbstractToken
    {
        public Regex definition = new(@"^(-?\d*\.\d+|-?\d+)");

        public NumberToken(int index, string rawText) : base(index, rawText) { }

        public override void Accept(ITokenVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override TryCreateTokenResult TryCreateToken(string s)
        {
            var matchResult = definition.Match(s);
            if ( matchResult.Success )
            {
                return new TryCreateTokenResult(new NumberToken(matchResult.Value.Length, matchResult.Value), s);
            }
            else
            {
                return new TryCreateTokenResult(token: null, s);
            }
        }
    }
}
