using AdventOfCode2022.Code.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Code.Days
{
    internal class Day3 : AbstractDay
    {
        public override Day Day => Day.Three;
        public override string Title => "Rucksack Reorganization";
        public override bool EnableSampleInput => false;
        public override string[] SampleInput => new string[]
        {
            "vJrwpWtwJgWrhcsFMMfFFhFp",
            "jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL",
            "PmmdzqPrVvPwwTWBwg",
            "wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn",
            "ttgJtRGJQctTZtZT",
            "CrZsJsPPZsGzwwsLwLmpwMDw",
        };

        public override void PartOne(string[] puzzleInput)
        {
            int sumOfPriorities = 0;

            foreach (string rucksackContent in puzzleInput)
            {
                Rucksack rucksack = new Rucksack(rucksackContent);
                var duplicateItems = rucksack.GetDuplicateItems();
                sumOfPriorities += duplicateItems.Sum(o => ConvertToPriority(o));
            }

            Answers.Add(sumOfPriorities);
        }

        public override void PartTwo(string[] puzzleInput)
        {
            int sumOfPriorities = 0;

            for (int i = 0; i < puzzleInput.Length; i += 3)
            {
                char badgeItem = GetBadgeItem(puzzleInput[i], puzzleInput[i + 1], puzzleInput[i + 2]);
                sumOfPriorities += ConvertToPriority(badgeItem);
            }

            Answers.Add(sumOfPriorities);
        }

        private int ConvertToPriority(char item)
        {
            if ((int)item >= 97)
            {
                return (int)item - 96;
            }
            else
            {
                return (int)item - 38;
            }
        }

        private char GetBadgeItem(string rucksack1, string rucksack2, string rucksack3)
        {
            foreach (char item in rucksack1)
            {
                if (rucksack2.Contains(item) && rucksack3.Contains(item))
                {
                    return item;
                }
            }

            throw new ArgumentException("Wrong group rucksacks");
        }

        private class Rucksack
        {
            public string FirstCompartment { get; }
            public string SecondCompartment { get; }
            
            public Rucksack(string content)
            {
                int length = content.Length / 2;
                FirstCompartment = content.Substring(0, length);
                SecondCompartment = content.Substring(length, length);
            }

            public List<char> GetDuplicateItems()
            {
                List<char> items = new List<char>();

                foreach (char item in FirstCompartment)
                {
                    if (SecondCompartment.Contains(item) && !items.Contains(item))
                    {
                        items.Add(item);
                    }
                }

                return items;
            }
        }
    }
}
