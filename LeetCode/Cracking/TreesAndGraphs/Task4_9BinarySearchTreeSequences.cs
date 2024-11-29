using DataStructures.Tree.BinarySearchTree;
using FluentAssertions;
using NUnit.Framework;

namespace LeetCode.Cracking.TreesAndGraphs
{
    /// <summary>
    /// A binary search tree was created by traversing through an array from left to right and inserting each element.
    /// Given a binary search tree with distinct elements, print all possible arrays that could have led to this tree.
    ///
    /// Discussion:
    /// 
    /// Solutions:
    /// </summary>
    internal class Task4_9BinarySearchTreeSequences
    {
        public List<List<int>> GetSequences(BinaryTreeNode<int>? root)
        {
            var result = new List<List<int>>();

            if (root == null)
                return result;

            var first = new List<int>();
            result.Add(first);

            CreateSequences(result, first, root);

            return result;
        }
        
        private void CreateSequences(List<List<int>> result, List<int> current, BinaryTreeNode<int>? currentNode)
        {
            if (currentNode == null || (currentNode.Left == null && currentNode.Right == null))
                return;

            if (currentNode.Left == null || currentNode.Right == null)
            {
                if ()
                var copy = current.ToList();
            }

            return result;
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
}
