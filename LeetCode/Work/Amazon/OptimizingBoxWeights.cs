using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace LeetCode.Work.Amazon
{
    /// <summary>
    /// PASSING 15/15
    /// 
    /// An Amazon Fulfillment Associate has a set of items that need to be packed into two boxes.
    /// Given an integer array of the item weights (arr) to be packed, divide the item weights into two subsets, A and B,
    /// for packing into the associated boxes, while respecting the following conditions:
    /// - The intersection of A and B is null,
    /// - The union of A and B is equal to the original array,
    /// - The number of elements in subset A is minimal,
    /// - The sum of A's weights is greater than the sum of B's weights.
    /// Return the subset A in increasing order where the sum of A's weights in greater than the sum of B's weights.
    /// If more than one subset exists, return the one with the maximal total weight.
    /// </summary>
    public class OptimizingBoxWeights
    {
        public static List<int> minimalHeaviestSetA(List<int> arr)
        {
            var ordered = arr.OrderByDescending(n => n);
            long halfSum = 0;
            long sum = 0;

            foreach (var item in arr)
            {
                halfSum += item;
            }
            halfSum /= 2;

            var stack = new Stack<int>();
            
            foreach (var item in ordered)
            {
                stack.Push(item);
                sum += item;
                if (sum > halfSum)
                {
                    break;
                }
            }

            var result = new List<int>();
            foreach (var item in stack)
            {
                result.Add(item);
            }

            return result;
        }

        public static void TestCase()
        {
            var simpleCase1 = minimalHeaviestSetA(new List<int> { 5, 3, 2, 4, 1, 2 });
            var simpleCase2_ShouldBe_6_7 = minimalHeaviestSetA(new List<int> { 3, 7, 5, 6, 2 });
            var complexCase = minimalHeaviestSetA(new List<int> { 99398, 90664, 95026, 93226, 99364, 94435, 93954, 94169, 92725, 96487, 99082, 98044, 94770, 97527, 92038, 90138, 99713, 96057, 95285, 92107, 92471, 90643, 95207, 94180, 99630, 91667, 90043, 96867, 99827, 98216, 94966, 96169, 94520, 99753, 96852, 91038, 92455, 90177, 98181, 95629, 95003, 94002, 92873, 91745, 94974, 90518, 96869, 98817, 98533, 91981, 92773, 99707, 98386, 96181, 90987, 90730, 96560, 92039, 99415, 93343, 96757, 99288, 90039, 93158, 99541, 99748, 93458, 99176, 98419, 98226, 91815, 92406, 91249, 95634, 96448, 98426, 99370, 95593, 99094, 92850, 96316, 98458, 94457, 92165, 99078, 93502, 97618, 94923, 97470, 90802, 91848, 99927, 91380, 99268, 91168, 98683, 91397, 96227, 97242, 99148, 95671, 96968, 92056, 96602, 94682, 99749, 96241, 95324, 96084, 92903, 97856, 91689, 93768, 95897, 97998, 95269, 98843, 95443, 97693, 99599, 95389, 91112, 95179, 98349, 90502, 92287, 93913, 95843, 96408, 99740, 99420, 93085, 92252, 90638, 97451, 91754, 94517, 95382, 96814, 99005, 97998, 98288, 97719, 95316, 93736, 91858, 90523, 96120, 96393, 96144, 90331, 95511, 91165, 90534, 90083, 96916, 99539, 95183, 98494, 91305, 93925, 93284, 99452, 98245, 93536, 93354, 96376, 98907, 97741, 92838, 98403 });
            var complexCase2 = minimalHeaviestSetA(new List<int> { 63674, 55705, 58985, 75116, 51746, 80073, 15135, 41001, 99327, 52397, 69393, 23314, 76, 31706, 45026, 69764, 23626, 73740, 29035, 10440, 85360, 38291, 31249, 73066, 942, 37206, 14001, 22444, 81375, 33772, 30866, 39705, 49913, 30438, 21224, 78616, 78193, 23775, 56346, 23351, 45754, 63045, 22242, 24997, 24443, 21923, 54695, 12512, 77184, 81678, 12705, 57942, 39242, 28516, 87979, 25383, 19304, 87803, 186, 48558, 17600, 53528, 20321, 62781, 8750, 22654, 97887, 63693, 78152, 10872, 41457, 77370, 32454, 8810, 16809, 4195, 27027, 35897, 81919, 42389, 66332, 62837, 73973, 6148, 36694, 3052, 99931, 28057, 25240, 18471, 59258, 75767, 63604, 45687, 45093, 18898, 10512, 24772, 28450, 53688, 53459, 4934, 28934, 30156, 66422, 8721, 15065, 29318, 7946, 59625, 57401, 6520, 81886, 16699, 78533, 50980, 11743, 14203, 5381, 11361, 98739, 74313, 31321, 2542, 43650, 46568, 38319, 39674, 12917, 29006, 12564, 42230, 20648, 42826, 11012, 12939, 79865, 64931, 799, 24335, 48739, 70883, 98669, 39106, 60501, 4506, 83359, 49519, 79374, 69845, 40296, 45891, 69073, 36138, 25029, 14414, 95311, 57165, 99499, 20605, 9222, 32545, 3515, 56222, 74723, 80508, 72782, 14704, 9594, 49141, 4331, 13023, 7391, 66273, 408 });
        }
    }
}
