using AdventOfCode2022.Code.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Code.Days
{
    internal class Day4 : AbstractDay
    {
        public override Day Day => Day.Four;
        public override string Title => "Camp Cleanup";
        public override bool EnableSampleInput => false;
        public override string[] SampleInput => new string[]
        {
            "2-4,6-8",
            "2-3,4-5",
            "5-7,7-9",
            "2-8,3-7",
            "6-6,4-6",
            "2-6,4-8",
        };

        public override void PartOne(string[] puzzleInput)
        {
            int totalFullOverlap = 0;

            foreach(var sectionRangePairs in puzzleInput)
            {
                var sectionRanges = GetSectionRanges(sectionRangePairs);
                if (sectionRanges[0].HasFullOverlap(sectionRanges[1]))
                {
                    totalFullOverlap++;
                }
            }

            Answers.Add(totalFullOverlap);
        }

        public override void PartTwo(string[] puzzleInput)
        {
            int totalPartlyOverlap = 0;

            foreach (var sectionRangePairs in puzzleInput)
            {
                var sectionRanges = GetSectionRanges(sectionRangePairs);
                if (sectionRanges[0].HasPartlyOverlap(sectionRanges[1]))
                {
                    totalPartlyOverlap++;
                }
            }

            Answers.Add(totalPartlyOverlap);
        }

        private List<SectionRange> GetSectionRanges(string sectionRangePairs)
        {
            List<SectionRange> sectionRanges = new List<SectionRange>();

            foreach (var range in sectionRangePairs.Split(','))
            {
                sectionRanges.Add(new SectionRange(range));
            }

            return sectionRanges;
        }

        private class SectionRange
        {
            public int StartId { get; }
            public int EndId { get; }
            public SectionRange(string range)
            {
                var splitted = range.Split('-');
                
                if (splitted.Length != 2)
                {
                    throw new ArgumentException();
                }

                StartId = int.Parse(splitted[0]);
                EndId = int.Parse(splitted[1]);                
            }
            public bool HasFullOverlap(SectionRange range)
            {
                return (StartId >= range.StartId && EndId <= range.EndId) || (range.StartId >= StartId && range.EndId <= EndId);
            }
            public bool HasPartlyOverlap(SectionRange range)
            {
                return (StartId >= range.StartId && StartId <= range.EndId) || (EndId >= range.StartId && EndId <= range.EndId) ||
                    (range.StartId >= StartId && range.StartId <= EndId) || (range.EndId >= StartId && range.EndId <= EndId);
            }
        }
    }
}
