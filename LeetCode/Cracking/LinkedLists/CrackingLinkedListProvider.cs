namespace LeetCode.Cracking.LinkedLists
{
    internal static class CrackingLinkedListProvider
    {
        public static CrackingLinkedListNode CreateLinkedListWithDuplicates()
        {
            return new CrackingLinkedListNode(1,
                new CrackingLinkedListNode(2,
                    new CrackingLinkedListNode(2,
                        new CrackingLinkedListNode(3,
                            new CrackingLinkedListNode(4,
                                new CrackingLinkedListNode(5, new CrackingLinkedListNode(5), null), null), null), null), null), null);
        }

        public static CrackingLinkedListNode CreateLinkedListWithDepth(int depth)
        {
            var head = new CrackingLinkedListNode(0);

            var current = head;
            for (var i = 0; i < depth; i++)
            {
                current.Next = new CrackingLinkedListNode(i + 1);
                current = current.Next;
            }

            return head;
        }

        public static CrackingLinkedListNode CreateLinkedListWithReverseDepth(int depth)
        {
            var head = new CrackingLinkedListNode(depth);

            var current = head;
            for (var i = depth - 1; i >= 0; i--)
            {
                current.Next = new CrackingLinkedListNode(i);
                current = current.Next;
            }

            return head;
        }

        public static CrackingLinkedListNode CreateLinkedListWithContent(List<int> list)
        {
            var head = new CrackingLinkedListNode(list[0]);

            var current = head;
            for (var i = 1; i < list.Count; i++)
            {
                var item = list[i];
                current.Next = new CrackingLinkedListNode(item);
                current = current.Next;
            }

            return head;
        }
    }
}
