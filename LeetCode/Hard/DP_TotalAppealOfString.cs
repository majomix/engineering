using System.Text;

namespace LeetCode.Hard
{
    public class DP_TotalAppealOfString
    {
        private Dictionary<int, string> _memo = new Dictionary<int, string>();
        
        public long AppealSum(string s)
        {
            if (s.Length == 0)
            {
                return 0;
            }

            if (_memo.ContainsKey(s.Length) && _memo[s.Length] == s)
            {
                _memo.Remove(s.Length);
                return 0;
            }

            long totalAppeal = s.Distinct().Count();

            var windowSize = s.Length - 1;
            for (var i = 0; i + windowSize <= s.Length; i++)
            {
                var substring = s.Substring(i, windowSize);
                totalAppeal += AppealSum(substring);
            }

            _memo[s.Length] = s;

            return totalAppeal;
        }

        /// <summary>
        /// The appeal of a string is the number of distinct characters found in the string. Given a string s, return the total appeal of all of its substrings.
        /// 1 <= s.length <= 10^5 = 100 000
        /// s consists of lowercase English letters.
        /// </summary>
        public long AppealSumCalculation(string s)
        {
            var previousOccurenceIndices = new int[128];
            var previousAppeal = 0;
            var currentAppeal = 0;

            for (var currentCharacterIndex = 1; currentCharacterIndex < s.Length; currentCharacterIndex++)
            {
                var characterIndex = s[currentCharacterIndex] - 'a';
                var previousCharacterIndex = previousOccurenceIndices[characterIndex];
                currentAppeal = previousAppeal * 2 + currentCharacterIndex - previousCharacterIndex;
                previousAppeal = currentAppeal;
            }

            return currentAppeal;
        }

        public static void TestCase()
        {
            var dpCase = new DP_TotalAppealOfString();
            var shouldBe28 = dpCase.AppealSum("abbca");
            var shouldBe20 = dpCase.AppealSum("code");
            var shouldBeMany = dpCase.AppealSum("vvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvaaaaaaayyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyfffffffffffffffffffffffffffpppppppppppppppppppppppppppppppppppppppppppnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnvvvvvvvvvvvvvvvvvvvvvvvvvvvvbbbbbbbbbbbbbbbbbbbbbbbbbbggggggggggggggggggggggggggggggggggggggrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkmmmmmmmmmmmmmmmmmmmmuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuoooooooooooooooooooooooooollllllllllllllllllllllllllllllwwwwwwwwwwwwwwwwwwwwwwwwwwxxxxxxxxxxxxxxxjjjjjzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzziiiiiiiiiiiiiooooooooooooooooooooooooooooooddddddddddddddddddddddddddddddddddddddnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnoooooooooooooooooooooooooopppppppppppppppppppppppppcccccccccccccccccccccccqqqqqqqwwwwwwwwwwwwwwwwwwwwkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkktttttttttttttttttttttttttttttttttttttttaaaaaaaaaaaaaaxxxxxxxxxxxxxxxxxxxxxxx");
            var shouldBeMany2 = dpCase.AppealSum("wbffqrcpivfxfqgpsjkiwfxvfeapxxeyibdtbqggjnltvfgacwimvdlfnioczezyxtrwwihqpzuvmwklowbxwvrjlwlqbcgsvfdnznaqzlquqmxekwlgrfrdqkmwhjhdsrlubbvbqleipeqnwtrqslqrdzzqcecwfrtuvtkxiiawllvsxgnumzjfxjsvyirjqzvqcdrwvbueujxbyrgwcamzblqisezxhpdveedzfguatroojlvraicceobtbbmefkunlixmpdkzvxhpdfotivyyzxmcwxfmifdawbodltntdbnlgzatvwmefslpbptycundhwcmurhzrknbtzgeleqkahwlgfefksebeveqjaidlajrumpdpoqnwfjwqjqrkbmwvyshjtdnspbsqcyhnbdalbqcyzotvrikvfzbqmjtuiwyqywuukpduawqjeuwsypszndnmokwlcgiexycbmbqlssgnpkkuogdlucywjlopbzufnznshrvkqiojlcvnadumrakohspzvzukmkojrpkxtjrbwfopckxjysdafzvxdsuoltursfmwobtcewr");
        }
    }
}
