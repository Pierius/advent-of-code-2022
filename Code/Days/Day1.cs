using AdventOfCode2022.Code.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Code.Days
{
    internal class Day1 : AbstractDay
    {
        public override Day Day => Day.One;
        public override string Title => "Calorie Counting";
        public override string[] SampleInput => new string[]
        {
            "1000",
            "2000",
            "3000",
            "",
            "4000",
            "",
            "5000",
            "6000",
            "",
            "7000",
            "8000",
            "9000",
            "",
            "10000",
        };

        public override void Execute()
        {
            ExecutePartOne(GetPuzzleInput());
            ExecutePartTwo(GetPuzzleInput());

            WriteAnswers();
        }

        private void ExecutePartOne(string[] puzzleInput)
        {
            int mostCalories = 0;
            int sum = 0;

            foreach(var input in puzzleInput)
            {
                if (!int.TryParse(input, out int calories))
                {
                    mostCalories = sum > mostCalories ? sum : mostCalories;
                    sum = 0;
                    continue;
                }
                sum += calories;
            }

            Answers.Add(mostCalories);
        }

        private void ExecutePartTwo(string[] puzzleInput)
        {
            int[] topThreeMostCalories = new int[3]{ 0, 0, 0 };
            int sum = 0;

            foreach (var input in puzzleInput)
            {
                if (!int.TryParse(input, out int calories))
                {
                    ReplaceLowestSumOfCalories(sum, topThreeMostCalories);
                    sum = 0;
                    continue;
                }
                sum += calories;
            }

            ReplaceLowestSumOfCalories(sum, topThreeMostCalories);

            Answers.Add(topThreeMostCalories.Sum());
        }

        private void ReplaceLowestSumOfCalories(int sum, int[] calories)
        {
            int lowest = calories.Min();
            if (lowest < sum)
            {
                for (int i = 0; i < calories.Length; i++)
                {
                    if (calories[i] == lowest)
                    {
                        calories[i] = sum;
                        break;
                    }
                }
            }
        }
    }
}
