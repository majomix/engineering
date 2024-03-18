using System.Text;

namespace LeetCode.Medium
{
    // https://www.lintcode.com/problem/659/
    /// <summary>
    /// Design an algorithm to encode a list of strings to a string. The encoded string is then sent over the network and is decoded back to the original list of strings
    /// 
    /// </summary>
    internal class ArrayHashing_EncodeAndDecodeStrings
    {
        public string Encode(List<string> strs)
        {
            var sb = new StringBuilder();
            
            foreach (var str in strs)
            {
                sb.Append($"{str.Length}_{str}");
            }

            return sb.ToString();
        }

        public List<string> Decode(string str)
        {
            var result = new List<string>();

            var lengthSb = new StringBuilder();
            var currentLength = 0;
            var readingNumber = true;
            int pos = 0;
            while (pos < str.Length)
            {
                if (readingNumber)
                {
                    if (str[pos] == '_')
                    {
                        readingNumber = false;
                        currentLength = int.Parse(lengthSb.ToString());
                        lengthSb.Clear();
                        pos++;
                    }
                    else
                    {
                        lengthSb.Append(str[pos++]);
                    }
                }
                else
                {
                    var substring = str.Substring(pos, currentLength);
                    result.Add(substring);
                    pos += currentLength;
                    readingNumber = true;
                }
            }

            return result;
        }

        public static void TestCase()
        {
            var arrhash = new ArrayHashing_EncodeAndDecodeStrings();
            var encoded = arrhash.Encode(new List<string> { "lint", "code", "love", "you" });
            var decoded = arrhash.Decode(encoded);

            var encoded2 = arrhash.Encode(new List<string> { "we", "say", ":", "yes" });
            var decoded2 = arrhash.Decode(encoded2);
        }
    }
}
