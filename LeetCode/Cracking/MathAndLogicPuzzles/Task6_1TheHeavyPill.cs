using FluentAssertions;
using NUnit.Framework;

namespace LeetCode.Cracking.MathAndLogicPuzzles;

/// <summary>
/// You have 20 bottles of pills. 19 bottles have 1.0 gram pills, but one has pills of weight 1.1 grams.
/// Given a scale that provides an exact measurement, how would you find the heavy bottle?
/// You can only use the scale once.
/// 
/// Solution:
/// Trick is to distinguish the pills by putting different number of each kind since there is only single allowed use of scale.
/// This assumes there is enough pills available from every kind.
/// 
/// Then, expected weight is (a1 + an) / (n/2). Subtracting it from the actual sum will give difference in weight. Division by 10 gives index.
/// </summary>
internal class Task6_1TheHeavyPill
{
    public int GetHeavyPillIndex(float[] pillWeights)
    {
        var scaleResult = UseScale(pillWeights);

        var expectedWeight = (1 + pillWeights.Length) * (pillWeights.Length / 2);

        return (int)((scaleResult - expectedWeight) / 0.1f);
    }

    private float UseScale(float[] pillWeights)
    {
        var result = 0f;

        for (var i = 0; i < pillWeights.Length; i++)
        {
            result += (i + 1) * pillWeights[i];
        }

        return result;
    }
}

[TestFixture]
internal class Task6_1TheHeavyPillTests
{
    private static object[] testCases =
    {
        1, 5, 10, 20
    };

    [TestCaseSource(nameof(testCases))]
    public void UpdateBitsTest(int heavyPillIndex)
    {
        // arrange
        var sut = new Task6_1TheHeavyPill();
        var pillWeights = GeneratePillsWeights(heavyPillIndex);

        // act
        var result = sut.GetHeavyPillIndex(pillWeights);

        // assert
        result.Should().Be(heavyPillIndex);
    }

    private float[] GeneratePillsWeights(int heavyPill)
    {
        var pillWeights = new float[20];

        for (var i = 0; i < pillWeights.Length; i++)
        {
            pillWeights[i] = i + 1 == heavyPill ? 1.1f : 1;
        }
        
        return pillWeights;
    }
}
