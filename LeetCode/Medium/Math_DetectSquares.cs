using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.Medium
{
    /// <summary>
    /// You are given a stream of points on the X-Y plane. Design an algorithm that:
    /// Adds new points from the stream into a data structure. Duplicate points are allowed and should be treated as different points.
    /// Given a query point, counts the number of ways to choose three points from the data structure such that the three points
    /// and the query point form an axis-aligned square with positive area.
    /// 
    /// An axis-aligned square is a square whose edges are all the same length and are either parallel or perpendicular to the x-axis and y-axis.
    /// 
    /// Implement the DetectSquares class:
    /// DetectSquares() Initializes the object with an empty data structure.
    /// void add(int[] point) Adds a new point point = [x, y] to the data structure.
    /// int count(int[] point) Counts the number of ways to form axis-aligned squares with point point = [x, y] as described above.
    /// 
    /// Constraints:
    /// point.length == 2
    /// 0 <= x, y <= 1000
    /// At most 3000 calls in total will be made to add and count.
    /// </summary>
    internal class Math_DetectSquares
    {
        public class DetectSquares
        {
            private Dictionary<int, Dictionary<int, int>> _pointsByX = new();
            private Dictionary<int, Dictionary<int, int>> _pointsByY = new();

            public DetectSquares() { }

            public void Add(int[] point)
            {
                if (!_pointsByX.ContainsKey(point[0]))
                {
                    _pointsByX[point[0]] = new Dictionary<int, int>();
                }

                if (!_pointsByY.ContainsKey(point[1]))
                {
                    _pointsByY[point[1]] = new Dictionary<int, int>();
                }

                var yCollectionForX = _pointsByX[point[0]];
                if (!yCollectionForX.ContainsKey(point[1]))
                {
                    yCollectionForX[point[1]] = 0;
                }

                var xCollectionForY = _pointsByY[point[1]];
                if (!xCollectionForY.ContainsKey(point[0]))
                {
                    xCollectionForY[point[0]] = 0;
                }

                yCollectionForX[point[1]]++;
                xCollectionForY[point[0]]++;
            }

            public int Count(int[] point)
            {
                var result = 0;
                if (!_pointsByX.TryGetValue(point[0], out var pointsOnYAxisForGivenX) ||
                    !_pointsByY.TryGetValue(point[1], out var pointsOnXaxisForGivenY))
                {
                    return 0;
                }

                // p[0] . x
                // .    . .
                // d      y

                foreach (var x in pointsOnXaxisForGivenY)
                {
                    if (_pointsByX.TryGetValue(x.Key, out var pointsOnYaxisForInspectedX))
                    {
                        foreach (var y in pointsOnYaxisForInspectedX)
                        {
                            var dx = point[0];
                            var dy = y.Key;

                            if (!pointsOnYAxisForGivenX.ContainsKey(dy) || Math.Abs(point[1] - y.Key) == 0 || Math.Abs(point[0] - x.Key) != Math.Abs(point[1] - y.Key))
                            {
                                continue;
                            }

                            var diagonalPointCount = pointsOnYAxisForGivenX[dy];

                            result += y.Value * x.Value * diagonalPointCount;
                        }
                    }
                }

                return result;
            }
        }

        public static void TestCase()
        {
            var detectSquares = new DetectSquares();

            detectSquares.Add(new int[] { 3, 10 });
            detectSquares.Add(new int[] { 11, 2 });
            detectSquares.Add(new int[] { 3, 2 });
            var shouldBe1 = detectSquares.Count(new int[] { 11, 10 }); // return 1. You can choose: - The first, second, and third points
            var shouldBe0 = detectSquares.Count(new int[] { 14, 8 });  // return 0. The query point cannot form a square with any points in the data structure.
            detectSquares.Add(new int[] { 11, 2 });    // Adding duplicate points is allowed.
            var shouldBe2 = detectSquares.Count(new int[] { 11, 10 }); // return 2. You can choose: - The first, second, and third points, - The first, third, and fourth points
        }
    }
}
