// See https://aka.ms/new-console-template for more information

using ExpressionTree;

Console.WriteLine("Hello, World!");

//var calc = new CalculationVisitor();
//var plus = new AddBinaryNumericNode(new NumericNode(5), new NumericNode(10));
//plus.Accept(calc);
//Console.WriteLine("Expecting 15, Got " + calc.result);

//calc = new CalculationVisitor();
//var mul = new MulBinaryNumericNode(new NumericNode(5), new NumericNode(10));
//mul.Accept(calc);
//Console.WriteLine("Expecting 50, Got " + calc.result);


/*
 * 5 + 6 + 7
 * 
 * +
 * 5 +
 *   6 7
 */

//calc = new CalculationVisitor();

// 5 + 10
var expressionTree = 
    new BinaryOperation.Add(
        new Value(5),
        new Value(10));
Console.WriteLine(ExpressionStringifier.Stringify(expressionTree));
Console.WriteLine(expressionTree.Evaluate().value);


// 3 + 5 + 7
expressionTree =
    new BinaryOperation.Add(
        new Value(3),
        new BinaryOperation.Add(
            new Value(5),
            new Value(7)));
Console.WriteLine(ExpressionStringifier.Stringify(expressionTree));
Console.WriteLine(expressionTree.Evaluate().value);


// 3 + 5 + 7 + 11
expressionTree =
    new BinaryOperation.Add(
        new Value(3),
        new BinaryOperation.Add(
            new Value(5),
            new BinaryOperation.Add(
                new Value(7),
                new Value(11))));

Console.WriteLine(ExpressionStringifier.Stringify(expressionTree));
Console.WriteLine(expressionTree.Evaluate().value);

// 3 + 5 * 7 + 11
expressionTree =
    new BinaryOperation.Add(
        new Value(99),
        new BinaryOperation.Add(
            new BinaryOperation.Mul(
                new Value(5),
                new Value(7)
            ),
            new Value(11)));

Console.WriteLine(ExpressionStringifier.Stringify(expressionTree));
Console.WriteLine(expressionTree.Evaluate().value);

// 3 + 5 * x + 11
expressionTree =
    new BinaryOperation.Add(
        new Value(3),
        new BinaryOperation.Add(
            new BinaryOperation.Mul(
                new Value(5),
                new Variable("x")
            ),
            new Value(11)));
Console.WriteLine(ExpressionStringifier.Stringify(expressionTree));


Dictionary<string, int> variableDefinitions = new Dictionary<string, int>() { { "x", 5 } };
Console.WriteLine(expressionTree.Evaluate( variableDefinitions ).value);

variableDefinitions = new Dictionary<string, int>() { { "x", 10 } };
Console.WriteLine(expressionTree.Evaluate(variableDefinitions).value);

variableDefinitions = new Dictionary<string, int>() { { "x", 20 } };
Console.WriteLine(expressionTree.Evaluate(variableDefinitions).value);
