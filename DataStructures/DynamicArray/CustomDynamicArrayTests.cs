using DataStructures.Helpers;
using FluentAssertions;
using NUnit.Framework;

namespace DataStructures.DynamicArray
{
    [TestFixture]
    internal class CustomDynamicArrayTests
    {
        [Test]
        public void List_AddItems_CountCorrect()
        {
            // arrange
            var list = new CustomDynamicArray<int>();

            // act
            list.Add(1);
            list.Add(2);
            list.Add(3);

            // assert
            list.Count.Should().Be(3);
        }

        [Test]
        public void List_AddItems_Resize_CountCorrect()
        {
            // arrange
            var list = new CustomDynamicArray<int>();

            // act
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

            // assert
            list.Count.Should().Be(10);
        }

        [Test]
        public void List_AddLargeAmountOfItems_Resize_CountCorrect()
        {
            // arrange
            var count = 2_000_000u;
            var list = new CustomDynamicArray<int>();

            // act
            list.Count.Should().Be(0);

            for (var i = 0; i < count; i++)
            {
                list.Add(i);
            }

            // assert
            list.Count.Should().Be(count);
        }

        [Test]
        public void List_AddLargeAmountOfItems_Resize_CustomFactors_Addition_CountCorrect()
        {
            // arrange
            var count = 10_000u;
            var list = new CustomDynamicArray<int>(new [] { 1.0, 1.0 });

            // act
            list.Count.Should().Be(0);

            for (var i = 0; i < count; i++)
            {
                list.Add(i);
            }

            // assert
            list.Count.Should().Be(count);
        }

        [Test]
        public void List_AddLargeAmountOfItems_Preallocation_CountCorrect()
        {
            // arrange
            var count = 2_000_000u;
            var list = new CustomDynamicArray<int>(count);

            // act
            list.Count.Should().Be(0);

            for (var i = 0; i < count; i++)
            {
                list.Add(i);
            }

            // assert
            list.Count.Should().Be(count);
        }

        [Test]
        public void List_CopyConstructor_CountCorrect()
        {
            var backingArray = new[] { 1, 2, 3 };

            // arrange
            var list = new CustomDynamicArray<int>(backingArray);

            // act

            // assert
            list.Count.Should().Be((uint)backingArray.Length);
        }

        [Test]
        public void List_CopyConstructor_Clear_CountCorrect()
        {
            var backingArray = new[] { 1, 2, 3 };

            // arrange
            var list = new CustomDynamicArray<int>(backingArray);

            // act
            list.Clear();

            // assert
            list.Count.Should().Be(0);
        }

        [Test]
        public void List_CopyConstructor_Remove_CountCorrect_ItemsCorrect()
        {
            var backingArray = new[] { 1, 2, 3 };

            // arrange
            var list = new CustomDynamicArray<int>(backingArray);

            // act
            var removed = list.Remove(1);

            // assert
            removed.Should().BeTrue();
            list.Count.Should().Be(2);
            list[0].Should().Be(2);
            list[1].Should().Be(3);
        }

        [Test]
        public void List_CopyConstructor_RemoveLastItem_CountCorrect_ItemsCorrect()
        {
            var backingArray = new[] { 1, 2, 3 };

            // arrange
            var list = new CustomDynamicArray<int>(backingArray);

            // act
            var removed = list.Remove(3);

            // assert
            removed.Should().BeTrue();
            list.Count.Should().Be(2);
            list[0].Should().Be(1);
            list[1].Should().Be(2);
        }

        [Test]
        public void List_CopyConstructor_Remove_NotFound()
        {
            var backingArray = new[] { 1, 2, 3 };

            // arrange
            var list = new CustomDynamicArray<int>(backingArray);

            // act
            var removed = list.Remove(4);

            // assert
            removed.Should().BeFalse();
            list.Count.Should().Be(3);
        }

        [Test]
        public void List_CopyConstructor_RemoveAt_Success()
        {
            var backingArray = new[] { 1, 2, 3 };

            // arrange
            var list = new CustomDynamicArray<int>(backingArray);

            // act
            var removed = list.RemoveAt(1);

            // assert
            removed.Should().BeTrue();
            list.Count.Should().Be(2);
            list[0].Should().Be(1);
            list[1].Should().Be(3);
        }

        [Test]
        public void List_CopyConstructor_RemoveAt_OutOfRange()
        {
            var backingArray = new[] { 1, 2, 3 };

            // arrange
            var list = new CustomDynamicArray<int>(backingArray);

            // act
            var removed = list.RemoveAt(3);

            // assert
            removed.Should().BeFalse();
            list.Count.Should().Be(3);
        }

        [Test]
        public void List_CopyConstructor_ContainsFalse()
        {
            var backingArray = new[] { 1, 2, 3 };

            // arrange
            var list = new CustomDynamicArray<int>(backingArray);

            // act
            var contains = list.Contains(7);

            // assert
            contains.Should().BeFalse();
        }

        [Test]
        public void List_CopyConstructor_ContainsTrue()
        {
            var backingArray = new[] { 1, 2, 3 };

            // arrange
            var list = new CustomDynamicArray<int>(backingArray);

            // act
            var contains = list.Contains(1);

            // assert
            contains.Should().BeTrue();
        }

        [Test]
        public void List_CopyConstructor_InsertFront()
        {
            var backingArray = new[] { 1, 2, 3 };

            // arrange
            var list = new CustomDynamicArray<int>(backingArray);

            // act
            var contains = list.Insert(0, 20);

            // assert
            contains.Should().BeTrue();
            list[0].Should().Be(20);
            list[1].Should().Be(1);
            list[2].Should().Be(2);
            list[3].Should().Be(3);
        }

        [Test]
        public void List_CopyConstructor_InsertBack()
        {
            var backingArray = new[] { 1, 2, 3 };

            // arrange
            var list = new CustomDynamicArray<int>(backingArray);

            // act
            var contains = list.Insert(3, 20);

            // assert
            contains.Should().BeTrue();
            list[0].Should().Be(1);
            list[1].Should().Be(2);
            list[2].Should().Be(3);
            list[3].Should().Be(20);
        }


        [Test]
        public void List_CopyConstructor_InsertMiddle()
        {
            var backingArray = new[] { 1, 2, 3, 4, 5, 6, 7 };

            // arrange
            var list = new CustomDynamicArray<int>(backingArray);

            // act
            var contains = list.Insert(5, 20);

            // assert
            contains.Should().BeTrue();
            list[0].Should().Be(1);
            list[1].Should().Be(2);
            list[2].Should().Be(3);
            list[3].Should().Be(4);
            list[4].Should().Be(5);
            list[5].Should().Be(20);
            list[6].Should().Be(6);
            list[7].Should().Be(7);
        }

        [Test]
        public void List_CopyConstructor_Contains_CustomEquatableTrue()
        {
            // arrange
            var list = new CustomDynamicArray<CustomEquatableClass> { new(true) };

            // act
            var contains = list.Contains(new CustomEquatableClass(true));

            // assert
            contains.Should().BeTrue();
        }

        [Test]
        public void List_CopyConstructor_Contains_CustomEquatableFalse()
        {
            // arrange
            var list = new CustomDynamicArray<CustomEquatableClass> { new(false) };

            // act
            var contains = list.Contains(new CustomEquatableClass(true));

            // assert
            contains.Should().BeFalse();
        }

        [Test]
        public void List_EnumerationReturnsAllElements()
        {
            // arrange
            var backingArray = new[] { 1, 2, 3, 4, 5, 6, 7 };

            // arrange
            var list = new CustomDynamicArray<int>(backingArray);
            var enumerator = list.GetEnumerator();

            // assert
            // initially enumerator points before the first element
            enumerator.Current.Should().Be(default);

            // enumerate all items
            foreach (var item in backingArray)
            {
                enumerator.MoveNext();
                enumerator.Current.Should().Be(item);
            }

            // now it should point behind the last element
            enumerator.MoveNext();
            enumerator.Current.Should().Be(default);

            // after reset it should point again before the first element
            enumerator.Reset();
            enumerator.Current.Should().Be(default);

            // verify that first element after reset is the first element
            enumerator.MoveNext();
            enumerator.Current.Should().Be(backingArray[0]);

            enumerator.Dispose();
        }
    }
}
