using DataStructures.Tree.BinarySearchTree;
using FluentAssertions;
using NUnit.Framework;

namespace LeetCode.Cracking.TreesAndGraphs;

/// <summary>
/// A binary search tree was created by traversing through an array from left to right and inserting each element.
/// Given a binary search tree with distinct elements, print all possible arrays that could have led to this tree.
///
/// Discussion:
/// Leetcode hard! Solution based on book.
/// 
/// Solutions:
/// Based on my own analysis, the root must be always added first.
/// The order of left and right nodes does not matter as long as the parent - child order is respected.
/// This means that order of nodes in individual levels does not need to be preserved.
/// 
/// Implemented solution is created by interweaving as on Pg 264.
/// </summary>
internal class Task4_9BinarySearchTreeSequences
{
    public List<LinkedList<int>> GetSequences(BinaryTreeNode<int>? root)
    {
        var result = new List<LinkedList<int>>();

        if (root == null)
        {
            result.Add(new LinkedList<int>());
            return result;
        }

        var prefix = new LinkedList<int>();
        prefix.AddFirst(root.Value);

        var leftSubtreeSequences = GetSequences(root.Left);
        var rightSubtreeSequences = GetSequences(root.Right);

        foreach (var leftSequence in leftSubtreeSequences)
        {
            foreach (var rightSequence in rightSubtreeSequences)
            {
                var weaved = new List<LinkedList<int>>();

                WeaveLists(leftSequence, rightSequence, weaved, prefix);

                result.AddRange(weaved);
            }
        }

        return result;
    }

    private void WeaveLists(
        LinkedList<int> leftSequence,
        LinkedList<int> rightSequence,
        List<LinkedList<int>> weaved,
        LinkedList<int> prefix)
    {
        if (leftSequence.Count == 0 || rightSequence.Count == 0)
        {
            var result = new LinkedList<int>(prefix);
            
            foreach (var treeNode in leftSequence)
            {
                result.AddLast(treeNode);
            }

            foreach (var treeNode in rightSequence)
            {
                result.AddLast(treeNode);
            }

            weaved.Add(result);
            return;
        }

        var leftHead = leftSequence.First;
        leftSequence.RemoveFirst();
        prefix.AddLast(leftHead);
        WeaveLists(leftSequence, rightSequence, weaved, prefix);
        prefix.RemoveLast();
        leftSequence.AddFirst(leftHead);

        var rightHead = rightSequence.First;
        rightSequence.RemoveFirst();
        prefix.AddLast(rightHead);
        WeaveLists(leftSequence, rightSequence, weaved, prefix);
        prefix.RemoveLast();
        leftSequence.AddFirst(rightHead);
    }
}

[TestFixture]
internal class Task4_9BinarySearchTreeSequencesTests
{
    [Test]
    public void GetInOrderSuccessorTest()
    {
        // arrange
        var sut = new Task4_9BinarySearchTreeSequences();
        var tree = CreateSmallTreeUnderTest();

        // act
        var result = sut.GetSequences(tree);

        // assert
        result[0].Should().BeEquivalentTo(new[] { 2, 1, 3 });
        result[1].Should().BeEquivalentTo(new[] { 2, 3, 1 });
    }

    private BinaryTreeNode<int> CreateSmallTreeUnderTest()
    {
        var tree = new BinaryTreeNode<int>(2)
        {
            Left = new BinaryTreeNode<int>(1),
            Right = new BinaryTreeNode<int>(3)
        };

        return tree;
    }
}
