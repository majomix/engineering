using FluentAssertions;
using NUnit.Framework;

namespace DataStructures.LinkedList
{
    [TestFixture]
    internal class CustomCircularDoublyLinkedListTests
    {
        [Test]
        public void LinkedList_AddItem_HeadLinkedToItself()
        {
            // arrange
            var linkedList = new CustomCircularDoublyLinkedList<int>();

            // act
            linkedList.Add(5);
            var head = linkedList.GetHead()!;

            // assert
            head.Value.Should().Be(5);
            head.Next.Should().Be(head);
            head.Previous.Should().Be(head);
        }

        [Test]
        public void LinkedList_AddItem_TailLinkedToItself()
        {
            // arrange
            var linkedList = new CustomCircularDoublyLinkedList<int>();

            // act
            linkedList.Add(5);
            var tail = linkedList.GetTail()!;

            // assert
            tail.Value.Should().Be(5);
            tail.Next.Should().Be(tail);
            tail.Previous.Should().Be(tail);
        }

        [Test]
        public void LinkedList_AddThreeItems_LinkedCorrectly()
        {
            // arrange
            var linkedList = new CustomCircularDoublyLinkedList<int>();

            // act
            linkedList.Add(5);
            linkedList.Add(10);
            linkedList.Add(15);
            var head = linkedList.GetHead()!;
            var middle = head.Next!;
            var tail = middle.Next!;

            // assert
            head.Value.Should().Be(5);
            head.Next.Should().Be(middle);
            head.Previous.Should().Be(tail);

            middle.Value.Should().Be(10);
            middle.Next.Should().Be(tail);
            middle.Previous.Should().Be(head);

            tail.Value.Should().Be(15);
            tail.Next.Should().Be(head);
            tail.Previous.Should().Be(middle);
        }

        [Test]
        public void LinkedList_AddThreeItems_LinkedCorrectlyFromTail()
        {
            // arrange
            var linkedList = new CustomCircularDoublyLinkedList<int>();

            // act
            linkedList.Add(5);
            linkedList.Add(10);
            linkedList.Add(15);
            var tail = linkedList.GetTail()!;
            var middle = tail.Previous!;
            var head = middle.Previous!;

            // assert
            head.Value.Should().Be(5);
            head.Next.Should().Be(middle);
            head.Previous.Should().Be(tail);

            middle.Value.Should().Be(10);
            middle.Next.Should().Be(tail);
            middle.Previous.Should().Be(head);

            tail.Value.Should().Be(15);
            tail.Next.Should().Be(head);
            tail.Previous.Should().Be(middle);
        }

        [Test]
        public void LinkedList_AddFront_HeadLinkedToItself()
        {
            // arrange
            var linkedList = new CustomCircularDoublyLinkedList<int>();

            // act
            linkedList.AddFront(5);
            var head = linkedList.GetHead()!;

            // assert
            head.Value.Should().Be(5);
            head.Next.Should().Be(head);
            head.Previous.Should().Be(head);
        }

        [Test]
        public void LinkedList_AddFrontThreeItems_LinkedCorrectly()
        {
            // arrange
            var linkedList = new CustomCircularDoublyLinkedList<int>();

            // act
            linkedList.AddFront(5);
            linkedList.AddFront(10);
            linkedList.AddFront(15);
            var head = linkedList.GetHead()!;
            var middle = head.Next!;
            var tail = middle.Next!;

            // assert
            head.Value.Should().Be(15);
            head.Next.Should().Be(middle);
            head.Previous.Should().Be(tail);

            middle.Value.Should().Be(10);
            middle.Next.Should().Be(tail);
            middle.Previous.Should().Be(head);

            tail.Value.Should().Be(5);
            tail.Next.Should().Be(head);
            tail.Previous.Should().Be(middle);
        }

        [Test]
        public void LinkedList_AddItems_Clear_Empty()
        {
            // arrange
            var linkedList = new CustomCircularDoublyLinkedList<int>();
            linkedList.Add(5);

            // act
            linkedList.Clear();
            var head = linkedList.GetHead()!;

            // assert
            head.Should().Be(null);
        }

        [Test]
        public void LinkedList_AddItems_FindNode_ExistsHead()
        {
            // arrange
            var linkedList = new CustomCircularDoublyLinkedList<int>();
            linkedList.Add(5);
            linkedList.Add(10);
            linkedList.Add(15);
            linkedList.Add(20);

            // act
            var node = linkedList.FindNode(5);

            // assert
            node!.Value.Should().Be(5);
        }

        [Test]
        public void LinkedList_AddItems_FindNode_ExistsTail()
        {
            // arrange
            var linkedList = new CustomCircularDoublyLinkedList<int>();
            linkedList.Add(5);
            linkedList.Add(10);
            linkedList.Add(15);
            linkedList.Add(20);

            // act
            var node = linkedList.FindNode(20);

            // assert
            node!.Value.Should().Be(20);
        }

        [Test]
        public void LinkedList_AddItems_FindNode_DoesNotExist()
        {
            // arrange
            var linkedList = new CustomCircularDoublyLinkedList<int>();
            linkedList.Add(5);
            linkedList.Add(10);
            linkedList.Add(15);
            linkedList.Add(20);

            // act
            var node = linkedList.FindNode(30);

            // assert
            node.Should().Be(null);
        }

        [Test]
        public void LinkedList_AddItems_FindNode_EmptyList()
        {
            // arrange
            var linkedList = new CustomCircularDoublyLinkedList<int>();

            // act
            var node = linkedList.FindNode(30);

            // assert
            node.Should().Be(null);
        }

        [Test]
        public void LinkedList_AddItems_Remove_ExistsHead()
        {
            // arrange
            var linkedList = new CustomCircularDoublyLinkedList<int>();
            linkedList.Add(5);
            linkedList.Add(10);
            linkedList.Add(15);
            linkedList.Add(20);

            // act
            var removed = linkedList.Remove(5);
            var head = linkedList.GetHead()!;

            // assert
            removed.Should().BeTrue();
            head.Value.Should().Be(10);
            head.Previous!.Value.Should().Be(20);
            head.Next!.Value.Should().Be(15);
        }

        [Test]
        public void LinkedList_AddItem_Remove_ExistsHead()
        {
            // arrange
            var linkedList = new CustomCircularDoublyLinkedList<int>();
            linkedList.Add(5);

            // act
            var removed = linkedList.Remove(5);
            var head = linkedList.GetHead()!;

            // assert
            removed.Should().BeTrue();
            head.Should().BeNull();
        }

        [Test]
        public void LinkedList_AddItems_Remove_ExistsTail()
        {
            // arrange
            var linkedList = new CustomCircularDoublyLinkedList<int>();
            linkedList.Add(5);
            linkedList.Add(10);
            linkedList.Add(15);
            linkedList.Add(20);

            // act
            var removed = linkedList.Remove(20);
            var head = linkedList.GetHead()!;

            // assert
            removed.Should().BeTrue();
            head.Value.Should().Be(5);
            head.Previous!.Value.Should().Be(15);
            head.Next!.Value.Should().Be(10);
        }

        [Test]
        public void LinkedList_AddItems_Remove_DoesNotExist()
        {
            // arrange
            var linkedList = new CustomCircularDoublyLinkedList<int>();
            linkedList.Add(5);
            linkedList.Add(10);
            linkedList.Add(15);
            linkedList.Add(20);

            // act
            var removed = linkedList.Remove(30);

            // assert
            removed.Should().BeFalse();
        }

        [Test]
        public void LinkedList_AddItems_Remove_EmptyList()
        {
            // arrange
            var linkedList = new CustomCircularDoublyLinkedList<int>();

            // act
            var removed = linkedList.Remove(30);

            // assert
            removed.Should().BeFalse();
        }

        [Test]
        public void LinkedList_AddItems_EnumeratorReturnsCorrectItems()
        {
            // arrange
            var linkedList = new CustomCircularDoublyLinkedList<int>();

            // act
            linkedList.Add(5);
            linkedList.Add(10);
            linkedList.Add(15);

            // assert
            var enumerator = linkedList.GetEnumerator();

            enumerator.MoveNext();
            enumerator.Current.Should().Be(5);

            enumerator.MoveNext();
            enumerator.Current.Should().Be(10);

            enumerator.MoveNext();
            enumerator.Current.Should().Be(15);

            var moveBehindLast = enumerator.MoveNext();
            moveBehindLast.Should().BeFalse();

            enumerator.Dispose();
        }
    }
}
