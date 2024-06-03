using FluentAssertions;
using NUnit.Framework;

namespace LeetCode.Cracking.ArraysAndStrings
{
    /// <summary>
    /// Write an algorithm such that if an element in an M x N matrix is 0, its entire row and column would be set to 0.
    ///
    /// Solution:
    /// * store which rows and columns shall be deleted and then delete them in second pass
    /// * space complexity optimization -> mark columns and rows to delete directly in the first column and row
    /// </summary>
    internal class Task1_8ZeroMatrix
    {
        public int[,] ZeroMatrix(int[,] matrix)
        {
            var height = matrix.GetLength(0);
            var width = matrix.GetLength(1);

            var rowsToZero = new HashSet<int>();
            var columnsToZero = new HashSet<int>();

            for (var row = 0; row < height; row++)
            {
                for (var column = 0; column < width; column++)
                {
                    if (matrix[row, column] == 0)
                    {
                        rowsToZero.Add(row);
                        columnsToZero.Add(column);
                    }
                }
            }

            for (var row = 0; row < height; row++)
            {
                for (var column = 0; column < width; column++)
                {
                    if (rowsToZero.Contains(row) || columnsToZero.Contains(column))
                    {
                        matrix[row, column] = 0;
                    }
                }
            }

            return matrix;
        }

        public int[,] ZeroMatrixNoExtraSpace(int[,] matrix)
        {
            var height = matrix.GetLength(0);
            var width = matrix.GetLength(1);

            // 1. check if first row or column shall be zeroed because this information will be lost
            var shouldZeroFirstColumn = false;
            var shouldZeroFirstRow = false;

            for (var column = 0; column < width; column++)
            {
                if (matrix[0, column] == 0)
                {
                    shouldZeroFirstRow = true;
                }
            }

            for (var row = 0; row < height; row++)
            {
                if (matrix[row, 0] == 0)
                {
                    shouldZeroFirstColumn = true;
                }
            }

            // 2. check the whole matrix and mark zero flags in the first row and column
            for (var row = 0; row < height; row++)
            {
                for (var column = 0; column < width; column++)
                {
                    if (matrix[row, column] == 0)
                    {
                        matrix[row, 0] = 0;
                        matrix[0, column] = 0;
                    }
                }
            }

            // 3. zero the matrix
            for (var row = 1; row < height; row++)
            {
                for (var column = 1; column < width; column++)
                {
                    if (matrix[row, 0] == 0 || matrix[0, column] == 0)
                    {
                        matrix[row, column] = 0;
                    }
                }
            }

            // 4. zero first row and column if needed
            if (shouldZeroFirstColumn)
            {
                for (var row = 0; row < height; row++)
                {
                    matrix[row, 0] = 0;
                }
            }

            if (shouldZeroFirstRow)
            {
                for (var column = 0; column < width; column++)
                {
                    matrix[0, column] = 0;
                }
            }


            return matrix;
        }
    }

    [TestFixture]
    public class Task1_8ZeroMatrixTests
    {
        private static object[] testCases =
        {
            new object[]
            {
                new[,]
                {
                    { 1,2,3 },
                    { 4,5,6 },
                    { 7,8,0 },
                    { 10,11,12 },
                },
                new[,]
                {
                    { 1,2,0 },
                    { 4,5,0 },
                    { 0,0,0 },
                    { 10,11,0 },
                },
            },
            new object[]
            {
                new[,]
                {
                    { 1,0,3 },
                    { 0,5,6 },
                    { 7,8,9 },
                    { 10,11,12 },
                },
                new[,]
                {
                    { 0,0,0 },
                    { 0,0,0 },
                    { 0,0,9 },
                    { 0,0,12 },
                },
            },
        };

        [TestCaseSource(nameof(testCases))]
        public void ZeroMatrixTest(int[,] input, int[,] output)
        {
            // arrange
            var sut = new Task1_8ZeroMatrix();

            // act
            var result = sut.ZeroMatrix(input);

            // assert
            result.Should().BeEquivalentTo(output);
        }

        [TestCaseSource(nameof(testCases))]
        public void ZeroMatrixNoExtraSpaceTest(int[,] input, int[,] output)
        {
            // arrange
            var sut = new Task1_8ZeroMatrix();

            // act
            var result = sut.ZeroMatrixNoExtraSpace(input);

            // assert
            result.Should().BeEquivalentTo(output);
        }
    }
}
