using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LinkedLists.Tests;

[TestFixture]
public class GetDecimalValueShould
{
    [Test, Timeout(1000)]
    [TestCase("[0]", 0, TestName = "0")]
    [TestCase("[0,0]", 0, TestName = "00")]
    [TestCase("[1]", 1, TestName = "1")]
    [TestCase("[1,0,0,1,0,0,1,1,1,0,0,0,0,0,0]", 18880, TestName = "100100111000000")]
    public void Process(string input, int expected)
    {
        var items = JsonConvert.DeserializeObject<int[]>(input);
        var list = ListNode.Create(items);
        var solution = new Solution();

        var result = solution.GetDecimalValue(list);
        result.Should().Be(expected);
    }
}