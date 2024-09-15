using DataStructures.Tree.BinarySearchTree;
using FluentAssertions;
using NUnit.Framework;

namespace LeetCode.Cracking.TreesAndGraphs
{
    /// <summary>
    /// Write an algorithm to find in-order successor of a given node in a binary search tree. You may assume that each node has a link to its parent.
    /// 
    /// Discussion:
    /// In-order successor is:
    /// 1. The left-most child in the right sub-tree.
    /// 2. The nearest right parent.
    /// 3. Null if the node is the right-most node.
    /// 
    /// Solutions:
    /// * In-order traversal. Search for the node whose successor should be found and then get it.
    /// * Start at the given node and follow the rules described above.
    /// </summary>
    public class Task4_6Successor
    {
        public BinaryTreeNodeWithParentLink<int>? GetInOrderSuccessor(BinaryTreeNodeWithParentLink<int>? root)
        {
            if (root == null)
                return null;

            if (root.Right != null)
            {
                var current = root.Right;
                while (current.Left != null)
                {
                    current = current.Left;
                }

                return current;
            }

            var child = root;
            var parent = root.Parent;
            while (parent != null && parent.Left != child)
            {
                child = parent;
                parent = parent.Parent;
            }

            return parent;
        }
    }

    [TestFixture]
    internal class Task4_6SuccessorTests
    {
        [Test]
        public void GetInOrderSuccessorTest()
        {
            // arrange
            var sut = new Task4_6Successor();

            //              20
            //                \
            //                 40
            //                /  \
            //              30    41
            //             /  \
            //           28    35
            //          /  \     \
            //        22    29    37
            //                      \
            //                       38
            var nodeTwentyRoot = new BinaryTreeNodeWithParentLink<int>(20, null);

            var nodeForty = new BinaryTreeNodeWithParentLink<int>(40, nodeTwentyRoot);
            nodeTwentyRoot.Right = nodeForty;

            var nodeFortyOne = new BinaryTreeNodeWithParentLink<int>(41, nodeForty);
            nodeForty.Right = nodeFortyOne;

            var nodeThirty = new BinaryTreeNodeWithParentLink<int>(30, nodeForty);
            nodeForty.Left = nodeThirty;

            var nodeTwentyEight = new BinaryTreeNodeWithParentLink<int>(28, nodeThirty);
            nodeThirty.Left = nodeTwentyEight;

            var nodeThirtyFive = new BinaryTreeNodeWithParentLink<int>(35, nodeThirty);
            nodeThirty.Right = nodeThirtyFive;

            var nodeTwentyTwo = new BinaryTreeNodeWithParentLink<int>(22, nodeTwentyEight);
            nodeTwentyEight.Left = nodeTwentyTwo;

            var nodeTwentyNine = new BinaryTreeNodeWithParentLink<int>(29, nodeTwentyEight);
            nodeTwentyEight.Right = nodeTwentyNine;

            var nodeThirtySeven = new BinaryTreeNodeWithParentLink<int>(37, nodeThirtyFive);
            nodeThirtyFive.Right = nodeThirtySeven;

            var nodeThirtyEight = new BinaryTreeNodeWithParentLink<int>(38, nodeThirtySeven);
            nodeThirtySeven.Right = nodeThirtyEight;

            // act
            var successorOfTwenty = sut.GetInOrderSuccessor(nodeTwentyRoot);
            var successorOfThirtyEight = sut.GetInOrderSuccessor(nodeThirtyEight);
            var successorOfFortyOne = sut.GetInOrderSuccessor(nodeFortyOne);

            // assert
            successorOfTwenty.Should().Be(nodeTwentyTwo);
            successorOfThirtyEight.Should().Be(nodeForty);
            successorOfFortyOne.Should().Be(null);
        }
    }
}
