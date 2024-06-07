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
    }

    [TestFixture]
    public class Task2_5SumListsTasks
    {
        [Test]
        public void RemoveDuplicatesTest_ThreeDigitsResult()
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
        public void RemoveDuplicatesTest_FourDigitsResult()
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
    }
}
