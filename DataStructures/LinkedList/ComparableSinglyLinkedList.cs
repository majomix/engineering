namespace DataStructures.LinkedList
{
    public class ComparableSinglyLinkedList<T> : CustomSinglyLinkedList<T> where T : IComparable
    {
        public bool MergeSortedLists(ComparableSinglyLinkedList<T> otherList)
        {
            var thisCurrentNode = GetHead();
            var otherCurrentNode = otherList.GetHead();

            if (thisCurrentNode == null)
            {
                SetHead(otherCurrentNode);
                return true;
            }

            if (otherCurrentNode == null)
            {
                return true;
            }

            SinglyLinkedListNode<T>? mergedListCurrent = null;

            while (thisCurrentNode != null || otherCurrentNode != null)
            {
                var pivot = SelectPivot(thisCurrentNode, otherCurrentNode);

                if (mergedListCurrent == null)
                {
                    mergedListCurrent = pivot;
                    SetHead(mergedListCurrent);
                }
                else
                {
                    mergedListCurrent.Next = pivot;
                    mergedListCurrent = mergedListCurrent.Next;
                }

                if (pivot == thisCurrentNode)
                    thisCurrentNode = thisCurrentNode.Next;
                
                if (pivot == otherCurrentNode)
                    otherCurrentNode = otherCurrentNode.Next;

                mergedListCurrent.Parent = this;
            }

            return true;
        }

        private SinglyLinkedListNode<T> SelectPivot(SinglyLinkedListNode<T>? thisListItem, SinglyLinkedListNode<T>? otherListItem)
        {
            var comparer = Comparer<T>.Default;

            // take first if other is null or if both exist and first has higher value
            var shouldSelectFirst = otherListItem == null ||
                                    (thisListItem != null &&
                                     comparer.Compare(thisListItem.Value, otherListItem.Value) <= 0);
            
            return shouldSelectFirst ? thisListItem! : otherListItem!;
        }
    }
}
