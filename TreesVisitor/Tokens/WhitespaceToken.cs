using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Tokens
{

    public class WhitespaceToken : AbstractToken
    {
        public static Regex definition = new(@"^\s+");
        public WhitespaceToken(int index, string rawText) : base(index, rawText) { }

        public override void Accept(ITokenVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override TryCreateTokenResult TryCreateToken(string s)
        {
            var matchResult = definition.Match(s);
            if (matchResult.Success)
            {
                return new TryCreateTokenResult(new WhitespaceToken(matchResult.Value.Length, matchResult.Value), s);
            }
            else
            {
                return new TryCreateTokenResult(token: null, s);
            }
        }
    }
}
