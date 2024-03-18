using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.HackerRank.OneWeek.Day2
{
    internal class FlippingMatrix
    {
        /// <summary>
        /// PASSES 8/8
        /// 
        /// Maximize 2n x 2n matrix so that numbers in upper left n x n quadrant has the largest sum.
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static int flippingMatrix(List<List<int>> matrix)
        {
            var matrixSize = matrix.Count;
            var subMatrixSize = matrixSize / 2;

            int output = 0;

            for (var i = 0; i < subMatrixSize; i++)
            {
                for (var j = 0; j < subMatrixSize; j++)
                {
                    var currentRow = i;
                    var currentColumn = j;
                    var mirroredRow = matrixSize - i - 1;
                    var mirroredColumn = matrixSize - j - 1;

                    output += Math.Max(
                        Math.Max(matrix[currentRow][currentColumn], matrix[currentRow][mirroredColumn]),
                        Math.Max(matrix[mirroredRow][currentColumn], matrix[mirroredRow][mirroredColumn]));
                }
            }

            return output;
        }

        public static void TestCase()
        {
            var shouldBe414 = flippingMatrix(new List<List<int>>
            {
                new() { 112, 42, 83, 119 },
                new() { 56, 125, 56, 49 },
                new() { 15, 78, 101, 43 },
                new() { 62, 98, 114, 108 },
            });
        }
    }
}
