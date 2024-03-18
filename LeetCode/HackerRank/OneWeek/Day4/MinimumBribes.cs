using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.HackerRank.OneWeek.Day4
{
    internal class MinimumBribes
    {
        /// <summary>
        /// It is New Year's Day and people are in line for the Wonderland rollercoaster ride. Each person wears a sticker indicating their initial position in the queue from to.
        /// Any person can bribe the person directly in front of them to swap positions, but they still wear their original sticker.One person can bribe at most two others.
        /// Determine the minimum number of bribes that took place to get to a given queue order. Print the number of bribes, or, if anyone has bribed more than two people, print Too chaotic.
        /// </summary>
        public static void minimumBribes(List<int> q)
        {
            var pos = 0;
            var bribes = 0;

            while (pos < q.Count)
            {
                if (pos + 1 != q[pos])
                {
                    var dif = q[pos] - (pos + 1);
                    if (dif > 2 || dif < 0)
                    {
                        Console.WriteLine("Too chaotic");
                        bribes = -1;
                        break;
                    }

                    bribes += dif;

                    for (var i = 0; i < dif; i++)
                    {
                        (q[pos + i], q[pos + 1 + i]) = (q[pos + 1 + i], q[pos + i]);
                    }
                }

                pos++;
            }

            if (bribes != -1)
            {
                Console.WriteLine(bribes);
            }
        }

        public static void TestCase()
        {
            minimumBribes(new List<int> { 2, 1, 5, 3, 4 });
            minimumBribes(new List<int> { 2, 5, 1, 3, 4 });
        }
    }
}
