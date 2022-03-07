namespace Tokens
{
    public interface ITokenVisitor
    {
        public void Visit(NumberToken token);

        public void Visit(OpToken token);

        public void Visit(WhitespaceToken token);

        public void Visit(VariableToken token);
    }
}
