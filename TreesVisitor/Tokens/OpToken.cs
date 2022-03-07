using System.Text.RegularExpressions;

namespace Tokens
{
    public class OpToken : AbstractToken
    {
        public static Regex definition = new(@"^(\+|-|\*|/|\^)");
        public OpToken(int index, string rawText) : base(index, rawText) { }

        public override void Accept(ITokenVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override TryCreateTokenResult TryCreateToken(string s)
        {
            var matchResult = definition.Match(s);
            if (matchResult.Success)
            {
                return new TryCreateTokenResult(new OpToken(matchResult.Value.Length, matchResult.Value), s);
            }
            else
            {
                return new TryCreateTokenResult(token: null, s);
            }
        }
    }
}
