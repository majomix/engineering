using System.Collections;

namespace DataStructures.LinkedList
{
    /// <summary>
    /// Custom alternative doubly linked list.
    /// </summary>
    public class CustomCircularDoublyLinkedList<T> : ILinkedList<T>, IEnumerable<T>
    {
        private DoublyLinkedListNode<T>? _head;

        public void Add(T item)
        {
            var node = new DoublyLinkedListNode<T>(item, this);

            if (_head == null)
            {
                InsertNodeToEmptyList(node);
            }
            else
            {
                InsertNodeBefore(_head, node);
            }
        }

        public DoublyLinkedListNode<T>? GetHead()
        {
            return _head;
        }

        public DoublyLinkedListNode<T>? GetTail()
        {
            return _head?.Previous;
        }

        public void AddFront(T item)
        {
            var node = new DoublyLinkedListNode<T>(item, this);

            if (_head == null)
            {
                InsertNodeToEmptyList(node);
            }
            else
            {
                InsertNodeBefore(_head, node);
                _head = node;
            }
        }

        public void Clear()
        {
            _head = null;
        }

        public bool Remove(T value)
        {
            var nodeToRemove = FindNode(value);

            if (nodeToRemove == null)
                return false;

            RemoveNode(nodeToRemove);

            return true;
        }

        public void RemoveNode(DoublyLinkedListNode<T> nodeToRemove)
        {
            // single item in list
            if (nodeToRemove.Next == nodeToRemove)
            {
                _head = null;
            }
            else
            {
                // go from
                // A <-> nodeToRemove <-> C
                // to
                // A <-> C
                nodeToRemove.Previous!.Next = nodeToRemove.Next;
                nodeToRemove.Next!.Previous = nodeToRemove.Previous;

                // if head is to be removed
                if (_head == nodeToRemove)
                {
                    _head = _head.Next;
                }
            }
        }

        public DoublyLinkedListNode<T>? FindNode(T value)
        {
            var current = _head;

            if (current == null)
                return null;

            var comparer = EqualityComparer<T>.Default;

            do
            {
                if (comparer.Equals(current!.Value, value))
                    return current;

                current = current.Next;
            } while (current != _head);

            return null;
        }

        private void InsertNodeBefore(DoublyLinkedListNode<T> node, DoublyLinkedListNode<T> newNode)
        {
            var previous = node.Previous!;

            // connect new node to the previous node and make links
            newNode.Next = node;
            newNode.Previous = previous;
            previous.Next = newNode;
            node.Previous = newNode;
        }

        private void InsertNodeToEmptyList(DoublyLinkedListNode<T> node)
        {
            _head = node;
            _head.Previous = node;
            _head.Next = node;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(_head);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public struct Enumerator : IEnumerator<T>
        {
            private readonly DoublyLinkedListNode<T>? _head;
            private DoublyLinkedListNode<T>? _current;
            private bool _isAtTail;

            public Enumerator(DoublyLinkedListNode<T>? head)
            {
                _current = null;
                _head = head;
                _isAtTail = false;
            }

            public bool MoveNext()
            {
                if (_head == null || _isAtTail)
                    return false;

                if (_current == null)
                {
                    _current ??= _head;
                    return true;
                }

                if (_current.Next != _head)
                {
                    _current = _current.Next;
                    return true;
                }

                _isAtTail = true;

                return false;
            }

            public void Reset()
            {
                _current = null;
            }

            public T? Current => PointsAtInvalidElement() ? default : _current!.Value;

            object? IEnumerator.Current => Current;

            public void Dispose()
            { }

            private bool PointsAtInvalidElement()
            {
                return _current == null || _isAtTail;
            }
        }
    }
}
