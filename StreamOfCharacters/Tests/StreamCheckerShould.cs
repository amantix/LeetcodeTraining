using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace StreamOfCharacters.Tests;

[TestFixture]
public class StreamCheckerShould
{
    [Test, Timeout(1000)]
    [TestCase("[\"StreamChecker\", \"query\", \"query\", \"query\", \"query\", \"query\", \"query\", \"query\", \"query\", \"query\", \"query\", \"query\", \"query\"]",
    "[[[\"cd\", \"f\", \"kl\"]], [\"a\"], [\"b\"], [\"c\"], [\"d\"], [\"e\"], [\"f\"], [\"g\"], [\"h\"], [\"i\"], [\"j\"], [\"k\"], [\"l\"]]", 
    "[null, false, false, false, true, false, true, false, false, false, false, false, true]")]
    [TestCase("[\"StreamChecker\",\"query\",\"query\",\"query\",\"query\",\"query\",\"query\",\"query\",\"query\",\"query\",\"query\",\"query\",\"query\",\"query\",\"query\",\"query\",\"query\",\"query\",\"query\",\"query\",\"query\",\"query\",\"query\",\"query\",\"query\",\"query\",\"query\",\"query\",\"query\",\"query\",\"query\"]",
        "[[[\"ab\",\"ba\",\"aaab\",\"abab\",\"baa\"]],[\"a\"],[\"a\"],[\"a\"],[\"a\"],[\"a\"],[\"b\"],[\"a\"],[\"b\"],[\"a\"],[\"b\"],[\"b\"],[\"b\"],[\"a\"],[\"b\"],[\"a\"],[\"b\"],[\"b\"],[\"b\"],[\"b\"],[\"a\"],[\"b\"],[\"a\"],[\"b\"],[\"a\"],[\"a\"],[\"a\"],[\"b\"],[\"a\"],[\"a\"],[\"a\"]]",
    "[null,false,false,false,false,false,true,true,true,true,true,false,false,true,true,true,true,false,false,false,true,true,true,true,true,true,false,true,true,true,false]")]
    public void Check(string commands, string arguments, string results)
    {
        var commandList = JsonConvert.DeserializeObject<string[]>(commands);
        var argumentList = JsonConvert.DeserializeObject<object[][]>(arguments);
        var expectedResponses = JsonConvert.DeserializeObject<bool?[]>(results);

        var responses = new bool?[commandList.Length];
        StreamChecker streamChecker = null;
        for (var i = 0; i < commandList.Length; i++)
        {
            if (commandList[i] == "StreamChecker")
            {
                var arg = JsonConvert.DeserializeObject<string[]>(argumentList[i][0].ToString());
                streamChecker = new StreamChecker(arg);
                responses[i] = null;
            }
            else
            {
                var arg = char.Parse(argumentList[i][0].ToString() ?? string.Empty);
                responses[i] = streamChecker.Query(arg);
            }
        }

        responses.Should().BeEquivalentTo(expectedResponses);
    }
}

