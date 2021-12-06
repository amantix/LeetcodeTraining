using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LinkedLists.Tests;

[TestFixture]
public class PartitionShould
{
    [Test, Timeout(1000)]
    [TestCase("[1,4,3,2,5,2]", 3, "[1,2,2,4,3,5]", TestName = "five item list with item repetition")]
    [TestCase("[2,1]", 2, "[1,2]", TestName = "two items list")]
    [TestCase("", 1, "[]", TestName = "null")]
    [TestCase("[]", 0, "[]", TestName = "empty list")]
    public void Process(string input, int x, string expected)
    {
        var items = JsonConvert.DeserializeObject<int[]>(input);
        var list = ListNode.Create(items);
        var solution = new Solution();

        var result = solution.Partition(list, x);
        var resultArray = ListNode.ToArray(result);
        var resultString = JsonConvert.SerializeObject(resultArray);
        resultString.Should().Be(expected);
    }
}