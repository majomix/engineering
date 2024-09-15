using DataStructures.Tree.BinarySearchTree;

namespace LeetCode.Cracking.TreesAndGraphs
{
    public static class TestTrees
    {
        //        0
        //       /
        //      1
        //     /
        //    2
        //   /
        //  3
        public static BinaryTreeNode<int> CreateLeftSkewedBinaryTree()
        {
            var tree = new BinaryTreeNode<int>(0)
            {
                Left = new BinaryTreeNode<int>(1)
                {
                    Left = new BinaryTreeNode<int>(2)
                    {
                        Left = new BinaryTreeNode<int>(3)
                    },
                }
            };

            return tree;
        }

        //  0
        //   \
        //    1
        //     \
        //      2
        //       \
        //        3
        public static BinaryTreeNode<int> CreateRightSkewedBinarySearchTree()
        {
            var tree = new BinaryTreeNode<int>(0)
            {
                Right = new BinaryTreeNode<int>(1)
                {
                    Right = new BinaryTreeNode<int>(2)
                    {
                        Right = new BinaryTreeNode<int>(3)
                    },
                }
            };

            return tree;
        }

        //         3
        //        / \
        //       2   4
        //      /     \
        //     1       5
        //    /         \
        //   0           6
        public static BinaryTreeNode<int> CreateTriangleBinarySearchTree()
        {
            var tree = new BinaryTreeNode<int>(3)
            {
                Right = new BinaryTreeNode<int>(4)
                {
                    Right = new BinaryTreeNode<int>(5)
                    {
                        Right = new BinaryTreeNode<int>(6)
                    }
                },
                Left = new BinaryTreeNode<int>(2)
                {
                    Left = new BinaryTreeNode<int>(1)
                    {
                        Left = new BinaryTreeNode<int>(0)
                    }
                }
            };

            return tree;
        }

        //         0
        //        / \
        //       2   1
        //      /    
        //     3     
        //      \      
        //       4       
        public static BinaryTreeNode<int> CreateHalfDiamondTree()
        {
            var tree = new BinaryTreeNode<int>(0)
            {
                Right = new BinaryTreeNode<int>(1)
                {
                    Right = new BinaryTreeNode<int>(2)
                    {
                        Right = new BinaryTreeNode<int>(3)
                    },
                },
                Left = new BinaryTreeNode<int>(4)
                {
                    Left = new BinaryTreeNode<int>(5)
                    {
                        Left = new BinaryTreeNode<int>(6)
                    }
                }
            };

            return tree;
        }

        //         8
        //        / \
        //       4   9
        //      /  \ 
        //     2    6
        //    / \  / \
        //   1  3 5   7
        public static BinaryTreeNode<int> CreateLeftHeavyBinarySearchTree()
        {
            var tree = new BinaryTreeNode<int>(8)
            {
                Right = new BinaryTreeNode<int>(9),
                Left = new BinaryTreeNode<int>(4)
                {
                    Left = new BinaryTreeNode<int>(2)
                    {
                        Left = new BinaryTreeNode<int>(1),
                        Right = new BinaryTreeNode<int>(3)
                    },
                    Right = new BinaryTreeNode<int>(6)
                    {
                        Left = new BinaryTreeNode<int>(5),
                        Right = new BinaryTreeNode<int>(7)
                    }
                }
            };

            return tree;
        }

        //         0
        //        / \
        //       2   1
        //      / \   \
        //     3   4   5   
        public static BinaryTreeNode<int> CreateBalancedTree()
        {
            var tree = new BinaryTreeNode<int>(0)
            {
                Right = new BinaryTreeNode<int>(1)
                {
                    Right = new BinaryTreeNode<int>(5)
                },
                Left = new BinaryTreeNode<int>(2)
                {
                    Left = new BinaryTreeNode<int>(3),
                    Right = new BinaryTreeNode<int>(4)
                }
            };

            return tree;
        }

        //         20
        //        /  \
        //      10    30
        //       \
        //        25
        public static BinaryTreeNode<int> CreateQuadrupleTree()
        {
            var tree = new BinaryTreeNode<int>(20)
            {
                Right = new BinaryTreeNode<int>(30),
                Left = new BinaryTreeNode<int>(10)
                {
                    Right = new BinaryTreeNode<int>(25)
                }
            };

            return tree;
        }
    }
}
