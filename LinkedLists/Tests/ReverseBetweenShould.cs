using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LinkedLists.Tests;

[TestFixture]
public class ReverseBetweenShould
{
    [Test, Timeout(1000)]
    [TestCase("[1]",1,1,"[1]", TestName = "single item list")]
    [TestCase("[1,2,3,4,5]",2,4,"[1,4,3,2,5]", TestName = "five elements list from 2 to 4")]
    [TestCase("[1,2,3,4,5]",1,5,"[5,4,3,2,1]", TestName = "five elements list from 1 to 5")]
    [TestCase("[1,2,3,4,5]",1,3,"[3,2,1,4,5]", TestName = "five elements list from 1 to 3")]
    [TestCase("[1,2,3,4,5]",3,5,"[1,2,5,4,3]", TestName = "five elements list from 3 to 5")]
    [TestCase("[1,2,3,4,5]",1,1,"[1,2,3,4,5]", TestName = "five elements list from 1 to 1")]
    [TestCase("[1,2,3,4,5]",3,3,"[1,2,3,4,5]", TestName = "five elements list from 3 to 3")]
    [TestCase("[1,2,3,4,5]",5,5,"[1,2,3,4,5]", TestName = "five elements list from 5 to 5")]
    public void Reverse(string input, int left, int right, string expected)
    {
        var items = JsonConvert.DeserializeObject<int[]>(input);
        var list = ListNode.Create(items);
        var solution = new Solution();

        var result = solution.ReverseBetween(list, left, right);
        var resultArray = ListNode.ToArray(result);
        var resultString = JsonConvert.SerializeObject(resultArray);
        resultString.Should().Be(expected);
    }
}