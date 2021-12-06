using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LinkedLists.Tests;

[TestFixture]
public class SwapPairsShould
{
    [Test, Timeout(1000)]
    [TestCase("[1,2,3,4]", "[2,1,4,3]", TestName = "two pairs")]
    [TestCase("[1,2,3]", "[2,1,3]", TestName = "one pair with one more item")]
    [TestCase("[1]", "[1]", TestName = "single item list")]
    [TestCase("", "[]", TestName = "null")]
    [TestCase("[]", "[]", TestName = "empty list")]
    public void Process(string input, string expected)
    {
        var items = JsonConvert.DeserializeObject<int[]>(input);
        var list = ListNode.Create(items);
        var solution = new Solution();

        var result = solution.SwapPairs(list);
        var resultArray = ListNode.ToArray(result);
        var resultString = JsonConvert.SerializeObject(resultArray);
        resultString.Should().Be(expected);
    }
}