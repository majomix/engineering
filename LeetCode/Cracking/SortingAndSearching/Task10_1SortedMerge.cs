using LeetCode.Cracking.Recursion;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace LeetCode.Cracking.SortingAndSearching;

/// <summary>
/// You are given two sorted arrays, A and B, where A has a large enough buffer to hold B. Write a method to merge B into A in sorted order.
///
/// Solution:
/// </summary>
internal class Task10_1SortedMerge
{
    public void MergeSorted(int[] a, int[] b)
    {
        var pointerB = 0;

        for (var pointerA = 0; pointerA < a.Length; pointerA++)
        {
            if (a[pointerA] > b[pointerB])
            {
                (a[pointerA], b[pointerB]) = (b[pointerB], a[pointerA]);
            }
            else
            {

            }
        }
    }
}

[TestFixture]
public class Task10_1SortedMergeTests
{
    private static object[] testCases =
    {
        new object[] { new[] { 1,7,10,15,18,0,0,0,0,0,0,0 }, new[] { 2,3,4,8,9,12,19 }, new[] { 1,2,3,4,7,8,9,10,12,15,18,19 }},
    };

    [TestCaseSource(nameof(testCases))]
    public void CountWaysTest(int[] a, int[] b, int[] expectedResult)
    {
        // arrange
        var sut = new Task10_1SortedMerge();

        // act
        sut.MergeSorted(a, b);

        // assert
        a.Should().BeEquivalentTo(expectedResult, options => options.WithStrictOrdering());
    }
}
