using FluentAssertions;
using NUnit.Framework;
using System.Text;

namespace AdventOfCode._2024;

internal static class Day19
{
    /// <summary>
    /// Every towel at this onsen is marked with a pattern of colored stripes. There are only a few patterns, but for any
    /// particular pattern, the staff can get you as many towels with that pattern as you need. Each stripe can be
    /// white (w), blue (u), black (b), red (r), or green (g). So, a towel with the pattern ggr would have a green stripe,
    /// a green stripe, and then a red stripe, in that order. (You can't reverse a pattern by flipping a towel upside-down,
    /// as that would cause the onsen logo to face the wrong way.)
    /// 
    /// The Official Onsen Branding Expert has produced a list of designs - each a long sequence of stripe colors -
    /// that they would like to be able to display.You can use any towels you want, but all of the towels' stripes must
    /// exactly match the desired design. So, to display the design rgrgr, you could use two rg towels and then an r towel,
    /// an rgr towel and then a gr towel, or even a single massive rgrgr towel (assuming such towel patterns were actually
    /// available). To start, collect together all of the available towel patterns and the list of desired designs.
    /// 
    /// To get into the onsen as soon as possible, consult your list of towel patterns and desired designs carefully.
    /// How many designs are possible?
    /// </summary>
    public static int GetNumberOfPossibleDesigns(string[] input)
    {
        var possibleDesigns = 0;

        var towels = ParseTowels(input[0]);

        for (var i = 2; i < input.Length; i++)
        {
            var memo = new Dictionary<int, long>();
            if (CheckDesignPlausibility(input[i], 0, towels, memo) > 0)
            {
                possibleDesigns++;
            }
        }

        return possibleDesigns;
    }

    /// <summary>
    /// DP through memo!
    /// 
    /// Overlapping part is solving the same remaining substring for different combination of matching prefixes.
    /// Example: if abcdef has valid prefixes a + bc or ab + c, then the remaining substring would be examined twice.
    /// </summary>
    private static long CheckDesignPlausibility(string design, int startIndex, HashSet<string> towels, Dictionary<int, long> memo)
    {
        if (memo.ContainsKey(startIndex))
            return memo[startIndex];

        if (startIndex == design.Length)
            return 1;

        var isDesignPlausible = 0L;
        var sb = new StringBuilder();
        for (var i = 0; i < 8; i++)
        {
            if (startIndex + i == design.Length)
                break;

            sb.Append(design[startIndex + i]);
            var currentString = sb.ToString();
            if (towels.Contains(currentString))
            {
                isDesignPlausible = isDesignPlausible + CheckDesignPlausibility(design, startIndex + currentString.Length, towels, memo);
            }
        }

        memo[startIndex] = isDesignPlausible;

        return isDesignPlausible;
    }

    private static HashSet<string> ParseTowels(string input)
    {
        var set = new HashSet<string>();

        var split = input.Split(", ", StringSplitOptions.RemoveEmptyEntries);
        foreach (var towel in split)
        {
            set.Add(towel);
        }

        return set;
    }

    /// <summary>
    /// The staff don't really like some of the towel arrangements you came up with. To avoid an endless cycle of towel rearrangement,
    /// maybe you should just give them every possible option.
    /// 
    /// What do you get if you add up the number of different ways you could make each design?
    /// </summary>
    public static long GetNumberOfDifferentWaysToCombineDesigns(string[] input)
    {
        var possibleDesigns = 0L;

        var towels = ParseTowels(input[0]);

        for (var i = 2; i < input.Length; i++)
        {
            var memo = new Dictionary<int, long>();
            possibleDesigns = possibleDesigns + CheckDesignPlausibility(input[i], 0, towels, memo);
        }

        return possibleDesigns;
    }
}

[TestFixture]
internal class Day19Tests
{
    [Test]
    public void Day19Task1Example()
    {
        string[] input =
        {
            "r, wr, b, g, bwu, rb, gb, br",
            "",
            "brwrr",
            "bggr",
            "gbbr",
            "rrbgbr",
            "ubwu",
            "bwurrg",
            "brgr",
            "bbrgwb"
        };

        Day19.GetNumberOfPossibleDesigns(input).Should().Be(6);
    }

    [Test]
    public void Day19Task1Example2()
    {
        string[] input =
        {
            "grbuu, bbg, rwrwrru, bw, bubrwg, rubrbg, ggubr, uw, gru, bwgrw, rrrggbwr, ugwurr, wuwbrrr, uwruruu, grgwbrb, ubw, gbu, ru, bbgu, rwrbug, grrbr, rwgw, brb, uwug, bugrub, gwwg, uuggguwb, uwu, rwgu, urg, uuug, wgwb, wgwrbwuu, bgbbb, grgr, gwurgr, ubu, gbur, rrbwruu, rrrr, rgugu, bwugw, ug, brbb, wbbrrbr, wub, wr, wrw, bburrur, rrbwgu, rbwbgw, brr, ggb, wuurguu, brgr, b, wggb, buugrr, bgrgr, wurwb, rgrub, bbw, rug, rwwgub, rwrgb, grgu, wgwrub, grwg, uuwgrw, ubgu, wwg, bwurwrg, wbg, grbu, ubbgbrr, wbw, uwuu, ubg, gbbw, gbbugg, bru, ggwwub, rugg, buub, gbwb, uurbb, urgu, uubg, rgwrrbb, uub, ugww, wgbub, gug, grb, wuruu, rgb, ggbbwrb, rrb, ruuuw, grurw, bwguw, gwgbwrwb, grwu, rwuwu, bwg, ubwgbb, bbrbb, wrgrbbw, rbbwub, gr, wbr, bbb, rrur, gu, wwrwgww, gwgbr, bwb, buugur, wru, wgww, ubgwwru, gwbuw, brrg, wuw, ugrw, wwgwwu, rgbubr, brrrrw, uurwb, rbubb, wwwb, bwr, urbbbr, guww, buw, bww, grugur, ggbr, urbg, ruwuwb, br, gwgrurr, wwb, wrrwur, wrb, wrug, wwbrg, rugurgww, gwggwg, wrgbb, wgg, brw, wuu, gub, uug, ruwgurrb, wrruu, bwgwurw, ubbrrg, ugu, ubr, ggbu, bgwgwr, urwwgw, bbbrw, gbru, bgu, ugbgrurr, bgg, rwrbwug, rwgbu, wwgb, wurgru, ugwggg, rbgggb, urwu, gwwbuubw, bgw, bugg, rbu, rrr, rwrbgggu, gw, gb, bwbbu, gbwrb, wwww, wrbgg, gguwur, gwrw, wuwrb, gwu, wgbgwu, guu, rwrur, ww, rwwruug, ggwugg, wbu, wbwwbrw, rwu, wuuug, ur, wurubbw, rbbgg, rrbw, bgbgwr, wgb, wwbu, wgrw, wwrgggu, rwb, wwbg, bgbg, guub, ggr, uuu, rguugw, buwg, ugr, rrgg, bbuugbrr, wgwggwwu, guwu, guw, gg, wubg, uggr, ubbb, wgu, wburrwb, ugbrr, bbwrb, ggw, bgr, bug, buu, gbrrg, rr, rw, w, wbguuu, guggb, r, grrrg, rrbub, grgubuu, rrw, ubb, gbb, uubw, rruuw, bbu, bur, rgwu, gbgr, gubu, rrurwrbw, grw, gwwbubw, ugrbu, gbr, wggr, rgw, rrggb, wwbbg, brbuwwbg, bbguwug, bwu, gww, wur, bwbuu, bgrgg, burr, brrr, rrwu, uuuw, brg, uwggbr, gggw, rwwgggg, ugb, rwru, gbw, bgub, rgwgbr, rbuu, gbgu, bugbr, uu, wgw, wgbug, bguww, wrwgb, wggbbb, brwrb, ubgww, bbbrr, uru, rbw, gbbu, rgr, wbrruw, rbg, wbb, brwr, bgb, uwr, urw, bbgru, rwgrbb, bbr, grr, rbgbrbbw, wwwu, bub, gwwbrgur, ubww, wug, ugwb, uur, gubbwruu, rrbrugw, gwr, rgrg, ugw, grgggur, rwr, brruw, rur, rgu, gwwwww, wuwr, uwb, ruu, wrg, rrggu, wggw, ggbwwrb, rbrg, rwrb, bgugwb, wwr, rbrbwr, bwwr, gwb, bb, uwgu, wrrw, wugg, gur, wrr, uubgrurr, ggu, wrrbu, wruubu, wgr, gruw, rub, wubgbr, bbwb, gbuwrbbw, gbrrrwgg, gburubu, rwg, wurb, rrbuu, gwurb, urr, rb, uuw, wgguu, rugbb, grrb, ruw, rrrrwg, g, ggg, wwguu, rbr, rwrr, bugw, rrbburr, gurb, rrbbw, wrruw, bg, grrrww, bggubu, gbg, rwwu, uwg, uuurg, urrbwg, wb, rubwguu, rrg, ubrbbbb, wbwbuu, ub, ubbgbrw, ggbgg, rgg, rbrrg, gbbgg, wrrb, ugrubr, gbuw, wuugr, ugrgr, buurr, urbrbub, uggu, gwwu, rru, ugg, buuwuw, grg, gbgg, uwwwugu, guwbwwr, rbbu, rrrurw, wrgbuw, ubgwgb, gbruurw, bu, wubbw, www, bwggr, wrrr, brbbur, rg, gbguw, gwg, bguubbrg, uwbug, ubbwg, rwbguw, gwwbg, rubu, urbw, wgrbbg, gwburwr, gubgu, wrbug, brrb",
            "",
            "bgbrwwgubuwugbgbrwgbrruggrrwgrbgbbugrbbbbururuugwwgbbr"
        };

        Day19.GetNumberOfPossibleDesigns(input).Should().Be(1);
    }

    [Test]
    public void Day19Task1Example3()
    {
        string[] input =
        {
            "grbuu, bbg, rwrwrru, bw, bubrwg, rubrbg, ggubr, uw, gru, bwgrw, rrrggbwr, ugwurr, wuwbrrr, uwruruu, grgwbrb, ubw, gbu, ru, bbgu, rwrbug, grrbr, rwgw, brb, uwug, bugrub, gwwg, uuggguwb, uwu, rwgu, urg, uuug, wgwb, wgwrbwuu, bgbbb, grgr, gwurgr, ubu, gbur, rrbwruu, rrrr, rgugu, bwugw, ug, brbb, wbbrrbr, wub, wr, wrw, bburrur, rrbwgu, rbwbgw, brr, ggb, wuurguu, brgr, b, wggb, buugrr, bgrgr, wurwb, rgrub, bbw, rug, rwwgub, rwrgb, grgu, wgwrub, grwg, uuwgrw, ubgu, wwg, bwurwrg, wbg, grbu, ubbgbrr, wbw, uwuu, ubg, gbbw, gbbugg, bru, ggwwub, rugg, buub, gbwb, uurbb, urgu, uubg, rgwrrbb, uub, ugww, wgbub, gug, grb, wuruu, rgb, ggbbwrb, rrb, ruuuw, grurw, bwguw, gwgbwrwb, grwu, rwuwu, bwg, ubwgbb, bbrbb, wrgrbbw, rbbwub, gr, wbr, bbb, rrur, gu, wwrwgww, gwgbr, bwb, buugur, wru, wgww, ubgwwru, gwbuw, brrg, wuw, ugrw, wwgwwu, rgbubr, brrrrw, uurwb, rbubb, wwwb, bwr, urbbbr, guww, buw, bww, grugur, ggbr, urbg, ruwuwb, br, gwgrurr, wwb, wrrwur, wrb, wrug, wwbrg, rugurgww, gwggwg, wrgbb, wgg, brw, wuu, gub, uug, ruwgurrb, wrruu, bwgwurw, ubbrrg, ugu, ubr, ggbu, bgwgwr, urwwgw, bbbrw, gbru, bgu, ugbgrurr, bgg, rwrbwug, rwgbu, wwgb, wurgru, ugwggg, rbgggb, urwu, gwwbuubw, bgw, bugg, rbu, rrr, rwrbgggu, gw, gb, bwbbu, gbwrb, wwww, wrbgg, gguwur, gwrw, wuwrb, gwu, wgbgwu, guu, rwrur, ww, rwwruug, ggwugg, wbu, wbwwbrw, rwu, wuuug, ur, wurubbw, rbbgg, rrbw, bgbgwr, wgb, wwbu, wgrw, wwrgggu, rwb, wwbg, bgbg, guub, ggr, uuu, rguugw, buwg, ugr, rrgg, bbuugbrr, wgwggwwu, guwu, guw, gg, wubg, uggr, ubbb, wgu, wburrwb, ugbrr, bbwrb, ggw, bgr, bug, buu, gbrrg, rr, rw, w, wbguuu, guggb, r, grrrg, rrbub, grgubuu, rrw, ubb, gbb, uubw, rruuw, bbu, bur, rgwu, gbgr, gubu, rrurwrbw, grw, gwwbubw, ugrbu, gbr, wggr, rgw, rrggb, wwbbg, brbuwwbg, bbguwug, bwu, gww, wur, bwbuu, bgrgg, burr, brrr, rrwu, uuuw, brg, uwggbr, gggw, rwwgggg, ugb, rwru, gbw, bgub, rgwgbr, rbuu, gbgu, bugbr, uu, wgw, wgbug, bguww, wrwgb, wggbbb, brwrb, ubgww, bbbrr, uru, rbw, gbbu, rgr, wbrruw, rbg, wbb, brwr, bgb, uwr, urw, bbgru, rwgrbb, bbr, grr, rbgbrbbw, wwwu, bub, gwwbrgur, ubww, wug, ugwb, uur, gubbwruu, rrbrugw, gwr, rgrg, ugw, grgggur, rwr, brruw, rur, rgu, gwwwww, wuwr, uwb, ruu, wrg, rrggu, wggw, ggbwwrb, rbrg, rwrb, bgugwb, wwr, rbrbwr, bwwr, gwb, bb, uwgu, wrrw, wugg, gur, wrr, uubgrurr, ggu, wrrbu, wruubu, wgr, gruw, rub, wubgbr, bbwb, gbuwrbbw, gbrrrwgg, gburubu, rwg, wurb, rrbuu, gwurb, urr, rb, uuw, wgguu, rugbb, grrb, ruw, rrrrwg, g, ggg, wwguu, rbr, rwrr, bugw, rrbburr, gurb, rrbbw, wrruw, bg, grrrww, bggubu, gbg, rwwu, uwg, uuurg, urrbwg, wb, rubwguu, rrg, ubrbbbb, wbwbuu, ub, ubbgbrw, ggbgg, rgg, rbrrg, gbbgg, wrrb, ugrubr, gbuw, wuugr, ugrgr, buurr, urbrbub, uggu, gwwu, rru, ugg, buuwuw, grg, gbgg, uwwwugu, guwbwwr, rbbu, rrrurw, wrgbuw, ubgwgb, gbruurw, bu, wubbw, www, bwggr, wrrr, brbbur, rg, gbguw, gwg, bguubbrg, uwbug, ubbwg, rwbguw, gwwbg, rubu, urbw, wgrbbg, gwburwr, gubgu, wrbug, brrb",
            "",
            "gggrwbugubwbrwgguwrwrgurrugwbrbbgbubuuubrurbrbgugbgubugww"
        };

        Day19.GetNumberOfPossibleDesigns(input).Should().Be(1);
    }

    [Test]
    public void Day19Task1()
    {
        var input = EmbeddedResourceHelper.GetResourceLines("AdventOfCode._2024.day19.txt");
        var result = Day19.GetNumberOfPossibleDesigns(input);
        result.Should().Be(330);
    }

    [Test]
    public void Day19Task2Example()
    {
        string[] input =
        {
            "r, wr, b, g, bwu, rb, gb, br",
            "",
            "brwrr",
            "bggr",
            "gbbr",
            "rrbgbr",
            "ubwu",
            "bwurrg",
            "brgr",
            "bbrgwb"
        };

        Day19.GetNumberOfDifferentWaysToCombineDesigns(input).Should().Be(16);
    }

    [Test]
    public void Day19Task2()
    {
        var input = EmbeddedResourceHelper.GetResourceLines("AdventOfCode._2024.day19.txt");
        var result = Day19.GetNumberOfDifferentWaysToCombineDesigns(input);
        result.Should().Be(950763269786650);
    }
}