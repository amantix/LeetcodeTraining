using System.Text;

namespace BasicCalculator;

public class Solution
{
    public int Calculate(string s)
    {
        var tokens = Parse(s);
        var polish = ToPolishNotation(tokens);
        return Evaluate(polish);
    }

    private enum TokenType
    {
        Number,
        Operator,
        Bracket
    };

    private static (string token, TokenType type)[] Parse(string input)
    {
        var number = new StringBuilder();
        var result = new List<(string token, TokenType type)>();

        foreach (var symbol in input)
        {
            switch (symbol)
            {
                case var c when "+-*/".Contains(c):
                    if (number.Length > 0)
                    {
                        result.Add((number.ToString(), TokenType.Number));
                        number.Clear();
                    }

                    result.Add((c.ToString(), TokenType.Operator));
                    break;
                case var c when char.IsDigit(c):
                    number.Append(c);
                    break;
                case var c when "()".Contains(c):
                    if (number.Length > 0)
                    {
                        result.Add((number.ToString(), TokenType.Number));
                        number.Clear();
                    }

                    result.Add((c.ToString(), TokenType.Bracket));
                    break;
                default:
                    if (number.Length > 0)
                    {
                        result.Add((number.ToString(), TokenType.Number));
                        number.Clear();
                    }

                    break;
            }
        }

        if (number.Length > 0)
        {
            result.Add((number.ToString(), TokenType.Number));
        }

        return result.ToArray();
    }

    private static (string token, TokenType type)[] ToPolishNotation((string token, TokenType type)[] tokens)
    {
        var result = new List<(string token, TokenType type)>(tokens.Length);
        var stack = new Stack<string>();
        foreach (var currentToken in tokens)
        {
            switch (currentToken)
            {
                case (var currentOperator, TokenType.Operator):
                    while (stack.Count > 0 && stack.Peek()!="(")
                    {
                        var previousStackItem = stack.Peek();

                        if (new[] {"+", "-"}.Contains(currentOperator)
                            || new[] {currentOperator, previousStackItem}.All(o => new[] {"*", "/"}.Contains(o)))
                        {
                            result.Add((stack.Pop(), TokenType.Operator));
                            continue;
                        }

                        break;
                    }

                    stack.Push(currentOperator);
                    break;
                case (")", TokenType.Bracket):
                    while (stack.Peek() != "(")
                    {
                        result.Add((stack.Pop(), TokenType.Operator));
                    }

                    stack.Pop();

                    break;
                case ("(", TokenType.Bracket):
                    stack.Push("(");
                    break;
                default:
                    result.Add(currentToken);
                    break;
            }
        }

        while (stack.Count > 0)
        {
            result.Add((stack.Pop(), TokenType.Operator));
        }

        return result.ToArray();
    }

    private static int Evaluate((string token, TokenType type)[] polishNotation)
    {
        var stack = new Stack<int>();
        foreach (var token in polishNotation)
        {
            switch (token)
            {
                case (var number, TokenType.Number):
                    stack.Push(int.Parse(number));
                    break;
                case (var currentOperator, TokenType.Operator):
                    var right = stack.Pop();
                    var left = stack.Pop();
                    stack.Push(Operators[currentOperator](left, right));
                    break;
            }
        }

        return stack.Count == 1 ? stack.Peek() : 0;
    }

    private static readonly Dictionary<string, Func<int, int, int>> Operators = new()
    {
        ["+"] = (left, right) => left + right,
        ["-"] = (left, right) => left - right,
        ["*"] = (left, right) => left * right,
        ["/"] = (left, right) => left / right,
    };
}