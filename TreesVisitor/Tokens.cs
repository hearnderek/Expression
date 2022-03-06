using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Tokens
{
    /* Ways to match a string
     * 
     * 1. regex
     * 
     * 2. reference other TokenDefinition 
     *    -  
     *
     * /regex/
     *  - / usages must be doubled to //
     * 
     * {TokenDefinition}
     * 
     */

    public class Tokenizer
    {
        public static Token[] Tokenize(string s)
        {
            List<Token> xs = new List<Token>();
            while (s != "")
            {
                var tryCreates = TokenPresidence.tokens
                    .Select(token => token.TryCreate(s))
                    .Where(tryResult => tryResult.token != null);

                foreach (var tryResult in tryCreates)
                {
                    s = tryResult.newString;
                    xs.Add(tryResult.token);
                }
            }

            return xs.ToArray();
        }
    }

    public class TokenPresidence
    {
        public static Token[] tokens = new Token[]
        {
            new WhitespaceToken(0,""),
            new VariableToken(0,""),
            new OpToken(0,""),
            new IntToken(0,""),
        };
    }

    public interface ITryCreateResult
    {
        public TryCreateResult TryCreate(string s);
    }

    public abstract class Token
    {
        public int index;

        public string rawText;

        public Token(int index, string rawText)
        {
            this.index = index;
            this.rawText = rawText;
        }

        public override string ToString()
        {
            return index.ToString() + "\t'" + rawText + "'\t" + base.ToString();
        }

        public abstract TryCreateResult TryCreate(string s);
    }

    public struct TryCreateResult
    {
        public Token token;
        public int movedStringPointerBy;
        public string newString;

        public TryCreateResult(Token token, string s)
        {
            this.token = token;
            
            movedStringPointerBy = token?.rawText.Length ?? 0;
            if (movedStringPointerBy == s.Length)
            {
                newString = "";
            }
            else if (movedStringPointerBy >= 0)
            {
                newString = s.Substring(movedStringPointerBy);
            }
            else
            {
                newString = s;
            }
        }
    }

    /// <summary>
    /// /^-?\d+/
    /// </summary>
    public class IntToken : Token
    {
        public Regex definition = new(@"^\d+");

        public IntToken(int index, string rawText) : base(index, rawText) { }

        public override TryCreateResult TryCreate(string s)
        {
            var matchResult = definition.Match(s);
            if ( matchResult.Success )
            {
                return new TryCreateResult(new IntToken(matchResult.Value.Length, matchResult.Value), s);
            }
            else
            {
                return new TryCreateResult(token: null, s);
            }
        }
    }

    public class OpToken : Token
    {
        public static Regex definition = new(@"^(\+|-|\*|/|\^)");
        public OpToken(int index, string rawText) : base(index, rawText) { }

        public override TryCreateResult TryCreate(string s)
        {
            var matchResult = definition.Match(s);
            if (matchResult.Success)
            {
                return new TryCreateResult(new OpToken(matchResult.Value.Length, matchResult.Value), s);
            }
            else
            {
                return new TryCreateResult(token: null, s);
            }
        }
    }

    public class VariableToken : Token
    {
        public static Regex definition = new(@"^[A-Za-z][a-zA-Z0-9_]*");
        public VariableToken(int index, string rawText) : base(index, rawText) { }

        public override TryCreateResult TryCreate(string s)
        {
            var matchResult = definition.Match(s);
            if (matchResult.Success)
            {
                return new TryCreateResult(new VariableToken(matchResult.Value.Length, matchResult.Value), s);
            }
            else
            {
                return new TryCreateResult(token: null, s);
            }
        }
    }

    public class WhitespaceToken : Token
    {
        public static Regex definition = new(@"^\s+");
        public WhitespaceToken(int index, string rawText) : base(index, rawText) { }

        public override TryCreateResult TryCreate(string s)
        {
            var matchResult = definition.Match(s);
            if (matchResult.Success)
            {
                return new TryCreateResult(new WhitespaceToken(matchResult.Value.Length, matchResult.Value), s);
            }
            else
            {
                return new TryCreateResult(token: null, s);
            }
        }
    }
}
