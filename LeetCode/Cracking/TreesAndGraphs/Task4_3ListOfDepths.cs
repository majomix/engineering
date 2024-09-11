using DataStructures.LinkedList;
using NUnit.Framework;
using DataStructures.Tree.BinarySearchTree;
using FluentAssertions;

namespace LeetCode.Cracking.TreesAndGraphs
{
    /// <summary>
    /// Given a binary tree, design an algorithm which creates a linked list of all the nodes at each depth.
    /// E.g. if you have a tree with depth D, you'll have D linked lists.
    ///
    /// Solutions:
    /// * pre-order recursively, pass down the level number
    /// * level-order iteratively
    /// </summary>
    internal class Task4_3ListOfDepths
    {
        public List<CustomSinglyLinkedList<BinaryTreeNode<int>>> CreateLevelLinkedListsByPreOrder(BinaryTreeNode<int>? root)
        {
            var result = new List<CustomSinglyLinkedList<BinaryTreeNode<int>>>();

            CreateLevelLinkedListsByPreOrderRecursively(root, result, 0);

            return result;
        }

        private void CreateLevelLinkedListsByPreOrderRecursively(BinaryTreeNode<int>? root, List<CustomSinglyLinkedList<BinaryTreeNode<int>>> lists, int level)
        {
            if (root == null)
                return;

            if (lists.Count <= level)
            {
                lists.Add(new CustomSinglyLinkedList<BinaryTreeNode<int>>());
            }

            var list = lists[level];

            list.Add(root);
            CreateLevelLinkedListsByPreOrderRecursively(root.Left, lists, level + 1);
            CreateLevelLinkedListsByPreOrderRecursively(root.Right, lists, level + 1);
        }

        public List<CustomSinglyLinkedList<BinaryTreeNode<int>>> CreateLevelLinkedListsByLevelOrder(BinaryTreeNode<int>? root)
        {
            var result = new List<CustomSinglyLinkedList<BinaryTreeNode<int>>>();

            var currentLevel = new CustomSinglyLinkedList<BinaryTreeNode<int>>();

            if (root != null)
            {
                currentLevel.Add(root);
            }

            while (currentLevel.GetHead() != null)
            {
                result.Add(currentLevel);

                var parentLevel = currentLevel;
                currentLevel = new CustomSinglyLinkedList<BinaryTreeNode<int>>();

                foreach (var parent in parentLevel)
                {
                    if (parent.Left != null)
                    {
                        currentLevel.Add(parent.Left);
                    }

                    if (parent.Right != null)
                    {
                        currentLevel.Add(parent.Right);
                    }
                }
            }

            return result;
        }
    }

    [TestFixture]
    internal class Task4_3ListOfDepthsTests
    {
        [Test]
        public void CreateLevelLinkedListsByPreOrderTest()
        {
            // arrange
            var sut = new Task4_3ListOfDepths();
            var tree = CreateTreeUnderTest();

            // act
            var lists = sut.CreateLevelLinkedListsByPreOrder(tree);

            // assert
            var levelZeroHead = lists[0].GetHead()!;
            levelZeroHead.Value.Value.Should().Be(0);
            levelZeroHead.Next.Should().BeNull();

            var levelOneHead = lists[1].GetHead()!;
            levelOneHead.Value.Value.Should().Be(1);
            levelOneHead.Next!.Value.Value.Should().Be(2);
            levelOneHead.Next.Next.Should().BeNull();

            var levelTwoHead = lists[2].GetHead()!;
            levelTwoHead.Value.Value.Should().Be(3);
            levelTwoHead.Next!.Value.Value.Should().Be(4);
            levelTwoHead.Next.Next!.Value.Value.Should().Be(5);
            levelTwoHead.Next.Next.Next!.Value.Value.Should().Be(6);
            levelTwoHead.Next.Next.Next.Next.Should().BeNull();

            var levelThreeHead = lists[3].GetHead()!;
            levelThreeHead.Value.Value.Should().Be(7);
            levelThreeHead.Next.Should().BeNull();
        }

        [Test]
        public void CreateLevelLinkedListsByLevelOrderTest()
        {
            // arrange
            var sut = new Task4_3ListOfDepths();
            var tree = CreateTreeUnderTest();

            // act
            var lists = sut.CreateLevelLinkedListsByLevelOrder(tree);

            // assert
            var levelZeroHead = lists[0].GetHead()!;
            levelZeroHead.Value.Value.Should().Be(0);
            levelZeroHead.Next.Should().BeNull();

            var levelOneHead = lists[1].GetHead()!;
            levelOneHead.Value.Value.Should().Be(1);
            levelOneHead.Next!.Value.Value.Should().Be(2);
            levelOneHead.Next.Next.Should().BeNull();

            var levelTwoHead = lists[2].GetHead()!;
            levelTwoHead.Value.Value.Should().Be(3);
            levelTwoHead.Next!.Value.Value.Should().Be(4);
            levelTwoHead.Next.Next!.Value.Value.Should().Be(5);
            levelTwoHead.Next.Next.Next!.Value.Value.Should().Be(6);
            levelTwoHead.Next.Next.Next.Next.Should().BeNull();

            var levelThreeHead = lists[3].GetHead()!;
            levelThreeHead.Value.Value.Should().Be(7);
            levelThreeHead.Next.Should().BeNull();
        }

        private BinaryTreeNode<int> CreateTreeUnderTest()
        {
            var tree = new BinaryTreeNode<int>(0)
            {
                Left = new BinaryTreeNode<int>(1)
                {
                    Left = new BinaryTreeNode<int>(3)
                    {
                        Left = new BinaryTreeNode<int>(7)
                    },
                    Right = new BinaryTreeNode<int>(4)
                },
                Right = new BinaryTreeNode<int>(2)
                {
                    Left = new BinaryTreeNode<int>(5),
                    Right = new BinaryTreeNode<int>(6)
                }
            };

            return tree;
        }
    }
}
