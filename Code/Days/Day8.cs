using AdventOfCode2022.Code.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Code.Days
{
    internal class Day8 : AbstractDay
    {
        public override Day Day => Day.Eight;
        public override string Title => "Treetop Tree House";
        public override bool EnableSampleInput => false;
        public override string[] SampleInput => new string[]
        {
            "30373",
            "25512",
            "65332",
            "33549",
            "35390"
        };

        public override void PartOne(string[] puzzleInput)
        {
            TreePatch treePatch = new TreePatch(puzzleInput);
            treePatch.FindVisibleTrees();

            Answers.Add(treePatch.VisibleTrees);
        }

        public override void PartTwo(string[] puzzleInput)
        {
            TreePatch treePatch = new TreePatch(puzzleInput);
            treePatch.FindHighestScenicScore();

            Answers.Add(treePatch.HighestScenicScore);
        }

        private class TreePatch
        {
            private List<int[]> _treePatch;
            private enum Direction
            {
                Up,
                Down,
                Left,
                Right
            }
            public int VisibleTrees { get; private set; }
            public int NotVisibleTrees { get; private set; }
            public int HighestScenicScore { get; private set; }

            public TreePatch(string[] treeRows)
            {
                _treePatch = new List<int[]>();
                VisibleTrees = 0;
                NotVisibleTrees = 0;
                HighestScenicScore = 0;

                foreach (var row in treeRows)
                {
                    AddToPatch(row);
                }
            }

            private void AddToPatch(string row)
            {
                int[] rowOfTrees = Array.ConvertAll(row.ToArray(), c => (int)Char.GetNumericValue(c));
                _treePatch.Add(rowOfTrees);
            }

            public void FindVisibleTrees()
            {
                for (int row = 0; row < _treePatch.Count; row++)
                {
                    for (int column = 0; column < _treePatch[row].Length; column++)
                    {
                        if (IsBorderTree(row, column))
                        {
                            VisibleTrees++;
                        }
                        else if (!IsLowestTree(row, column))
                        {
                            VisibleTrees++;
                        }
                        else
                        {
                            NotVisibleTrees++;
                        }
                    }
                }
            }

            public void FindHighestScenicScore()
            {
                for (int row = 0; row < _treePatch.Count; row++)
                {
                    for (int column = 0; column < _treePatch[row].Length; column++)
                    {
                        if (IsBorderTree(row, column))
                        {
                            continue;
                        }

                        int score = CalculateScenicScore(row, column);

                        if (score > HighestScenicScore)
                        {
                            HighestScenicScore = score;
                        }
                    }
                }
            }

            private bool IsBorderTree(int row, int column)
            {
                return ((row == 0 || row == _treePatch.Count - 1) ||
                (column == 0 || column == _treePatch[row].Length - 1));
            }

            private bool IsLowestTree(int row, int column)
            {
                return IsLowestTreeInRange(row, column, Direction.Up) &&
                    IsLowestTreeInRange(row, column, Direction.Down) &&
                    IsLowestTreeInRange(row, column, Direction.Left) &&
                    IsLowestTreeInRange(row, column, Direction.Right);
            }

            private bool IsLowestTreeInRange(int row, int column, Direction direction)
            {
                int treeHeight = _treePatch[row][column];
                var nextTree = GetNextTree(row, column, direction);

                while (true)
                {
                    if (IsBorderTree(nextTree.row, nextTree.column))
                    {
                        return _treePatch[nextTree.row][nextTree.column] >= treeHeight;
                    }

                    if (_treePatch[nextTree.row][nextTree.column] >= treeHeight)
                    {
                        return true;
                    }

                    nextTree = GetNextTree(nextTree.row, nextTree.column, direction);
                }
            }

            private int CalculateScenicScore(int row, int column)
            {
                return GetScoreOfDirection(row, column, Direction.Up) *
                    GetScoreOfDirection(row, column, Direction.Down) *
                    GetScoreOfDirection(row, column, Direction.Left) *
                    GetScoreOfDirection(row, column, Direction.Right);
            }

            private int GetScoreOfDirection(int row, int column, Direction direction)
            {
                int score = 0;
                int treeHeight = _treePatch[row][column];
                var nextTree = GetNextTree(row, column, direction);

                while (true)
                {
                    if (IsBorderTree(nextTree.row, nextTree.column))
                    {
                        if (_treePatch[nextTree.row][nextTree.column] <= treeHeight)
                        {
                            return score + 1;
                        }
                    }

                    if (_treePatch[nextTree.row][nextTree.column] < treeHeight)
                    {
                        score++;
                    }
                    else
                    {
                        return score + 1;
                    }

                    nextTree = GetNextTree(nextTree.row, nextTree.column, direction);
                }
            }

            private (int row, int column) GetNextTree(int row, int column, Direction direction)
            {
                return direction switch
                {
                    Direction.Up => (--row, column),
                    Direction.Down => (++row, column),
                    Direction.Left => (row, --column),
                    Direction.Right => (row, ++column),
                    _ => throw new ArgumentException()
                };
            }
        }
    }
}