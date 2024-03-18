namespace LeetCode.Medium
{
    /// <summary>
    /// https://leetcode.com/problems/add-two-numbers/
    /// Input: l1 = [2,4,3], l2 = [5,6,4]
    /// Output: [7,0,8]
    /// Explanation: 342 + 465 = 807.
    /// </summary>

    public class AddTwoNumbersGivenByLinkedList
    {
        public class ListNode
        {
            public int val;
            public ListNode next;
            public ListNode(int val = 0, ListNode next = null)
            {
                this.val = val;
                this.next = next;
            }
        }

        public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            var currentNodeOfFirstNumber = l1;
            var currentNodeOfSecondNumber = l2;

            ListNode head = null;
            ListNode currentResultNode = null;
            var carryOverTen = false;

            while (currentNodeOfFirstNumber != null || currentNodeOfSecondNumber != null)
            {
                var number = 0;

                if (currentNodeOfFirstNumber != null)
                {
                    number += currentNodeOfFirstNumber.val;
                }

                if (currentNodeOfSecondNumber != null)
                {
                    number += currentNodeOfSecondNumber.val;
                }

                if (carryOverTen)
                {
                    number += 1;
                }

                var numberToStore = number % 10;
                carryOverTen = number >= 10;

                if (head == null)
                {
                    currentResultNode = new ListNode(numberToStore);
                    head = currentResultNode;
                }
                else if (currentResultNode != null)
                {
                    currentResultNode.next = new ListNode(numberToStore);
                    currentResultNode = currentResultNode.next;
                }

                currentNodeOfFirstNumber = currentNodeOfFirstNumber?.next;
                currentNodeOfSecondNumber = currentNodeOfSecondNumber?.next;
            }

            if (carryOverTen)
            {
                currentResultNode.next = new ListNode(1);
            }

            return head;
        }

        public static void TestCase()
        {
            var linkedCalculator = new AddTwoNumbersGivenByLinkedList();

            {
                var firstNumber = new ListNode(2, new ListNode(4, new ListNode(3)));
                var secondNumber = new ListNode(5, new ListNode(6, new ListNode(4)));

                var result = linkedCalculator.AddTwoNumbers(firstNumber, secondNumber);
            }

            {
                var firstNumber = new ListNode(9, new ListNode(9, new ListNode(9, new ListNode(9, new ListNode(9, new ListNode(9, new ListNode(9)))))));
                var secondNumber = new ListNode(9, new ListNode(9, new ListNode(9, new ListNode(9))));

                var result = linkedCalculator.AddTwoNumbers(firstNumber, secondNumber);
            }
        }
    }
}
