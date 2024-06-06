using NUnit.Framework;
using FluentAssertions;

namespace LeetCode.Cracking.LinkedLists
{
    /// <summary>
    /// Remove duplicates from an unsorted linked list.
    ///
    /// Solution:
    /// * hashmap
    /// </summary>
    internal class Task2_1RemoveDuplicates
    {
        public void RemoveDuplicates(CrackingLinkedListNode? head)
        {
            if (head == null)
                return;

            var set = new HashSet<int> { head.Data };

            var current = head;
            while (current.Next != null)
            {
                if (set.Add(current.Next.Data))
                {
                    current = current.Next;
                }
                else
                {
                    current.Next = current.Next.Next;
                }
            }
        }

        /// <summary>
        /// How would you solve this problem if a temporary buffer is not allowed?
        /// </summary>
        public void RemoveDuplicatesNoDataStructure(CrackingLinkedListNode? head)
        {
            if (head == null)
                return;

            var current = head;
            while (current != null)
            {
                var runner = current;
                while (runner.Next != null)
                {
                    if (current.Data == runner.Next.Data)
                    {
                        runner.Next = runner.Next.Next;
                    }
                    
                    if (runner.Next != null)
                    {
                        runner = runner.Next;
                    }
                }

                current = current.Next;
            }
        }
    }

    [TestFixture]
    public class Task2_1RemoveDuplicatesTests
    {
        [Test]
        public void RemoveDuplicatesTest()
        {
            // arrange
            var sut = new Task2_1RemoveDuplicates();
            var linkedList = CreateLinkedListWithDuplicates();

            // act
            sut.RemoveDuplicates(linkedList);

            // assert
            linkedList.Data.Should().Be(1);
            linkedList.Next.Data.Should().Be(2);
            linkedList.Next.Next.Data.Should().Be(3);
            linkedList.Next.Next.Next.Data.Should().Be(4);
            linkedList.Next.Next.Next.Next.Data.Should().Be(5);
            linkedList.Next.Next.Next.Next.Next.Should().BeNull();
        }

        [Test]
        public void RemoveDuplicatesNoDataStructureTest()
        {
            // arrange
            var sut = new Task2_1RemoveDuplicates();
            var linkedList = CreateLinkedListWithDuplicates();

            // act
            sut.RemoveDuplicatesNoDataStructure(linkedList);

            // assert
            linkedList.Data.Should().Be(1);
            linkedList.Next.Data.Should().Be(2);
            linkedList.Next.Next.Data.Should().Be(3);
            linkedList.Next.Next.Next.Data.Should().Be(4);
            linkedList.Next.Next.Next.Next.Data.Should().Be(5);
            linkedList.Next.Next.Next.Next.Next.Should().BeNull();
        }

        private CrackingLinkedListNode CreateLinkedListWithDuplicates()
        {
            return new CrackingLinkedListNode(1,
                new CrackingLinkedListNode(2,
                    new CrackingLinkedListNode(2,
                        new CrackingLinkedListNode(3,
                            new CrackingLinkedListNode(4,
                                new CrackingLinkedListNode(5, new CrackingLinkedListNode(5), null), null), null), null), null), null);
        }
    }
}
