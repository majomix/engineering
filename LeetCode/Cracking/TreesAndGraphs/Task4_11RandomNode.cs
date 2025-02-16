using DataStructures.Tree.BinarySearchTree;

namespace LeetCode.Cracking.TreesAndGraphs;

internal class Task4_11RandomNode
{
    /// <summary>
    /// You are implementing a binary search tree class from scratch, which, in addition to insert, find, and delete
    /// has a method getRandomNode which returns a random node from the tree. All nodes should be equally likely to be chosen.
    /// 
    /// Solutions:
    /// 1. Dedicated index structure.
    /// All nodes can be added to an array at O(n) speed and O(n) extra space.
    /// Optimizations:
    /// a) Maintain this array internally and update it with each insert/delete operation.
    /// b) Instead of keeping an array, assign a number to each node and use binary search to quickly retrieve the random node.
    /// Both methods require keeping an up-to-date structure of indices.
    /// 
    /// 2. Calculate probability for each node.
    /// Generate node index upfront, same like in the previous approach, and make sure every node is selected with P(n)=1/N.
    /// </summary>
    public BinaryTreeNode<int> GetRandomNodeByStoringNodes(BinaryTreeNode<int> tree)
    {
        var allNodes = new List<BinaryTreeNode<int>>();
        AddSubtreeToList(tree, allNodes);

        var random = new Random();
        return allNodes[random.Next(0, allNodes.Count - 1)];
    }

    private void AddSubtreeToList(BinaryTreeNode<int>? root, List<BinaryTreeNode<int>> list)
    {
        if (root == null)
            return;

        list.Add(root);
        AddSubtreeToList(root.Left, list);
        AddSubtreeToList(root.Right, list);
    }

    public TreeNodeWithSizeCounter? GetRandomNodeByProbability(TreeWithRandomNode tree)
    {
        return tree.GetRandomNode();
    }
}

public class TreeWithRandomNode
{
    private TreeNodeWithSizeCounter? _root;

    public int Size
    {
        get
        {
            if (_root == null)
                return 0;

            return _root.Size;
        }
    }
    
    public TreeNodeWithSizeCounter? GetRandomNode()
    {
        if (_root == null)
            return null;

        var generator = new Random();
        var randomIndex = generator.Next(0, Size);

        return _root.GetIthNode(randomIndex);
    }

    public void InsertInOrder(int value)
    {
        if (_root == null)
        {
            _root = new TreeNodeWithSizeCounter(value);
            return;
        }
           
        _root.InsertInOrder(value);
    }
}

public class TreeNodeWithSizeCounter
{
    public int Value { get; set; }
    public TreeNodeWithSizeCounter? Left { get; set; }
    public TreeNodeWithSizeCounter? Right { get; set; }
    public int Size { get; private set; }

    public TreeNodeWithSizeCounter(int value)
    {
        Value = value;
        Size = 1;
    }

    internal TreeNodeWithSizeCounter? GetIthNode(int randomIndex)
    {
        var leftSize = Left == null ? 0 : Left.Size;
        if (randomIndex < leftSize)
        {
            return Left!.GetIthNode(randomIndex);
        }
        else if (randomIndex == leftSize)
        {
            return this;
        }
        else
        {
            return Right!.GetIthNode(randomIndex - (leftSize + 1));
        }
    }

    internal void InsertInOrder(int valueToInsert)
    {
        if (valueToInsert < Value)
        {
            if (Left == null)
                Left = new TreeNodeWithSizeCounter(valueToInsert);
            else
                Left.InsertInOrder(valueToInsert);
        }
        else
        {
            if (Right == null)
                Right = new TreeNodeWithSizeCounter(valueToInsert);
            else
                Right.InsertInOrder(valueToInsert);
        }
        Size++;
    }
}
