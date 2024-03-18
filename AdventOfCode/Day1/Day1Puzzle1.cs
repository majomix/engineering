using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day1
{
    /// <summary>
    /// The jungle must be too overgrown and difficult to navigate in vehicles or access from the air; the Elves' expedition traditionally goes on foot.
    /// As your boats approach land, the Elves begin taking inventory of their supplies. One important consideration is food - in particular, the number
    /// of Calories each Elf is carrying (your puzzle input.
    /// The Elves take turns writing down the number of Calories contained by the various meals, snacks, rations, etc.that they've brought with them, one
    /// item per line. Each Elf separates their own inventory from the previous Elf's inventory(if any) by a blank line.
    ///
    /// In case the Elves get hungry and need extra snacks, they need to know which Elf to ask: they'd like to know how many Calories are being carried
    /// by the Elf carrying the most Calories. In the example above, this is 24000 (carried by the fourth Elf).
    /// Find the Elf carrying the most Calories. How many total Calories is that Elf carrying?
    /// </summary>
    internal class Day1Puzzle1
    {
        public void Solve()
        {
            var allLines = File.ReadAllLines(@"Day1\puzzle1input.txt");
            var result = Calculate(allLines);
            var result2 = CalculateSumOfTopThree(allLines);
        }

        private int Calculate(string[] allLines)
        {
            var max = -1;
            var current = 0;

            foreach (var line in allLines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    if (max < current)
                    {
                        max = current;
                    }

                    current = 0;
                }
                else
                {
                    var amount = int.Parse(line);
                    current += amount;
                }
            }

            return max;
        }

        private int CalculateSumOfTopThree(string[] allLines)
        {
            var queue = new PriorityQueue<int, int>();

            var current = 0;

            foreach (var line in allLines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    queue.Enqueue(current, -current);
                    current = 0;
                }
                else
                {
                    var amount = int.Parse(line);
                    current += amount;
                }
            }

            return queue.Dequeue() + queue.Dequeue() + queue.Dequeue();
        }
    }
}
