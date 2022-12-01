using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode2022.Code.Converters;
using AdventOfCode2022.Code.Enums;

namespace AdventOfCode2022.Code.Days
{
    internal abstract class AbstractDay
    {
        public abstract Day Day { get; }
        public abstract string Title { get; }
        public abstract bool EnableSampleInput { get; }
        public abstract string[] SampleInput { get; }

        public List<object> Answers = new List<object>();

        public void Execute()
        {
            PartOne(GetPuzzleInput());
            PartTwo(GetPuzzleInput());

            WriteAnswers();
        }
        public abstract void PartOne(string[] puzzleInput);
        public abstract void PartTwo(string[] puzzleInput);

        public string[] GetPuzzleInput()
        {
            if (EnableSampleInput)
            {
                return SampleInput;
            }

            string path;
            int dayNumber = DayConverter.ConvertToInt(Day);

            path = Path.Combine(Environment.CurrentDirectory, @"Resources\puzzle_input\", $"day{dayNumber}.txt");

            if (!File.Exists(path))
            {
                Console.WriteLine($"Resource file is missing: {path}");
                return Array.Empty<string>();
            }

            return File.ReadAllLines(path);
        }

        public void WriteAnswers()
        {
            Console.WriteLine($"--- Answer(s) of Day {DayConverter.ConvertToInt(Day)}: {Title} ---");

            int number = 1;
            foreach (var answer in Answers)
            {
                Console.WriteLine($"Answer #{number}: {answer}");
                number++;
            }
        }
    }
}
