using System.Diagnostics;
using System.Drawing;
using System.Reflection.PortableExecutable;
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
    public static int GetFewestTokensToWinAllPrizes(string[] input)
    {
        var clawMachines = ParseInput(input);

        var totalFewestTokens = 0;

        foreach (var machine in clawMachines)
        {
            var smallestNumberOfTokens = machine.GetSmallestNumberOfTokensToWin();
            if (smallestNumberOfTokens.CanBeSolved)
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
            var pointA = new Point(int.Parse(pointALine[0]), int.Parse(pointALine[1]));

            var pointBLine = input[i + 1].Split(new[] { "Button B: X", ", Y" }, StringSplitOptions.RemoveEmptyEntries);
            var pointB = new Point(int.Parse(pointBLine[0]), int.Parse(pointBLine[1]));

            var prizeLine = input[i + 2].Split(new[] { "Prize: X=", ", Y=" }, StringSplitOptions.RemoveEmptyEntries);
            var prize = new Point(int.Parse(prizeLine[0]), int.Parse(prizeLine[1]));

            var clawMachine = new ClawMachine(pointA, pointB, prize);
            result.Add(clawMachine);
        }

        return result;
    }

    /// <summary>
    ///
    /// </summary>
    public static int Task2(string[] input)
    {


        return 0;
    }
}

public class ClawMachine
{
    private readonly Point _buttonA;
    private readonly Point _buttonB;
    private readonly Point _prize;
    private readonly Dictionary<Point, ButtonPressPlan> _memo = new();

    public ClawMachine(Point buttonA, Point buttonB, Point prize)
    {
        _buttonA = buttonA;
        _buttonB = buttonB;
        _prize = prize;
    }

    public ButtonPressPlan GetSmallestNumberOfTokensToWin()
    {
        var buttonPressPlan = new ButtonPressPlan(0, 0);

        return GetSmallestNumberOfTokensToWin(buttonPressPlan);
    }

    private ButtonPressPlan GetSmallestNumberOfTokensToWin(ButtonPressPlan plan)
    {
        Debug.WriteLine(plan.GetCurrentPoint(_buttonA, _buttonB));

        if (plan.HasExceededThreshold())
        {
            plan.CanBeSolved = false;
            return plan;
        }

        var currentPoint = plan.GetCurrentPoint(_buttonA, _buttonB);

        //if (_memo.ContainsKey(currentPoint))
        //{
        //    return _memo[currentPoint];
        //}

        var reminder = new Point(_prize.X - currentPoint.X, _prize.Y - currentPoint.Y);

        if (_memo.ContainsKey(reminder))
        {
            return _memo[reminder];
        }

        if (currentPoint.X > _prize.X || currentPoint.Y > _prize.Y)
        {
            plan.CanBeSolved = false;
            return plan;
        }

        if (currentPoint == _prize)
        {
            plan.CanBeSolved = true;
            return plan;
        }

        var planAfterPressingA = plan.PressA();
        planAfterPressingA = GetSmallestNumberOfTokensToWin(planAfterPressingA);

        var planAfterPressingB = plan.PressB();
        planAfterPressingB = GetSmallestNumberOfTokensToWin(planAfterPressingB);

        var suitablePlan = planAfterPressingA.GetMoreEfficientPlan(planAfterPressingB);

        _memo[reminder] = new ButtonPressPlan(suitablePlan.PressedTimesA - plan.PressedTimesA, suitablePlan.PressedTimesB - plan.PressedTimesB);
        //_memo[currentPoint] = suitablePlan;

        return suitablePlan;
    }
}

public class ButtonPressPlan
{
    public int PressedTimesA { get; }
    public int PressedTimesB { get; }
    public bool CanBeSolved { get; set; }

    public ButtonPressPlan(int pressedTimesA, int pressedTimesB)
    {
        PressedTimesA = pressedTimesA;
        PressedTimesB = pressedTimesB;
    }
    public Point GetCurrentPoint(Point pointA, Point pointB)
    {
        var totalX = pointA.X * PressedTimesA + pointB.X * PressedTimesB;
        var totalY = pointA.Y * PressedTimesA + pointB.Y * PressedTimesB;

        return new Point(totalX, totalY);
    }

    public bool HasExceededThreshold()
    {
        return PressedTimesA + PressedTimesB > 100;
    }

    public int GetTokens()
    {
        return PressedTimesA * 3 + PressedTimesB * 1;
    }

    public ButtonPressPlan PressA()
    {
        return new ButtonPressPlan(PressedTimesA + 1, PressedTimesB);
    }

    public ButtonPressPlan PressB()
    {
        return new ButtonPressPlan(PressedTimesA, PressedTimesB + 1);
    }

    public ButtonPressPlan GetMoreEfficientPlan(ButtonPressPlan otherPlan)
    {
        if (CanBeSolved && otherPlan.CanBeSolved)
        {
            if (GetTokens() < otherPlan.GetTokens())
                return this;

            return otherPlan;
        }

        if (otherPlan.CanBeSolved)
            return otherPlan;

        return this;
    }
}

[TestFixture]
internal class Day13Tests
{
    [Test]
    public void Day13Task1Example0()
    {
        string[] input =
        {
            "Button A: X+10, Y+10",
            "Button B: X+3, Y+3",
            "Prize: X=13, Y=13",
            "",
        };

        Day13.GetFewestTokensToWinAllPrizes(input).Should().Be(4);
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

        Day13.GetFewestTokensToWinAllPrizes(input).Should().Be(12);
    }

    //[Test]
    //public void Day13Task1Example1()
    //{
    //    string[] input =
    //    {
    //        "Button A: X+94, Y+34",
    //        "Button B: X+22, Y+67",
    //        "Prize: X=8400, Y=5400",
    //        "",
    //        "Button A: X+26, Y+66",
    //        "Button B: X+67, Y+21",
    //        "Prize: X=12748, Y=12176",
    //        "",
    //        "Button A: X+17, Y+86",
    //        "Button B: X+84, Y+37",
    //        "Prize: X=7870, Y=6450",
    //        "",
    //        "Button A: X+69, Y+23",
    //        "Button B: X+27, Y+71",
    //        "Prize: X=18641, Y=10279"
    //    };

    //    Day13.GetFewestTokensToWinAllPrizes(input).Should().Be(480);
    //}

    //[Test]
    //public void Day13Task1()
    //{
    //    var input = EmbeddedResourceHelper.GetResourceLines("AdventOfCode._2024.day13.txt");
    //    var result = Day13.GetFewestTokensToWinAllPrizes(input);
    //    result.Should().Be(0);
    //}

    [Test]
    public void Day13Task2Example()
    {
        string[] input =
        {

        };

        Day13.Task2(input).Should().Be(0);
    }

    [Test]
    public void Day13Task2()
    {
        var input = EmbeddedResourceHelper.GetResourceLines("AdventOfCode._2024.day13.txt");
        var result = Day13.Task2(input);
        result.Should().Be(0);
    }
}
