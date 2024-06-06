namespace LeetCode.Cracking.LinkedLists
{
    public class CrackingLinkedListNode
    {
        public CrackingLinkedListNode? Next { get; set; }
        public CrackingLinkedListNode? Previous { get; set; }
        public CrackingLinkedListNode? Last { get; set; }

        public int Data { get; set; }

        public CrackingLinkedListNode(
            int value,
            CrackingLinkedListNode? next,
            CrackingLinkedListNode? previous)
        {
            Data = value;
            Next = next;
            Previous = previous;
        }

        public CrackingLinkedListNode(int value)
        {
            Data = value;
        }

        public CrackingLinkedListNode()
        { }

        public void SetNext(CrackingLinkedListNode? node)
        {
            Next = node;

            if (this == Last)
            {
                Last = node;
            }

            if (node != null && node.Previous != this)
            {
                node.SetPrevious(this);
            }
        }

        public void SetPrevious(CrackingLinkedListNode? node)
        {
            Previous = node;

            if (node != null && node.Next != this)
            {
                node.SetNext(this);
            }
        }

        public CrackingLinkedListNode Clone()
        {
            CrackingLinkedListNode? copyOfNext = null;

            if (Next != null)
            {
                copyOfNext = Next.Clone();
            }

            var copyOfThis = new CrackingLinkedListNode(Data, copyOfNext, null);
            
            return copyOfThis;
        }
    }
}
