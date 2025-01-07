namespace Algorithms.Graph.Dijkstra;

public class DijkstraResult<T>
{
    public const int Infinity = int.MaxValue;
    public static readonly T UnknownParent = default;

    public Dictionary<T, int> PathWeight { get; } = new();
    public Dictionary<T, T> PreviousVertex { get; } = new();
}
