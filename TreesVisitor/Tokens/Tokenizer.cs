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
        public static AbstractToken[] Tokenize(string s)
        {
            int stringPointer = 0;
            List<AbstractToken> xs = new List<AbstractToken>();
            while (s != "")
            {
                var tryCreates = TokenPresidence.tokens
                    .Select(token => token.TryCreateToken(s))
                    .Where(tryResult => tryResult.token != null);

                foreach (var tryResult in tryCreates)
                {
                    s = tryResult.newString;

                    var tmp = stringPointer;
                    stringPointer += tryResult.token.index;
                    tryResult.token.index = tmp;
                    xs.Add(tryResult.token);
                }
            }

            return xs.ToArray();
        }
    }
}
