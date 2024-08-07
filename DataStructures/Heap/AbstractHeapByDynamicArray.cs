using DataStructures.DynamicArray;

namespace DataStructures.Heap
{
    public abstract class AbstractHeapByDynamicArray<TKey, TValue> where TKey : IComparable<TKey>
    {
        private const int IndexOfFirstItem = 1;
        private readonly HeapNode<TKey, TValue> _dummyNode = new();
        private readonly CustomDynamicArray<HeapNode<TKey, TValue>> _array;

        protected const int InitialArraySize = 4;

        protected AbstractHeapByDynamicArray(uint capacity)
        {
            _array = new CustomDynamicArray<HeapNode<TKey, TValue>>(capacity) { _dummyNode };
        }

        public void Insert(TKey key, TValue value)
        {
            var heapNodeToInsert = new HeapNode<TKey, TValue> { Key = key, Value = value };
            _array.Add(heapNodeToInsert);

            var indexOfNewItem = _array.Count - 1;
            SiftUp(indexOfNewItem);
        }

        public TValue ExtractRoot()
        {
            if (_array.Count == 1)
                throw new InvalidOperationException("Heap is empty.");

            var topElement = _array[IndexOfFirstItem];

            var indexOfLastElement = _array.Count - 1;
            var lastElement = _array[indexOfLastElement];
            _array.RemoveAt(indexOfLastElement);

            if (indexOfLastElement != IndexOfFirstItem)
            {
                _array[IndexOfFirstItem] = lastElement;
                SiftDown(IndexOfFirstItem);
            }

            return topElement.Value!;
        }

        public void BuildHeap(TValue[] items, Func<TValue, TKey> keySelector)
        {
            foreach (var item in items)
            {
                _array.Add(new HeapNode<TKey, TValue> { Key = keySelector(item), Value = item });
            }

            var itemsToSiftDown = (uint)items.Length / 2;
            for (uint i = itemsToSiftDown; i >= 1; i--)
            {
                SiftDown(i);
            }
        }

        public void Clear()
        {
            _array.Clear();
            _array.Add(_dummyNode);
        }

        protected abstract bool NodesBreaksHeapProperty(TKey leftKey, TKey rightKey);

        private void SiftUp(uint indexToSiftUp)
        {
            var heapNodeToBubbleUp = _array[indexToSiftUp];
            var parentIndex = GetParentIndex(indexToSiftUp);

            while (indexToSiftUp > IndexOfFirstItem && NodesBreaksHeapProperty(_array[parentIndex].Key!, heapNodeToBubbleUp.Key!))
            {
                _array[indexToSiftUp] = _array[parentIndex];
                indexToSiftUp = parentIndex;
                parentIndex = GetParentIndex(indexToSiftUp);
            }

            _array[indexToSiftUp] = heapNodeToBubbleUp;
        }


        private void SiftDown(uint indexToSiftDown)
        {
            var leftChildIndex = GetLeftChildIndex(indexToSiftDown);
            var rightChildIndex = GetRightChildIndex(indexToSiftDown);

            if (_array.Count <= leftChildIndex)
                return;

            var childIndexFulfillingHeapProperty = leftChildIndex;
            if (_array.Count > rightChildIndex && NodesBreaksHeapProperty(_array[leftChildIndex].Key!, _array[rightChildIndex].Key!))
            {
                childIndexFulfillingHeapProperty = rightChildIndex;
            }

            if (NodesBreaksHeapProperty(_array[indexToSiftDown].Key!, _array[childIndexFulfillingHeapProperty].Key!))
            {
                SwapElements(indexToSiftDown, childIndexFulfillingHeapProperty);
                SiftDown(childIndexFulfillingHeapProperty);
            }
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
            (_array[left], _array[right]) = (_array[right], _array[left]);
        }
    }
}
