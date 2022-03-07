namespace Tokens
{
    public abstract class AbstractToken : ITokenVisitorAcceptor
    {
        public int index;

        public string rawText;

        public AbstractToken(int index, string rawText)
        {
            this.index = index;
            this.rawText = rawText;
        }

        public override string ToString()
        {
            return index.ToString() + "\t'" + rawText + "'\t" + base.ToString();
        }

        public abstract TryCreateTokenResult TryCreateToken(string s);

        public abstract void Accept(ITokenVisitor visitor);
    }
}
