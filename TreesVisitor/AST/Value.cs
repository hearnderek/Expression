/// <summary>
/// TODO move this to it's own folder, and move inner classes to their own files
/// </summary>
namespace AST
{
    public class Value : Expression
    {
        public int value;
        public Value(int value, Expression parent = null)
            : base(new Expression[] { }, parent)
        {
            this.value = value;
        }

        public override void Accept(IExpressionVisitor visitor) => visitor.Visit(this);


        public override Value Evaluate(Dictionary<string, int> variableDefinitions = null)
        {
            return this;
        }
    }
}
