using FluentAssertions;
using NUnit.Framework;

namespace LeetCode.Cracking.Recursion;

/// <summary>
/// Write a method to compute all permutations of a string of unique characters.
/// 
/// Solution:
/// Create the string from the beginning to the end. Pass down the prefix to always add a character.
/// Use histogram to always add a character once even if it occured multiple times in the input string.
/// </summary>
internal class Task8_8PermutationsWithDuplication
{
    public List<string> GetPermutations(string input)
    {
        var result = new List<string>();
        var histogram = GetHistogram(input);

        GetPermutationsRecursively(histogram, string.Empty, result);

        return result;
    }

    private void GetPermutationsRecursively(Dictionary<char, int> input, string prefix, List<string> result)
    {
        if (input.Count == 0)
        {
            result.Add(prefix);
            return;
        }

        foreach (var key in input.Keys.ToList())
        {
            // to avoid expensive dictionary re-allocation in every stack frame, remove the item, then add it back
            input[key]--;
            if (input[key] == 0)
            {
                input.Remove(key);
            }

            GetPermutationsRecursively(input, $"{prefix}{key}", result);

            if (!input.TryAdd(key, 1))
            {
                input[key]++;
            }
        }
    }

    private Dictionary<char, int> GetHistogram(string input)
    {
        var result = new Dictionary<char, int>();

        foreach (var ch in input)
        {
            if (!result.TryAdd(ch, 1))
            {
                result[ch]++;
            }
        }

        return result;
    }
}

[TestFixture]
public class Task8_8PermutationsWithDuplicationTests
{
    private static object[] testCases =
    {
        new object[] { "a", new List<string> { "a" } },
        new object[] { "aa", new List<string> { "aa" } },
        new object[] { "aab", new List<string> { "aab", "aba", "baa" } },
        new object[] { "aaab", new List<string> { "aaab", "aaba", "abaa", "baaa" } },
        new object[] { "aaaaaaaaaaaaaaaa", new List<string> { "aaaaaaaaaaaaaaaa" } },
        new object[] { "abcd", new List<string> {"abcd", "abdc", "acbd", "acdb", "adbc", "adcb", "bacd", "badc", "bcad", "bcda", "bdac", "bdca", "cabd", "cadb", "cbad", "cbda", "cdab", "cdba", "dabc", "dacb", "dbac", "dbca", "dcab", "dcba"} },
    };

    [TestCaseSource(nameof(testCases))]
    public void GetPermutationsTest(string input, List<string> expectedResult)
    {
        // arrange
        var sut = new Task8_8PermutationsWithDuplication();

        // act
        var result = sut.GetPermutations(input);

        // assert
        result.Should().BeEquivalentTo(expectedResult, options => options.WithStrictOrdering());
    }
}
