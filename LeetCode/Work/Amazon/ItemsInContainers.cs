using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.Work.Amazon
{
    public static class ItemsInContainers
    {
        /// <summary>
        /// PASSING 15/15
        /// 
        /// <p>Amazon would like to know how much inventory exists in their closed inventory compartments. Given a string <em>s </em>consisting of items as "*" and closed compartments as an open and close "|", an array of starting indices <em>startIndices</em>, and an array of ending indices <em>endIndices</em>, determine the number of items in closed compartments within the substring between the two indices, inclusive.</p>
        /// <li>An item is represented as an asterisk('*' = ascii decimal 42)</li>
        /// <li>A compartment is represented as a pair of pipes that may or may not have items between them('|' = ascii decimal 124).</li>
        /// <strong>Example</strong></p>
        /// <p><em>s = '</em>|**|*|*'</p>
        /// <p><em>startIndices = [1, 1]</em></p>
        /// <p><em>endIndices = [5, 6]</em></p>
        /// <p>The string has a total of 2 closed compartments, one with 2 items and one with 1 item.For the first pair of indices, <em>(1, 5),</em> the substring is '|**|*'. There are<em>2</em> items in a compartment.</p>
        /// <p>For the second pair of indices, <em>(1, 6)</em>, the substring is '|**|*|' and there are<em>2 + 1 = 3</em> items in compartments.</p>
        /// <p>Both of the answers are returned in an array, <em> [2, 3].</em></p>
        /// <p><strong>Function Description. </strong></p>
        /// <p>Complete the <em>numberOfItems</em> function in the editor below.The function must return an integer array that contains the results for each of the<em> startIndices[i]</em> and<em> endIndices[i] </em>pairs<i>.</i></p>
        /// </summary>
        /// <param name="s"></param>
        /// <param name="startIndices"></param>
        /// <param name="endIndices"></param>
        public static List<int> numberOfItems(string s, List<int> startIndices, List<int> endIndices)
        {
            var items = new Dictionary<int, int>();

            var totalSumOfItems = 0;
            var sumOfItemsInCompartment = 0;
            var isInsideCompartment = false;
            
            for (var i = 0; i < s.Length; i++)
            {
                var character = s[i];
                if (character == '*' && isInsideCompartment)
                {
                    sumOfItemsInCompartment++;
                }
                else if (character == '|')
                {
                    if (isInsideCompartment)
                    {
                        totalSumOfItems += sumOfItemsInCompartment;
                    }
                    items[i] = totalSumOfItems;
                    isInsideCompartment = true;
                    sumOfItemsInCompartment = 0;
                }
            }

            var result = new List<int>();

            for (var i = 0; i < startIndices.Count; i++)
            {
                var startIndex = startIndices[i] - 1;
                for (var j = startIndex; j < s.Length; j++)
                {
                    if (s[j] == '|')
                    {
                        startIndex = j;
                        break;
                    }
                }

                var endIndex = endIndices[i] - 1;
                for (var j = endIndex; j >= 0; j--)
                {
                    if (s[j] == '|')
                    {
                        endIndex = j;
                        break;
                    }
                }

                if (startIndex > endIndex)
                {
                    result.Add(0);
                }
                else
                {
                    result.Add(items[endIndex] - items[startIndex]);
                }
            }

            return result;
        }

        public static void TestCase()
        {
            var shouldBe0 = numberOfItems("*|*|", new List<int> { 1 }, new List<int> { 3 });
            var shouldBe11 = numberOfItems("**|***||***|*|*", new List<int> { 1,1,6 }, new List<int> { 6,15,15 });
        }
    }
}
