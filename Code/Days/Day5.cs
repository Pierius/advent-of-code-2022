using AdventOfCode2022.Code.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2022.Code.Days
{
    internal class Day5 : AbstractDay
    {
        public override Day Day => Day.Five;
        public override string Title => "Supply Stacks";
        public override bool EnableSampleInput => false;
        public override string[] SampleInput => new string[]
        {
            "    [D]    ",
            "[N] [C]    ",
            "[Z] [M] [P]",
            " 1   2   3 ",
            "",
            "move 1 from 2 to 1",
            "move 3 from 1 to 3",
            "move 2 from 2 to 1",
            "move 1 from 1 to 2",
        };

        public override void PartOne(string[] puzzleInput)
        {
            var supplyConfiguration = new SupplyConfiguration(puzzleInput);
            var crateRearrangements = GetCrateRearrangements(puzzleInput);
            
            foreach (var action in crateRearrangements)
            {
                supplyConfiguration.SingleCrateRearrangement(action.move, action.from, action.to);
            }
            
            Answers.Add(supplyConfiguration.PrintTopCrates());
        }

        public override void PartTwo(string[] puzzleInput)
        {
            SupplyConfiguration supplyConfiguration = new SupplyConfiguration(puzzleInput);
            var crateRearrangements = GetCrateRearrangements(puzzleInput);

            foreach (var action in crateRearrangements)
            {
                supplyConfiguration.MultipleCratesRearrangement(action.move, action.from, action.to);
            }

            Answers.Add(supplyConfiguration.PrintTopCrates());
        }

        private List<(int move, int from, int to)> GetCrateRearrangements(string[] input)
        {
            var result = new List<(int move, int from, int to)>();

            foreach (var line in input)
            {
                Regex regex = new Regex(@"^move (\d+) from (\d+) to (\d+)$");
                var matches = regex.Matches(line);

                if (matches.Count != 1)
                {
                    continue;
                }

                int move = int.Parse(matches[0].Groups[1].Value);
                int from = int.Parse(matches[0].Groups[2].Value);
                int to = int.Parse(matches[0].Groups[3].Value);

                result.Add((move, from, to));
            }

            return result;
        }

        private class SupplyConfiguration
        {
            public Dictionary<int, List<string>> CrateStacks { get; }

            public SupplyConfiguration(string[] input)
            {
                CrateStacks = new Dictionary<int, List<string>>();
                foreach (var line in input)
                {
                    if (!line.Contains('['))
                    {
                        break;
                    }

                    Regex regex = new Regex(@"(\D{3}\s|\D{3})");
                    var matches = regex.Matches(line);

                    int index = 1;
                    foreach (Match match in matches)
                    {
                        if (!string.IsNullOrWhiteSpace(match.Value))
                        {
                            if (!CrateStacks.ContainsKey(index))
                            {
                                CrateStacks.Add(index, new List<string>());
                            }
                            CrateStacks[index].Insert(0, match.Value.Replace("[", "").Replace("]", "").Trim());
                            
                        }
                        index++;
                    }
                }
            }

            public void SingleCrateRearrangement(int move, int from, int to)
            {
                if (move == 0)
                {
                    return;
                }

                int positionLast = CrateStacks[from].Count() - 1;
                string crate = CrateStacks[from][positionLast];
                CrateStacks[to].Add(crate);
                CrateStacks[from].RemoveAt(positionLast);

                SingleCrateRearrangement(move - 1, from, to);
            }

            public void MultipleCratesRearrangement(int move, int from, int to)
            {
                int bottomPosition = CrateStacks[from].Count() - move;
                var crates = CrateStacks[from].GetRange(bottomPosition, move);
                CrateStacks[to].AddRange(crates);
                CrateStacks[from].RemoveRange(bottomPosition, move);
            }

            public string PrintTopCrates()
            {
                string result = "";

                foreach (var stack in CrateStacks.OrderBy(o => o.Key))
                {
                    if (stack.Value.Count() > 0)
                    {
                        result += stack.Value.Last();
                    }
                }                

                return result;
            }
        }
    }
}
