/// <summary>
/// TODO move this to it's own folder, and move inner classes to their own files
/// </summary>
namespace AST
{
    /// <summary>
    /// Non-Recursive Visitor purely to provide double-dispatch
    /// </summary>
    public interface IExpressionVisitor
    {
        public void Visit(BinaryOperation.Add op);
        public void Visit(BinaryOperation.Mul op);
        public void Visit(BinaryOperation.Sub op);
        public void Visit(BinaryOperation.Div op);
        public void Visit(BinaryOperation.Exp op);
        public void Visit(Value value);
        public void Visit(Variable variable);
    }
}
