using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace JumpGame3.Tests;

[TestFixture]
public class JumpGame3TestsShould
{
    [Test, Timeout(1000)]
    [TestCase("[4,2,3,0,3,1,2]",5,true)]
    [TestCase("[0,1]",1,true)]
    [TestCase("[4,2,3,0,3,1,2]",0,true)]
    [TestCase("[3,0,2,1,2]",2,false)]
    public void CheckCanReach(string arrString, int start, bool expectedResult)
    {
        var arr = JsonConvert.DeserializeObject<int[]>(arrString);
        var solution = new Solution();
        solution.CanReach(arr, start).Should().Be(expectedResult);
    }
}