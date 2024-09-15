using DataStructures.Tree.BinarySearchTree;
using FluentAssertions;
using NUnit.Framework;

namespace LeetCode.Cracking.TreesAndGraphs
{
    /// <summary>
    /// Implement a function to check if a binary tree is a binary search tree.
    /// 
    /// Discussion:
    /// Important #1! The BST property cannot be derived from the parent - child relationship. Grandparent - grandchild relationship can be broken.
    /// Important #2! How to handle equal values? Are duplicate entries allowed? In that case, do they have to be in the left sub-tree?
    /// 
    /// Solutions:
    /// * In-order traversal. Keep the previously visited element and compare it to the current.
    /// * Min/max. Grandparent and parent give value boundaries.
    ///   In left traversal, values cannot be smaller than grandparent but larger than parent.
    ///   In right traversal, values cannot be smaller than parent but larger than grandparent.
    /// </summary>
    internal class Task4_5ValidateBst
    {
        public bool IsBinarySearchTreeByInOrder(BinaryTreeNode<int> root)
        {
            int? previousValue = null;

            return IsBinarySearchTreeByRecursiveInOrder(root, ref previousValue);
        }

        private bool IsBinarySearchTreeByRecursiveInOrder(BinaryTreeNode<int>? root, ref int? previousValue)
        {
            if (root == null)
                return true;

            // left
            if (!IsBinarySearchTreeByRecursiveInOrder(root.Left, ref previousValue))
            {
                return false;
            }

            // current
            if (previousValue != null && previousValue >= root.Value)
            {
                return false;
            }
            
            previousValue = root.Value;

            // right
            if (!IsBinarySearchTreeByRecursiveInOrder(root.Right, ref previousValue))
            {
                return false;
            }

            return true;
        }

        public bool IsBinarySearchTreeByMinMax(BinaryTreeNode<int> root)
        {
            return IsBinarySearchTreeByRecursiveMinMax(root, null, null);
        }

        private bool IsBinarySearchTreeByRecursiveMinMax(BinaryTreeNode<int>? root, int? minimumAllowedValue, int? maximumAllowedValue)
        {
            if (root == null)
                return true;

            if ((minimumAllowedValue != null && root.Value <= minimumAllowedValue) || (maximumAllowedValue != null && root.Value > maximumAllowedValue))
            {
                return false;
            }

            if (!IsBinarySearchTreeByRecursiveMinMax(root.Left, minimumAllowedValue, root.Value) ||
                !IsBinarySearchTreeByRecursiveMinMax(root.Right, root.Value, maximumAllowedValue))
            {
                return false;
            }

            return true;
        }
    }

    [TestFixture]
    internal class Task4_5ValidateBstTests
    {
        private static readonly object[] testCases =
        {
            new object[] { TestTrees.CreateLeftSkewedBinaryTree(), false },
            new object[] { TestTrees.CreateRightSkewedBinarySearchTree(), true },
            new object[] { TestTrees.CreateHalfDiamondTree(), false },
            new object[] { TestTrees.CreateTriangleBinarySearchTree(), true },
            new object[] { TestTrees.CreateLeftHeavyBinarySearchTree(), true },
            new object[] { TestTrees.CreateBalancedTree(), false },
            new object[] { TestTrees.CreateQuadrupleTree(), false }
        };

        [TestCaseSource(nameof(testCases))]
        public void IsBinarySearchTreeByInOrderTest(BinaryTreeNode<int> root, bool expectedResult)
        {
            // arrange
            var sut = new Task4_5ValidateBst();

            // act
            var result = sut.IsBinarySearchTreeByInOrder(root);

            // assert
            result.Should().Be(expectedResult);
        }

        [TestCaseSource(nameof(testCases))]
        public void IsBinarySearchTreeByMinMaxTest(BinaryTreeNode<int> root, bool expectedResult)
        {
            // arrange
            var sut = new Task4_5ValidateBst();

            // act
            var result = sut.IsBinarySearchTreeByMinMax(root);

            // assert
            result.Should().Be(expectedResult);
        }
    }
}
