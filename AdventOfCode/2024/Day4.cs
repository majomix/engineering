using System.Drawing;
using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2024;

internal static class Day4
{
    /// <summary>
    /// As the search for the Chief continues, a small Elf who lives on the station tugs on your shirt; she'd like to know if you could help her with her word search.
    /// She only has to find one word: XMAS
    /// This word search allows words to be horizontal, vertical, diagonal, written backwards, or even overlapping other words.
    /// It's a little unusual, though, as you don't merely need to find one instance of XMAS - you need to find all of them.
    ///
    /// How many times does XMAS appear?
    /// </summary>
    public static int GetOccurrencesOfXmas(string[] input)
    {
        const string SearchedString = "XMAS";

        var letterPositions = BuildLetterPositionsDictionary(input, SearchedString);
        var startPoints = letterPositions[SearchedString[0]];
        var validWords = 0;
        var directions = new List<Point>
        {
            new(1, 0), // forward
            new(-1, 0), // backward
            new(0, 1), // down
            new(0, -1), // up
            new(1, 1), // diagonal forward-down
            new(-1, 1), // diagonal backward-down
            new(1, -1), // diagonal forward-up
            new(-1, -1), // diagonal backward-up
        };

        foreach (var startPoint in startPoints)
        {
            foreach (var direction in directions)
            {
                if (IsValidWord(letterPositions, startPoint, SearchedString, direction.X, direction.Y))
                {
                    validWords++;
                }
            }
        }

        return validWords;
    }

    private static Dictionary<char, HashSet<Point>> BuildLetterPositionsDictionary(string[] input, string SearchedString)
    {
        var letterPositions = SearchedString.ToDictionary(keyFunction => keyFunction, elementFunction => new HashSet<Point>());

        for (var row = 0; row < input.Length; row++)
        {
            for (var column = 0; column < input[row].Length; column++)
            {
                var character = input[row][column];
                if (SearchedString.Contains(character))
                {
                    letterPositions[character].Add(new Point(column, row));
                }
            }
        }

        return letterPositions;
    }

    private static bool IsValidWord(Dictionary<char, HashSet<Point>> letterPositions, Point startPoint, string word, int xShift, int yShift)
    {
        var lastPoint = startPoint;

        for (var i = 1; i < word.Length; i++)
        {
            var currentCharacter = word[i];
            var nextPoint = new Point(lastPoint.X + xShift, lastPoint.Y + yShift);
            if (!letterPositions[currentCharacter].Contains(nextPoint))
            {
                return false;
            }

            lastPoint = nextPoint;
        }

        return true;
    }

    /// <summary>
    /// Looking for the instructions, you flip over the word search to find that this isn't actually an XMAS puzzle.
    /// It's an X-MAS puzzle in which you're supposed to find two MAS in the shape of an X.
    ///
    /// How many times does an X-MAS appear?
    /// </summary>
    public static int GetOccurrencesOfCrossMas(string[] input)
    {
        const string SearchedString = "MAS";
        var letterPositions = BuildLetterPositionsDictionary(input, SearchedString);
        var validWords = 0;

        var pivotStartPoints = letterPositions[SearchedString[0]];
        foreach (var startPoint in pivotStartPoints)
        {
            if (IsDiagonallyMirrored(letterPositions, startPoint, pivotStartPoints, SearchedString))
            {
                validWords++;
            }
        }

        const string ReversedString = "SAM";
        var reversePivotStartPoints = letterPositions[SearchedString[^1]];
        foreach (var startPoint in reversePivotStartPoints)
        {
            if (IsDiagonallyMirrored(letterPositions, startPoint, reversePivotStartPoints, ReversedString))
            {
                validWords++;
            }
        }

        return validWords;
    }

    private static bool IsDiagonallyMirrored(Dictionary<char, HashSet<Point>> letterPositions, Point startPoint, HashSet<Point> startPoints, string word)
    {
        var isRightDiagonalValid = IsValidWord(letterPositions, startPoint, word, 1, 1);
        if (!isRightDiagonalValid)
            return false;

        var rightMirror = new Point(startPoint.X + 2, startPoint.Y);
        var downMirror = new Point(startPoint.X, startPoint.Y + 2);

        return (startPoints.Contains(rightMirror) && IsValidWord(letterPositions, rightMirror, word, -1, 1)) ||
                 (startPoints.Contains(downMirror) && IsValidWord(letterPositions, downMirror, word, 1, -1));
    }
}

[TestFixture]
internal class Day4Tests
{
    [Test]
    public void Day4Task1Example()
    {
        string[] input =
        {
            "MMMSXXMASM",
            "MSAMXMSMSA",
            "AMXSXMAAMM",
            "MSAMASMSMX",
            "XMASAMXAMM",
            "XXAMMXXAMA",
            "SMSMSASXSS",
            "SAXAMASAAA",
            "MAMMMXMMMM",
            "MXMXAXMASX"
        };

        Day4.GetOccurrencesOfXmas(input).Should().Be(18);
    }

    [Test]
    public void Day4Task1()
    {
        var input = EmbeddedResourceHelper.GetResourceLines("AdventOfCode._2024.day4.txt");
        var result = Day4.GetOccurrencesOfXmas(input);
        result.Should().Be(2633);
    }

    [Test]
    public void Day4Task2Example()
    {
        string[] input =
        {
            "MMMSXXMASM",
            "MSAMXMSMSA",
            "AMXSXMAAMM",
            "MSAMASMSMX",
            "XMASAMXAMM",
            "XXAMMXXAMA",
            "SMSMSASXSS",
            "SAXAMASAAA",
            "MAMMMXMMMM",
            "MXMXAXMASX"
        };

        Day4.GetOccurrencesOfCrossMas(input).Should().Be(9);
    }

    [Test]
    public void Day4Task2()
    {
        var input = EmbeddedResourceHelper.GetResourceLines("AdventOfCode._2024.day4.txt");
        var result = Day4.GetOccurrencesOfCrossMas(input);
        result.Should().Be(1936);
    }
}