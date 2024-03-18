using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.HackerRank.OneWeek.Day4
{
    public class GridChallenge
    {
        /// <summary>
        /// PASSES 12/12
        /// 
        /// Given a square grid of characters in the range ascii[a-z], rearrange elements of each row alphabetically, ascending.
        /// Determine if the columns are also in ascending alphabetical order, top to bottom. Return YES if they are or NO if they are not.
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public static string gridChallenge(List<string> grid)
        {
            for (var i = 0; i < grid.Count; i++)
            {
                var row = grid[i];
                grid[i] = new string(row.OrderBy(s => s).ToArray());
            }

            var ascendingOrder = true;

            for (var i = 0; i < grid[0].Length; i++)
            {
                var last = grid[0][i];

                for (var j = 1; j < grid.Count; j++)
                {
                    if (last > grid[j][i])
                    {
                        ascendingOrder = false;
                    }

                    last = grid[j][i];
                }
            }

            return ascendingOrder ? "YES" : "NO";
        }

        public static void TestCase()
        {
            var shouldBeYes = gridChallenge(new List<string> { "ebacd", "fghij", "olmkn", "trpqs", "xywuv" });
            var shouldBeNo = gridChallenge(new List<string> { "qbacd", "fghij", "olmkn", "trpqs", "xywuv" });
            var shouldBeYes1 = gridChallenge(new List<string> { "l" });
        }
    }
}
