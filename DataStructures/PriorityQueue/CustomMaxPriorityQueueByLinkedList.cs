using DataStructures.LinkedList;

namespace DataStructures.PriorityQueue
{
    /// <summary>
    /// Custom implementation of priority queue (priority given to the largest keys) by linked list.
    /// </summary>
    internal class CustomMaxPriorityQueueByLinkedList<TKey, TValue> : IMaxPriorityQueue<TKey, TValue> where TKey : IComparable<TKey>
    {
        private readonly CustomSinglyLinkedList<PriorityItem<TKey, TValue>> _linkedList = new();

        public void Insert(TKey key, TValue value)
        {
            var priorityItem = new PriorityItem<TKey, TValue> { Key = key, Value = value };
            var linkedListItem = new SinglyLinkedListNode<PriorityItem<TKey, TValue>>(priorityItem, _linkedList);

            SinglyLinkedListNode<PriorityItem<TKey, TValue>>? previous = null;
            var current = _linkedList.GetHead();

            if (current == null || (previous == null && IsLarger(priorityItem, current.Value)))
            {
                _linkedList.AddFront(priorityItem);
                return;
            }

            while (current != null && IsLarger(current.Value, priorityItem))
            {
                previous = current;
                current = current.Next;
            }

            linkedListItem.Next = current;
            previous!.Next = linkedListItem;
        }

        private bool IsLarger(PriorityItem<TKey, TValue> left, PriorityItem<TKey, TValue> right)
        {
            var comparer = Comparer<PriorityItem<TKey, TValue>>.Default;

            return comparer.Compare(left, right) > 0;
        }

        public TValue GetMaximum()
        {
            var head = _linkedList.GetHead();
            if (head == null)
                throw new InvalidOperationException("Priority queue is empty.");

            return head.Value.Value!;
        }

        public TValue ExtractMaximum()
        {
            var head = _linkedList.GetHead();
            if (head == null)
                throw new InvalidOperationException("Priority queue is empty.");

            _linkedList.Remove(head);

            return head.Value.Value!;
        }
    }
}
