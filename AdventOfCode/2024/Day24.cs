using Algorithms.Graph.DataStructures;
using Algorithms.Graph.TopologicalSort;
using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2024;

internal static class Day24
{
    /// <summary>
    /// 
    /// </summary>
    public static ulong Task1(string[] input)
    {
        var resolvedGraphValues = new Dictionary<string, int>();
        var gates = new Dictionary<string, LogicalGate>();
        var isParsingInputs = true;

        var g = new GraphByAdjacencyList<string>();

        foreach (var line in input)
        {
            if (isParsingInputs)
            {
                var split = line.Split(new[] { ": "}, StringSplitOptions.RemoveEmptyEntries);

                if (split.Length != 2)
                {
                    isParsingInputs = false;
                    continue;
                }

                var inputName = split[0];
                var inputValue = int.Parse(split[1]);

                resolvedGraphValues[inputName] = inputValue;
                g.AddVertex(inputName, Array.Empty<string>());
            }
            else
            {
                var split = line.Split(new[] { " ", "->" }, StringSplitOptions.RemoveEmptyEntries);
                if (split.Length != 4)
                    continue;

                var firstInput = split[0];
                var secondInput = split[2];
                var gateName = split[1];
                var gateOutput = split[3];

                gates[gateOutput] = new LogicalGate { FirstInput = firstInput, SecondInput = secondInput, Operator = gateName };

                if (!g.Vertices.ContainsKey(gateOutput))
                {
                    g.AddVertex(gateOutput, Array.Empty<string>());
                }

                g.AddEdgeToVertex(firstInput, gateOutput);
                g.AddEdgeToVertex(secondInput, gateOutput);
            }
        }

        var topologicalSort = new KahnTopologicalSortAlgorithm<string>();
        var sorted = topologicalSort.TopologicalSort(g);
        var operators = new Dictionary<string, LogicalOperation>
        {
            { "AND", new AndOperator() },
            { "OR", new OrOperator() },
            { "XOR", new XorOperator() }
        };

        var outputs = 0;
        foreach (var signal in sorted)
        {
            if (gates.ContainsKey(signal))
            {
                var gate = gates[signal];
                var op = operators[gate.Operator];
                var firstValue = resolvedGraphValues[gate.FirstInput];
                var secondValue = resolvedGraphValues[gate.SecondInput];
                resolvedGraphValues.Add(signal, op.Execute(firstValue, secondValue));

                if (signal.StartsWith("z"))
                {
                    outputs++;
                }
            }
        }

        var result = 0UL;
        for (var i = 0; i < outputs; i++)
        {
            var name = $"z{i:D2}";
            if (resolvedGraphValues.ContainsKey(name))
            {
                var value = resolvedGraphValues[name];
                result |= ((ulong)value) << i;
            }
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

public class LogicalGate
{
    public string FirstInput { get; set; }
    public string SecondInput { get; set; }
    public string Operator { get; set; }
}

public abstract class LogicalOperation
{
    public abstract int Execute(int firstValue, int secondValue);
}

public class AndOperator : LogicalOperation
{
    public override int Execute(int firstValue, int secondValue)
    {
        return firstValue & secondValue;
    }
}

public class OrOperator : LogicalOperation
{
    public override int Execute(int firstValue, int secondValue)
    {
        return firstValue | secondValue;
    }
}

public class XorOperator : LogicalOperation
{
    public override int Execute(int firstValue, int secondValue)
    {
        return firstValue ^ secondValue;
    }
}

[TestFixture]
internal class Day24Tests
{
    [Test]
    public void Day24Task1Example1()
    {
        string[] input =
        {
            "x00: 1",
            "x01: 1",
            "x02: 1",
            "y00: 0",
            "y01: 1",
            "y02: 0",
            "",
            "x00 AND y00 -> z00",
            "x01 XOR y01 -> z01",
            "x02 OR y02 -> z02"
        };

        Day24.Task1(input).Should().Be(4);
    }

    [Test]
    public void Day24Task1Example2()
    {
        string[] input =
        {
            "x00: 1",
            "x01: 0",
            "x02: 1",
            "x03: 1",
            "x04: 0",
            "y00: 1",
            "y01: 1",
            "y02: 1",
            "y03: 1",
            "y04: 1",
            "",
            "ntg XOR fgs->mjb",
            "y02 OR x01->tnw",
            "kwq OR kpj->z05",
            "x00 OR x03->fst",
            "tgd XOR rvg->z01",
            "vdt OR tnw->bfw",
            "bfw AND frj->z10",
            "ffh OR nrd->bqk",
            "y00 AND y03->djm",
            "y03 OR y00->psh",
            "bqk OR frj->z08",
            "tnw OR fst->frj",
            "gnj AND tgd->z11",
            "bfw XOR mjb->z00",
            "x03 OR x00->vdt",
            "gnj AND wpb->z02",
            "x04 AND y00->kjc",
            "djm OR pbm->qhw",
            "nrd AND vdt->hwm",
            "kjc AND fst->rvg",
            "y04 OR y02->fgs",
            "y01 AND x02->pbm",
            "ntg OR kjc->kwq",
            "psh XOR fgs->tgd",
            "qhw XOR tgd->z09",
            "pbm OR djm->kpj",
            "x03 XOR y03->ffh",
            "x00 XOR y04->ntg",
            "bfw OR bqk->z06",
            "nrd XOR fgs->wpb",
            "frj XOR qhw->z04",
            "bqk OR frj->z07",
            "y03 OR x01->nrd",
            "hwm AND bqk->z03",
            "tgd XOR rvg->z12",
            "tnw OR pbm->gnj"
        };

        Day24.Task1(input).Should().Be(2024);
    }

    [Test]
    public void Day24Task1()
    {
        var input = EmbeddedResourceHelper.GetResourceLines("AdventOfCode._2024.day24.txt");
        var result = Day24.Task1(input);
        result.Should().Be(43942008931358);
    }

    [Test]
    public void Day24Task2Example()
    {
        string[] input =
        {

        };

        Day24.Task2(input).Should().Be(0);
    }

    [Test]
    public void Day24Task2()
    {
        var input = EmbeddedResourceHelper.GetResourceLines("AdventOfCode._2024.day24.txt");
        var result = Day24.Task2(input);
        result.Should().Be(0);
    }
}