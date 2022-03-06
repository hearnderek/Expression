using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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

    /// <summary>
    /// Since we're not using the visitor pattern we need to do some refection and cast
    /// </summary>
    public static class StaticExpressionStringifier
    {

        public static string Stringify(BinaryOperation.Add op) => Stringify(op.left) + " + " + Stringify(op.right);

        public static string Stringify(BinaryOperation.Mul op) => Stringify(op.left) + " * " + Stringify(op.right);

        public static string Stringify(BinaryOperation.Sub op) => Stringify(op.left) + " - " + Stringify(op.right);

        public static string Stringify(BinaryOperation.Div op) => Stringify(op.left) + " / " + Stringify(op.right);

        public static string Stringify(BinaryOperation.Exp op) => Stringify(op.left) + " ^ " + Stringify(op.right);

        public static string Stringify(Value value) => value.value.ToString();

        public static string Stringify(Variable variable) => variable.identifier;

        public static string Stringify(Expression expression)
        {

            switch (expression.GetType().Name)
            {
                case "Add":
                    return Stringify((BinaryOperation.Add)expression);
                case "Mul":
                    return Stringify((BinaryOperation.Mul)expression);
                case "Sub":
                    return Stringify((BinaryOperation.Sub)expression);
                case "Div":
                    return Stringify((BinaryOperation.Div)expression);
                case "Exp":
                    return Stringify((BinaryOperation.Exp)expression);
                case "Value":
                    return Stringify((Value)expression);
                case "Variable":
                    return Stringify((Variable)expression);
                default:
                    Console.WriteLine(expression.GetType().Name);
                    throw new NotImplementedException();
            }
        }
    }

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

    public class UndefinedVariableException : Exception
    {
        public UndefinedVariableException(Variable variable) : base($"{variable.identifier} was not defined")
        {

        }
    }
}
