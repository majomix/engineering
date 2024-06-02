namespace DataStructures.Tree.BinarySearchTree
{
    public class BinaryTreeNode<T>
    {
        public BinaryTreeNode(T value)
        {
            Value = value;
        }

        public T Value { get; set; }
        public BinaryTreeNode<T>? Left { get; set; }
        public BinaryTreeNode<T>? Right { get; set; }

        public bool IsLeaf => Left == null && Right == null;

        public bool HasOneSubtree => (Left == null && Right != null) || (Left != null && Right == null);

        public bool HasTwoSubtrees => Left != null && Right != null;
    }
}
