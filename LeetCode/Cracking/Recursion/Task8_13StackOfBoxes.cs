using FluentAssertions;
using NUnit.Framework;

namespace LeetCode.Cracking.Recursion;

/// <summary>
/// You have a stack of n boxes with widths wi, heights hi and depths di. The boxers cannot be rotated and can only be stacked on top of one another
/// if each box in the stack is strictly larger than the box above it in width, height and depth.
///
/// Implement a method to compute the height of the tallest possible stack. The height of a stack is the sum of the heights of each box.
/// 
/// Solution:
/// Backtracking with memoization.
/// </summary>
internal class Task8_13StackOfBoxes
{
    private readonly Dictionary<int, int> _memo = new();

    public int GetTallestStack(List<Box> boxes)
    {
        return GetTallestStackRecursively(boxes, -1);
    }

    private int GetTallestStackRecursively(List<Box> boxes, int lastIndex)
    {
        if (_memo.ContainsKey(lastIndex))
            return _memo[lastIndex];

        var depthOfTallestStackOfBoxes = 0;

        for (var i = 0; i < boxes.Count; i++)
        {
            var currentBox = boxes[i];

            if (lastIndex == -1 || currentBox.CanGoOnTopOf(boxes[lastIndex]))
            {
                var depthWithCurrentBoxAtBottom = currentBox.Depth + GetTallestStackRecursively(boxes, i);
                depthOfTallestStackOfBoxes = Math.Max(depthOfTallestStackOfBoxes, depthWithCurrentBoxAtBottom);
            }
        }

        if (lastIndex != -1)
        {
            _memo[lastIndex] = depthOfTallestStackOfBoxes;
        }

        return depthOfTallestStackOfBoxes;
    }
}

public class Box
{
    private readonly int _width;
    private readonly int _height;
    private readonly int _depth;

    public int Depth => _depth;

    public Box(int width, int depth, int height)
    {
        _width = width;
        _height = height;
        _depth = depth;
    }

    public bool CanGoOnTopOf(Box otherBox)
    {
        return otherBox._width > _width &&
               otherBox._height > _height &&
               otherBox._depth > _depth;
    }
}

[TestFixture]
public class Task8_13StackOfBoxesTests
{
    private static object[] testCases =
    {
        new object[] { new List<Box>(), 0 },
        new object[] { new List<Box> { new(5, 10, 5) }, 10 },
        new object[] { new List<Box> { new(6, 15, 6), new(5, 10, 5) }, 25 },
        new object[] { new List<Box> { new(5, 10, 5), new(6, 8, 6) }, 10 },
        new object[] {
            new List<Box>
            {
                new(8, 20, 8),
                new(7, 15, 7),
                new(6, 10, 6),
                new(5, 5, 5)
            },
            50 // 20 + 15 + 10 + 5
        },
        new object[] {
            new List<Box>
            {
                new(10, 50, 10),
                new(8, 40, 8),
                new(9, 5, 9), // This one can't be stacked on 10,50,10
                new(7, 30, 7),
                new(6, 20, 6),
                new(5, 10, 5)
            },
            50 + 40 + 30 + 20 + 10 // The valid stacking sequence
        },
        new object[] { new List<Box> { new(5, 10, 5), new(5, 10, 5) }, 10 }
    };

    [TestCaseSource(nameof(testCases))]
    public void GetTallestStackTest(List<Box> input, int expectedResult)
    {
        // arrange
        var sut = new Task8_13StackOfBoxes();

        // act
        var result = sut.GetTallestStack(input);

        // assert
        result.Should().Be(expectedResult);
    }
}
