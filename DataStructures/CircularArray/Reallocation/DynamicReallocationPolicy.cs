namespace DataStructures.CircularArray.Reallocation
{
    internal class DynamicReallocationPolicy<T> : IReallocationPolicy<T>
    {
        public bool Reallocate(ref T[] array, ref int readIndex, ref int writeIndex, int count)
        {
            // when casting positive fractional values, they are rounded down
            var newSize = array.Length * 2;

            // copy content
            var temp = new T[newSize];
            var lengthToEnd = count - readIndex;
            Array.Copy(array, readIndex, temp, 0, lengthToEnd);
            Array.Copy(array, 0, temp, lengthToEnd, readIndex);

            readIndex = 0;
            writeIndex = count;
            array = temp;

            return true;
        }
    }
}
