using FluentAssertions;
using NUnit.Framework;

namespace DecodeString.Tests;

[TestFixture]
public class DecodeStringTests
{
    [Test, Timeout(1000)]
    [TestCase("3[a]2[bc]","aaabcbc")]
    [TestCase("3[a2[c]]","accaccacc")]
    [TestCase("2[abc]3[cd]ef","abcabccdcdcdef")]
    [TestCase("abc3[cd]xyz","abccdcdcdxyz")]
    public void CheckDecode(string input, string expectedResult)
    {
        var solution = new Solution();
        solution.DecodeString(input).Should().Be(expectedResult);
    }
}