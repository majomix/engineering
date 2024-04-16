using FluentAssertions;
using NUnit.Framework;

namespace DataStructures.LinkedList
{
    [TestFixture]
    public class ComparableSinglyLinkedListTests
    {
        [Test]
        public void SinglyLinkedList_Merge_Enumeration()
        {
            // arrange
            var targetList = new ComparableSinglyLinkedList<int>
            {
                5,
                10,
                15,
                20
            };

            var secondList = new ComparableSinglyLinkedList<int>
            {
                7,
                11,
                12,
                21
            };

            // act
            var sorted = targetList.MergeSortedLists(secondList);
            var enumerator = targetList.GetEnumerator();

            // assert
            sorted.Should().BeTrue();

            enumerator.MoveNext();
            enumerator.Current.Should().Be(5);

            enumerator.MoveNext();
            enumerator.Current.Should().Be(7);

            enumerator.MoveNext();
            enumerator.Current.Should().Be(10);

            enumerator.MoveNext();
            enumerator.Current.Should().Be(11);

            enumerator.MoveNext();
            enumerator.Current.Should().Be(12);

            enumerator.MoveNext();
            enumerator.Current.Should().Be(15);

            enumerator.MoveNext();
            enumerator.Current.Should().Be(20);

            enumerator.MoveNext();
            enumerator.Current.Should().Be(21);

            enumerator.Dispose();
        }

        [Test]
        public void SinglyLinkedList_MergeSwapperOrder_Enumeration()
        {
            // arrange
            var targetList = new ComparableSinglyLinkedList<int>
            {
                7,
                11,
                12,
                21
            };

            var secondList = new ComparableSinglyLinkedList<int>
            {
                5,
                10,
                15,
                20
            };

            // act
            var sorted = targetList.MergeSortedLists(secondList);
            var enumerator = targetList.GetEnumerator();

            // assert
            sorted.Should().BeTrue();

            enumerator.MoveNext();
            enumerator.Current.Should().Be(5);

            enumerator.MoveNext();
            enumerator.Current.Should().Be(7);

            enumerator.MoveNext();
            enumerator.Current.Should().Be(10);

            enumerator.MoveNext();
            enumerator.Current.Should().Be(11);

            enumerator.MoveNext();
            enumerator.Current.Should().Be(12);

            enumerator.MoveNext();
            enumerator.Current.Should().Be(15);

            enumerator.MoveNext();
            enumerator.Current.Should().Be(20);

            enumerator.MoveNext();
            enumerator.Current.Should().Be(21);

            enumerator.Dispose();
        }
    }
}
