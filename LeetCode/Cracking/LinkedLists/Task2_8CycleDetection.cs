using System.Runtime.Serialization.Formatters;
using FluentAssertions;
using NUnit.Framework;

namespace LeetCode.Cracking.LinkedLists
{
    /// <summary>
    /// Given a linked list which might contain a loop, implement an algorithm that detects the loop.
    ///
    /// Solutions:
    /// * hashset
    /// * tortoise & hare
    /// </summary>
    internal class Task2_8CycleDetection
    {
        public bool ContainsCycleThroughHashSet(CrackingLinkedListNode? head)
        {
            var hashSet = new HashSet<CrackingLinkedListNode>();

            var current = head;
            
            while (current != null)
            {
                if (!hashSet.Add(current))
                {
                    return true;
                }

                current = current.Next;
            }

            return false;
        }

        public bool ContainsCycleThroughFloyd(CrackingLinkedListNode? head)
        {
            var tortoise = head;
            var hare = head;

            while (hare != null && hare.Next != null)
            {
                tortoise = tortoise!.Next;
                hare = hare.Next.Next;

                if (tortoise == hare)
                {
                    return true;
                }
            }

            return false;
        }
    }

    [TestFixture]
    public class Task2_8CycleDetectionTests
    {
        [Test]
        public void ContainsCycleThroughHashSet_ContainsCycle()
        {
            // arrange
            var sut = new Task2_8CycleDetection();
            var list = CrackingLinkedListProvider.CreateLinkedListWithContent(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            var nodeFive = list.Next!.Next!.Next!.Next!.Next!;
            var nodeNine = nodeFive.Next!.Next!.Next!.Next!;
            nodeNine.Next = nodeFive;

            // act
            var result = sut.ContainsCycleThroughHashSet(list);

            // assert
            result.Should().BeTrue();
        }


        [Test]
        public void ContainsCycleThroughHashSet_DoesNotContainsCycle()
        {
            // arrange
            var sut = new Task2_8CycleDetection();
            var list = CrackingLinkedListProvider.CreateLinkedListWithContent(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 });

            // act
            var result = sut.ContainsCycleThroughHashSet(list);

            // assert
            result.Should().BeFalse();
        }

        [Test]
        public void ContainsCycleThroughFloyd_ContainsCycle()
        {
            // arrange
            var sut = new Task2_8CycleDetection();
            var list = CrackingLinkedListProvider.CreateLinkedListWithContent(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            var nodeFive = list.Next!.Next!.Next!.Next!.Next!;
            var nodeNine = nodeFive.Next!.Next!.Next!.Next!;
            nodeNine.Next = nodeFive;

            // act
            var result = sut.ContainsCycleThroughFloyd(list);

            // assert
            result.Should().BeTrue();
        }


        [Test]
        public void ContainsCycleThroughFloyd_DoesNotContainsCycle()
        {
            // arrange
            var sut = new Task2_8CycleDetection();
            var list = CrackingLinkedListProvider.CreateLinkedListWithContent(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 });

            // act
            var result = sut.ContainsCycleThroughFloyd(list);

            // assert
            result.Should().BeFalse();
        }
    }
}
