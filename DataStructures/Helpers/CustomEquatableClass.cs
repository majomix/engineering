namespace DataStructures.Helpers
{
    internal class CustomEquatableClass : IEquatable<CustomEquatableClass>
    {
        private readonly bool _shouldBeEqual;

        public CustomEquatableClass(bool shouldBeEqual)
        {
            _shouldBeEqual = shouldBeEqual;
        }

        public bool Equals(CustomEquatableClass? other)
        {
            if (other == null)
                return false;

            return _shouldBeEqual && other._shouldBeEqual;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null)
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            return obj.GetType() == GetType() && Equals((CustomEquatableClass)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
