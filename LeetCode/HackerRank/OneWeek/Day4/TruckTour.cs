using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.HackerRank.OneWeek.Day4
{
    internal class TruckTour
    {
        public static int truckTour(List<List<int>> petrolpumps)
        {
            var minimumIndex = -1;
            var minimumValue = Int32.MaxValue;
            var solutionExists = false;

            for (var i = 0; i < petrolpumps.Count; i++)
            {
                var currentPump = petrolpumps[i];
                var fuel = currentPump[0] - currentPump[1];

                if (fuel < minimumValue)
                {
                    minimumIndex = i;
                    minimumValue = fuel;
                }
            }

            return -1;
        }

        public static void TestCase()
        {
            truckTour(new List<List<int>>
            {
                new() { 1, 5 },
                new() { 10, 3 },
                new() { 3, 4 }
            });
        }
    }
}
