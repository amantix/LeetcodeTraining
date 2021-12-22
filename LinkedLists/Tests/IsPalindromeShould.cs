using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LinkedLists.Tests;

[TestFixture]
public class IsPalindromeShould
{
    [Test, Timeout(1000)]
    [TestCase("", false, TestName = "null")]
    [TestCase("[]", false, TestName = "empty list")]
    [TestCase("[1]", true, TestName = "single item list")]
    [TestCase("[1,2]", false, TestName = "two items non palindrome list")]
    [TestCase("[1,1]", true, TestName = "two items palindrome list")]
    [TestCase("[1,2,1]", true, TestName = "three items palindrome list")]
    [TestCase("[1,2,3]", false, TestName = "three items non palindrome list")]
    [TestCase("[1,2,2,1]", true, TestName = "four items palindrome list")]
    [TestCase("[1,2,2,3]", false, TestName = "four items non palindrome list")]
    [TestCase("[1,2,3,2,1]", true, TestName = "five items palindrome list")]
    public void Check(string input, bool expected)
    {
        var items = JsonConvert.DeserializeObject<int[]>(input);
        var list = ListNode.Create(items);
        var solution = new Solution();

        var result = solution.IsPalindrome(list);
        result.Should().Be(expected);
    }
}