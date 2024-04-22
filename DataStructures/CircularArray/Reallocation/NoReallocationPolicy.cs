namespace DataStructures.CircularArray.Reallocation
{
    internal class NoReallocationPolicy<T> : IReallocationPolicy<T>
    {
        public bool Reallocate(ref T[] array, ref int readIndex, ref int writeIndex, int count)
        {
            return false;
        }
    }
}
