using FluentAssertions;
using NUnit.Framework;

namespace BasicCalculator.Tests;

[TestFixture]
public class BasicCalculatorShould
{
    [Test, Timeout(1000)]
    [TestCase("3+2*2",7)]
    [TestCase("3/2 ",1)]
    [TestCase(" 3+5 / 2 ",5)]
    [TestCase("(1+(4+5+2)-3)+(6+8)",23)]
    public void Calculate(string input, int expectedResult)
    {
        var solution = new Solution();
        solution.Calculate(input).Should().Be(expectedResult);
    }
}