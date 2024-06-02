using FluentAssertions;
using NUnit.Framework;

namespace DataStructures.Tree.BinarySearchTree
{
    [TestFixture]
    public class CustomBinarySearchTreeTests
    {
        [Test]
        public void BinarySearchTree_InsertNodes_RootAndChildrenCorrect()
        {
            // arrange
            var bst = new CustomBinarySearchTree<int>();

            // act
            //        2
            //       / \
            //      1   4
            //     /   / \
            //    0   3   6
            //           /
            //          5 
            bst.Insert(2);
            bst.Insert(4);
            bst.Insert(6);
            bst.Insert(1);
            bst.Insert(3);
            bst.Insert(5);
            bst.Insert(0);
            var root = bst.GetRoot();

            // assert
            // root
            root!.Value.Should().Be(2);
            
            // depth 1
            var nodeOne = root.Left!;
            var nodeFour = root.Right!;
            nodeOne.Value.Should().Be(1);
            nodeFour.Value.Should().Be(4);
            
            // depth 2
            var nodeZero = nodeOne.Left!;
            nodeZero.Value.Should().Be(0);
            nodeOne.Right.Should().BeNull();

            var nodeThree = nodeFour.Left!;
            var nodeSix = nodeFour.Right!;
            nodeThree.Value.Should().Be(3);
            nodeSix.Value.Should().Be(6);

            // depth 3
            nodeZero.Left.Should().BeNull();
            nodeZero.Right.Should().BeNull();
            nodeThree.Left.Should().BeNull();
            nodeThree.Right.Should().BeNull();
            
            var nodeFive = nodeSix.Left!;
            nodeFive.Value.Should().Be(5);
            nodeSix.Right.Should().BeNull();
        }

        [Test]
        public void BinarySearchTree_InsertNodes_InOrderEnumeration()
        {
            // arrange
            //        2
            //       / \
            //      1   4
            //     /   / \
            //    0   3   6
            //           /
            //          5 
            var bst = new CustomBinarySearchTree<int>();
            bst.Insert(2);
            bst.Insert(4);
            bst.Insert(6);
            bst.Insert(1);
            bst.Insert(3);
            bst.Insert(5);
            bst.Insert(0);

            // act
            var enumerator = bst.GetEnumerator();

            // assert in-order 0,1,2,3,4,5,6
            enumerator.MoveNext();
            enumerator.Current.Should().Be(0);

            enumerator.MoveNext();
            enumerator.Current.Should().Be(1);

            enumerator.MoveNext();
            enumerator.Current.Should().Be(2);

            enumerator.MoveNext();
            enumerator.Current.Should().Be(3);

            enumerator.MoveNext();
            enumerator.Current.Should().Be(4);

            enumerator.MoveNext();
            enumerator.Current.Should().Be(5);

            enumerator.MoveNext();
            enumerator.Current.Should().Be(6);

            var moveBehindLast = enumerator.MoveNext();
            moveBehindLast.Should().BeFalse();

            enumerator.Dispose();
        }

        [Test]
        public void BinarySearchTree_InsertNodes_PreOrderEnumeration()
        {
            // arrange
            //        2
            //       / \
            //      1   4
            //     /   / \
            //    0   3   6
            //           /
            //          5 
            var bst = new CustomBinarySearchTree<int>();
            bst.Insert(2);
            bst.Insert(4);
            bst.Insert(6);
            bst.Insert(1);
            bst.Insert(3);
            bst.Insert(5);
            bst.Insert(0);

            // act
            var enumerator = bst.GetPreOrderEnumerator();

            // assert pre-order 2,1,0,4,3,6,5
            enumerator.MoveNext();
            enumerator.Current.Should().Be(2);

            enumerator.MoveNext();
            enumerator.Current.Should().Be(1);

            enumerator.MoveNext();
            enumerator.Current.Should().Be(0);

            enumerator.MoveNext();
            enumerator.Current.Should().Be(4);

            enumerator.MoveNext();
            enumerator.Current.Should().Be(3);

            enumerator.MoveNext();
            enumerator.Current.Should().Be(6);

            enumerator.MoveNext();
            enumerator.Current.Should().Be(5);

            var moveBehindLast = enumerator.MoveNext();
            moveBehindLast.Should().BeFalse();

            enumerator.Dispose();
        }

        [Test]
        public void BinarySearchTree_InsertNodes_PostOrderEnumeration()
        {
            // arrange
            //        2
            //       / \
            //      1   4
            //     /   / \
            //    0   3   6
            //           /
            //          5 
            var bst = new CustomBinarySearchTree<int>();
            bst.Insert(2);
            bst.Insert(4);
            bst.Insert(6);
            bst.Insert(1);
            bst.Insert(3);
            bst.Insert(5);
            bst.Insert(0);

            // act
            var enumerator = bst.GetPostOrderEnumerator();

            // assert post-order 0,1,3,5,6,4,2
            enumerator.MoveNext();
            enumerator.Current.Should().Be(0);

            enumerator.MoveNext();
            enumerator.Current.Should().Be(1);

            enumerator.MoveNext();
            enumerator.Current.Should().Be(3);

            enumerator.MoveNext();
            enumerator.Current.Should().Be(5);

            enumerator.MoveNext();
            enumerator.Current.Should().Be(6);

            enumerator.MoveNext();
            enumerator.Current.Should().Be(4);

            enumerator.MoveNext();
            enumerator.Current.Should().Be(2);

            var moveBehindLast = enumerator.MoveNext();
            moveBehindLast.Should().BeFalse();

            enumerator.Dispose();
        }

        [Test]
        public void BinarySearchTree_InsertNodes_LevelOrderEnumeration()
        {
            // arrange
            //        2
            //       / \
            //      1   4
            //     /   / \
            //    0   3   6
            //           /
            //          5 
            var bst = new CustomBinarySearchTree<int>();
            bst.Insert(2);
            bst.Insert(4);
            bst.Insert(6);
            bst.Insert(1);
            bst.Insert(3);
            bst.Insert(5);
            bst.Insert(0);

            // act
            var enumerator = bst.GetLevelOrderEnumerator();

            // assert post-order 2,1,4,0,3,6,5
            enumerator.MoveNext();
            enumerator.Current.Should().Be(2);

            enumerator.MoveNext();
            enumerator.Current.Should().Be(1);

            enumerator.MoveNext();
            enumerator.Current.Should().Be(4);

            enumerator.MoveNext();
            enumerator.Current.Should().Be(0);

            enumerator.MoveNext();
            enumerator.Current.Should().Be(3);

            enumerator.MoveNext();
            enumerator.Current.Should().Be(6);

            enumerator.MoveNext();
            enumerator.Current.Should().Be(5);

            var moveBehindLast = enumerator.MoveNext();
            moveBehindLast.Should().BeFalse();

            enumerator.Dispose();
        }

        [Test]
        public void BinarySearchTree_InsertNodes_Search()
        {
            // arrange
            //        2
            //       / \
            //      1   4
            //     /   / \
            //    0   3   6
            //           /
            //          5 
            var bst = new CustomBinarySearchTree<int>();
            bst.Insert(2);
            bst.Insert(4);
            bst.Insert(6);
            bst.Insert(1);
            bst.Insert(3);
            bst.Insert(5);
            bst.Insert(0);

            // act
            var twoNode = bst.Search(2);
            var fourNode = bst.Search(4);
            var sixNode = bst.Search(6);
            var oneNode = bst.Search(1);
            var threeNode = bst.Search(3);
            var fiveNode = bst.Search(5);
            var zeroNode = bst.Search(0);
            var nonExistentNode = bst.Search(99);

            // assert
            twoNode.Result!.Value.Should().Be(2);
            fourNode.Result!.Value.Should().Be(4);
            sixNode.Result!.Value.Should().Be(6);
            oneNode.Result!.Value.Should().Be(1);
            threeNode.Result!.Value.Should().Be(3);
            fiveNode.Result!.Value.Should().Be(5);
            zeroNode.Result!.Value.Should().Be(0);
            nonExistentNode.Result.Should().BeNull();
        }

        [Test]
        public void BinarySearchTree_InsertNodes_RemoveLeafNodesUpToRoot()
        {
            // arrange
            //        2
            //       / \
            //      1   4
            //     /   / \
            //    0   3   6
            //           /
            //          5 
            var bst = new CustomBinarySearchTree<int>();
            bst.Insert(2);
            bst.Insert(4);
            bst.Insert(6);
            bst.Insert(1);
            bst.Insert(3);
            bst.Insert(5);
            bst.Insert(0);

            // act
            //    1
            //   /
            //  0
            bst.Remove(5);
            bst.Remove(3);
            bst.Remove(6);
            bst.Remove(4);
            bst.Remove(2);
            var root = bst.GetRoot()!;

            // assert
            root.Value.Should().Be(1);
            root.Left!.Value.Should().Be(0);
        }

        [Test]
        public void BinarySearchTree_InsertNodes_RemoveNodesWithOneSubtree()
        {
            // arrange
            //        2
            //       / \
            //      1   3
            //     /     \
            //    0       4
            var bst = new CustomBinarySearchTree<int>();
            bst.Insert(2);
            bst.Insert(3);
            bst.Insert(4);
            bst.Insert(1);
            bst.Insert(0);

            // act
            //    2
            //   / \
            //  0   4
            bst.Remove(3);
            bst.Remove(1);
            var root = bst.GetRoot()!;

            // assert
            root.Value.Should().Be(2);
            root.Left!.Value.Should().Be(0);
            root.Left!.Left.Should().BeNull();
            root.Left!.Right.Should().BeNull();

            root.Right!.Value.Should().Be(4);
            root.Right!.Left.Should().BeNull();
            root.Right!.Right.Should().BeNull();
        }

        [Test]
        public void BinarySearchTree_InsertNodes_RemoveNonExistentNode()
        {
            // arrange
            //    2
            //   / \
            //  1   3
            var bst = new CustomBinarySearchTree<int>();
            bst.Insert(2);
            bst.Insert(3);
            bst.Insert(1);

            // act
            var removeResult = bst.Remove(0);
            var root = bst.GetRoot()!;

            // assert
            removeResult.Should().BeFalse();
            root.Value.Should().Be(2);
            root.Left!.Value.Should().Be(1);
            root.Left!.Left.Should().BeNull();
            root.Left!.Right.Should().BeNull();

            root.Right!.Value.Should().Be(3);
            root.Right!.Left.Should().BeNull();
            root.Right!.Right.Should().BeNull();
        }

        [Test]
        public void BinarySearchTree_InsertNodes_RemoveRootWithTwoSubtrees()
        {
            // arrange
            //        4
            //       / \
            //      1   5
            //     / \   \
            //    0   3   6
            //       /
            //      2
            var bst = new CustomBinarySearchTree<int>();
            bst.Insert(4);
            bst.Insert(5);
            bst.Insert(6);
            bst.Insert(1);
            bst.Insert(3);
            bst.Insert(2);
            bst.Insert(0);

            // act
            //        3
            //       / \
            //      1   5
            //     / \   \
            //    0   2   6
            bst.Remove(4);
            var root = bst.GetRoot()!;

            // assert
            // root
            root.Value.Should().Be(3);

            // depth 1
            var nodeOne = root.Left!;
            var nodeFive = root.Right!;
            nodeOne.Value.Should().Be(1);
            nodeFive.Value.Should().Be(5);

            // depth 2
            var nodeZero = nodeOne.Left!;
            var nodeTwo = nodeOne.Right!;
            nodeZero.Value.Should().Be(0);
            nodeTwo.Value.Should().Be(2);

            var nodeSix = nodeFive.Right!;
            nodeSix.Value.Should().Be(6);

            // depth 3
            nodeZero.Left.Should().BeNull();
            nodeZero.Right.Should().BeNull();
            nodeTwo.Left.Should().BeNull();
            nodeTwo.Right.Should().BeNull();
            nodeSix.Left.Should().BeNull();
            nodeSix.Right.Should().BeNull();
        }
    }
}
