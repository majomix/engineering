using FluentAssertions;
using NUnit.Framework;

namespace LeetCode.Cracking.ArraysAndStrings
{
    /// <summary>
    /// Given an image represented by an N x N matrix where each pixel in the image is represented by an integer,
    /// write a method to rotate the image by 90 degrees.
    ///
    /// Questions to ask:
    /// Clockwise or anti-clockwise?
    ///
    /// Solution:
    /// * go layer by layer, outwards to inwards
    /// </summary>
    internal class Task1_7RotateMatrix
    {
        public int[,] RotateMatrix(int[,] matrix)
        {
            if (matrix.GetLength(0) != matrix.GetLength(1))
                throw new ArgumentException("Unsupported type of matrix!");

            var length = matrix.GetLength(0);

            var result = new int[length, length];

            for (var row = 0; row < length; row++)
            {
                for (var column = 0; column < length; column++)
                {
                    // [0,0] -> [0,2]
                    // [0,1] -> [1,2]
                    // [0,2] -> [2,2]
                    // [1,0] -> [0,1]
                    // [1,1] -> [1,1]
                    // [1,2] -> [2,1]
                    // [2,0] -> [0,0]
                    // [2,1] -> [1,0]
                    // [2,2] -> [2,0]
                    var item = matrix[row, column];
                    result[column, length - 1 - row] = item;
                }
            }

            return result;
        }

        /// <summary>
        /// Can you do it in place?
        /// </summary>
        public int[,] RotateMatrixInPlace(int[,] matrix)
        {
            if (matrix.GetLength(0) != matrix.GetLength(1))
                throw new ArgumentException("Unsupported type of matrix!");

            var length = matrix.GetLength(0);
            var layers = length / 2;

            for (var layer = 0; layer < layers; layer++)
            {
                var start = layer;
                var end = length - 1 - layer;
                
                for (var column = start; column < end; column++)
                {
                    int offset = column - start;

                    // [row, column]
                    var tmp = matrix[start, column];

                    // left top corner <- left bottom corner
                    matrix[start, column] = matrix[end - offset, start];

                    // left bottom corner <- right bottom corner
                    matrix[end - offset, start] = matrix[end, end - offset];

                    // right bottom corner <- right top corner
                    matrix[end, end - offset] = matrix[column, end];

                    // right top corner <- tmp
                    matrix[column, end] = tmp;
                }
            }

            return matrix;
        }
    }

    [TestFixture]
    public class Task1_7RotateMatrixTests
    {
        private static object[] testCases =
        {
            new object[] {
                new[,] 
                { 
                    { 1,2,3 },
                    { 4,5,6 },
                    { 7,8,9 }
                },
                new[,] 
                { 
                    { 7,4,1 }, 
                    { 8,5,2 }, 
                    { 9,6,3 }
                }
            }
        };

        [TestCaseSource(nameof(testCases))]
        public void RotateMatrixTest(int[,] input, int[,] output)
        {
            // arrange
            var sut = new Task1_7RotateMatrix();

            // act
            var result = sut.RotateMatrix(input);

            // assert
            result.Should().BeEquivalentTo(output);
        }

        [TestCaseSource(nameof(testCases))]
        public void RotateMatrixInPlaceTest(int[,] input, int[,] output)
        {
            // arrange
            var sut = new Task1_7RotateMatrix();

            // act
            var result = sut.RotateMatrixInPlace(input);

            // assert
            result.Should().BeEquivalentTo(output);
        }
    }
}
