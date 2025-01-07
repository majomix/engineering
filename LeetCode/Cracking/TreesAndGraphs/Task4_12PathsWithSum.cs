using DataStructures.Tree.BinarySearchTree;
using FluentAssertions;
using NUnit.Framework;

namespace LeetCode.Cracking.TreesAndGraphs;

/// <summary>
/// You are given a binary tree in which each node contains an integer value (which might be positive or negative).
/// Design an algoritm to count the number of paths that will sum to a given value.
/// The path does not need to start or end at the root or at a leaf but it must go downwards.
/// 
/// Solutions:
/// * Recursion with memoization.
/// </summary>
internal class Task4_12PathsWithSum
{
    public int GetNumberOfPathsWithSum(BinaryTreeNode<int> tree, int sum)
    {
        var memo = new Dictionary<(BinaryTreeNode<int>, int), int>();

        return GetNumberOfPathsWithSum(tree, sum, sum, memo);
    }

    private int GetNumberOfPathsWithSum(
        BinaryTreeNode<int>? node,
        int sum,
        int totalSum,
        Dictionary<(BinaryTreeNode<int>, int), int> memo)
    {
        if (node == null)
            return 0;

        var currentPoint = (node, sum);

        if (memo.ContainsKey(currentPoint))
        {
            return memo[currentPoint];
        }

        if (sum - node.Value == 0)
            return 1;

        var result = 0;
        result += GetNumberOfPathsWithSum(node.Left, totalSum, totalSum, memo);
        result += GetNumberOfPathsWithSum(node.Left, sum - node.Value, totalSum, memo);
        result += GetNumberOfPathsWithSum(node.Right, totalSum, totalSum, memo);
        result += GetNumberOfPathsWithSum(node.Right, sum - node.Value, totalSum, memo);

        memo[currentPoint] = result;

        return result;
    }
}

[TestFixture]
internal class Task4_12PathsWithSumTests
{
    private static readonly object[] testCases =
    {
        new object[] { 18, 3 },
        new object[] { 8, 3 },
        new object[] { 7, 2 }
    };

    [TestCaseSource(nameof(testCases))]
    public void GetNumberOfPathsWithSumTest(int targetSum, int expectedResult)
    {
        // arrange
        var sut = new Task4_12PathsWithSum();
        var root = CreateTreeUnderTest();

        // act
        var result = sut.GetNumberOfPathsWithSum(root, targetSum);

        // assert
        result.Should().Be(expectedResult);
    }

    private BinaryTreeNode<int> CreateTreeUnderTest()
    {
        var tree = new BinaryTreeNode<int>(10)
        {
            Left = new BinaryTreeNode<int>(5)
            {
                Left = new BinaryTreeNode<int>(3)
                {
                    Left = new BinaryTreeNode<int>(3),
                    Right = new BinaryTreeNode<int>(-2)
                },
                Right = new BinaryTreeNode<int>(2)
                {
                    Right = new BinaryTreeNode<int>(1)
                }
            },
            Right = new BinaryTreeNode<int>(-3)
            {
                Right = new BinaryTreeNode<int>(11)
            }
        };

        return tree;
    }
}
