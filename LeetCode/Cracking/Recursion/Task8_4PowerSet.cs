using FluentAssertions;
using NUnit.Framework;

namespace LeetCode.Cracking.Recursion;

/// <summary>
/// Write a method to return all subsets of a set.
///
/// Solution:
/// 1. (Inverted) Recursion
/// !New approach!
/// Rather than passing the intermediate sets down the recursion, recurse to the base case and build it upwards.
/// If a list has N elements, recurse N times to make the base case of empty set and pass it up.
/// Now recursion level N-k copies all the intermediate subsets and adds the element at index N-k to all of them.
///   
/// 2. Combinatorics
/// Treat the list of numbers as indexable elements. Then for length N, there is 2^N subsets.
/// This can be represented by generating numbers where each bit counts as flag whether or not to include number at its index.
/// </summary>
internal class Task8_4PowerSet
{
    public List<HashSet<int>> GetAllSubsetsByRecursion(List<int> input)
    {
        return GetAllSubsetsRecursively(input, 0);
    }

    private List<HashSet<int>> GetAllSubsetsRecursively(List<int> input, int elementIndex)
    {
        List<HashSet<int>> result;

        if (input.Count == elementIndex)
        {
            result = new();
            result.Add(new HashSet<int>());
        }
        else
        {
            result = GetAllSubsetsRecursively(input, elementIndex + 1);
            var element = input[elementIndex];
            var resultCount = result.Count;
            for (int i = 0; i < resultCount; i++)
            {
                var subset = result[i];
                var newSubset = new HashSet<int>();
                foreach (var e in subset)
                {
                    newSubset.Add(e);
                }
                newSubset.Add(element);
                result.Add(newSubset);
            }
        }

        return result;
    }

    public List<HashSet<int>> GetAllSubsetsByCombinatorics(List<int> input)
    {
        var result = new List<HashSet<int>>();

        var numberOfSets = 1 << input.Count;
        for (var i = 0; i < numberOfSets; i++)
        {
            var set = CreateSetFromFlags(input, i);
            result.Add(set);
        }

        return result;
    }

    private HashSet<int> CreateSetFromFlags(List<int> input, int flags)
    {
        var set = new HashSet<int>();

        var index = 0;
        for (var mask = flags; mask > 0; mask >>= 1)
        {
            if ((mask & 1) == 1)
            {
                set.Add(input[index]);
            }
            index++;
        }

        return set;
    }
}

[TestFixture]
public class Task8_4PowerSetTests
{
    private static object[] testCases =
    {
        new object[]
        {
            new List<int> { 1,2,3 },
            new List<HashSet<int>>
            {
                new HashSet<int>(),
                new HashSet<int> { 1 },
                new HashSet<int> { 2 },
                new HashSet<int> { 3 },
                new HashSet<int> { 1,2 },
                new HashSet<int> { 2,3 },
                new HashSet<int> { 1,3 },
                new HashSet<int> { 1,2,3 },
            }
        }
    };

    [TestCaseSource(nameof(testCases))]
    public void GetAllSubsetsByRecursionTest(List<int> input, List<HashSet<int>> expectedResult)
    {
        // arrange
        var sut = new Task8_4PowerSet();

        // act
        var result = sut.GetAllSubsetsByRecursion(input);

        // assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    [TestCaseSource(nameof(testCases))]
    public void GetAllSubsetsByCombinatoricsTest(List<int> input, List<HashSet<int>> expectedResult)
    {
        // arrange
        var sut = new Task8_4PowerSet();

        // act
        var result = sut.GetAllSubsetsByCombinatorics(input);

        // assert
        result.Should().BeEquivalentTo(expectedResult);
    }
}
