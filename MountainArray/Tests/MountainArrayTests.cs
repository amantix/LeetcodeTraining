using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace MountainArray;

[TestFixture]
public class MountainArrayTests
{
    [Test, Timeout(1000)]
    [TestCase("[1, 2, 3, 4, 5, 3, 1]",3,2)]
    [TestCase("[1, 2, 3, 4, 3, 1]",3,2)]
    [TestCase("[1, 2, 3, 4, 3, 1]",2,1)]
    [TestCase("[1, 2, 3, 4, 3, 1]",4,3)]
    [TestCase("[1, 2, 3, 4, 5, 3, 1]",1,0)]
    [TestCase("[1, 2, 3, 4, 5, 3, 1]",4,3)]
    [TestCase("[1, 2, 3, 4, 5, 3, 1]",5,4)]
    [TestCase("[1, 2, 3, 4, 5, 3, 1]",6,-1)]
    public void Test(string input, int target, int expected)
    {
        var array = JsonConvert.DeserializeObject<int[]>(input);
        var mountainArray = new MountainArray(array);
        var solution = new Solution();
        solution.FindInMountainArray(target, mountainArray).Should().Be(expected);
        mountainArray.QueryCount.Should().BeLessThan(100);
    }
    
    [Test, Timeout(1000)]
    [TestCase(10000, false)]
    [TestCase(9000, true)]
    [TestCase(1000, true)]
    public void TestLarge(int target, bool contains)
    {
        var array = Enumerable.Range(0, 9000).Concat(Enumerable.Range(0, 1000).Select(x => 9001 - x)).ToArray();
        var mountainArray = new MountainArray(array);
        var solution = new Solution();
        var result = solution.FindInMountainArray(target, mountainArray);
        if (contains)
        {
            result.Should().BeGreaterOrEqualTo(0);
        }
        else
        {
            result.Should().Be(-1);
        }
        mountainArray.QueryCount.Should().BeLessThan(100);
    }
}