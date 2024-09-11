using FluentAssertions;
using NUnit.Framework;
using DataStructures.Tree.BinarySearchTree;

namespace LeetCode.Cracking.TreesAndGraphs
{
    /// <summary>
    /// Given an array with unique integer elements sorted in an increasing order, write an algorithm to create a binary search tree with minimal height.
    ///
    /// Discussion:
    /// The tree does not have to be a complete binary tree so the elements can be divided just approximately.
    /// Make the middle element root and divide the array into two sub-arrays. Repeat the process by taking the middle element.
    /// 
    /// Solutions:
    /// * Iterative - needs additional data structure to keep track of what was iterated over
    /// * Recursive
    /// </summary>
    internal class Task4_2MinimalTree
    {
        public BinaryTreeNode<int>? CreateMinimalBinarySearchTree(int[] input)
        {
            return CreateMinimalBinarySearchTreeRecursively(input, 0, input.Length - 1);
        }

        private BinaryTreeNode<int>? CreateMinimalBinarySearchTreeRecursively(int[] input, int startIndex, int endIndex)
        {
            if (startIndex > endIndex)
                return null;

            var middleIndex = (startIndex + endIndex) / 2;
            var node = new BinaryTreeNode<int>(input[middleIndex]);

            node.Left = CreateMinimalBinarySearchTreeRecursively(input, startIndex, middleIndex - 1);
            node.Right = CreateMinimalBinarySearchTreeRecursively(input, middleIndex + 1, endIndex);

            return node;
        }
    }

    [TestFixture]
    internal class Task4_2MinimalTreeTests
    {
        [Test]
        public void CreateMinimalBinarySearchTreeTest()
        {
            // arrange
            var sut = new Task4_2MinimalTree();

            // act
            var root = sut.CreateMinimalBinarySearchTree(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });

            // assert
            //         5
            //       /   \
            //      2     7
            //     / \   / \
            //    1   3 6   8
            //         \     \
            //          4     9
            root.Value.Should().Be(5);
            var nodeTwo = root.Left!;
            var nodeSeven = root.Right!;
            nodeTwo.Value.Should().Be(2);
            nodeSeven.Value.Should().Be(7);

            // depth 2
            var nodeOne = nodeTwo.Left!;
            var nodeThree = nodeTwo.Right!;
            nodeOne.Value.Should().Be(1);
            nodeThree.Value.Should().Be(3);

            var nodeSix = nodeSeven.Left!;
            var nodeEight = nodeSeven.Right!;
            nodeSix.Value.Should().Be(6);
            nodeEight.Value.Should().Be(8);

            // depth 3
            var nodeFour = nodeThree.Right!;
            var nodeNine = nodeEight.Right!;

            nodeFour.Value.Should().Be(4);
            nodeNine.Value.Should().Be(9);
        }
    }
}
