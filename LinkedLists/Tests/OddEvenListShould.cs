using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LinkedLists.Tests;

[TestFixture]
public class OddEvenListShould
{
    [TestCase("","[]", TestName = "null")]
    [TestCase("[]","[]", TestName = "Empty list")]
    [TestCase("[1]","[1]", TestName = "Single item")]
    [TestCase("[1,2]","[1,2]", TestName = "Two items")]
    [TestCase("[1,2,3,4,5]","[1,3,5,2,4]", TestName = "Five ordered items")]
    [TestCase("[2,1,3,5,6,4,7]","[2,3,6,7,1,5,4]", TestName = "Seven random items")]
    public void Process(string input, string output)
    {
        var items = JsonConvert.DeserializeObject<int[]>(input);
        var list = ListNode.Create(items);
        var solution = new Solution();

        var result = solution.OddEvenList(list);
        var resultArray = ListNode.ToArray(result);
        var resultString = JsonConvert.SerializeObject(resultArray);
        resultString.Should().Be(output);
    }
}