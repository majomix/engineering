using System.Collections;

namespace DataStructures.LinkedList
{
    /// <summary>
    /// Custom alternative linked list.
    /// </summary>
    public class CustomSinglyLinkedList<T> : ILinkedList<T>, ISinglyLinkedList<T>, IEnumerable<T>
    {
        private SinglyLinkedListNode<T>? _head;

        public void Add(T item)
        {
            var node = new SinglyLinkedListNode<T>(item, this); 

            if (_head == null)
            {
                _head = node;
            }
            else
            {
                var current = _head;
                while (current.Next != null)
                {
                    current = current.Next;
                }

                current.Next = node;
            }
        }

        public void AddFront(T item)
        {
            var node = new SinglyLinkedListNode<T>(item, this);
            node.Next = _head;
            _head = node;
        }

        public void Reverse()
        {
            if (_head == null || _head.Next == null)
                return;

            var iterationCurrent = _head;
            var reverseCurrent = _head;

            SinglyLinkedListNode<T>? previous = null;

            while (iterationCurrent.Next != null)
            {
                iterationCurrent = iterationCurrent.Next;
                reverseCurrent.Next = previous;
                previous = reverseCurrent;
                reverseCurrent = iterationCurrent;
            }

            _head = reverseCurrent;
            _head.Next = previous;
        }

        public void Clear()
        {
            _head = null;
        }

        public bool Remove(T item)
        {
            // linked list is empty
            if (_head == null)
                return false;

            // item is in head
            var comparer = EqualityComparer<T>.Default;
            if (comparer.Equals(_head.Value, item))
            {
                if (_head.Next != null)
                {
                    _head = _head.Next;
                }

                return true;
            }

            var predecessorNode = FindPredecessorNode(item);

            // item was not found in linked list
            if (predecessorNode == null)
                return false;

            // item was found
            var nodeToRemove = predecessorNode.Next;
            var successorNode = nodeToRemove?.Next;
            predecessorNode.Next = successorNode;

            return true;
        }

        public SinglyLinkedListNode<T>? GetHead()
        {
            return _head;
        }

        public bool InsertAfter(SinglyLinkedListNode<T> node, T value)
        {
            if (node.Parent != this)
                return false;

            var nextOfNewNode = node.Next;
            var newNode = new SinglyLinkedListNode<T>(value, this);
            node.Next = newNode;
            newNode.Next = nextOfNewNode;
            
            return true;
        }

        public bool InjectNode(SinglyLinkedListNode<T> node, SinglyLinkedListNode<T> nodeToInject)
        {
            if (node.Parent != this)
                return false;

            var nextOfInjectedNode = node.Next;
            node.Next = nodeToInject;
            node.Parent = this;
            nodeToInject.Next = nextOfInjectedNode;

            return true;
        }

        public SinglyLinkedListNode<T>? FindNode(T value)
        {
            var current = _head;

            if (current == null)
                return null;

            var comparer = EqualityComparer<T>.Default;

            do
            {
                if (comparer.Equals(current.Value, value))
                    return current;

                current = current.Next;
            } while (current != null);

            return null;
        }

        public bool Remove(SinglyLinkedListNode<T> node)
        {
            if (node.Parent != this)
                return false;

            if (_head == node)
            {
                _head = _head.Next;

                return true;
            }

            var predecessorNode = FindPredecessorNode(node);

            if (predecessorNode == null)
            {
                return false;
            }

            predecessorNode.Next = node.Next;

            return true;
        }

        public SinglyLinkedListNode<T>? FindPredecessorNode(T value)
        {
            var current = _head;

            if (current == null)
                return null;

            SinglyLinkedListNode<T>? previous = null;

            var comparer = EqualityComparer<T>.Default;

            do
            {
                if (comparer.Equals(current.Value, value))
                    return previous;

                previous = current;
                current = current.Next;
            } while (current != null);

            return null;
        }

        internal SinglyLinkedListNode<T>? FindPredecessorNode(SinglyLinkedListNode<T> node)
        {
            if (node.Parent != this)
                return null;

            var current = _head;

            if (current == null)
                return null;

            SinglyLinkedListNode<T>? previous = null;

            do
            {
                if (current == node)
                    return previous;

                previous = current;
                current = current.Next;
            } while (current != null);

            return null;
        }

        protected void SetHead(SinglyLinkedListNode<T>? node)
        {
            _head = node;
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
            private readonly SinglyLinkedListNode<T>? _head;
            private SinglyLinkedListNode<T>? _current;
            private bool _isAtTail;

            public Enumerator(SinglyLinkedListNode<T>? head)
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

                if (_current.Next != null)
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
