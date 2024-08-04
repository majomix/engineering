namespace LeetCode.Cracking.BitManipulation
{
    /// <summary>
    /// Explain what the following code does: (n & (n-1)) == 0)
    ///
    /// Discussion:
    /// n = 1, n-1 = 0, 1 & 0 = 0, 2^0 & 0,  0 == 0 true
    /// n = 2, n-1 = 1, 2 & 1 = 0, 2^2 & 2^0, 0 == 0 true
    /// n = 3, n-1 = 2, 3 & 2 = 2, 2^1 + 2^0 & 2^1 2 == 0 false
    /// n = 4, n-1 = 3, 4 & 3 = 0, 2^2 & 2^1 + 2^0, 0 == 0 true
    /// n = 5, n-1 = 4, 5 & 4 = 4, 2^2 + 2^1 & 2^2, 4 == 0 false
    /// n = 6, n-1 = 5, 6 & 5 = 4, 2^2 + 2^1 & 2^2 + 2^0, 4 == 0 false
    /// n = 7, n-1 = 6, 7 & 6 = 4, 2^2 + 2^1 + 2^0 & 2^2 + 2^1, 4 == 0 false
    /// n = 8, n-1 = 7, 8 & 7 = 4, 2^3 & 2^2 + 2^1 + 2^0, 0 == 0 true
    ///
    /// Conclusion:
    /// true if n is power of 2
    /// </summary>
    internal class Task5_5Debugger { }
}
