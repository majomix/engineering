using System.Diagnostics;
using FluentAssertions;
using NUnit.Framework;

namespace LeetCode.Cracking.Recursion;

/// <summary>
/// Write an algorithm to print all ways of arranging eight queens on an 8x8 chess board so that none of them share the same row, column or diagonal.
/// In this case, diagonal means all diagonals, not just the longest two.
/// 
/// Solution:
/// Backtracking. Place queens one row at a time. Make sure that each queen is placed in a valid column where it is not attacked by previously placed queens.
/// </summary>
internal class Task8_12EightQueens
{
    private const int BoardDimension = 8;

    public List<List<int>> ArrangeQueens(bool printResult)
    {
        var result = new List<List<int>>();

        ArrangeQueens(0, new List<int>(), result);

        if (printResult)
        {
            for (var i = 0; i < result.Count; i++)
            {
                PrintBoard(result[i], i);
            }
        }

        return result;
    }

    public void ArrangeQueens(int currentRow, List<int> rows, List<List<int>> result)
    {
        if (currentRow == BoardDimension)
        {
            result.Add(rows.ToList());
            return;
        }

        for (var currentColumn = 0; currentColumn < BoardDimension; currentColumn++)
        {
            if (IsValid(rows, currentColumn))
            {
                var rowsCopy = rows.ToList();
                rowsCopy.Add(currentColumn);
                ArrangeQueens(currentRow + 1, rowsCopy, result);
            }
        }
    }

    private bool IsValid(List<int> rows, int columnWithNewlyAddedQueen)
    {
        for (var i = 0; i < rows.Count; i++)
        {
            if (rows[i] == columnWithNewlyAddedQueen)
                return false;

            var xDistance = rows.Count - i;
            var yDistance = Math.Abs(rows[i] - columnWithNewlyAddedQueen);

            if (xDistance == yDistance)
                return false;
        }

        return true;
    }

    private void PrintBoard(List<int> board, int boardNumber)
    {
        Debug.WriteLine($"*** PRINTING BOARD {boardNumber} ***");

        Debug.WriteLine("-----------------");

        for (var i = 0; i < 8; i++)
        {
            Debug.Write("|");

            for (var y = 0; y < 8; y++)
            {
                Debug.Write(board[i] == y ? "x|" : " |");
            }

            Debug.WriteLine(string.Empty);
            Debug.WriteLine("-----------------");
        }

        Debug.WriteLine(string.Empty);
    }
}

[TestFixture]
public class Task8_12EightQueensTests
{
    [Test]
    public void ArrangeQueensTest()
    {
        // arrange
        var sut = new Task8_12EightQueens();

        // act
        var result = sut.ArrangeQueens(false);

        // assert
        result.Count.Should().Be(92);
    }
}