using FluentAssertions;
using NUnit.Framework;

namespace LeetCode.Cracking.LinkedLists
{
    /// <summary>
    /// Implement a function to check if a linked list is a palindrome.
    ///
    /// Solution:
    /// * create a reversed list and compare
    /// * two pointers
    /// * recursion
    /// </summary>
    internal class Task2_6Palindrome
    {
        public bool IsPalindromeByReversal(CrackingLinkedListNode? head)
        {
            var reversed = Reverse(head);

            var current = head;
            var currentReversed = reversed;

            while (current != null)
            {
                if (currentReversed == null || current.Data != currentReversed.Data)
                    return false;

                currentReversed = currentReversed.Next;
                current = current.Next;
            }

            return true;
        }

        private CrackingLinkedListNode? Reverse(CrackingLinkedListNode? head)
        {
            CrackingLinkedListNode? reversedHead = null;

            var current = head;
            while (current != null)
            {
                var reversedNode = new CrackingLinkedListNode(current.Data);
                reversedNode.Next = reversedHead;
                reversedHead = reversedNode;

                current = current.Next;
            }

            return reversedHead;
        }

        public bool IsPalindromeByRunner(CrackingLinkedListNode? head)
        {
            var slow = head;
            var fast = head;
            var stack = new Stack<CrackingLinkedListNode>();

            while (fast != null && fast.Next != null)
            {
                stack.Push(slow!);

                slow = slow!.Next;
                fast = fast.Next.Next;
            }

            // odd number of elements, skip middle
            if (fast != null)
            {
                slow = slow!.Next;
            }

            while (slow != null)
            {
                if (slow.Data != stack.Pop().Data)
                    return false;

                slow = slow.Next;
            }

            return true;
        }

        public bool IsPalindromeByRecursion(CrackingLinkedListNode? head)
        {
            var length = GetLength(head);
            var result = Recurse(head, length);
            return result.Item2;
        }

        private (CrackingLinkedListNode?, bool) Recurse(CrackingLinkedListNode? node, int length)
        {
            // we're in the middle so the base case is to pass the next node
            // even number of elements
            if (length == 0)
            {
                return (node, true);
            }

            // odd number of elements, skip this one
            if (length == 1)
            {
                return (node!.Next, true);
            }

            var mirror = Recurse(node?.Next, length - 2);

            if (!mirror.Item2)
                return mirror;

            // we pass up the successor of the mirror
            return (mirror.Item1.Next, node.Data == mirror.Item1.Data);
        }

        private int GetLength(CrackingLinkedListNode? head)
        {
            var result = 0;
            while (head != null)
            {
                result++;
                head = head.Next;
            }

            return result;
        }
    }

    [TestFixture]
    public class Task2_6PalindromeTests
    {
        private static object[] testCases =
        {
            new object[] { new List<int> { 0, 1, 2, 3, 4 }, false },
            new object[] { new List<int> { 0, 1, 2, 1, 0 }, true },
            new object[] { new List<int> { 0, 1, 1, 0 }, true },
            new object[] { new List<int> { 0, 1, 2, 1 }, false },
            new object[] { new List<int> { 0, 1, 2, 1, 2 }, false }
        };

        [TestCaseSource(nameof(testCases))]
        public void IsPalindromeByReversalTest(List<int> input, bool expectedResult)
        {
            // arrange
            var sut = new Task2_6Palindrome();
            var linkedList = CrackingLinkedListProvider.CreateLinkedListWithContent(input);

            // act
            var result = sut.IsPalindromeByReversal(linkedList);

            // assert
            result.Should().Be(expectedResult);
        }

        [TestCaseSource(nameof(testCases))]
        public void IsPalindromeByRunnerTest(List<int> input, bool expectedResult)
        {
            // arrange
            var sut = new Task2_6Palindrome();
            var linkedList = CrackingLinkedListProvider.CreateLinkedListWithContent(input);

            // act
            var result = sut.IsPalindromeByRunner(linkedList);

            // assert
            result.Should().Be(expectedResult);
        }

        [TestCaseSource(nameof(testCases))]
        public void IsPalindromeByRecursionTest(List<int> input, bool expectedResult)
        {
            // arrange
            var sut = new Task2_6Palindrome();
            var linkedList = CrackingLinkedListProvider.CreateLinkedListWithContent(input);

            // act
            var result = sut.IsPalindromeByRecursion(linkedList);

            // assert
            result.Should().Be(expectedResult);
        }
    }
}
