using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LinkedLists.Tests;


[TestFixture]
public class DetectCycleShould
{
    [Test, Timeout(1000)]
    [TestCase("", -1, TestName = "null")]
    [TestCase("[3,2,0,-4]", 1, TestName = "four nodes with cycle to position 1")]
    [TestCase("[3,2,0,-4]", 0,TestName = "four nodes with cycle to position 0")]
    [TestCase("[1,2]", 0,TestName = "two nodes cycled")]
    [TestCase("[1]", -1, TestName = "single node, no cycle")]
    [TestCase("[-1,-7,7,-4,19,6,-9,-5,-2,-5]", 6,TestName = "ten nodes with cycle to 6 position")]
    public void Process(string input, int cycleBegin)
    {
        var items = JsonConvert.DeserializeObject<int[]>(input);
        var list = ListNode.Create(items);
        var tail = list;
        var i = 0;
        ListNode? begin = null;
        while (tail?.next != null)
        {
            if (i == cycleBegin)
            {
                begin = tail;
            }
            tail = tail.next;
            i++;
        }

        if (tail != null)
        {
            tail.next = begin;
        }
        var solution = new Solution();

        var result = solution.DetectCycle(list);
        if (cycleBegin < 0)
        {
            result.Should().BeNull();
        }
        else
        {
            if (items!=null && result != null)
            {
                Array.IndexOf(items, result.val).Should().Be(cycleBegin);
            }
        }
    }
}