using FluentAssertions;
using NUnit.Framework;

namespace LeetCode.Cracking.Recursion;

/// <summary>
/// Write a method to compute all permutations of a string of unique characters.
/// 
/// Solution:
/// * Create the string from the beginning to the end. Pass down the prefix to always add a character.
/// </summary>
internal class Task8_7PermutationsWithoutDuplication
{
    public List<string> GetPermutations(string input)
    {
        var result = new List<string>();
        GetPermutationsRecursively(input, new HashSet<int>(), string.Empty, result);

        return result;
    }

    private void GetPermutationsRecursively(string input, HashSet<int> usedIndices, string prefix, List<string> result)
    {
        if (prefix.Length == input.Length)
        {
            result.Add(prefix);
            return;
        }

        for (var i = 0; i < input.Length; i++)
        {
            // to avoid expensive hashmap re-allocation in every stack frame, add item, then remove it
            if (!usedIndices.Add(i))
                continue;

            GetPermutationsRecursively(input, usedIndices, $"{prefix}{input[i]}", result);
            usedIndices.Remove(i);
;        }
    }
}

[TestFixture]
public class Task8_7PermutationsWithoutDuplicationTests
{
    private static object[] testCases =
    {
        new object[] { "a", new List<string> { "a" } },
        new object[] { "ab", new List<string> { "ab", "ba" } },
        new object[] { "abc", new List<string> { "abc", "acb", "bac", "bca", "cab", "cba" } },
        new object[] { "abcd", new List<string> {"abcd", "abdc", "acbd", "acdb", "adbc", "adcb", "bacd", "badc", "bcad", "bcda", "bdac", "bdca", "cabd", "cadb", "cbad", "cbda", "cdab", "cdba", "dabc", "dacb", "dbac", "dbca", "dcab", "dcba"} },
    };

    [TestCaseSource(nameof(testCases))]
    public void GetPermutationsTest(string input, List<string> expectedResult)
    {
        // arrange
        var sut = new Task8_7PermutationsWithoutDuplication();

        // act
        var result = sut.GetPermutations(input);

        // assert
        result.Should().BeEquivalentTo(expectedResult, options => options.WithStrictOrdering());
    }
}