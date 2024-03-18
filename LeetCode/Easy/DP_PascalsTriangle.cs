using System.Collections.Generic;

namespace LeetCode.Easy
{
    public class DP_PascalsTriangle
    {
        public IList<IList<int>> Generate(int numRows)
        {
            var triangle = new List<IList<int>>(numRows);

            triangle.Add(new List<int> { 1 });
            
            for (var row = 1; row < numRows; row++)
            {
                var currentRow = new List<int> { 1 };
                var previousRow = triangle[row - 1];
                for (var i = 1; i < previousRow.Count; i++)
                {
                    currentRow.Add(previousRow[i - 1] + previousRow[i]);
                }
                currentRow.Add(1);

                triangle.Add(currentRow);
            }

            return triangle;
        }

        public static void TestCase()
        {
            var roman = new DP_PascalsTriangle();
            var triangle2 = roman.Generate(2);
            var triangle5 = roman.Generate(5);
        }
    }
}
