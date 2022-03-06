using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionTree
{

    public abstract class Expression
    {
        public Expression parent;
        public List<Expression> expressions = new List<Expression>();
        public abstract Value Evaluate(Dictionary<string, int> variableDefinitions = null);

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

            public override Value Evaluate(Dictionary<string, int> variableDefinitions = null)
            {
                return new Value(left.Evaluate(variableDefinitions).value ^ right.Evaluate(variableDefinitions).value);
            }
        }
    }

    public class Value : Expression
    {
        public int value;
        public Value(int value, Expression parent = null)
            : base(new Expression[] { }, parent)
        {
            this.value = value;
        }

        public override Value Evaluate(Dictionary<string, int> variableDefinitions = null)
        {
            return this;
        }
    }

    public class Variable : Expression
    {
        public string identifier;

        public Variable(string identifier, Expression parent = null) :
            base(new Expression[0], parent)
        {
            this.identifier = identifier;
        }


        public override Value Evaluate(Dictionary<string, int> variableDefinitions = null)
        {
            if(variableDefinitions?.ContainsKey(identifier) ?? false)
            {
                return new Value(variableDefinitions[identifier]);
            }

            throw new UndefinedVariableException(this);
        }
    }

    public class UndefinedVariableException : Exception
    {
        public UndefinedVariableException(Variable variable) : base($"{variable.identifier} was not defined")
        {

        }
    }
}
