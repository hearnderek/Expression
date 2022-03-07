/// <summary>
/// TODO move this to it's own folder, and move inner classes to their own files
/// </summary>
namespace AST
{
    public abstract class Expression : IExpressionVisitorAcceptor
    {
        public Expression parent;
        public List<Expression> expressions = new List<Expression>();
        public abstract Value Evaluate(Dictionary<string, int> variableDefinitions = null);

        public abstract void Accept(IExpressionVisitor visitor);

        public Expression(IEnumerable<Expression> expressions = null, Expression parent = null)
        {
            this.parent = parent;
            if (expressions != null)
            {
                this.expressions = expressions.ToList();

                foreach(var expression in expressions)
                {
                    expression.parent = this;
                }
            }
            else
            {
                this.expressions = new List<Expression>();
            }
        }
    }
}
