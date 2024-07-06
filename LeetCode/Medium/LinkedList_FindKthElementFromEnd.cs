using FluentAssertions;
using LeetCode.Cracking.LinkedLists;
using NUnit.Framework;

namespace LeetCode.Medium
{
    /// <summary>
    /// Given a linked list, find the k-th element from the end.
    ///
    /// Discussion:
    /// Two passes -> find length in first pass, count elements in the second pass.
    /// Two passes -> move one pointer k elements ahead and keep advancing both pointers by 1. Once the ahead pointer reaches end, the other is k elements from the end.
    /// 
    /// Solutions:
    /// * two pointers
    /// </summary>
    internal class LinkedList_FindKthElementFromEnd
    {
        public CrackingLinkedListNode? FindKthElementOfLinkedList(CrackingLinkedListNode? head, int k)
        {
            var ahead = head;
            var behind = head;

            for (var i = 0; i < k; i++)
            {
                ahead = ahead?.Next;
            }

            if (ahead == null)
                return null;

            while (ahead.Next != null)
            {
                ahead = ahead.Next;
                behind = behind!.Next;
            }

            return behind;
        }
    }

    [TestFixture]
    public class LinkedList_FindKthElementFromEndTests
    {
        private static object[] testCases =
        {
            new object[] { new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 2, 7 },
            new object[] { new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 7, 2 },
            new object[] { new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 0, 9 },
            new object[] { new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 9, 0 }
        };

        [TestCaseSource(nameof(testCases))]
        public void FindKthElementOfLinkedListTests_KthElementInRange(List<int> input, int k, int expectedResult)
        {
            // arrange
            var sut = new LinkedList_FindKthElementFromEnd();
            var list = CrackingLinkedListProvider.CreateLinkedListWithContent(input);

            // act
            var result = sut.FindKthElementOfLinkedList(list, k);

            // assert
            result!.Data.Should().Be(expectedResult);
        }

        [Test]
        public void FindKthElementOfLinkedListTests_KthElementOutOfRange()
        {
            // arrange
            var sut = new LinkedList_FindKthElementFromEnd();
            var input = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var kOutOfRange = input.Count;
            var list = CrackingLinkedListProvider.CreateLinkedListWithContent(input);

            // act
            var result = sut.FindKthElementOfLinkedList(list, kOutOfRange);

            // assert
            result.Should().Be(null);
        }
    }
}
