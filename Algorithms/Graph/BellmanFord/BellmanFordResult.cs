namespace Algorithms.Graph.BellmanFord;

public class BellmanFordResult
{
    public const int Infinity = int.MaxValue;
    public const int UnknownParent = int.MinValue;

    public Dictionary<int, int> PathWeight { get; } = new();
    public Dictionary<int, int> PreviousVertex { get; } = new();
    public List<int> NegativeCycle { get; } = new();
}
