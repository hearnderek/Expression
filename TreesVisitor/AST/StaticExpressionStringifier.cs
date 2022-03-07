/// <summary>
/// TODO move this to it's own folder, and move inner classes to their own files
/// </summary>
namespace AST
{
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
}
