using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.Easy
{
    public class DP_ClimbingStairs
    {
        private Dictionary<int, int> _memo = new Dictionary<int, int>();

        public int ClimbStairs(int n)
        {
            if (n <= 1)
            {
                return 1;
            }

            if (_memo.ContainsKey(n))
            {
                return _memo[n];
            }

            var res = ClimbStairs(n - 1) + ClimbStairs(n - 2);

            _memo[n] = res;

            return res;
        }

        public static void TestCase()
        {
            var dp = new DP_ClimbingStairs();
            var res2 = dp.ClimbStairs(2);
            var res3 = dp.ClimbStairs(3);
        }
    }
}
