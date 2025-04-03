using System.Diagnostics;
using System.Drawing;

namespace DataStructures.Helpers;

public enum PointDirection
{
    Up,
    Down,
    Left,
    Right
}

[DebuggerDisplay("[{X},{Y}] at {Direction}")]
public readonly struct DirectedPoint : IEquatable<DirectedPoint>
{
    private readonly Point _point;

    public int X => _point.X;
    public int Y => _point.Y;

    public PointDirection Direction { get; }

    public DirectedPoint(Point point, PointDirection direction)
    {
        _point = point;
        Direction = direction;
    }

    public bool Equals(DirectedPoint other)
    {
        return _point.Equals(other._point) && Direction == other.Direction;
    }

    public override bool Equals(object? obj)
    {
        return obj is DirectedPoint other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_point, (int)Direction);
    }
}
