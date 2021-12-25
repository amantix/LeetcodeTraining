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
    [TestCase("-5+2",-3)]
    [TestCase("5+(-5)",0)]
    [TestCase(" 6-4 / 2 ",4)]
    [TestCase("2*(5+5*2)/3+(6/2+8)",21)]
    [TestCase("(2+6* 3+5- (3*14/7+2)*5)+3",-12)]
    public void Calculate(string input, int expectedResult)
    {
        var solution = new Solution();
        solution.Calculate(input).Should().Be(expectedResult);
    }
}