using System.Text.RegularExpressions;

namespace Tokens
{
    public class VariableToken : AbstractToken
    {
        public static Regex definition = new(@"^[A-Za-z][a-zA-Z0-9_]*");
        public VariableToken(int index, string rawText) : base(index, rawText) { }

        public override void Accept(ITokenVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override TryCreateTokenResult TryCreateToken(string s)
        {
            var matchResult = definition.Match(s);
            if (matchResult.Success)
            {
                return new TryCreateTokenResult(new VariableToken(matchResult.Value.Length, matchResult.Value), s);
            }
            else
            {
                return new TryCreateTokenResult(token: null, s);
            }
        }
    }
}
