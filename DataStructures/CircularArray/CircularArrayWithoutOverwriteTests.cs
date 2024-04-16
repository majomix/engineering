using FluentAssertions;
using NUnit.Framework;

namespace DataStructures.CircularArray
{
    [TestFixture]
    internal class CircularArrayWithoutOverwriteTests
    {
        [Test]
        public void CircularArray_AddItemsToVastCapacity_CountCorrect()
        {
            // arrange
            var defaultCapacity = 50;
            var list = new CircularArrayWithoutOverwrite<int>(defaultCapacity);

            // act
            list.Add(1);
            list.Add(2);
            list.Add(3);

            // assert
            list.Count.Should().Be(3);
        }

        [Test]
        public void CircularArray_AddItemsFull_CountCorrect()
        {
            // arrange
            var defaultCapacity = 5;
            var list = new CircularArrayWithoutOverwrite<int>(defaultCapacity);

            // act
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);
            list.Add(5);

            // assert
            list.Count.Should().Be(5);
        }

        [Test]
        public void CircularArray_AddItemsOverflowNotPossible_CountCorrect()
        {
            // arrange
            var defaultCapacity = 5;
            var list = new CircularArrayWithoutOverwrite<int>(defaultCapacity);

            // act
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);
            list.Add(5);
            var addedOverflow = list.Add(6);

            // assert
            list.Count.Should().Be(5);
            addedOverflow.Should().BeFalse();
        }

        [Test]
        public void CircularArray_AddItemsGetItemsWithOverrun_Correct()
        {
            // arrange
            var defaultCapacity = 5;
            var list = new CircularArrayWithoutOverwrite<int>(defaultCapacity);

            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);
            list.Add(5);

            // act
            var firstFetchedItem = list.Get();
            var countAfterFetch = list.Count;
            var addedOverflow = list.Add(6);
            var countAfterAdd = list.Count;

            // assert
            firstFetchedItem.Should().Be(1);
            countAfterFetch.Should().Be(4);
            addedOverflow.Should().BeTrue();
            countAfterAdd.Should().Be(5);
            list.Get().Should().Be(2);
            list.Get().Should().Be(3);
            list.Get().Should().Be(4);
            list.Get().Should().Be(5);
            list.Get().Should().Be(6);
            list.Count.Should().Be(0);
            list.Get().Should().Be(default);
        }

        [Test]
        public void CircularArray_AddItemsGetItemsOverrun()
        {
            // arrange
            var defaultCapacity = 2;
            var list = new CircularArrayWithoutOverwrite<int>(defaultCapacity);

            // act
            list.Add(1);
            list.Get();
            list.Add(2);
            list.Add(3);
            var receivedItem = list.Get();

            // assert
            list.Count.Should().Be(1);
            receivedItem.Should().Be(2);
        }

        [Test]
        public void CircularArray_Clear()
        {
            // arrange
            var defaultCapacity = 5;
            var list = new CircularArrayWithoutOverwrite<int>(defaultCapacity);
            list.Add(1);
            list.Add(2);
            list.Add(3);

            // act
            list.Clear();

            // assert
            list.Count.Should().Be(0);
        }
    }
}
