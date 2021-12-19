using System.Text;

namespace DecodeString;

public class Solution
{
    public string DecodeString(string s)
    {
        return Parse(s).str;
    }

    private static (string str, int length) Parse(string s)
    {
        var parsedSymbolsCount = 0;
        var result = new StringBuilder();
        var parseNext = true;
        while (s.Length > 0 && parseNext)
        {
            switch (s[0])
            {
                case ']':
                    parseNext = false;
                    break;
                case var _ when char.IsLetter(s[0]):
                    var letters = new string(s.TakeWhile(char.IsLetter).ToArray());
                    parsedSymbolsCount += letters.Length;
                    s = s[letters.Length..];
                    result.Append(letters);
                    break;
                case var _ when char.IsDigit(s[0]):
                    var digits = new string(s.TakeWhile(char.IsDigit).ToArray());
                    if (digits.Length > 0)
                    {
                        var number = int.Parse(digits);
                        s = s[(digits.Length + 1)..];
                        parsedSymbolsCount += digits.Length + 1;

                        var (str, length) = Parse(s);
                        s = s[(length + 1)..];
                        parsedSymbolsCount += length + 1;
                        var unwrappedString = Enumerable
                            .Repeat(str, number)
                            .Aggregate("", (acc, str) => string.Join("", acc, str));
                        result.Append(unwrappedString);
                    }
                    break;
            }
        }

        return (result.ToString(), parsedSymbolsCount);
    }
}