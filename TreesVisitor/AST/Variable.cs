/// <summary>
/// TODO move this to it's own folder, and move inner classes to their own files
/// </summary>
namespace AST
{
    public class Variable : Expression
    {
        public string identifier;

        public Variable(string identifier, Expression parent = null) :
            base(new Expression[0], parent)
        {
            this.identifier = identifier;
        }

        public override void Accept(IExpressionVisitor visitor) => visitor.Visit(this);


        public override Value Evaluate(Dictionary<string, int> variableDefinitions = null)
        {
            if(variableDefinitions?.ContainsKey(identifier) ?? false)
            {
                return new Value(variableDefinitions[identifier]);
            }

            throw new UndefinedVariableException(this);
        }
    }
}
