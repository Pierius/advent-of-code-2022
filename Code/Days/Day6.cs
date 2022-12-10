using AdventOfCode2022.Code.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Code.Days
{
    internal class Day6 : AbstractDay
    {
        public override Day Day => Day.Six;
        public override string Title => "Tuning Trouble";
        public override bool EnableSampleInput => false;
        public override string[] SampleInput => new string[]
        {
            "mjqjpqmgbljsphdztnvjfqwrcgsmlb"
        };
        
        public override void PartOne(string[] puzzleInput)
        {
            var markerLocator = new MarkerLocator(4);

            foreach (var character in puzzleInput[0])
            {
                if (markerLocator.FindMarker(character))
                {
                    break;
                }
            }
            Answers.Add(markerLocator.NumberOfProcessedCharacters);
        }

        public override void PartTwo(string[] puzzleInput)
        {
            var markerLocator = new MarkerLocator(14);

            foreach (var character in puzzleInput[0])
            {
                if (markerLocator.FindMarker(character))
                {
                    break;
                }
            }
            Answers.Add(markerLocator.NumberOfProcessedCharacters);
        }

        private class MarkerLocator
        {
            private Queue<char> _queue = new Queue<char>();
            private List<char> _received = new List<char>();
            private int _numChars;

            public int NumberOfProcessedCharacters
            {
                get { return _received.Count + _queue.Count; }
            }

            public MarkerLocator(int numberOfDistinctCharacters)
            {
                _numChars = numberOfDistinctCharacters;
            }

            public bool FindMarker(char character)
            {
                if (_queue.Count == _numChars)
                {
                    _received.Add(_queue.Dequeue());
                }
                _queue.Enqueue(character);

                return _queue.Distinct().Count() == _numChars;
            }
        }
    }
}
