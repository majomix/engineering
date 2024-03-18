using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.HackerRank.OneWeek.Week1
{
    internal class TimeConversion
    {
        /// <summary>
        /// PASSES 15/15
        /// 
        /// Given a time in hour AM/PM format, convert it to military(24-hour) time.
        /// Note: 12:00:00AM on a 12-hour clock is 00:00:00 on a 24-hour clock.
        /// 12:00:00PM on a 12-hour clock is 12:00:00 on a 24-hour clock. 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string timeConversion(string s)
        {
            var afternoon = s.EndsWith("PM");
            var cutOff = s.Substring(0, s.Length - 2);
            var split = cutOff.Split(':');

            var hours = Int32.Parse(split[0]);

            if (!afternoon && hours == 12)
            {
                hours -= 12;
            }
            else if (afternoon && hours != 12)
            {
                hours += 12;
            }
            
            var minutes = Int32.Parse(split[1]);
            var seconds = Int32.Parse(split[2]);

            return $"{hours:D2}:{minutes:D2}:{seconds:D2}";
        }

        public static void TestCase()
        {
            var time_19_05_45 = timeConversion("07:05:45PM");
            var time_12_01_01 = timeConversion("12:00:01PM");
            var time00_00_01 = timeConversion("12:00:01AM");

        }
    }
}
