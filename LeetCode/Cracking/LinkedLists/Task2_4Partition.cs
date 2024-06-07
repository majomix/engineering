using FluentAssertions;
using NUnit.Framework;

namespace LeetCode.Cracking.LinkedLists
{
    /// <summary>
    /// Write code to partition a linked list around a value x, such that all nodes less than x come before all nodes greater than or equal to x.
    ///
    /// Example:
    /// 3 -> 5 -> 8 -> 5 -> 10 -> 2 -> 1
    /// partition 5
    /// 3 -> 2 -> 1 -> 5 -> 5 -> 10 -> 8
    ///
    /// Questions to ask:
    /// * Stable?
    /// </summary>
    internal class Task2_4Partition
    {
        public CrackingLinkedListNode Partition(CrackingLinkedListNode head, int partitionValue)
        {
            var newHead = head;
            var newTail = head;

            var current = head;
            while (current != null)
            {
                var next = current.Next;

                if (current.Data < partitionValue)
                {
                    current.Next = newHead;
                    newHead = current;
                }
                else
                {
                    newTail.Next = current;
                    newTail = current;
                }

                current = next;
            }

            newTail.Next = null;

            return newHead;
        }
    }

    [TestFixture]
    public class Task2_4PartitionTests
    {
        [Test]
        public void RemoveDuplicatesTest()
        {
            // arrange
            var sut = new Task2_4Partition();
            var linkedList = CrackingLinkedListProvider.CreateLinkedListWithReverseDepth(8);

            // act
            var result = sut.Partition(linkedList, 4);

            // assert
            result.Data.Should().Be(0);
            result.Next.Data.Should().Be(1);
            result.Next.Next.Data.Should().Be(2);
            result.Next.Next.Next.Data.Should().Be(3);
            result.Next.Next.Next.Next.Data.Should().Be(8);
            result.Next.Next.Next.Next.Next.Data.Should().Be(7);
            result.Next.Next.Next.Next.Next.Next.Data.Should().Be(6);
            result.Next.Next.Next.Next.Next.Next.Next.Data.Should().Be(5);
            result.Next.Next.Next.Next.Next.Next.Next.Next.Data.Should().Be(4);
        }
    }
}
