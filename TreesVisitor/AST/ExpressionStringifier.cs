/// <summary>
/// TODO move this to it's own folder, and move inner classes to their own files
/// </summary>
namespace AST
{
    public class ExpressionStringifier : IExpressionVisitor
    {
        public string result = "";

        public static string Stringify(Expression expression)
        {
            var visitor = new ExpressionStringifier();
            expression.Accept(visitor);
            return visitor.result;
        }

        public void Visit(BinaryOperation.Add op)
        {
            op.left.Accept(this);
            result += " + ";
            op.right.Accept(this);
        }

        public void Visit(BinaryOperation.Mul op)
        {
            op.left.Accept(this);
            result += " * ";
            op.right.Accept(this);
        }

        public void Visit(BinaryOperation.Sub op)
        {
            op.left.Accept(this);
            result += " - ";
            op.right.Accept(this);
        }

        public void Visit(BinaryOperation.Div op)
        {
            op.left.Accept(this);
            result += " / ";
            op.right.Accept(this);
        }

        public void Visit(BinaryOperation.Exp op)
        {
            op.left.Accept(this);
            result += " ^ ";
            op.right.Accept(this);
        }

        public void Visit(Value value)
        {
            result += value.value.ToString();
        }

        public void Visit(Variable variable)
        {
            result += variable.identifier;
        }
    }
}
