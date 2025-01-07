using FluentAssertions;
using NUnit.Framework;

namespace LeetCode.Cracking.Recursion;

/// <summary>
/// A magic index in an array A[1...n-1] is defined to be an index such that A[i] = i.
/// Given a sorted array of distinct integers, write a method to find a magic index, if one exists, in array A.
///
/// Example of distinct values:
///  0    1    2    3    4    5    6    7
/// -2   -1    0    2    4    7    9   10
/// 
/// Example if not distinct values:
///  0    1    2    3    4    5    6    7
/// -1    0    2    2    5    7    9   10
/// 
/// Solution:
/// - naive with simple loop O(n)
/// - leveraging the sorted property, more effective approach is using binary search
/// - if all the elements are distinct, any arbitrary index with value lower than the index means the indices on the left side are not magic
/// - if the elements are not distinct, magic index can be on either side, but even then some elements can be skipped
/// </summary>
internal class Task8_3MagicIndex
{
    public int FindMagicIndex(int[] input)
    {
        return FindMagicIndex(input, 0, input.Length - 1);
    }

    private int FindMagicIndex(int[] input, int leftIndex, int rightIndex)
    {
        var middleIndex = leftIndex + (rightIndex - leftIndex) / 2;
        var middleValue = input[middleIndex];

        if (middleValue == middleIndex)
            return middleIndex;

        var result = -1;
        
        if (leftIndex >= rightIndex)
            return result;

        var leftPart = FindMagicIndex(input, leftIndex, Math.Min(middleIndex - 1, middleValue));
        var rightPart = FindMagicIndex(input, Math.Max(middleIndex + 1, middleValue), rightIndex);

        if (leftPart != -1)
            result = leftPart;

        if (rightPart != -1)
            result = rightPart;

        return result;
    }
}

[TestFixture]
public class Task8_3MagicIndexTests
{
    private static object[] testCases =
    {
        new object[] { new int[] { -2, -1, 0, 2, 4, 7, 9, 10 }, 4 },
        new object[] { new int[] { -1, 0, 2, 2, 5, 7, 9, 10 }, 2 },
        new object[] { new int[] { -1, 0, 1, 2, 3, 4, 5, 6 }, -1 }
    };

    [TestCaseSource(nameof(testCases))]
    public void FindMagicIndexTest(int[] input, int expectedResult)
    {
        // arrange
        var sut = new Task8_3MagicIndex();

        // act
        var result = sut.FindMagicIndex(input);

        // assert
        result.Should().Be(expectedResult);
    }
}