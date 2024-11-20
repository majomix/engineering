using DataStructures.Tree.BinarySearchTree;
using FluentAssertions;
using NUnit.Framework;

namespace LeetCode.Cracking.TreesAndGraphs
{
    /// <summary>
    /// Design an algorithm to find the first common ancestor of two nodes in a binary tree.
    /// Avoid storing additional nodes in a data structure.
    ///
    /// Discussion:
    /// Is a node in a tree considered its own ancestor?
    /// - In this implementation it's not.
    /// 
    /// Is parent link available?
    /// - Let's consider both cases.
    /// 
    /// Solutions:
    /// 1. Parent link is available.
    /// a) Find depth of both nodes. Iterate upwards from the deeper node to the level of the shallower node. Then move both upwards together and check for equality.
    ///    O(d) for d = depth of the deeper node.
    /// b) Iterate upwards from deeper node and always check newly uncovered subtree.
    ///    O(n) worst case.
    /// 
    /// 2. Parent link is not available.
    /// Start from root and recursively check subtrees.
    /// </summary>
    internal class Task4_8FirstCommonAncestor
    {
        public BinaryTreeNode<int>? GetFirstCommonAncestor(BinaryTreeNode<int> root, BinaryTreeNode<int> firstNode, BinaryTreeNode<int> secondNode)
        {
            return IsCommonAncestor(root, firstNode, secondNode).CommonAncestor;
        }

        private CommonAncestorSearchResult IsCommonAncestor(BinaryTreeNode<int>? root, BinaryTreeNode<int> firstNode, BinaryTreeNode<int> secondNode)
        {
            if (root == null)
                return new CommonAncestorSearchResult { CommonAncestor = null, HasFirstNode = false, HasSecondNode = false };

            var resultLeft = IsCommonAncestor(root.Left, firstNode, secondNode);
            var resultRight = IsCommonAncestor(root.Right, firstNode, secondNode);

            if (resultLeft.CommonAncestor != null)
                return resultLeft;

            if (resultRight.CommonAncestor != null)
                return resultRight;

            var hasFirstNode = resultLeft.HasFirstNode || resultRight.HasFirstNode || root.Left == firstNode || root.Right == firstNode;
            var hasSecondNode = resultLeft.HasSecondNode || resultRight.HasSecondNode || root.Left == secondNode || root.Right == secondNode;
            var commonAncestor = hasFirstNode && hasSecondNode ? root : null;

            return new CommonAncestorSearchResult { HasFirstNode = hasFirstNode, HasSecondNode = hasSecondNode, CommonAncestor = commonAncestor };
        }

        public BinaryTreeNodeWithParentLink<int>? GetFirstCommonAncestor(BinaryTreeNodeWithParentLink<int> firstNode, BinaryTreeNodeWithParentLink<int> secondNode)
        {
            var firstDepth = GetDepth(firstNode);
            var secondDepth = GetDepth(secondNode);
            
            var deeper = firstDepth > secondDepth ? firstNode : secondNode;
            var shallower = firstDepth > secondDepth ? secondNode : firstNode;

            var difference = Math.Abs(firstDepth - secondDepth);
            for (var i = 0; i < difference; i++)
            {
                deeper = deeper.Parent;
            }

            while (deeper != null)
            {
                deeper = deeper.Parent;
                shallower = shallower.Parent;

                if (shallower == deeper)
                {
                    return shallower;
                }
            }

            return null;
        }

        public BinaryTreeNodeWithParentLink<int>? GetFirstCommonAncestorByCheckingUncoveredSubtrees(
            BinaryTreeNodeWithParentLink<int> root,
            BinaryTreeNodeWithParentLink<int> firstNode,
            BinaryTreeNodeWithParentLink<int> secondNode)
        {
            if (!IsNodeInSubtree(root, firstNode) || !IsNodeInSubtree(root, secondNode))
                return null;

            if (IsNodeInSubtree(firstNode, secondNode))
                return firstNode.Parent;

            if (IsNodeInSubtree(secondNode, firstNode))
                return secondNode.Parent;

            var sibling = GetSibling(firstNode);
            var parent = firstNode.Parent;
            while (!IsNodeInSubtree(sibling, secondNode))
            {
                sibling = GetSibling(parent);
                parent = parent.Parent;
            }

            return parent;
        }

        private bool IsNodeInSubtree(BinaryTreeNodeWithParentLink<int>? root, BinaryTreeNodeWithParentLink<int>? node)
        {
            if (root == null || node == null)
                return false;

            if (root == node)
                return true;

            return IsNodeInSubtree(root.Left, node) || IsNodeInSubtree(root.Right, node);
        }

        private BinaryTreeNodeWithParentLink<int>? GetSibling(BinaryTreeNodeWithParentLink<int>? node)
        {
            if (node?.Parent == null)
                return null;

            var parent = node.Parent;
            return parent.Left == node ? parent.Right : parent.Left;
        }

        private int GetDepth(BinaryTreeNodeWithParentLink<int>? root)
        {
            int depth = 0;
            while (root != null)
            {
                root = root.Parent;
                depth++;
            }

            return depth;
        }
    }

    public class CommonAncestorSearchResult
    {
        public bool HasFirstNode;
        public bool HasSecondNode;
        public BinaryTreeNode<int>? CommonAncestor;
    }

    [TestFixture]
    internal class Task4_8FirstCommonAncestorTests
    {
        [Test]
        public void FirstCommonAncestorWithoutParentLinkTest()
        {
            // arrange
            var sut = new Task4_8FirstCommonAncestor();
            var tree = CreateTreeUnderTest();

            // act
            sut.GetFirstCommonAncestor(tree, tree.Left.Left.Left, tree.Left.Left.Right).Should().Be(tree.Left.Left);
            sut.GetFirstCommonAncestor(tree, tree.Left.Left.Left, tree.Left).Should().Be(tree);
            sut.GetFirstCommonAncestor(tree, tree.Left, tree.Right).Should().Be(tree);
            sut.GetFirstCommonAncestor(tree, tree.Left.Left.Left, tree.Right.Right).Should().Be(tree);
            sut.GetFirstCommonAncestor(tree, tree.Left.Left, tree.Left.Right).Should().Be(tree.Left);
            sut.GetFirstCommonAncestor(tree, tree.Right.Left, tree.Right.Right).Should().Be(tree.Right);
            sut.GetFirstCommonAncestor(tree, tree.Right, tree.Right.Left).Should().Be(tree);
            sut.GetFirstCommonAncestor(tree, new BinaryTreeNode<int>(12), tree.Right.Right).Should().Be(null);
        }

        private BinaryTreeNode<int> CreateTreeUnderTest()
        {
            var tree = new BinaryTreeNode<int>(2)
            {
                Left = new BinaryTreeNode<int>(5)
                {
                    Left = new BinaryTreeNode<int>(7)
                    {
                        Left = new BinaryTreeNode<int>(1)
                    },
                    Right = new BinaryTreeNode<int>(3)
                },
                Right = new BinaryTreeNode<int>(8)
                {
                    Left = new BinaryTreeNode<int>(9),
                    Right = new BinaryTreeNode<int>(4)
                }
            };

            return tree;
        }

        [Test]
        public void FirstCommonAncestorWithParentLinkTest()
        {
            // arrange
            var sut = new Task4_8FirstCommonAncestor();
            var tree = CreateTreeUnderTestWithParentLink();

            // act
            sut.GetFirstCommonAncestor(tree.Left.Left.Left, tree.Left.Left.Right).Should().Be(tree.Left.Left);
            sut.GetFirstCommonAncestor(tree.Left.Left.Left, tree.Left).Should().Be(tree);
            sut.GetFirstCommonAncestor(tree.Left, tree.Right).Should().Be(tree);
            sut.GetFirstCommonAncestor(tree.Left.Left.Left, tree.Right.Right).Should().Be(tree);
            sut.GetFirstCommonAncestor(tree.Left.Left, tree.Left.Right).Should().Be(tree.Left);
            sut.GetFirstCommonAncestor(tree.Right.Left, tree.Right.Right).Should().Be(tree.Right);
            sut.GetFirstCommonAncestor(tree.Right, tree.Right.Left).Should().Be(tree);
            sut.GetFirstCommonAncestor(new BinaryTreeNodeWithParentLink<int>(12, new BinaryTreeNodeWithParentLink<int>(13, null)), tree.Right.Right).Should().Be(null);
        }

        [Test]
        public void FirstCommonAncestorWithParentLinkByCheckingUncoveredSubtreesTest()
        {
            // arrange
            var sut = new Task4_8FirstCommonAncestor();
            var tree = CreateTreeUnderTestWithParentLink();

            // act
            sut.GetFirstCommonAncestorByCheckingUncoveredSubtrees(tree, tree.Left.Left.Left, tree.Left.Left.Right).Should().Be(tree.Left.Left);
            sut.GetFirstCommonAncestorByCheckingUncoveredSubtrees(tree, tree.Left.Left.Left, tree.Left).Should().Be(tree);
            sut.GetFirstCommonAncestorByCheckingUncoveredSubtrees(tree, tree.Left, tree.Right).Should().Be(tree);
            sut.GetFirstCommonAncestorByCheckingUncoveredSubtrees(tree, tree.Left.Left.Left, tree.Right.Right).Should().Be(tree);
            sut.GetFirstCommonAncestorByCheckingUncoveredSubtrees(tree, tree.Left.Left, tree.Left.Right).Should().Be(tree.Left);
            sut.GetFirstCommonAncestorByCheckingUncoveredSubtrees(tree, tree.Right.Left, tree.Right.Right).Should().Be(tree.Right);
            sut.GetFirstCommonAncestorByCheckingUncoveredSubtrees(tree, tree.Right, tree.Right.Left).Should().Be(tree);
            sut.GetFirstCommonAncestorByCheckingUncoveredSubtrees(tree, new BinaryTreeNodeWithParentLink<int>(12, new BinaryTreeNodeWithParentLink<int>(13, null)), tree.Right.Right).Should().Be(null);
        }

        //           2
        //         /   \
        //        5     8
        //       / \   / \
        //     7    3 9   4
        //    / \
        //   1   6
        private BinaryTreeNodeWithParentLink<int> CreateTreeUnderTestWithParentLink()
        {
            var root = new BinaryTreeNodeWithParentLink<int>(2, null);

            // level 1
            var nodeFive = new BinaryTreeNodeWithParentLink<int>(5, root);
            root.Left = nodeFive;

            var nodeEight = new BinaryTreeNodeWithParentLink<int>(8, root);
            root.Right = nodeEight;

            // level 2
            var nodeSeven = new BinaryTreeNodeWithParentLink<int>(7, nodeFive);
            nodeFive.Left = nodeSeven;

            var nodeThree = new BinaryTreeNodeWithParentLink<int>(3, nodeFive);
            nodeFive.Right = nodeThree;

            var nodeNine = new BinaryTreeNodeWithParentLink<int>(9, nodeEight);
            nodeEight.Left = nodeNine;
            
            var nodeFour = new BinaryTreeNodeWithParentLink<int>(4, nodeEight);
            nodeEight.Right = nodeFour;

            // level 3
            var nodeOne = new BinaryTreeNodeWithParentLink<int>(1, nodeSeven);
            nodeSeven.Left = nodeOne;

            var nodeSix = new BinaryTreeNodeWithParentLink<int>(6, nodeSeven);
            nodeSeven.Right = nodeSix;

            return root;
        }
    }
}
