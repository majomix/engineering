namespace DataStructures.CircularArray.Reallocation
{
    internal interface IReallocationPolicy<T>
    {
        bool Reallocate(ref T[] array, ref int readIndex, ref int writeIndex, int count);
    }
}
