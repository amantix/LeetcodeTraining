﻿using System.Text;

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
                    else if (number.Length == 0
                             && (result.Count == 0 || result[^1].token == "(" || result[^1].type == TokenType.Operator))
                    {
                        result.Add(("-", TokenType.SignInversion));
                        break;
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
        var stack = new Stack<(string token, TokenType type)>();
        foreach (var currentToken in tokens)
        {
            switch (currentToken)
            {
                case (var currentOperator, TokenType.Operator):
                    while (stack.Count > 0 && stack.Peek().token != "(")
                    {
                        var previousStackItem = stack.Peek();

                        if (new[] {"+", "-"}.Contains(currentOperator)
                            || new[] {currentOperator, previousStackItem.token}.All(o => new[] {"*", "/"}.Contains(o)))
                        {
                            result.Add(stack.Pop());
                            continue;
                        }

                        break;
                    }

                    stack.Push((currentOperator, TokenType.Operator));
                    break;
                case (")", TokenType.Bracket):
                    while (stack.Peek().token != "(")
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
                case (_, TokenType.SignInversion):
                    var numberToInverse = stack.Pop();
                    stack.Push(-numberToInverse);
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