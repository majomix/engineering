using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.HackerRank.OneWeek.Day3
{
    public class ZigZagArray
    {
        public static void findZigZagSequence(int[] a, int n)
        {
            a = a.OrderBy(a => a).ToArray();
            int mid = ((n + 1) / 2) - 1;
            int temp = a[mid];
            a[mid] = a[n - 1];
            a[n - 1] = temp;

            int st = mid + 1;
            int ed = n - 2;
            while (st <= ed)
            {
                temp = a[st];
                a[st] = a[ed];
                a[ed] = temp;
                st = st + 1;
                ed = ed - 1;
            }
            Debug.WriteLine("");
        }

        public static void TestCase()
        {
            findZigZagSequence(new []{ 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 9);
        }
    }
}
