using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LinkedLists.Tests;

[TestFixture]
public class ReverseListShould
{
    [Test, Timeout(1000)]
    [TestCase("", "[]", TestName = "null")]
    [TestCase("[]", "[]", TestName = "empty")]
    [TestCase("[1]", "[1]", TestName = "single node")]
    [TestCase("[1,2]", "[2,1]", TestName = "two nodes")]
    [TestCase("[1,2,3,4,5]", "[5,4,3,2,1]", TestName = "five nodes")]
    public void Process(string input, string expected)
    {
        var items = JsonConvert.DeserializeObject<int[]>(input);
        var list = ListNode.Create(items);
        var solution = new Solution();

        var result = solution.ReverseList(list);
        var resultArray = ListNode.ToArray(result);
        var resultString = JsonConvert.SerializeObject(resultArray);
        resultString.Should().Be(expected);
    }
}