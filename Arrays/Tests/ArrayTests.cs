using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Arrays.Tests;

[TestFixture]
public class RemoveElementTests
{
    [Test, Timeout(1000)]
    [TestCase("[3,2,2,3]", 3, 2)]
    [TestCase("[0,1,2,2,3,0,4,2]", 2, 5)]
    public void CheckRemoveElement(string input, int val, int expectedResult)
    {
        var array = JsonConvert.DeserializeObject<int[]>(input);

        var expectedValues = array.Where(item=>item!=val).OrderBy(item=>item).ToArray();
        var solution = new Solution();
        var lengthAfterRemove = solution.RemoveElement(array,val);
        lengthAfterRemove.Should().Be(expectedValues.Length);
        Array.Sort(array,0,lengthAfterRemove);
        array.Take(expectedResult).Should().BeEquivalentTo(expectedValues);
    }
}