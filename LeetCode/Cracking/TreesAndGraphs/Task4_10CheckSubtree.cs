using DataStructures.Tree.BinarySearchTree;
using FluentAssertions;
using NUnit.Framework;
using System.Text;

namespace LeetCode.Cracking.TreesAndGraphs;

/// <summary>
/// T1 and T2 are two very large binary trees, with T1 much bigger than T2. Create an algorithm to determine if T2 is a subtree of T1.
/// A tree T2 is a subtree of T1 if there exists a node n in T1 such that the subtree of n is identical to T2.
/// 
/// Discussion:
/// In-order traversal will not work because as long as the nodes are the same, they will give the same output independent of the
/// structure of the tree. Example:
/// 
/// 1.      8
///        / \
///       4   9
///      /  \  \
///     2    6  10
///    / \  / \
///   1  3 5   7
///
/// 2.      8
///        / \
///       5   9
///      /  \  \
///     2    7  10
///    / \  /
///   1  3 6
///       \
///        4
/// Pre-order and post-order would work if empty nodes are replaced by "X".
/// Pre-order without X marks would not work with the case of 1-2-3-4 subtree:
/// 1. 8,4,2,1,3,6,5,7,9,10
/// 2. 8,5,2,1,3,4,7,6,9,10
/// 
/// With X marks:
/// 1. 8,4,2,1,X,X,3,X,X,6,5,X,X,7,X,X,9,X,10,X,X
/// 2. 8,5,2,1,X,X,3,X,4,X,X,7,6,X,X,X,9,X,10,X,X
/// 
/// Solutions:
/// * Tree traversal. Described above.
/// * Look for matching node and then examine the subtree for match.
/// </summary>
internal class Task4_10CheckSubtree
{
    public bool IsSubtreeByInOrderTraversalStringComparison(BinaryTreeNode<int> root, BinaryTreeNode<int> subtree)
    {
        var sourceTree = new StringBuilder();
        GetPreOrderRepresentationWithTerminationMarks(root, sourceTree);
        
        var subtreeToFind = new StringBuilder();
        GetPreOrderRepresentationWithTerminationMarks(subtree, subtreeToFind);

        return sourceTree.ToString().Contains(subtreeToFind.ToString());
    }

    private void GetPreOrderRepresentationWithTerminationMarks(BinaryTreeNode<int>? root, StringBuilder sb)
    {
        if (root == null)
        {
            sb.Append("X");
            return;
        }

        sb.Append(root.Value);
        GetPreOrderRepresentationWithTerminationMarks(root.Left, sb);
        GetPreOrderRepresentationWithTerminationMarks(root.Right, sb);
    }

    public bool IsSubtreeByTreeComparison(BinaryTreeNode<int> root, BinaryTreeNode<int> subtree)
    {
        if (subtree == null)
            return true;

        return LookForMatchingSubtreeRoot(root, subtree);
    }

    private bool LookForMatchingSubtreeRoot(BinaryTreeNode<int>? root, BinaryTreeNode<int> subtree)
    {
        if (root == null)
            return false;

        var result = false;

        if (root.Value == subtree.Value)
        {
            result |= IsMatchingSubtree(root, subtree);
        }

        if (result != true)
        {
            result |= LookForMatchingSubtreeRoot(root.Left, subtree);
            result |= LookForMatchingSubtreeRoot(root.Right, subtree);
        }

        return result;
    }

    private bool IsMatchingSubtree(BinaryTreeNode<int>? root, BinaryTreeNode<int>? subtree)
    {
        if (root == null && subtree == null)
            return true;

        if (root == null || subtree == null)
            return false;

        var result = root.Value == subtree.Value;

        result &= IsMatchingSubtree(root.Left, subtree.Left);
        result &= IsMatchingSubtree(root.Right, subtree.Right);

        return result;
    }
}

[TestFixture]
internal class Task4_10CheckSubtreeTests
{
    private static readonly object[] testCases =
    {
        new object[] { TestTrees.CreateLeftSkewedBinaryTree(), TestTrees.CreateLeftSkewedBinaryTree().Left!.Left!, true },
        new object[] { TestTrees.CreateRightSkewedBinarySearchTree(), TestTrees.CreateRightSkewedBinarySearchTree().Right!, true },
        new object[] { TestTrees.CreateHalfDiamondTree(), TestTrees.CreateHalfDiamondTree().Left!.Left!, true },
        new object[] { TestTrees.CreateTriangleBinarySearchTree(), TestTrees.CreateTriangleBinarySearchTree().Right!, true },
        new object[] { TestTrees.CreateLeftHeavyBinarySearchTree(), TestTrees.CreateLeftHeavyBinarySearchTree().Left!, true },
        new object[] { TestTrees.CreateBalancedTree(), TestTrees.CreateLeftHeavyBinarySearchTree(), false },
        new object[] { TestTrees.CreateQuadrupleTree(), TestTrees.CreateBalancedTree(), false }
    };

    [TestCaseSource(nameof(testCases))]
    public void IsSubtreeByInOrderTraversalStringComparisonTest(BinaryTreeNode<int> tree, BinaryTreeNode<int> subtree, bool expectedResult)
    {
        // arrange
        var sut = new Task4_10CheckSubtree();

        // act
        var result = sut.IsSubtreeByInOrderTraversalStringComparison(tree, subtree);

        // assert
        result.Should().Be(expectedResult);
    }

    [TestCaseSource(nameof(testCases))]
    public void IsSubtreeByTreeComparisonTest(BinaryTreeNode<int> tree, BinaryTreeNode<int> subtree, bool expectedResult)
    {
        // arrange
        var sut = new Task4_10CheckSubtree();

        // act
        var result = sut.IsSubtreeByTreeComparison(tree, subtree);

        // assert
        result.Should().Be(expectedResult);
    }
}
