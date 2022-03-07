/// <summary>
/// TODO move this to it's own folder, and move inner classes to their own files
/// </summary>
namespace AST
{
    public abstract class BinaryOperation : Expression
    {
        public Expression left;
        public Expression right;

        public BinaryOperation(Expression left, Expression right, Expression parent = null) : 
            base(new[] { left, right }, parent)
        {
            this.left = left;
            this.right = right;
        }

        public class Add : BinaryOperation
        {
            public Add(Expression left, Expression right, Expression parent = null) : base(left, right, parent)
            {
            }

            public override void Accept(IExpressionVisitor visitor) => visitor.Visit(this);


            public override Value Evaluate(Dictionary<string, int> variableDefinitions = null)
            {
                return new Value(left.Evaluate(variableDefinitions).value + right.Evaluate(variableDefinitions).value);
            }
        }

        public class Mul : BinaryOperation
        {
            public Mul(Expression left, Expression right, Expression parent = null) : base(left, right, parent)
            {
            }

            public override void Accept(IExpressionVisitor visitor) => visitor.Visit(this);


            public override Value Evaluate(Dictionary<string, int> variableDefinitions = null)
            {
                return new Value(left.Evaluate(variableDefinitions).value * right.Evaluate(variableDefinitions).value);
            }
        }

        public class Sub : BinaryOperation
        {
            public Sub(Expression left, Expression right, Expression parent = null) : base(left, right, parent)
            {
            }

            public override void Accept(IExpressionVisitor visitor) => visitor.Visit(this);


            public override Value Evaluate(Dictionary<string, int> variableDefinitions = null)
            {
                return new Value(left.Evaluate(variableDefinitions).value - right.Evaluate(variableDefinitions).value);
            }
        }

        public class Div : BinaryOperation
        {
            public Div(Expression left, Expression right, Expression parent = null) : base(left, right, parent)
            {
            }

            public override void Accept(IExpressionVisitor visitor) => visitor.Visit(this);


            public override Value Evaluate(Dictionary<string, int> variableDefinitions = null)
            {
                return new Value(left.Evaluate(variableDefinitions).value - right.Evaluate(variableDefinitions).value);
            }
        }

        public class Exp : BinaryOperation
        {
            public Exp(Expression left, Expression right, Expression parent = null) : base(left, right, parent)
            {
            }

            public override void Accept(IExpressionVisitor visitor) => visitor.Visit(this);


            public override Value Evaluate(Dictionary<string, int> variableDefinitions = null)
            {
                return new Value(left.Evaluate(variableDefinitions).value ^ right.Evaluate(variableDefinitions).value);
            }
        }
    }
}
