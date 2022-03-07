namespace Tokens
{
    public interface ITokenVisitorAcceptor
    {
        public void Accept(ITokenVisitor visitor);
    }
}
