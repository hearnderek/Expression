/// <summary>
/// TODO move this to it's own folder, and move inner classes to their own files
/// </summary>
namespace AST
{
    public interface IExpressionVisitorAcceptor<T>
    {
        public T AcceptReturn(IExpressionVisitor visitor);
        public void Accept(IExpressionVisitor visitor);
    }
}
