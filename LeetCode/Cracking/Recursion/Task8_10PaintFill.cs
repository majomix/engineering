using System.Drawing;
using FluentAssertions;
using NUnit.Framework;

namespace LeetCode.Cracking.Recursion;

/// <summary>
/// Implement the "paint fill" function that one might see on many image editing programs.
/// Given a screen represented by 2D array of colors, a point, and a new color, fill in the surrounding area until the color changes from the original color.
/// 
/// Solution:
/// * depth-first search in surrounding directions
/// </summary>
internal class Task8_10PaintFill
{
    public void PaintFill(int[,] image, Point point, int newColor)
    {
        var previousColor = image[point.Y, point.X];
        PaintFill(image, point, newColor, previousColor);
    }

    private void PaintFill(int[,] image, Point point, int newColor, int previousColor)
    {
        if (point.Y < 0 || point.X < 0)
            return;

        if (point.Y >= image.GetLength(0) || point.X >= image.GetLength(1))
            return;

        if (image[point.Y, point.X] != previousColor)
            return;

        image[point.Y, point.X] = newColor;

        PaintFill(image, new Point(point.X, point.Y - 1), newColor, previousColor);
        PaintFill(image, new Point(point.X, point.Y + 1), newColor, previousColor);
        PaintFill(image, new Point(point.X - 1, point.Y), newColor, previousColor);
        PaintFill(image, new Point(point.X + 1, point.Y), newColor, previousColor);
    }
}

[TestFixture]
public class Task8_10PaintFillTests
{
    private static object[] testCases =
    {
        new object[] {
            new[,]
            {
                { 0,0,1,0,0 },
                { 0,1,1,1,0 },
                { 1,1,1,1,1 },
                { 0,1,1,1,0 },
                { 0,0,1,0,0 }
            },
            new Point(0, 2),
            2,
            new[,]
            {
                { 0,0,2,0,0 },
                { 0,2,2,2,0 },
                { 2,2,2,2,2 },
                { 0,2,2,2,0 },
                { 0,0,2,0,0 }
            }
        }
    };

    [TestCaseSource(nameof(testCases))]
    public void GetPermutationsTest(int[,] input, Point point, int newColor, int[,] expectedResult)
    {
        // arrange
        var sut = new Task8_10PaintFill();

        // act
        sut.PaintFill(input, point, newColor);

        // assert
        input.Should().BeEquivalentTo(expectedResult);
    }
}