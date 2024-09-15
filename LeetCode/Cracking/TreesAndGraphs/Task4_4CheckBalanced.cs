using DataStructures.Tree.BinarySearchTree;
using FluentAssertions;
using NUnit.Framework;

namespace LeetCode.Cracking.TreesAndGraphs
{
    /// <summary>
    /// Implement a function to check if a binary tree is balanced.
    /// For the purpose of this question, a balanced tree is defined bo to a tree such that the heights of the two subtrees of any node never differ by more than one.
    ///
    /// Solutions:
    /// * recurse through left and right subtree and look for any unbalanced node
    /// </summary>
    public class Task4_4CheckBalanced
    {
        public bool IsBalanced(BinaryTreeNode<int> root)
        {
            return !GetTreeHeight(root).IsUnbalanced;
        }

        private (int Height, bool IsUnbalanced) GetTreeHeight(BinaryTreeNode<int>? root)
        {
            // base case
            if (root == null)
                return (0, false);

            var leftHeight = GetTreeHeight(root.Left);
            var rightHeight = GetTreeHeight(root.Right);

            var bigger = leftHeight.Height;
            var smaller = rightHeight.Height;
            
            if (smaller > bigger)
            {
                bigger = rightHeight.Height;
                smaller = leftHeight.Height;
            }

            var isUnbalanced = leftHeight.IsUnbalanced | rightHeight.IsUnbalanced | bigger - smaller > 1;

            return (bigger + 1, isUnbalanced);
        }
    }

    [TestFixture]
    internal class Task4_4CheckBalancedTests
    {
        private static readonly object[] testCases =
        {
            new object[] { TestTrees.CreateLeftSkewedBinaryTree(), false },
            new object[] { TestTrees.CreateRightSkewedBinarySearchTree(), false },
            new object[] { TestTrees.CreateHalfDiamondTree(), false },
            new object[] { TestTrees.CreateTriangleBinarySearchTree(), false },
            new object[] { TestTrees.CreateLeftHeavyBinarySearchTree(), false },
            new object[] { TestTrees.CreateBalancedTree(), true },
        };

        [TestCaseSource(nameof(testCases))]
        public void IsBalancedTest(BinaryTreeNode<int> root, bool expectedResult)
        {
            // arrange
            var sut = new Task4_4CheckBalanced();

            // act
            var result = sut.IsBalanced(root);

            // assert
            result.Should().Be(expectedResult);
        }
    }
}
