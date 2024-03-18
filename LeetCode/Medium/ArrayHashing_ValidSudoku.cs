using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.Medium
{
    /// <summary>
    /// Determine if a 9 x 9 Sudoku board is valid. Only the filled cells need to be validated according to the following rules:
    /// 1. Each row must contain the digits 1-9 without repetition.
    /// 2. Each column must contain the digits 1-9 without repetition.
    /// 3. Each of the nine 3 x 3 sub-boxes of the grid must contain the digits 1-9 without repetition.
    /// A Sudoku board(partially filled) could be valid but is not necessarily solvable.
    /// Only the filled cells need to be validated according to the mentioned rules.
    /// </summary>
    internal class ArrayHashing_ValidSudoku
    {
        public bool IsValidSudoku(char[][] board)
        {
            var rowMemo = new Dictionary<int, HashSet<char>>();
            var columnMemo = new Dictionary<int, HashSet<char>>();
            var squareMemo = new Dictionary<string, HashSet<char>>();

            for (var i = 0; i < board.Length; i++)
            {
                var currentRow = board[i];

                if (!rowMemo.ContainsKey(i))
                {
                    rowMemo[i] = new HashSet<char>();
                }
                var currentRowMemo = rowMemo[i];

                for (var j = 0; j < board.Length; j++)
                {
                    var value = currentRow[j];
                    
                    if (!columnMemo.ContainsKey(j))
                    {
                        columnMemo[j] = new HashSet<char>();
                    }
                    var currentColumnMemo = columnMemo[j];

                    var currentSquareMemoKey = $"{j / 3} {i / 3}";
                    if (!squareMemo.ContainsKey(currentSquareMemoKey))
                    {
                        squareMemo[currentSquareMemoKey] = new HashSet<char>();
                    }
                    var currentSquareMemo = squareMemo[currentSquareMemoKey];

                    if (value != '.')
                    {
                        if (!currentRowMemo.Add(value) || !currentColumnMemo.Add(value) || !currentSquareMemo.Add(value))
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        public static void TestCase()
        {
            var arrhash = new ArrayHashing_ValidSudoku();
            var validBoard = new char[9][];
            validBoard[0] = new char[9] { '5', '3', '.', '.', '7', '.', '.', '.', '.' };
            validBoard[1] = new char[9] { '6', '.', '.', '1', '9', '5', '.', '.', '.' };
            validBoard[2] = new char[9] { '.', '9', '8', '.', '.', '.', '.', '6', '.' };
            validBoard[3] = new char[9] { '8', '.', '.', '.', '6', '.', '.', '.', '3' };
            validBoard[4] = new char[9] { '4', '.', '.', '8', '.', '3', '.', '.', '1' };
            validBoard[5] = new char[9] { '7', '.', '.', '.', '2', '.', '.', '.', '6' };
            validBoard[6] = new char[9] { '.', '6', '.', '.', '.', '.', '2', '8', '.' };
            validBoard[7] = new char[9] { '.', '.', '.', '4', '1', '9', '.', '.', '5' };
            validBoard[8] = new char[9] { '.', '.', '.', '.', '8', '.', '.', '7', '9' };
            var shouldBeTrue = arrhash.IsValidSudoku(validBoard);

            var invalidBoard = new char[9][];
            invalidBoard[0] = new char[9] { '8', '3', '.', '.', '7', '.', '.', '.', '.' };
            invalidBoard[1] = new char[9] { '6', '.', '.', '1', '9', '5', '.', '.', '.' };
            invalidBoard[2] = new char[9] { '.', '9', '8', '.', '.', '.', '.', '6', '.' };
            invalidBoard[3] = new char[9] { '8', '.', '.', '.', '6', '.', '.', '.', '3' };
            invalidBoard[4] = new char[9] { '4', '.', '.', '8', '.', '3', '.', '.', '1' };
            invalidBoard[5] = new char[9] { '7', '.', '.', '.', '2', '.', '.', '.', '6' };
            invalidBoard[6] = new char[9] { '.', '6', '.', '.', '.', '.', '2', '8', '.' };
            invalidBoard[7] = new char[9] { '.', '.', '.', '4', '1', '9', '.', '.', '5' };
            invalidBoard[8] = new char[9] { '.', '.', '.', '.', '8', '.', '.', '7', '9' };
            var shouldBeFalse = arrhash.IsValidSudoku(invalidBoard);
        }
    }
}
