using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LinkedLists.Tests;

[TestFixture]
public class ReorderListShould
{
    [Test, Timeout(1000)]
    [TestCase("", "[]", TestName = "null")]
    [TestCase("[]", "[]", TestName = "empty")]
    [TestCase("[1]", "[1]", TestName = "single element list")]
    [TestCase("[1,2]", "[1,2]", TestName = "two elements list")]
    [TestCase("[1,2,3]", "[1,3,2]", TestName = "three elements list")]
    [TestCase("[1,2,3,4]", "[1,4,2,3]", TestName = "four elements list")]
    [TestCase("[1,2,3,4,5]", "[1,5,2,4,3]", TestName = "five elements list")]
    public void Process(string input, string expected)
    {
        var items = JsonConvert.DeserializeObject<int[]>(input);
        var list = ListNode.Create(items);
        var solution = new Solution();

        solution.ReorderList(list);
        var resultArray = ListNode.ToArray(list);
        var resultString = JsonConvert.SerializeObject(resultArray);
        resultString.Should().Be(expected);
    }
}