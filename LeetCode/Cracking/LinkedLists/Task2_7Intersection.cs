using FluentAssertions;
using NUnit.Framework;

namespace LeetCode.Cracking.LinkedLists
{
    /// <summary>
    /// Given two singly linked lists, determine of the two lists intersect.
    /// Return the intersecting node.
    ///
    /// Intersection is defined based on reference.
    ///
    /// Solutions:
    /// * hashset
    /// * check if last node is common and backtrack
    /// </summary>
    internal class Task2_7Intersection
    {
        public CrackingLinkedListNode? FindIntersectionThroughHashSet(CrackingLinkedListNode? first, CrackingLinkedListNode? second)
        {
            var hashSet = new HashSet<CrackingLinkedListNode>();

            var currentFirst = first;
            while (currentFirst != null)
            {
                hashSet.Add(currentFirst);
                currentFirst = currentFirst.Next;
            }

            var currentSecond = second;
            while (currentSecond != null)
            {
                if (!hashSet.Add(currentSecond))
                {
                    return currentSecond;
                }

                currentSecond = currentSecond.Next;
            }

            return null;
        }

        /// <summary>
        /// There must be an intersection if the last node of both lists is the same.
        /// In that case we're interested in the common path. This common path is at most as long as the longest common path. For that reason, we trim the longer list.
        ///
        /// It is not relevant in which of the two lists we identify the common node.
        /// </summary>
        public CrackingLinkedListNode? FindIntersectionThroughLastNode(CrackingLinkedListNode? first, CrackingLinkedListNode? second)
        {
            if (first == null || second == null)
                return null;

            var firstLengthAndTail = GetLengthAndTail(first);
            var secondLengthAndTail = GetLengthAndTail(second);

            if (firstLengthAndTail.Tail != secondLengthAndTail.Tail)
                return null;

            // the last item is the same so there is an intersection
            var longer = firstLengthAndTail.Length > secondLengthAndTail.Length ? first : second;
            var shorter = longer == first ? second : first;
            longer = GetKthNode(longer, Math.Abs(firstLengthAndTail.Length - secondLengthAndTail.Length));

            while (shorter != longer)
            {
                shorter = shorter.Next;
                longer = longer.Next;
            }

            return shorter;
        }

        private (int Length, CrackingLinkedListNode? Tail) GetLengthAndTail(CrackingLinkedListNode? head)
        {
            var result = 0;
            CrackingLinkedListNode? previous = null;

            while (head != null)
            {
                result++;
                previous = head;
                head = head.Next;
            }

            return (result, previous);
        }

        private CrackingLinkedListNode? GetKthNode(CrackingLinkedListNode head, int k)
        {
            var current = head;
            for (var i = 0; i < k; i++)
            {
                current = current?.Next;
            }

            return current;
        }
    }

    [TestFixture]
    public class Task2_7IntersectionTests
    {
        [Test]
        public void FindIntersectionThroughHashSet_IntersectionExists()
        {
            // arrange
            var sut = new Task2_7Intersection();
            var first = CrackingLinkedListProvider.CreateLinkedListWithContent(new List<int> { 3, 1, 5, 9, 7, 2, 1 });
            var second = CrackingLinkedListProvider.CreateLinkedListWithContent(new List<int> { 4, 6 });
            second.Next!.Next = first.Next!.Next!.Next!.Next;

            // act
            var result = sut.FindIntersectionThroughHashSet(first, second);

            // assert
            result.Should().Be(second.Next.Next);
        }

        [Test]
        public void FindIntersectionThroughHashSet_IntersectionDoesNotExist()
        {
            // arrange
            var sut = new Task2_7Intersection();
            var first = CrackingLinkedListProvider.CreateLinkedListWithContent(new List<int> { 3, 1, 5, 9, 7, 2, 1 });
            var second = CrackingLinkedListProvider.CreateLinkedListWithContent(new List<int> { 4, 6, 7, 2, 1 });

            // act
            var result = sut.FindIntersectionThroughHashSet(first, second);

            // assert
            result.Should().BeNull();
        }

        [Test]
        public void FindIntersectionThroughLastNode_IntersectionExists()
        {
            // arrange
            var sut = new Task2_7Intersection();
            var first = CrackingLinkedListProvider.CreateLinkedListWithContent(new List<int> { 3, 1, 5, 9, 7, 2, 1 });
            var second = CrackingLinkedListProvider.CreateLinkedListWithContent(new List<int> { 4, 6 });
            second.Next!.Next = first.Next!.Next!.Next!.Next;

            // act
            var result = sut.FindIntersectionThroughLastNode(first, second);

            // assert
            result.Should().Be(second.Next.Next);
        }

        [Test]
        public void FindIntersectionThroughLastNode_IntersectionDoesNotExist()
        {
            // arrange
            var sut = new Task2_7Intersection();
            var first = CrackingLinkedListProvider.CreateLinkedListWithContent(new List<int> { 3, 1, 5, 9, 7, 2, 1 });
            var second = CrackingLinkedListProvider.CreateLinkedListWithContent(new List<int> { 4, 6, 7, 2, 1 });

            // act
            var result = sut.FindIntersectionThroughLastNode(first, second);

            // assert
            result.Should().BeNull();
        }
    }
}
