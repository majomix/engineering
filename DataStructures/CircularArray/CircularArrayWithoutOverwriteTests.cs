using DataStructures.Helpers;
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
            var list = new CircularArrayWithoutOverwrite<int>(ReallocationPolicy.NoReallocation, defaultCapacity);

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
            var list = new CircularArrayWithoutOverwrite<int>(ReallocationPolicy.NoReallocation, defaultCapacity);

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
            var list = new CircularArrayWithoutOverwrite<int>(ReallocationPolicy.NoReallocation, defaultCapacity);

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
            var list = new CircularArrayWithoutOverwrite<int>(ReallocationPolicy.NoReallocation, defaultCapacity);

            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);
            list.Add(5);

            // act
            var firstFetchedItemSuccess = list.TryGet(out var firstFetchedItem);
            var countAfterFetch = list.Count;
            var addedOverflow = list.Add(6);
            var countAfterAdd = list.Count;

            // assert
            firstFetchedItemSuccess.Should().BeTrue();
            firstFetchedItem.Should().Be(1);
            countAfterFetch.Should().Be(4);
            addedOverflow.Should().BeTrue();
            countAfterAdd.Should().Be(5);

            list.TryGet(out var getResult);
            getResult.Should().Be(2);
            
            list.TryGet(out getResult);
            getResult.Should().Be(3);

            list.TryGet(out getResult);
            getResult.Should().Be(4);

            list.TryGet(out getResult);
            getResult.Should().Be(5);
            
            list.TryGet(out getResult);
            getResult.Should().Be(6);
            
            list.Count.Should().Be(0);
            list.TryGet(out getResult);
            getResult.Should().Be(0);
        }

        [Test]
        public void CircularArray_AddItemsGetItemsOverrun()
        {
            // arrange
            var defaultCapacity = 2;
            var list = new CircularArrayWithoutOverwrite<int>(ReallocationPolicy.NoReallocation, defaultCapacity);

            // act
            list.Add(1);
            list.TryGet(out var _);
            list.Add(2);
            list.Add(3);
            list.TryGet(out var receivedItem);

            // assert
            list.Count.Should().Be(1);
            receivedItem.Should().Be(2);
        }

        [Test]
        public void CircularArray_Clear()
        {
            // arrange
            var defaultCapacity = 5;
            var list = new CircularArrayWithoutOverwrite<int>(ReallocationPolicy.NoReallocation, defaultCapacity);
            list.Add(1);
            list.Add(2);
            list.Add(3);

            // act
            list.Clear();

            // assert
            list.Count.Should().Be(0);
        }

        [Test]
        public void CircularArray_DynamicReallocation()
        {
            // arrange
            var list = new CircularArrayWithoutOverwrite<int>(ReallocationPolicy.DynamicReallocation, 10);
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);
            list.Add(5);
            list.Add(6);
            list.Add(7);
            list.Add(8);
            list.Add(9);
            list.Add(10);

            list.TryGet(out var _);
            list.TryGet(out var _);
            list.TryGet(out var _);
            list.TryGet(out var _);

            list.Add(11);
            list.Add(12);
            list.Add(13);
            list.Add(14);

            // act
            var success = list.Add(15);

            // assert
            success.Should().BeTrue();
        }
    }
}
