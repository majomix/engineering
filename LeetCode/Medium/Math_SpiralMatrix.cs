using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.Medium
{
    /// <summary>
    /// Given an m x n matrix, return all elements of the matrix in spiral order.
    /// </summary>
    internal class Math_SpiralMatrix
    {
        public IList<int> SpiralOrder(int[][] matrix)
        {
            var result = new List<int>();
            var topBoundary = 0;
            var bottomBoundary = matrix.Length - 1;
            var leftBoundary = 0;
            var rightBoundary = matrix[0].Length - 1;

            while (topBoundary <= bottomBoundary && leftBoundary <= rightBoundary)
            {
                for (var j = leftBoundary; j <= rightBoundary; j++)
                {
                    result.Add(matrix[topBoundary][j]);
                }
                topBoundary++;

                for (var i = topBoundary; i <= bottomBoundary; i++)
                {
                    result.Add(matrix[i][rightBoundary]);
                }
                rightBoundary--;

                if (topBoundary <= bottomBoundary)
                {
                    for (var j = rightBoundary; j >= leftBoundary; j--)
                    {
                        result.Add(matrix[bottomBoundary][j]);
                    }
                }
                bottomBoundary--;

                if (leftBoundary <= rightBoundary)
                {
                    for (var i = bottomBoundary; i >= topBoundary; i--)
                    {
                        result.Add(matrix[i][leftBoundary]);
                    }
                }
                leftBoundary++;
            }

            return result;
        }

        public static void TestCase()
        {
            var math = new Math_SpiralMatrix();
            var matrix1 = new int[3][];
            matrix1[0] = new int[3] { 1, 2, 3 };
            matrix1[1] = new int[3] { 4, 5, 6 };
            matrix1[2] = new int[3] { 7, 8, 9 };
            var shouldBe1_2_3_6_9_8_7_4_5 = math.SpiralOrder(matrix1);

            var matrix2 = new int[3][];
            matrix2[0] = new int[4] { 1, 2, 3, 4 };
            matrix2[1] = new int[4] { 5, 6, 7, 8 };
            matrix2[2] = new int[4] { 9, 10, 11, 12 };
            var shouldBe1_2_3_4_8_12_11_10_9_5_6_7 = math.SpiralOrder(matrix2);
        }
    }
}
