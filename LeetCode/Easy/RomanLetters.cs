namespace LeetCode.Easy
{
    /// <summary>
    /// https://leetcode.com/problems/roman-to-integer/
    /// </summary>
    public class RomanLetters
    {
        private Dictionary<char, int> _romanValues = new Dictionary<char, int>
        {
            { 'I', 1 },
            { 'V', 5 },
            { 'X', 10 },
            { 'L', 50 },
            { 'C', 100 },
            { 'D', 500 },
            { 'M', 1000 },
        };

        private Dictionary<char, List<char>> _subtractRules = new Dictionary<char, List<char>>
        {
            { 'I', new List<char> { 'V', 'X' } },
            { 'X', new List<char> { 'L', 'C' } },
            { 'C', new List<char> { 'D', 'M' } },

        };

        public int RomanToInt(string value)
        {
            var result = 0;
            char followingLetter = default;

            for (var i = value.Length - 1; i >= 0; i--)
            {
                var currentLetter = value[i];
                
                if (_subtractRules.ContainsKey(currentLetter) && _subtractRules[currentLetter].Contains(followingLetter))
                {
                    result -= _romanValues[currentLetter];
                }
                else
                {
                    result += _romanValues[currentLetter];
                }

                followingLetter = currentLetter;
            }

            return result;
        }

        public static void TestCase()
        {
            var roman = new RomanLetters();
            var num = roman.RomanToInt("MCMXCIV");
        }
    }
}
