using DataStructures.DynamicArray;

namespace DataStructures.Heap
{
    public abstract class AbstractHeapByDynamicArray<TKey, TValue> where TKey : IComparable<TKey>
    {
        private readonly HeapNode<TKey, TValue> _dummyNode = new();

        protected const uint IndexOfDummyItem = 0;
        protected const uint IndexOfFirstItem = 1;
        
        protected readonly CustomDynamicArray<HeapNode<TKey, TValue>> Array;
        protected const int InitialArraySize = 4;

        protected AbstractHeapByDynamicArray(uint capacity)
        {
            Array = new CustomDynamicArray<HeapNode<TKey, TValue>>(capacity) { _dummyNode };
        }

        public uint Count => Array.Count - IndexOfFirstItem;

        public void Insert(TKey key, TValue value)
        {
            var heapNodeToInsert = new HeapNode<TKey, TValue> { Key = key, Value = value };
            Array.Add(heapNodeToInsert);

            var indexOfNewItem = Array.Count - 1;
            SiftUp(indexOfNewItem);
        }

        public TValue PeekRoot()
        {
            if (Array.Count == 1)
                throw new InvalidOperationException("Heap is empty.");

            return Array[IndexOfFirstItem].Value!;
        }

        public TValue ExtractRoot()
        {
            if (Array.Count == 1)
                throw new InvalidOperationException("Heap is empty.");

            var topElement = Array[IndexOfFirstItem];

            var indexOfLastElement = Array.Count - 1;
            var lastElement = Array[indexOfLastElement];
            Array.RemoveAt(indexOfLastElement);

            if (indexOfLastElement != IndexOfFirstItem)
            {
                Array[IndexOfFirstItem] = lastElement;
                SiftDown(IndexOfFirstItem);
            }

            return topElement.Value!;
        }

        public void BuildHeap(TValue[] items, Func<TValue, TKey> keySelector)
        {
            foreach (var item in items)
            {
                Array.Add(new HeapNode<TKey, TValue> { Key = keySelector(item), Value = item });
            }

            var itemsToSiftDown = (uint)items.Length / 2;
            for (uint i = itemsToSiftDown; i >= 1; i--)
            {
                SiftDown(i);
            }
        }

        public void Clear()
        {
            Array.Clear();
            Array.Add(_dummyNode);
        }

        protected abstract bool NodesBreaksHeapProperty(TKey leftKey, TKey rightKey);

        protected void SiftUp(uint indexToSiftUp)
        {
            var heapNodeToBubbleUp = Array[indexToSiftUp];
            var parentIndex = GetParentIndex(indexToSiftUp);

            while (indexToSiftUp > IndexOfFirstItem && NodesBreaksHeapProperty(Array[parentIndex].Key!, heapNodeToBubbleUp.Key!))
            {
                Array[indexToSiftUp] = Array[parentIndex];
                indexToSiftUp = parentIndex;
                parentIndex = GetParentIndex(indexToSiftUp);
            }

            Array[indexToSiftUp] = heapNodeToBubbleUp;
        }

        protected void SiftDown(uint indexToSiftDown)
        {
            var leftChildIndex = GetLeftChildIndex(indexToSiftDown);
            var rightChildIndex = GetRightChildIndex(indexToSiftDown);

            if (Array.Count <= leftChildIndex)
                return;

            var childIndexFulfillingHeapProperty = leftChildIndex;
            if (Array.Count > rightChildIndex && NodesBreaksHeapProperty(Array[leftChildIndex].Key!, Array[rightChildIndex].Key!))
            {
                childIndexFulfillingHeapProperty = rightChildIndex;
            }

            if (NodesBreaksHeapProperty(Array[indexToSiftDown].Key!, Array[childIndexFulfillingHeapProperty].Key!))
            {
                SwapElements(indexToSiftDown, childIndexFulfillingHeapProperty);
                SiftDown(childIndexFulfillingHeapProperty);
            }
        }

        protected uint FindValueIndex(TValue value)
        {
            var comparer = EqualityComparer<TValue>.Default;

            for (uint i = 0; i < Array.Count; i++)
            {
                if (comparer.Equals(Array[i].Value, value))
                {
                    return i;
                }
            }

            return IndexOfDummyItem;
        }

        private uint GetParentIndex(uint index)
        {
            // integer division performs floor
            return index / 2;
        }

        private uint GetLeftChildIndex(uint index)
        {
            return index * 2;
        }

        private uint GetRightChildIndex(uint index)
        {
            return GetLeftChildIndex(index) + 1;
        }

        private void SwapElements(uint left, uint right)
        {
            (Array[left], Array[right]) = (Array[right], Array[left]);
        }
    }
}
