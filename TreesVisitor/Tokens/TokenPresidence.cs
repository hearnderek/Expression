namespace Tokens
{
    public class TokenPresidence
    {
        public static AbstractToken[] tokens = new AbstractToken[]
        {
            new WhitespaceToken(0,""),
            new VariableToken(0,""),
            new OpToken(0,""),
            new NumberToken(0,""),
        };
    }
}
