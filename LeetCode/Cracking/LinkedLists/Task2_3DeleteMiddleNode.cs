using FluentAssertions;
using NUnit.Framework;

namespace LeetCode.Cracking.LinkedLists
{
    /// <summary>
    /// Implement an algorithm to delete a node in the middle (i.e. any node but the first and last node)
    /// of a singly linked list, given only access to that node.
    ///
    /// Example:
    /// a -> b -> c -> d -> e -> f
    /// a -> b -> d -> e -> f
    /// 
    /// Discussion:
    /// Without previous node, it is impossible to delete the node because re-linking would be impossible.
    /// The only thing that is possible is copy content from the next one and delete it.
    /// If last node is to be deleted without link to the previous node, this is not possible.
    /// </summary>
    internal class Task2_3DeleteMiddleNode
    {
        public bool DeleteMiddleNode(CrackingLinkedListNode? nodeToRemove)
        {
            if (nodeToRemove?.Next == null)
                return false;

            var next = nodeToRemove.Next;
            nodeToRemove.Data = next.Data;
            nodeToRemove.Next = next.Next;

            return true;
        }
    }

    [TestFixture]
    public class Task2_3DeleteMiddleNodeTests
    {
        [Test]
        public void RemoveDuplicatesTest()
        {
            // arrange
            var sut = new Task2_3DeleteMiddleNode();
            var linkedList = CrackingLinkedListProvider.CreateLinkedListWithDepth(2);

            // act
            var result = sut.DeleteMiddleNode(linkedList.Next);

            // assert
            result.Should().BeTrue();
            linkedList.Data.Should().Be(0);
            linkedList.Next!.Data.Should().Be(2);
            linkedList.Next.Next.Should().BeNull();
        }
    }
}
