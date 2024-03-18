using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.HackerRank.OneWeek.Day2
{
    internal class DiagonalDifference
    {
        /// <summary>
        /// PASSES 15/15
        /// 
        /// Given a square matrix, calculate the absolute difference between the sums of its diagonals.
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static int diagonalDifference(List<List<int>> arr)
        {
            long primaryDiagonalSum = 0;
            long secondaryDiagonalSum = 0;
            for (var i = 0; i < arr.Count; i++)
            {
                primaryDiagonalSum += arr[i][i];
                secondaryDiagonalSum += arr[i][arr.Count - i - 1];
            }

            return (int)Math.Abs(primaryDiagonalSum - secondaryDiagonalSum);
        }
    }
}
