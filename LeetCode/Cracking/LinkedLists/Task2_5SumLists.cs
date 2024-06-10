using FluentAssertions;
using NUnit.Framework;

namespace LeetCode.Cracking.LinkedLists
{
    /// <summary>
    /// You have two numbers represented by a linked list where each node contains a single digit.
    /// The digits are stored in reverse order, such that the 1's digit is at the head of the list.
    /// Write a function trhat adds two numbers and returns the sum as a linked list.
    /// You are not allowed to cheat and just convert the linked list to an integer.
    ///
    /// Example:
    /// (7 -> 1 -> 6) + (5 -> 9 -> 2) = 617 + 295 = (2 -> 1 -> 9) = 912
    /// </summary>
    internal class Task2_5SumLists
    {
        public CrackingLinkedListNode SumLists(CrackingLinkedListNode first, CrackingLinkedListNode second)
        {
            CrackingLinkedListNode? sum = null;

            var carry = false;
            var firstCurrent = first;
            var secondCurrent = second;
            var sumCurrent = sum;

            while (firstCurrent != null || secondCurrent != null)
            {
                var digit = carry ? 1 : 0;
                if (firstCurrent != null)
                {
                    digit += firstCurrent.Data;
                    firstCurrent = firstCurrent.Next;
                }

                if (secondCurrent != null)
                {
                    digit += secondCurrent.Data;
                    secondCurrent = secondCurrent.Next;
                }

                if (digit > 9)
                {
                    digit -= 10;
                    carry = true;
                }
                else
                {
                    carry = false;
                }

                var digitNode = new CrackingLinkedListNode(digit);
                if (sum == null)
                {
                    sum = digitNode;
                    sumCurrent = sum;
                }
                else
                {
                    sumCurrent!.Next = digitNode;
                    sumCurrent = sumCurrent.Next;
                }
            }

            if (carry)
            {
                sumCurrent!.Next = new CrackingLinkedListNode(1);
            }

            return sum!;
        }

        /// <summary>
        /// Follow up:
        /// Suppose the digits are stored in forward order.
        ///
        /// Example:
        /// (6 -> 1 -> 7) + (2 -> 9 -> 5) = 617 + 295 = (9 -> 1 -> 2) = 912
        ///
        /// Discussion:
        /// In this case it makes sense to go from the end.
        /// The easiest way how to get to the end and then start processing is by recursion.
        ///
        /// First we need to make sure the lists will be right-aligned by padding them with zeros.
        /// To perform the padding, we need to find the length which already takes O(n).
        /// </summary>
        public CrackingLinkedListNode SumListsReverse(CrackingLinkedListNode? first, CrackingLinkedListNode? second)
        {
            if (first == null || second == null)
                return new CrackingLinkedListNode(0);

            var firstLength = GetLength(first);
            var secondLength = GetLength(second);

            if (firstLength > secondLength)
            {
                second = PadList(second, firstLength - secondLength);
            }
            else if (secondLength > firstLength)
            {
                first = PadList(first, secondLength - firstLength);
            }

            var sum = SumListsRecursively(first, second);
            var head = sum.Item1!;
            if (sum.Item2 == 1)
            {
                var next = head;
                head = new CrackingLinkedListNode(1, next, null);
            }

            return head;
        }

        public (CrackingLinkedListNode?, int) SumListsRecursively(CrackingLinkedListNode? first, CrackingLinkedListNode? second)
        {
            // base case: end of iteration
            if (first == null && second == null)
                return (null, 0);

            var result = SumListsRecursively(first!.Next, second!.Next);

            var sum = new CrackingLinkedListNode(first.Data + second.Data + result.Item2);
            sum.Next = result.Item1;

            var carry = 0;
            if (sum.Data > 9)
            {
                sum.Data -= 10;
                carry = 1;
            }

            return (sum, carry);
        }

        private CrackingLinkedListNode PadList(CrackingLinkedListNode list, int lengthToPad)
        {
            var head = new CrackingLinkedListNode(0);
            
            var current = head;
            for (var i = 1; i < lengthToPad; i++)
            {
                current.Next = new CrackingLinkedListNode(0);
                current = current.Next;
            }

            current.Next = list;

            return head;
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
    public class Task2_5SumListsTasks
    {
        [Test]
        public void SumListsTest_ThreeDigitsResult()
        {
            // arrange
            var sut = new Task2_5SumLists();
            var first = CrackingLinkedListProvider.CreateLinkedListWithContent(new List<int> { 7,1,6 });
            var second = CrackingLinkedListProvider.CreateLinkedListWithContent(new List<int> { 5,9,2 });
            
            // act
            var result = sut.SumLists(first, second);

            // assert
            result.Data.Should().Be(2);
            result.Next!.Data.Should().Be(1);
            result.Next.Next!.Data.Should().Be(9);
        }

        [Test]
        public void SumListsTest_FourDigitsResult()
        {
            // arrange
            var sut = new Task2_5SumLists();
            var first = CrackingLinkedListProvider.CreateLinkedListWithContent(new List<int> { 7, 1, 6 });
            var second = CrackingLinkedListProvider.CreateLinkedListWithContent(new List<int> { 5, 9, 4 });

            // act
            var result = sut.SumLists(first, second);

            // assert
            result.Data.Should().Be(2);
            result.Next!.Data.Should().Be(1);
            result.Next.Next!.Data.Should().Be(1);
            result.Next.Next.Next!.Data.Should().Be(1);
        }

        [Test]
        public void SumListsReverseTest_ThreeDigitsResult()
        {
            // arrange
            var sut = new Task2_5SumLists();
            var first = CrackingLinkedListProvider.CreateLinkedListWithContent(new List<int> { 6, 1, 7 });
            var second = CrackingLinkedListProvider.CreateLinkedListWithContent(new List<int> { 2, 9, 5 });

            // act
            var result = sut.SumListsReverse(first, second);

            // assert
            result.Data.Should().Be(9);
            result.Next!.Data.Should().Be(1);
            result.Next.Next!.Data.Should().Be(2);
        }

        [Test]
        public void RemoveDuplicatesTest_FourDigitsResult()
        {
            // arrange
            var sut = new Task2_5SumLists();
            var first = CrackingLinkedListProvider.CreateLinkedListWithContent(new List<int> { 6, 1, 7 });
            var second = CrackingLinkedListProvider.CreateLinkedListWithContent(new List<int> { 4, 9, 5 });

            // act
            var result = sut.SumListsReverse(first, second);

            // assert
            result.Data.Should().Be(1);
            result.Next!.Data.Should().Be(1);
            result.Next.Next!.Data.Should().Be(1);
            result.Next.Next.Next!.Data.Should().Be(2);
        }

        [Test]
        public void RemoveDuplicatesTest_UnevenLengthsLeft()
        {
            // arrange
            var sut = new Task2_5SumLists();
            var first = CrackingLinkedListProvider.CreateLinkedListWithContent(new List<int> { 7 });
            var second = CrackingLinkedListProvider.CreateLinkedListWithContent(new List<int> { 1, 0, 0 });

            // act
            var result = sut.SumListsReverse(first, second);

            // assert
            result.Data.Should().Be(1);
            result.Next!.Data.Should().Be(0);
            result.Next.Next!.Data.Should().Be(7);
        }

        [Test]
        public void RemoveDuplicatesTest_UnevenLengthsRight()
        {
            // arrange
            var sut = new Task2_5SumLists();
            var first = CrackingLinkedListProvider.CreateLinkedListWithContent(new List<int> { 1, 0, 0 });
            var second = CrackingLinkedListProvider.CreateLinkedListWithContent(new List<int> { 7 });

            // act
            var result = sut.SumListsReverse(first, second);

            // assert
            result.Data.Should().Be(1);
            result.Next!.Data.Should().Be(0);
            result.Next.Next!.Data.Should().Be(7);
        }
    }
}
