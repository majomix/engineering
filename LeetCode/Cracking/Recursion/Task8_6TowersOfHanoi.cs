using FluentAssertions;
using NUnit.Framework;

namespace LeetCode.Cracking.Recursion;

/// <summary>
/// In the classic problem of the Towers of Hanoi, you have 3 towers and N disks of different sizes which can slide onto any tower. 
/// The puzzle starts with disks sorted in ascending order of size from top to bottom. Rules:
/// 1. Only one disk can be moved at a time.
/// 2. A disk is slid off the top of one of tower onto another tower.
/// 3. A disk cannot be placed on top of a smaller disk. (!)
/// 
/// Write a program to move the disks from the first tower to the last using stacks.
/// 
/// Solution:
/// Recursive solution building on base case n = 1 where disk is just moved. 
/// For n = 2, solve n = 1 first but instead of moving disks to target tower, solve it for a temporary tower. Then move the last piece to target tower and solve n = 1 for temporary tower.
/// For n = 3, solve n = 2 first to temporary tower, move the last piece to target tower, and solve n = 2 from temporary to target.
/// </summary>
internal class Task8_6TowersOfHanoi
{
    public HanoiGame SolveHanoiTowers(int numberOfDisks)
    {
        var firstTower = new HanoiTower(numberOfDisks);
        var secondTower = new HanoiTower(0);
        var thirdTower = new HanoiTower(0);

        firstTower.MoveDisks(numberOfDisks, thirdTower, secondTower);

        return new HanoiGame(firstTower, secondTower, thirdTower);
    }
}

public record HanoiGame(HanoiTower FirstTower, HanoiTower SecondTower, HanoiTower ThirdTower);

public class HanoiTower
{
    private Stack<int> _disks = new();

    public int NumberOfDisks => _disks.Count;

    public HanoiTower(int numberOfDisks)
    {
        for (var i = 0; i < numberOfDisks; i++)
        {
            _disks.Push(i + 1);
        }
    }

    public void MoveDisks(int numberOfDisks, HanoiTower destination, HanoiTower buffer)
    {
        if (numberOfDisks <= 0)
            return;

        MoveDisks(numberOfDisks - 1, buffer, destination);

        MoveTopDiskTo(destination);

        buffer.MoveDisks(numberOfDisks - 1, destination, this);
    }

    private void MoveTopDiskTo(HanoiTower other)
    {
        var disk = _disks.Pop();
        other._disks.Push(disk);
    }
}

[TestFixture]
public class Task8_6TowersOfHanoiTests
{
    private static object[] testCases =
    {
        0, 1, 2, 3, 4, 5, 6, 7, 8
    };

    [TestCaseSource(nameof(testCases))]
    public void SolveHanoiTowersTest(int numberOfDisks)
    {
        // arrange
        var sut = new Task8_6TowersOfHanoi();

        // act
        var result = sut.SolveHanoiTowers(numberOfDisks);

        // assert
        result.FirstTower.NumberOfDisks.Should().Be(0);
        result.SecondTower.NumberOfDisks.Should().Be(0);
        result.ThirdTower.NumberOfDisks.Should().Be(numberOfDisks);
    }
}
