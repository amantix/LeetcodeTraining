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
        Bracket,
        SignInversion
    };
    
    private struct Token
    {
        public string Value { get; }
        public TokenType Type { get; }
        public Token(string value, TokenType tokenType)
        {
            Value = value;
            Type = tokenType;
        }
        public Token(char value, TokenType tokenType) : this(value.ToString(), TokenType.Operator)
        {
        }
        
        public Token(StringBuilder value) : this(value.ToString(), TokenType.Number)
        {
        }

        public void Deconstruct(out string value, out TokenType tokenType)
        {
            value = Value;
            tokenType = Type;
        }
    }

    private static List<Token> Parse(string input)
    {
        var number = new StringBuilder();
        var result = new List<Token>();

        foreach (var symbol in input)
        {
            switch (symbol)
            {
                case var @operator when "+-*/".Contains(@operator):
                    if (number.Length > 0)
                    {
                        result.Add(new Token(number));
                        number.Clear();
                    }
                    else if (result.Count == 0 || result[^1].Value == "(" || result[^1].Type == TokenType.Operator)
                    {
                        result.Add(new Token("-", TokenType.SignInversion));
                        break;
                    }

                    result.Add(new Token(@operator, TokenType.Operator));
                    break;
                case var digit when char.IsDigit(digit):
                    number.Append(digit);
                    break;
                case var bracket when "()".Contains(bracket):
                    if (number.Length > 0)
                    {
                        result.Add(new Token(number));
                        number.Clear();
                    }

                    result.Add(new Token(bracket.ToString(), TokenType.Bracket));
                    break;
                default:
                    if (number.Length > 0)
                    {
                        result.Add(new Token(number));
                        number.Clear();
                    }

                    break;
            }
        }

        if (number.Length > 0)
        {
            result.Add(new Token(number));
        }

        return result;
    }

    private static List<Token> ToPolishNotation(IEnumerable<Token> tokens)
    {
        var result = new List<Token>();
        var stack = new Stack<Token>();
        foreach (var currentToken in tokens)
        {
            switch (currentToken)
            {
                case (var currentOperator, TokenType.Operator):
                    while (stack.Count > 0 && stack.Peek().Value != "(")
                    {
                        var previousStackItem = stack.Peek();

                        if (new[] {"+", "-"}.Contains(currentOperator)
                            || new[] {currentOperator, previousStackItem.Value}.All(o => new[] {"*", "/"}.Contains(o)))
                        {
                            result.Add(stack.Pop());
                            continue;
                        }

                        break;
                    }

                    stack.Push(new (currentOperator, TokenType.Operator));
                    break;
                case (")", TokenType.Bracket):
                    while (stack.Peek().Value != "(")
                    {
                        result.Add(stack.Pop());
                    }

                    stack.Pop();

                    break;
                case ("(", TokenType.Bracket):
                case (_, TokenType.SignInversion):
                    stack.Push(currentToken);
                    break;
                default:
                    result.Add(currentToken);
                    break;
            }
        }

        while (stack.Count > 0)
        {
            result.Add(stack.Pop());
        }

        return result;
    }

    private static int Evaluate(IEnumerable<Token> polishNotation)
    {
        var stack = new Stack<int>();
        foreach (var token in polishNotation)
        {
            switch (token)
            {
                case {Type: TokenType.Number} number:
                    stack.Push(int.Parse(number.Value));
                    break;
                case {Type: TokenType.SignInversion}:
                    var numberToInverse = stack.Pop();
                    stack.Push(-numberToInverse);
                    break;
                case {Type: TokenType.Operator}:
                    var right = stack.Pop();
                    var left = stack.Pop();
                    stack.Push(Operators[token.Value](left, right));
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