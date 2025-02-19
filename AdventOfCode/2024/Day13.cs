using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2024;

internal static class Day13
{
    /// <summary>
    /// The claw machines here are a little unusual. Instead of a joystick or directional buttons to control the claw, these machines have two buttons labeled A and B.
    /// Worse, you can't just put in a token and play; it costs 3 tokens to push the A button and 1 token to push the B button. With a little experimentation, you figure out
    /// that each machine's buttons are configured to move the claw a specific amount to the right (along the X axis) and a specific amount forward (along the Y axis) each time that button is pressed.
    /// Each machine contains one prize; to win the prize, the claw must be positioned exactly above the prize on both the X and Y axes.
    /// 
    /// You wonder: what is the smallest number of tokens you would have to spend to win as many prizes as possible?
    /// You assemble a list of every machine's button behavior and prize location.
    ///
    /// You estimate that each button would need to be pressed no more than 100 times to win a prize. How else would someone be expected to play?
    /// Figure out how to win as many prizes as possible.
    ///
    /// What is the fewest tokens you would have to spend to win all possible prizes?
    /// </summary>
    public static long GetFewestTokensToWinAllPrizesByBacktracking(string[] input)
    {
        var clawMachines = ParseInput(input);

        var totalFewestTokens = 0L;

        foreach (var machine in clawMachines)
        {
            var smallestNumberOfTokens = machine.GetSmallestNumberOfTokensToWin();
            if (smallestNumberOfTokens.IsValid)
            {
                totalFewestTokens += smallestNumberOfTokens.GetTokens();
            }
        }

        return totalFewestTokens;
    }

    private static List<ClawMachine> ParseInput(string[] input)
    {
        var result = new List<ClawMachine>();

        for (var i = 0; i < input.Length; i += 4)
        {
            var pointALine = input[i].Split(new[] { "Button A: X", ", Y" }, StringSplitOptions.RemoveEmptyEntries);
            var pointA = new ClawMachinePoint(long.Parse(pointALine[0]), long.Parse(pointALine[1]));

            var pointBLine = input[i + 1].Split(new[] { "Button B: X", ", Y" }, StringSplitOptions.RemoveEmptyEntries);
            var pointB = new ClawMachinePoint(long.Parse(pointBLine[0]), long.Parse(pointBLine[1]));

            var prizeLine = input[i + 2].Split(new[] { "Prize: X=", ", Y=" }, StringSplitOptions.RemoveEmptyEntries);
            var prize = new ClawMachinePoint(long.Parse(prizeLine[0]), long.Parse(prizeLine[1]));

            var clawMachine = new ClawMachine(pointA, pointB, prize);
            result.Add(clawMachine);
        }

        return result;
    }

    /// <summary>
    /// As you go to win the first prize, you discover that the claw is nowhere near where you expected it would be.
    /// Due to a unit conversion error in your measurements, the position of every prize is actually 10000000000000 higher on both the X and Y axis!
    ///
    /// Add 10000000000000 to the X and Y position of every prize.
    ///
    /// Now, it is only possible to win a prize on the second and fourth claw machines. Unfortunately, it will take many more than 100 presses to do so.
    ///
    /// Using the corrected prize coordinates, figure out how to win as many prizes as possible. What is the fewest tokens you would have to spend to win all possible prizes?
    /// </summary>
    public static long GetFewestTokensToWinAllPrizesByLinearAlgebra(string[] input)
    {
        var clawMachines = ParseInput(input);

        var totalFewestTokens = 0L;

        foreach (var machine in clawMachines)
        {
            machine.AdjustForUnitConversion();
            var smallestNumberOfTokens = machine.CalculateSmallestNumberOfTokensToWin();
            if (smallestNumberOfTokens.IsValid)
            {
                totalFewestTokens += smallestNumberOfTokens.GetTokens();
            }
        }

        return totalFewestTokens;
    }
}

public class ClawMachine
{
    private readonly ClawMachinePoint _buttonA;
    private readonly ClawMachinePoint _buttonB;
    private readonly Dictionary<ClawMachinePoint, ButtonConsoleState> _memo = new();
    private ClawMachinePoint _prize;

    public ClawMachine(ClawMachinePoint buttonA, ClawMachinePoint buttonB, ClawMachinePoint prize)
    {
        _buttonA = buttonA;
        _buttonB = buttonB;
        _prize = prize;
    }

    public void AdjustForUnitConversion()
    {
        _prize = new ClawMachinePoint(_prize.X + 10000000000000, _prize.Y + 10000000000000);
    }

    public ButtonConsoleState GetSmallestNumberOfTokensToWin()
    {
        var buttonPressPlan = new ButtonConsoleState(0, 0);

        return GetSmallestNumberOfTokensToWin(buttonPressPlan);
    }

    private ButtonConsoleState GetSmallestNumberOfTokensToWin(ButtonConsoleState plan)
    {
        if (plan.HasExceededThreshold())
            return new ButtonConsoleState();

        var currentPoint = plan.GetCurrentPoint(_buttonA, _buttonB);
        var reminder = new ClawMachinePoint(_prize.X - currentPoint.X, _prize.Y - currentPoint.Y);

        if (_memo.ContainsKey(reminder))
            return _memo[reminder];

        if (reminder.X < 0 || reminder.Y < 0)
            return new ButtonConsoleState();

        if (reminder is { X: 0, Y: 0 })
            return new ButtonConsoleState(0, 0);

        var planAfterPressingA = plan.PressA();
        var resultA = GetSmallestNumberOfTokensToWin(planAfterPressingA);

        var planAfterPressingB = plan.PressB();
        var resultB = GetSmallestNumberOfTokensToWin(planAfterPressingB);

        var suitablePlan = resultA.GetMoreEfficientState(resultB);

        _memo[reminder] = suitablePlan;

        return suitablePlan;
    }

    public ButtonConsoleState CalculateSmallestNumberOfTokensToWin()
    {
        var pressedTimesB = (double)(_buttonA.Y * _prize.X - _buttonA.X * _prize.Y) / (_buttonA.Y * _buttonB.X - _buttonA.X * _buttonB.Y);
        var pressedTimesA = (double)(_prize.X - pressedTimesB * _buttonB.X) / _buttonA.X;

        if (pressedTimesA % 1 == 0 && pressedTimesB % 1 == 0)
        {
            return new ButtonConsoleState((long)pressedTimesA, (long)pressedTimesB);
        }
     
        return new ButtonConsoleState(-1, -1);
    }
}

public readonly struct ButtonConsoleState
{
    private const long InvalidState = -1;

    public long PressedTimesA { get; }
    public long PressedTimesB { get; }

    public bool IsValid => PressedTimesA != InvalidState && PressedTimesB != InvalidState;

    public ButtonConsoleState()
    {
        PressedTimesA = InvalidState;
        PressedTimesB = InvalidState;
    }

    public ButtonConsoleState(long pressedTimesA, long pressedTimesB)
    {
        PressedTimesA = pressedTimesA;
        PressedTimesB = pressedTimesB;
    }

    public long GetTokens()
    {
        return PressedTimesA * 3 + PressedTimesB * 1;
    }

    public ButtonConsoleState PressA()
    {
        return new ButtonConsoleState(PressedTimesA + 1, PressedTimesB);
    }

    public ButtonConsoleState PressB()
    {
        return new ButtonConsoleState(PressedTimesA, PressedTimesB + 1);
    }

    public ButtonConsoleState GetMoreEfficientState(ButtonConsoleState otherPlan)
    {
        var planA = new ButtonConsoleState(PressedTimesA + 1, PressedTimesB);
        var planB = new ButtonConsoleState(otherPlan.PressedTimesA, otherPlan.PressedTimesB + 1);

        if (IsValid && otherPlan.IsValid)
        {
            if (planA.GetTokens() < planB.GetTokens())
                return planA;

            return planB;
        }

        if (otherPlan.IsValid)
            return planB;

        return planA;
    }

    public ClawMachinePoint GetCurrentPoint(ClawMachinePoint pointA, ClawMachinePoint pointB)
    {
        var totalX = pointA.X * PressedTimesA + pointB.X * PressedTimesB;
        var totalY = pointA.Y * PressedTimesA + pointB.Y * PressedTimesB;

        return new ClawMachinePoint(totalX, totalY);
    }

    public bool HasExceededThreshold()
    {
        return PressedTimesA > 100 || PressedTimesB > 100;
    }
}

public struct ClawMachinePoint : IEquatable<ClawMachinePoint>
{
    public long X { get; set; }
    public long Y { get; set; }

    public ClawMachinePoint(long x, long y)
    {
        X = x;
        Y = y;
    }

    public bool Equals(ClawMachinePoint other)
    {
        return X == other.X && Y == other.Y;
    }

    public override bool Equals(object? obj)
    {
        return obj is ClawMachinePoint other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }
}

[TestFixture]
internal class Day13Tests
{
    [Test]
    public void Day13Task1Example1()
    {
        string[] input =
        {
            "Button A: X+10, Y+10",
            "Button B: X+3, Y+3",
            "Prize: X=13, Y=13",
            "",
        };
        Day13.GetFewestTokensToWinAllPrizesByBacktracking(input).Should().Be(4);
    }

    [Test]
    public void Day13Task1Example2()
    {
        string[] input =
        {
            "Button A: X+10, Y+10",
            "Button B: X+3, Y+3",
            "Prize: X=39, Y=39",
            ""
        };
        Day13.GetFewestTokensToWinAllPrizesByBacktracking(input).Should().Be(12);
    }

    [Test]
    public void Day13Task1Example3()
    {
        string[] input =
        {
            "Button A: X+94, Y+34",
            "Button B: X+22, Y+67",
            "Prize: X=8400, Y=5400",
            "",
            "Button A: X+26, Y+66",
            "Button B: X+67, Y+21",
            "Prize: X=12748, Y=12176",
            "",
            "Button A: X+17, Y+86",
            "Button B: X+84, Y+37",
            "Prize: X=7870, Y=6450",
            "",
            "Button A: X+69, Y+23",
            "Button B: X+27, Y+71",
            "Prize: X=18641, Y=10279"
        };
        Day13.GetFewestTokensToWinAllPrizesByBacktracking(input).Should().Be(480);
    }

    [Test]
    public void Day13Task1()
    {
        var input = EmbeddedResourceHelper.GetResourceLines("AdventOfCode._2024.day13.txt");
        var result = Day13.GetFewestTokensToWinAllPrizesByBacktracking(input);
        result.Should().Be(31623);
    }


    [Test]
    public void Day13Task2Example()
    {
        string[] input =
        {
            "Button A: X+94, Y+34",
            "Button B: X+22, Y+67",
            "Prize: X=8400, Y=5400",
            "",
            "Button A: X+26, Y+66",
            "Button B: X+67, Y+21",
            "Prize: X=12748, Y=12176",
            "",
            "Button A: X+17, Y+86",
            "Button B: X+84, Y+37",
            "Prize: X=7870, Y=6450",
            "",
            "Button A: X+69, Y+23",
            "Button B: X+27, Y+71",
            "Prize: X=18641, Y=10279"
        };

        Day13.GetFewestTokensToWinAllPrizesByLinearAlgebra(input).Should().Be(875318608908L);
    }

    [Test]
    public void Day13Task2()
    {
        var input = EmbeddedResourceHelper.GetResourceLines("AdventOfCode._2024.day13.txt");
        var result = Day13.GetFewestTokensToWinAllPrizesByLinearAlgebra(input);
        result.Should().Be(93209116744825);
    }
}
