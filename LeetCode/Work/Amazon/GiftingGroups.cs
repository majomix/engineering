namespace LeetCode.Work.Amazon
{
    /// <summary>
    /// PASSING 15/15
    /// 
    /// At Audible, a subscriber can gift an audiobook from his/her library to any other non-subscriber to kick start their audiobook journey.
    /// The first time subscriber can receive up to a maximum of N audiobooks from their friends/relatives.
    /// When a non-subscriber receives an audiobook, we can infer that the two may be related.
    /// Similarly, if the non-subscriber receives gifted books from two other subscribers, we can infer that all of them are related
    /// and the three of them form a group.
    /// 
    /// More formally, a group is composed of all of the people who know one another, whether directly or transitively.
    /// Audible would like your help finding out the number of such distinct groups from the input data.
    /// 
    /// Example. Consider the following input matrix M:
    /// 110
    /// 110
    /// 001
    /// https://hrcdn.net/s3_pub/istreet-assets/S3PlwRCklNWnBM33jPEejw/connected_groups_example.svg
    /// 
    /// Every row corresponds to a subscriber and the value M[i][j] determines if j was gifted a book by i.
    /// In the above example, user 0 has gifted a book to user 1 and so they are connected[0][1],
    /// while person 2 has not received a book from anyone or gifted book to anyone.
    /// Therefore, there are 2 groups. M[i][j] = 1 if i == j (Each of the people is known to self)
    /// Determine the number of groups represented in a matrix.
    /// 
    /// Constraints:
    /// 1 ≤ n ≤ 300
    /// 0 ≤ i < n
    /// |related| = n
    /// Each related[i] contains a binary string of n zeros and ones. related is a square matrix.
    /// 
    /// </summary>
    public class GiftingGroups
    {
        public static int countGroups(List<string> related)
        {
            var visited = new HashSet<string>();
            var count = 0;

            for (var row = 0; row < related.Count; row++)
            {
                if (ExploreAreaForTransitiveIsland(related, row, visited))
                {
                    count++;
                }
            }

            return count;
        }

        private static bool ExploreAreaForTransitiveIsland(List<string> related, int row, HashSet<string> visited)
        {
            var currentRow = related[row];
            var hasFoundAnUnexploredConnection = false;

            for (var column = 0; column < currentRow.Length; column++)
            {
                var currentPosition = $"{row},{column}";
                if (visited.Contains(currentPosition))
                {
                    return false;
                }
                visited.Add(currentPosition);

                if (related[row][column] == '1')
                {
                    hasFoundAnUnexploredConnection = true;
                    ExploreAreaForTransitiveIsland(related, column, visited);
                }
            }

            return hasFoundAnUnexploredConnection;
        }

        public static void TestCase()
        {
            var shouldReturn2 = countGroups(new List<string> { "1100", "1110", "0110", "0001" });
            var shouldReturn5 = countGroups(new List<string> { "10000", "01000", "00100", "00010", "00001" });
            var shouldReturnUnsure = countGroups(new List<string> { "1000001000", "0100010001", "0010100000", "0001000000", "0010100000", "0100010000", "1000001000", "0000000100", "0000000010", "0100000001" });
        }
    }
}
