using FluentAssertions;
using NUnit.Framework;

namespace DataStructures.LinkedList
{
    [TestFixture]
    internal class CustomSinglyLinkedListTests
    {
        [Test]
        public void LinkedList_AddItems_EnumeratorReturnsCorrectItems()
        {
            // arrange
            var linkedList = new CustomSinglyLinkedList<int>();

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

        [Test]
        public void LinkedList_AddFrontToPopulatedLinkedList_EnumeratorReturnsCorrectItems()
        {
            // arrange
            var linkedList = new CustomSinglyLinkedList<int>();

            // act
            linkedList.Add(5);
            linkedList.Add(10);
            linkedList.AddFront(15);

            // assert
            var enumerator = linkedList.GetEnumerator();

            enumerator.MoveNext();
            enumerator.Current.Should().Be(15);

            enumerator.MoveNext();
            enumerator.Current.Should().Be(5);

            enumerator.MoveNext();
            enumerator.Current.Should().Be(10);

            var moveBehindLast = enumerator.MoveNext();
            moveBehindLast.Should().BeFalse();

            enumerator.Dispose();
        }

        [Test]
        public void LinkedList_AddFrontToEmptyLinkedList_EnumeratorReturnsCorrectItems()
        {
            // arrange
            var linkedList = new CustomSinglyLinkedList<int>();

            // act
            linkedList.AddFront(15);

            // assert
            var enumerator = linkedList.GetEnumerator();

            enumerator.MoveNext();
            enumerator.Current.Should().Be(15);
            
            var moveBehindLast = enumerator.MoveNext();
            moveBehindLast.Should().BeFalse();

            enumerator.Dispose();
        }

        [Test]
        public void LinkedList_FindNode_Success()
        {
            // arrange
            var linkedList = new CustomSinglyLinkedList<int>
            {
                5,
                10,
                15
            };

            // act
            var firstNode = linkedList.FindNode(5)!;
            var middleNode = linkedList.FindNode(10)!;
            var lastNode = linkedList.FindNode(15)!;

            // assert
            firstNode.Value.Should().Be(5);
            firstNode.Next.Should().Be(middleNode);
            middleNode.Value.Should().Be(10);
            middleNode.Next.Should().Be(lastNode);
            lastNode.Next.Should().BeNull();
        }

        [Test]
        public void LinkedList_FindNodeInEmptyListReturnsNull()
        {
            // arrange
            var linkedList = new CustomSinglyLinkedList<int>();

            // act
            var node = linkedList.FindNode(10);

            // assert
            node.Should().BeNull();
        }

        [Test]
        public void LinkedList_FindNodeNotPresentInListReturnsNull()
        {
            // arrange
            var linkedList = new CustomSinglyLinkedList<int>
            {
                5,
                10,
                15
            };

            // act
            var node = linkedList.FindNode(20);

            // assert
            node.Should().BeNull();
        }

        [Test]
        public void LinkedList_FindPredecessorNode_Success()
        {
            // arrange
            var linkedList = new CustomSinglyLinkedList<int>
            {
                5,
                10,
                15
            };

            // act
            var predecessorOfFirstNode = linkedList.FindPredecessorNode(5)!;

            var middleNode = linkedList.FindNode(10)!;
            var predecessorOfMiddleNode = linkedList.FindPredecessorNode(10)!;

            var lastNode = linkedList.FindNode(15)!;
            var predecessorOfLastNode = linkedList.FindPredecessorNode(15)!;

            // assert
            predecessorOfFirstNode.Should().BeNull();

            predecessorOfMiddleNode.Value.Should().Be(5);
            predecessorOfMiddleNode.Next.Should().Be(middleNode);

            predecessorOfLastNode.Value.Should().Be(10);
            predecessorOfLastNode.Next.Should().Be(lastNode);
            predecessorOfLastNode.Next!.Next.Should().BeNull();
        }

        [Test]
        public void LinkedList_FindPredecessorNodeInEmptyListReturnsNull()
        {
            // arrange
            var linkedList = new CustomSinglyLinkedList<int>();

            // act
            var node = linkedList.FindPredecessorNode(10);

            // assert
            node.Should().BeNull();
        }

        [Test]
        public void LinkedList_FindPredecessorNodeNotPresentInListReturnsNull()
        {
            // arrange
            var linkedList = new CustomSinglyLinkedList<int>
            {
                5,
                10,
                15
            };

            // act
            var node = linkedList.FindPredecessorNode(20);

            // assert
            node.Should().BeNull();
        }

        [Test]
        public void LinkedList_GetHead_FindPredecessorNode_Success()
        {
            // arrange
            var linkedList = new CustomSinglyLinkedList<int>
            {
                5,
                10,
                15
            };

            var headNode = linkedList.GetHead()!;
            var middleNode = headNode.Next;
            var lastNode = middleNode!.Next;

            // act
            var predecessorOfFirstNode = linkedList.FindPredecessorNode(headNode)!;
            var predecessorOfMiddleNode = linkedList.FindPredecessorNode(middleNode)!;
            var predecessorOfLastNode = linkedList.FindPredecessorNode(lastNode!)!;

            // assert
            predecessorOfFirstNode.Should().BeNull();

            predecessorOfMiddleNode.Value.Should().Be(5);
            predecessorOfMiddleNode.Next.Should().Be(middleNode);

            predecessorOfLastNode.Value.Should().Be(10);
            predecessorOfLastNode.Next.Should().Be(lastNode);
            predecessorOfLastNode.Next!.Next.Should().BeNull();
        }

        [Test]
        public void LinkedList_FindPredecessorNodeInEmptyListReturnsNull_NonExistentNode()
        {
            // arrange
            var linkedList = new CustomSinglyLinkedList<int>();
            var nonExistentNode = new SinglyLinkedListNode<int>(5, linkedList);

            // act
            var node = linkedList.FindPredecessorNode(nonExistentNode);

            // assert
            node.Should().BeNull();
        }

        [Test]
        public void LinkedList_FindPredecessorNodeNotPresentInListReturnsNull_NonExistentNode()
        {
            // arrange
            var linkedList = new CustomSinglyLinkedList<int>
            {
                5,
                10,
                15
            };
            var nonExistentNode = new SinglyLinkedListNode<int>(5, linkedList);

            // act
            var node = linkedList.FindPredecessorNode(nonExistentNode);

            // assert
            node.Should().BeNull();
        }

        [Test]
        public void LinkedList_RemoveHeadSuccess_EnumeratorReturnsAllValues()
        {
            // arrange
            var linkedList = new CustomSinglyLinkedList<int>
            {
                5,
                10,
                15
            };

            // act
            var removed = linkedList.Remove(5);

            // assert
            removed.Should().BeTrue();

            var enumerator = linkedList.GetEnumerator();

            var moveToFirst = enumerator.MoveNext();
            moveToFirst.Should().BeTrue();
            enumerator.Current.Should().Be(10);

            var moveToLast = enumerator.MoveNext();
            moveToLast.Should().BeTrue();
            enumerator.Current.Should().Be(15);

            var moveBehindLast = enumerator.MoveNext();
            moveBehindLast.Should().BeFalse();

            enumerator.Dispose();
        }

        [Test]
        public void LinkedList_RemoveTailSuccess_EnumeratorReturnsAllValues()
        {
            // arrange
            var linkedList = new CustomSinglyLinkedList<int>
            {
                5,
                10,
                15
            };

            // act
            var removed = linkedList.Remove(15);

            // assert
            removed.Should().BeTrue();

            var enumerator = linkedList.GetEnumerator();

            var moveToFirst = enumerator.MoveNext();
            moveToFirst.Should().BeTrue();
            enumerator.Current.Should().Be(5);

            var moveToLast = enumerator.MoveNext();
            moveToLast.Should().BeTrue();
            enumerator.Current.Should().Be(10);

            var moveBehindLast = enumerator.MoveNext();
            moveBehindLast.Should().BeFalse();

            enumerator.Dispose();
        }


        [Test]
        public void LinkedList_RemoveMiddleSuccess_EnumeratorReturnsAllValues()
        {
            // arrange
            var linkedList = new CustomSinglyLinkedList<int>
            {
                5,
                10,
                15
            };

            // act
            var removed = linkedList.Remove(10);

            // assert
            removed.Should().BeTrue();

            var enumerator = linkedList.GetEnumerator();

            var moveToFirst = enumerator.MoveNext();
            moveToFirst.Should().BeTrue();
            enumerator.Current.Should().Be(5);

            var moveToLast = enumerator.MoveNext();
            moveToLast.Should().BeTrue();
            enumerator.Current.Should().Be(15);

            var moveBehindLast = enumerator.MoveNext();
            moveBehindLast.Should().BeFalse();

            enumerator.Dispose();
        }

        [Test]
        public void LinkedList_RemoveNotFound()
        {
            // arrange
            var linkedList = new CustomSinglyLinkedList<int>
            {
                5,
                10,
                15
            };

            // act
            var removed = linkedList.Remove(20);

            // assert
            removed.Should().BeFalse();
        }

        [Test]
        public void LinkedList_RemoveInEmptyList()
        {
            // arrange
            var linkedList = new CustomSinglyLinkedList<int>();

            // act
            var removed = linkedList.Remove(20);

            // assert
            removed.Should().BeFalse();
        }

        [Test]
        public void LinkedList_RemoveHeadNodeSuccess_EnumeratorReturnsAllValues()
        {
            // arrange
            var linkedList = new CustomSinglyLinkedList<int>
            {
                5,
                10,
                15
            };
            var headNode = linkedList.GetHead()!;

            // act
            var removed = linkedList.Remove(headNode);

            // assert
            removed.Should().BeTrue();

            var enumerator = linkedList.GetEnumerator();

            var moveToFirst = enumerator.MoveNext();
            moveToFirst.Should().BeTrue();
            enumerator.Current.Should().Be(10);

            var moveToLast = enumerator.MoveNext();
            moveToLast.Should().BeTrue();
            enumerator.Current.Should().Be(15);

            var moveBehindLast = enumerator.MoveNext();
            moveBehindLast.Should().BeFalse();

            enumerator.Dispose();
        }

        [Test]
        public void LinkedList_RemoveTailNodeSuccess_EnumeratorReturnsAllValues()
        {
            // arrange
            var linkedList = new CustomSinglyLinkedList<int>
            {
                5,
                10,
                15
            };

            // act
            var tailNode = linkedList.GetHead()!.Next!.Next!;
            var removed = linkedList.Remove(tailNode);

            // assert
            removed.Should().BeTrue();

            var enumerator = linkedList.GetEnumerator();

            var moveToFirst = enumerator.MoveNext();
            moveToFirst.Should().BeTrue();
            enumerator.Current.Should().Be(5);

            var moveToLast = enumerator.MoveNext();
            moveToLast.Should().BeTrue();
            enumerator.Current.Should().Be(10);

            var moveBehindLast = enumerator.MoveNext();
            moveBehindLast.Should().BeFalse();

            enumerator.Dispose();
        }

        [Test]
        public void LinkedList_RemoveMiddleNodeSuccess_EnumeratorReturnsAllValues()
        {
            // arrange
            var linkedList = new CustomSinglyLinkedList<int>
            {
                5,
                10,
                15
            };

            // act
            var middleNode = linkedList.GetHead()!.Next!;

            var removed = linkedList.Remove(middleNode);

            // assert
            removed.Should().BeTrue();

            var enumerator = linkedList.GetEnumerator();

            var moveToFirst = enumerator.MoveNext();
            moveToFirst.Should().BeTrue();
            enumerator.Current.Should().Be(5);

            var moveToLast = enumerator.MoveNext();
            moveToLast.Should().BeTrue();
            enumerator.Current.Should().Be(15);

            var moveBehindLast = enumerator.MoveNext();
            moveBehindLast.Should().BeFalse();

            enumerator.Dispose();
        }

        [Test]
        public void LinkedList_RemoveNodeNotFound()
        {
            // arrange
            var linkedList = new CustomSinglyLinkedList<int>
            {
                5,
                10,
                15
            };

            // act
            var nonExistentNode = new SinglyLinkedListNode<int>(20, linkedList);
            var removed = linkedList.Remove(nonExistentNode);

            // assert
            removed.Should().BeFalse();
        }

        [Test]
        public void LinkedList_RemoveNodeInEmptyList()
        {
            // arrange
            var linkedList = new CustomSinglyLinkedList<int>();

            // act
            var nonExistentNode = new SinglyLinkedListNode<int>(5, linkedList);
            var removed = linkedList.Remove(nonExistentNode);

            // assert
            removed.Should().BeFalse();
        }

        [Test]
        public void LinkedList_InsertAfter_Enumeration()
        {
            // arrange
            var linkedList = new CustomSinglyLinkedList<int>
            {
                5,
                10,
                15
            };
            var valueToInsert = 7;
            var headNode = linkedList.GetHead()!;

            // act
            linkedList.InsertAfter(headNode, valueToInsert);

            // assert
            var enumerator = linkedList.GetEnumerator();

            var moveToFirst = enumerator.MoveNext();
            moveToFirst.Should().BeTrue();
            enumerator.Current.Should().Be(5);

            var moveToInjected = enumerator.MoveNext();
            moveToInjected.Should().BeTrue();
            enumerator.Current.Should().Be(7);

            var moveToMiddle = enumerator.MoveNext();
            moveToMiddle.Should().BeTrue();
            enumerator.Current.Should().Be(10);

            var moveToLast = enumerator.MoveNext();
            moveToLast.Should().BeTrue();
            enumerator.Current.Should().Be(15);

            var moveBehindLast = enumerator.MoveNext();
            moveBehindLast.Should().BeFalse();

            enumerator.Dispose();
        }

        [Test]
        public void LinkedList_InjectNode_Enumeration()
        {
            // arrange
            var linkedList = new CustomSinglyLinkedList<int>
            {
                5,
                10,
                15
            };
            var nodeToInsert = new SinglyLinkedListNode<int>(7, linkedList);
            nodeToInsert.Next = new SinglyLinkedListNode<int>(500, new CustomSinglyLinkedList<int>());
            var headNode = linkedList.GetHead()!;

            // act
            linkedList.InjectNode(headNode, nodeToInsert);

            // assert
            var enumerator = linkedList.GetEnumerator();

            var moveToFirst = enumerator.MoveNext();
            moveToFirst.Should().BeTrue();
            enumerator.Current.Should().Be(5);

            var moveToInjected = enumerator.MoveNext();
            moveToInjected.Should().BeTrue();
            enumerator.Current.Should().Be(7);

            var moveToMiddle = enumerator.MoveNext();
            moveToMiddle.Should().BeTrue();
            enumerator.Current.Should().Be(10);

            var moveToLast = enumerator.MoveNext();
            moveToLast.Should().BeTrue();
            enumerator.Current.Should().Be(15);

            var moveBehindLast = enumerator.MoveNext();
            moveBehindLast.Should().BeFalse();

            enumerator.Dispose();
        }

        [Test]
        public void LinkedList_Clear_EnumeratorReturnsNothing()
        {
            // arrange
            var linkedList = new CustomSinglyLinkedList<int>
            {
                5,
                10,
                15
            };

            // act
            linkedList.Clear();

            // assert
            var enumerator = linkedList.GetEnumerator();

            var moveToFirst = enumerator.MoveNext();
            moveToFirst.Should().BeFalse();
            enumerator.Dispose();
        }

        [Test]
        public void LinkedList_Reverse()
        {
            // arrange
            var linkedList = new CustomSinglyLinkedList<int>
            {
                5,
                10,
                15,
                20,
                25,
                30,
                35,
                40
            };

            // act
            linkedList.Reverse();

            // assert
            var enumerator = linkedList.GetEnumerator();

            enumerator.MoveNext();
            enumerator.Current.Should().Be(40);

            enumerator.MoveNext();
            enumerator.Current.Should().Be(35);

            enumerator.MoveNext();
            enumerator.Current.Should().Be(30);

            enumerator.MoveNext();
            enumerator.Current.Should().Be(25);

            enumerator.MoveNext();
            enumerator.Current.Should().Be(20);

            enumerator.MoveNext();
            enumerator.Current.Should().Be(15);

            enumerator.MoveNext();
            enumerator.Current.Should().Be(10);

            enumerator.MoveNext();
            enumerator.Current.Should().Be(5);

            var moveBehindLast = enumerator.MoveNext();
            moveBehindLast.Should().BeFalse();

            enumerator.Dispose();
        }
    }
}
