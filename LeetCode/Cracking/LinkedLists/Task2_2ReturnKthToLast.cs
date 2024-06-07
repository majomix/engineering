using FluentAssertions;
using NUnit.Framework;

namespace LeetCode.Cracking.LinkedLists
{
    /// <summary>
    /// Implement an algorithm to find the kth to last element of a singly linked list.
    ///
    /// Solution:
    /// * recursion
    /// * iteratively - place runner pointer k elements ahead
    /// </summary>
    internal class Task2_2ReturnKthToLast
    {
        public CrackingLinkedListNode? FindKthElementRecursively(CrackingLinkedListNode? head, int k)
        {
            var index = 0;
            return FindKthElementRecursivelyInner(head, k, ref index);
        }

        private CrackingLinkedListNode? FindKthElementRecursivelyInner(CrackingLinkedListNode? node, int k, ref int currentLevel)
        {
            if (node == null)
                return null;

            var result = FindKthElementRecursivelyInner(node.Next, k, ref currentLevel);

            if (currentLevel == k)
            {
                result = node;
            }

            currentLevel += 1;

            return result;
        }

        public CrackingLinkedListNode? FindKthElementIteratively(CrackingLinkedListNode? head, int k)
        {
            if (head == null)
                return null;

            var behind = head;
            var ahead = head;

            for (var i = 0; i < k; i++)
            {
                if (ahead.Next == null)
                    return null;

                ahead = ahead.Next;
            }

            while (ahead.Next != null)
            {
                behind = behind.Next!;
                ahead = ahead.Next;
            }

            return behind;
        }
    }

    [TestFixture]
    public class Task2_2ReturnKthToLastTests
    {
        private static object[] testCases =
        {
            new object[] { 10, 0, 10 },
            new object[] { 1, 0, 1 },
            new object[] { 10, 2, 8 }
        };

        [TestCaseSource(nameof(testCases))]
        public void FindKthElementRecursivelyTest(int depth, int k, int expectedValue)
        {
            // arrange
            var sut = new Task2_2ReturnKthToLast();
            var linkedList = CrackingLinkedListProvider.CreateLinkedListWithDepth(depth);

            // act
            var result = sut.FindKthElementRecursively(linkedList, k)!;

            // assert
            result.Data.Should().Be(expectedValue);
        }

        [TestCaseSource(nameof(testCases))]
        public void FindKthElementIterativelyTest(int depth, int k, int expectedValue)
        {
            // arrange
            var sut = new Task2_2ReturnKthToLast();
            var linkedList = CrackingLinkedListProvider.CreateLinkedListWithDepth(depth);

            // act
            var result = sut.FindKthElementIteratively(linkedList, k)!;

            // assert
            result.Data.Should().Be(expectedValue);
        }
    }
}
