using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.Medium
{
    /// <summary>
    /// Given two non-negative integers num1 and num2 represented as strings, return the product of num1 and num2, also represented as a string.
    /// Note: You must not use any built-in BigInteger library or convert the inputs to integer directly.
    /// 
    /// 1 <= num1.length, num2.length <= 200
    /// num1 and num2 consist of digits only.
    /// Both num1 and num2 do not contain any leading zero, except the number 0 itself.
    /// </summary>
    internal class Math_MultiplyStrings
    {
        /// <summary>
        ///      49
        /// ×    57
        /// -------
        ///     343
        /// +  245 
        /// -------
        ///    2793
        ///    
        ///     456
        /// ×   123
        /// -------
        ///    1368
        ///    912 (10488)
        ///   456  
        /// -------
        ///   56088
        /// </summary>

        // simplification: store it as int array of size num1.Length + num2.Length (9999 * 9999 has result of 8 digits)
        public string Multiply(string num1, string num2)
        {
            if (num1 == "0" || num2 == "0")
            {
                return "0";
            }

            var carry = 0;
            var multiplicationResult = new List<string>();
            var pass = 0;

            for (var firstNumberIndex = num1.Length - 1; firstNumberIndex >= 0; firstNumberIndex--)
            {
                var pos = pass;
                for (var secondNumberIndex = num2.Length - 1; secondNumberIndex >= 0; secondNumberIndex--)
                {
                    var digit1 = num1[firstNumberIndex] - '0';
                    var digit2 = num2[secondNumberIndex] - '0';

                    var digitMultiplicationResult = digit1 * digit2 + carry;

                    if (pass > 0 && pos < multiplicationResult.Count)
                    {
                        var digit = multiplicationResult[pos][0] - '0';

                        var digitToAdd = (digitMultiplicationResult + digit) % 10;
                        carry = (digitMultiplicationResult + digit) / 10;
                        
                        multiplicationResult[pos] = digitToAdd.ToString();
                        pos++;
                    }
                    else
                    {
                        var digitToAdd = digitMultiplicationResult % 10;
                        carry = digitMultiplicationResult /= 10;

                        multiplicationResult.Add(digitToAdd.ToString());
                    }
                }

                if (carry > 0)
                {
                    multiplicationResult.Add($"{carry}");
                    carry = 0;
                }

                pass++;
            }

            var result = new StringBuilder();

            for (var i = multiplicationResult.Count - 1; i >= 0; i--)
            {
                result.Append(multiplicationResult[i]);
            }

            return result.ToString();
        }

        public static void TestCase()
        {
            var math = new Math_MultiplyStrings();
            var shouldBe2793 = math.Multiply("57", "49");
            var shouldBe6 = math.Multiply("2", "3");
            var shouldBe56088 = math.Multiply("123", "456");
        }
    }
}
