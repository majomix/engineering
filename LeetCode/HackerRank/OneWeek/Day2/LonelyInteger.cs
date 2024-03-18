using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.HackerRank.OneWeek.Day2
{
    internal class LonelyInteger
    {
        /// <summary>
        /// PASSES 15/15
        /// 
        /// Given an array of integers, where all elements but one occur twice, find the unique element.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static int lonelyinteger(List<int> a)
        {
            var hash = new HashSet<int>();

            foreach (var number in a)
            {
                if (!hash.Contains(number))
                {
                    hash.Add(number);
                }
                else
                {
                    hash.Remove(number);
                }
            }

            foreach (var value in hash)
            {
                return value;
            }

            return -1;
        }
    }
}
