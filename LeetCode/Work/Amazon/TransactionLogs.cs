using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.Work.Amazon
{
    /// <summary>
    /// PASSING 15/15
    /// 
    /// Your Amazonian team is responsible for maintaining a monetary transaction service. The transactions are tracked in a log file.
    /// A log file is provided as a string array where each entry represents a transaction to service.
    /// 
    /// Each transaction consists of:
    /// sender_user_id:  Unique identifier for the user that initiated the transaction.  It consists of only digits with at most 9 digits.
    /// recipient_user_id:  Unique identifier for the user that is receiving the transaction.It consists of only digits with at most 9 digits.
    /// amount_of_transaction:  The amount of the transaction.It consists of only digits with at most 9 digits.
    /// 
    /// The values are separated by a space. For example, "sender_user_id recipient_user_id amount_of_transaction".
    /// 
    /// Users that perform an excessive amount of transactions might be abusing the service so you have been tasked to identify the users that
    /// have a number of transactions over a threshold.
    /// 
    /// The list of user ids should be ordered in ascending numeric value.
    /// Example:
    /// logs = ["88 99 200", "88 99 300", "99 32 100", " 12 12 15"]
    /// threshold = 2
    /// 
    /// The transactions count for each user, regardless of role are:
    /// 
    /// ID     Transactions
    /// --     ------------
    /// 99     3
    /// 88     2
    /// 12     1
    /// 32     1
    /// 
    /// There are two users with at least threshold = 2 transactions: 99 and 88. In ascending order, the return array is ['88', '99'].
    /// Note: In the last log entry, user 12 was on both sides of the transaction. This counts as only 1 transaction for user 12.
    /// 
    /// Function Description:
    /// string logs[n]: each logs[i] denotes the i-th entry in the logs
    /// int threshold: the minimum number of transactions that a user must have to be included in the result
    /// Returns: string[]: an array of user id's as strings, sorted ascending by numeric value
    /// 
    /// 1 ≤ n ≤ 10^5
    /// 1 ≤ threshold ≤ n
    /// The sender_user_id, recipient_user_id and amount_of_transaction contain only characters in the range ascii['0' - '9'].
    /// The sender_user_id, recipient_user_id and amount_of_transaction start with a non-zero digit.
    /// 0 < length of sender_user_id, recipient_user_id, amount_of_transaction ≤ 9.
    /// The result will contain at least one element.
    /// </summary>
    public class TransactionLogs
    {
        /*
         * Complete the 'processLogs' function below.
         *
         * The function is expected to return a STRING_ARRAY.
         * The function accepts following parameters:
         *  1. STRING_ARRAY logs
         *  2. INTEGER threshold
         */

        // sorted dictionary
        public static List<string> processLogs(List<string> logs, int threshold)
        {
            var cache = new Dictionary<int, int>();
            var valuesAnswer = new Dictionary<int, bool>();

            foreach (var log in logs)
            {
                var split = log.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (split.Length == 3)
                {
                    var senderUserId = int.Parse(split[0]);
                    var recipientUserId = int.Parse(split[1]);

                    AddTransactionForUser(cache, valuesAnswer, senderUserId, threshold);

                    if (senderUserId != recipientUserId)
                    {
                        AddTransactionForUser(cache, valuesAnswer, recipientUserId, threshold);
                    }
                }
            }

            var result = new List<string>();

            foreach (var item in valuesAnswer.Keys.OrderBy(n => n))
            {
                result.Add($"{item}");
            }

            return result;
        }

        private static void AddTransactionForUser(Dictionary<int, int> list, Dictionary<int, bool> output, int userId, int threshold)
        {
            if (!list.ContainsKey(userId))
            {
                list.Add(userId, 1);
            }
            else
            {
                list[userId]++;
            }

            if (list[userId] >= threshold)
            {
                output[userId] = true;
            }
        }

        public static void TestCase()
        {
            var shouldOutput1_2 = TransactionLogs.processLogs(new List<string> { "1 2 50", "1 7 70", "1 3 20", "2 2 17"}, 2);
            var shouldOutput7 = TransactionLogs.processLogs(new List<string> { "22 7 20", "33 7 50", "22 7 30" }, 3);
        }
    }
}
