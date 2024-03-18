namespace LeetCode.Easy
{
    public class DP_IsSubsequence
    {
        public bool IsSubsequence(string s, string t)
        {
            var currentLetterIndexToFind = 0;

            if (s.Length == 0)
                return true;

            foreach (var letter in t)
            {
                if (letter == s[currentLetterIndexToFind])
                {
                    currentLetterIndexToFind++;
                }
                
                if (currentLetterIndexToFind == s.Length)
                    return true;
            }

            return false;
        }

        public static void TestCase()
        {
            var dpCase = new DP_IsSubsequence();
            var shouldBeTrue = dpCase.IsSubsequence("abc", "ahbgdc");
            var shouldBeFalse = dpCase.IsSubsequence("axc", "ahbgdc");
        }
    }
}
