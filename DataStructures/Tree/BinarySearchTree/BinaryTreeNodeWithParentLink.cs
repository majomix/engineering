namespace DataStructures.Tree.BinarySearchTree;

public class BinaryTreeNodeWithParentLink<T>
{
    public T Value { get; set; }
    public BinaryTreeNodeWithParentLink<T>? Left { get; set; }
    public BinaryTreeNodeWithParentLink<T>? Right { get; set; }

    public BinaryTreeNodeWithParentLink<T>? Parent { get; set; }

    public BinaryTreeNodeWithParentLink(T value, BinaryTreeNodeWithParentLink<T>? parent)
    {
        Value = value;
        Parent = parent;
    }
}
