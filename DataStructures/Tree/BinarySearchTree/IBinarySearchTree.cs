namespace DataStructures.Tree.BinarySearchTree
{
    public interface IBinarySearchTree<T>
    {
        /// <summary>
        /// Insert item into binary search tree.
        /// </summary>
        /// <param name="item">Item to add.</param>
        void Insert(T item);

        /// <summary>
        /// Remove item from binary search tree.
        /// </summary>
        /// <param name="item">Item to remove.</param>
        /// <returns>True if removed, false if not found.</returns>
        bool Remove(T item);

        /// <summary>
        /// Search for an item in binary search tree.
        /// </summary>
        /// <param name="item">Item to search.</param>
        /// <returns>Corresponding node or null if not found.</returns>
        BinaryTreeSearchResult<T>? Search(T item);
    }
}
