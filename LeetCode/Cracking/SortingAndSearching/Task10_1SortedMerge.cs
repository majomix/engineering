using NUnit.Framework;
using FluentAssertions;

namespace LeetCode.Cracking.SortingAndSearching;

/// <summary>
/// You are given two sorted arrays, A and B, where A has a large enough buffer to hold B. Write a method to merge B into A in sorted order.
///
/// Solution:
/// * 
/// </summary>
internal class Task10_1SortedMerge
{
    public void MergeSorted(int[] a, int[] b, int lengthA, int lengthB)
    {
        var targetPointer = lengthA + lengthB - 1;
        var pointerA = lengthA - 1;
        var pointerB = lengthB - 1;

        // when we run out of items in B, the remaining items in A are guaranteed to be sorted
        while (pointerB >= 0)
        {
            if (pointerA >= 0 && a[pointerA] > b[pointerB])
            {
                a[targetPointer] = a[pointerA--];
            }
            else
            {
                a[targetPointer] = b[pointerB--];
            }

            targetPointer--;
        }
    }
}

[TestFixture]
public class Task10_1SortedMergeTests
{
    private static object[] testCases =
    {
        new object[] { new[] { 1,7,10,15,18,0,0,0,0,0,0,0 }, new[] { 2,3,4,8,9,12,19 }, 5, 7, new[] { 1,2,3,4,7,8,9,10,12,15,18,19 }},
        new object[] { new[] { 2,7,10,15,18,0,0,0,0,0,0,0 }, new[] { 1,3,4,8,9,12,19 }, 5, 7, new[] { 1,2,3,4,7,8,9,10,12,15,18,19 }}
    };

    [TestCaseSource(nameof(testCases))]
    public void CountWaysTest(int[] a, int[] b, int lengthA, int lengthB, int[] expectedResult)
    {
        // arrange
        var sut = new Task10_1SortedMerge();

        // act
        sut.MergeSorted(a, b, lengthA, lengthB);

        // assert
        a.Should().BeEquivalentTo(expectedResult, options => options.WithStrictOrdering());
    }
}
