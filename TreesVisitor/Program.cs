// See https://aka.ms/new-console-template for more information


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
    new AST.BinaryOperation.Add(
        new AST.Value(5),
        new AST.Value(10));
Console.WriteLine(AST.ExpressionStringifier.Stringify(expressionTree));
Console.WriteLine(expressionTree.Evaluate().value);


// 3 + 5 + 7
expressionTree =
    new AST.BinaryOperation.Add(
        new AST.Value(3),
        new AST.BinaryOperation.Add(
            new AST.Value(5),
            new AST.Value(7)));
Console.WriteLine(AST.ExpressionStringifier.Stringify(expressionTree));
Console.WriteLine(expressionTree.Evaluate().value);


// 3 + 5 + 7 + 11
expressionTree =
    new AST.BinaryOperation.Add(
        new AST.Value(3),
        new AST.BinaryOperation.Add(
            new AST.Value(5),
            new AST.BinaryOperation.Add(
                new AST.Value(7),
                new AST.Value(11))));

Console.WriteLine(AST.ExpressionStringifier.Stringify(expressionTree));
Console.WriteLine(expressionTree.Evaluate().value);

// 3 + 5 * 7 + 11
expressionTree =
    new AST.BinaryOperation.Add(
        new AST.Value(99),
        new AST.BinaryOperation.Add(
            new AST.BinaryOperation.Mul(
                new AST.Value(5),
                new AST.Value(7)
            ),
            new AST.Value(11)));

Console.WriteLine(AST.ExpressionStringifier.Stringify(expressionTree));
Console.WriteLine(expressionTree.Evaluate().value);

// 3 + 5 * x + 11
expressionTree =
    new AST.BinaryOperation.Add(
        new AST.Value(3),
        new AST.BinaryOperation.Add(
            new AST.BinaryOperation.Mul(
                new AST.Value(5),
                new AST.Variable("x")
            ),
            new AST.Value(11)));
Console.WriteLine(AST.ExpressionStringifier.Stringify(expressionTree));


Dictionary<string, int> variableDefinitions = new Dictionary<string, int>() { { "x", 5 } };
Console.WriteLine(expressionTree.Evaluate( variableDefinitions ).value);

variableDefinitions = new Dictionary<string, int>() { { "x", 10 } };
Console.WriteLine(expressionTree.Evaluate(variableDefinitions).value);

variableDefinitions = new Dictionary<string, int>() { { "x", 20 } };
Console.WriteLine(expressionTree.Evaluate(variableDefinitions).value);

Console.WriteLine();



var tokens = Tokens.Tokenizer.Tokenize("300 + 512 * my_value_to_be + 1521");
foreach (var token in tokens)
{
    Console.WriteLine(token);
}

Console.WriteLine();

tokens = Tokens.Tokenizer.Tokenize("-300.0 + 512.0 * f + 1521.0");
foreach (var token in tokens)
{
    Console.WriteLine(token);
}

Console.WriteLine();


var tokenGrouper = new Tokens.TokenGrouper(tokens);
while (tokenGrouper.AttemptGroupingPass()) ;
tokens = tokenGrouper.tokens.ToArray();

foreach (var token in tokens)
{
    Console.WriteLine(token);
}

// The last '-Number' grouping doesn't work when there's whitespace
tokens = Tokens.Tokenizer.Tokenize("-300.0 - 512.0 * f + - 1521.0");
foreach (var token in tokens)
{
    Console.WriteLine(token);
}

Console.WriteLine();


tokenGrouper = new Tokens.TokenGrouper(tokens);
while (tokenGrouper.AttemptGroupingPass()) ;
tokens = tokenGrouper.tokens.ToArray();

foreach (var token in tokens)
{
    Console.WriteLine(token);
}