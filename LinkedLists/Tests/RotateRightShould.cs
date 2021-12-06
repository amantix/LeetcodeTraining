using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LinkedLists.Tests;

[TestFixture]
public class RotateRightShould
{
    [Test, Timeout(1000)]
    [TestCase("",1,"[]", TestName = "null")]
    [TestCase("[]",1,"[]", TestName = "empty list")]
    [TestCase("[1]",0,"[1]", TestName = "single item list to 0 positions")]
    [TestCase("[1]",1024,"[1]", TestName = "single item list to any number of positions")]
    [TestCase("[1,2]",1,"[2,1]", TestName = "two elements by two positions")]
    [TestCase("[1,2,3,4,5]",2,"[4,5,1,2,3]", TestName = "five elements by two positions")]
    public void Rotate(string input, int k, string expected)
    {
        var items = JsonConvert.DeserializeObject<int[]>(input);
        var list = ListNode.Create(items);
        var solution = new Solution();

        var result = solution.RotateRight(list, k);
        var resultArray = ListNode.ToArray(result);
        var resultString = JsonConvert.SerializeObject(resultArray);
        resultString.Should().Be(expected);
    }
}