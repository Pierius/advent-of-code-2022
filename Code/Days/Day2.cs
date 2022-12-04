using AdventOfCode2022.Code.Enums;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Code.Days
{
    internal class Day2 : AbstractDay
    {
        public override Day Day => Day.Two;
        public override string Title => "Rock Paper Scissors";
        public override bool EnableSampleInput => false;
        public override string[] SampleInput => new string[] 
        {
            "A Y",
            "B X",
            "C Z"            
        };

        public override void PartOne(string[] puzzleInput)
        {
            int totalPoints = 0;

            foreach (var input in puzzleInput)
            {
                var strategy = GetStrategy(input);
                var opponentHand = ConvertToHand(strategy.Opponent);
                var myHand = ConvertToHand(strategy.Yourself);
                totalPoints += GetScore(opponentHand, myHand);
            }

            Answers.Add(totalPoints);
        }

        public override void PartTwo(string[] puzzleInput)
        {
            int totalPoints = 0;

            foreach(var input in puzzleInput)
            {
                var strategy = GetStrategy(input);
                var opponentHand = ConvertToHand(strategy.Opponent);
                var myHand = GetStrategicHand(strategy.Yourself, opponentHand);
                totalPoints += GetScore(opponentHand, myHand);
            }

            Answers.Add(totalPoints);
        }

        private const int WIN_POINTS = 6;
        private const int TIE_POINTS = 3;
        private const int LOSE_POINTS = 0;

        private Hand GetStrategicHand(char yourStrategy, Hand opponentHand)
        {
            switch (yourStrategy)
            {
                case 'X':
                    return PossibleHands.First(o => o.Shape == opponentHand.WinsAgainst);                    
                case 'Y':
                    return opponentHand;
                case 'Z':
                    return PossibleHands.First(o => o.Shape == opponentHand.LosesAgainst);
            }
            return new Hand() { Shape = HandShape.Invalid, ShapePoints = 0, WinsAgainst = HandShape.Invalid, LosesAgainst = HandShape.Invalid };
        }

        private class Strategy
        {
            public char Opponent { get; set; }
            public char Yourself { get; set; }
        }

        private Strategy GetStrategy(string input)
        {
            List<char> opponentChoices = new List<char>() { 'A', 'B', 'C' };
            List<char> yourChoices = new List<char>() { 'X', 'Y', 'Z' };

            Strategy strategy = new Strategy();

            foreach (char letter in input)
            {
                if (opponentChoices.Contains(letter))
                {
                    strategy.Opponent = letter;
                }
                else if (yourChoices.Contains(letter))
                {
                    strategy.Yourself = letter;
                }
            }

            return strategy;
        }

        private int GetScore(Hand opponentHand, Hand myHand)
        {
            int score = 0;
            
            if (myHand.WinsAgainst == opponentHand.Shape)
            {
                score += myHand.ShapePoints + WIN_POINTS;
            }
            else if (myHand.LosesAgainst == opponentHand.Shape)
            {
                score += myHand.ShapePoints + LOSE_POINTS;
            }
            else
            {
                score += myHand.ShapePoints + TIE_POINTS;
            }

            return score;
        }

        private enum HandShape
        {
            Invalid,
            Rock,
            Paper,
            Siccor
        }

        private class Hand
        {
            public HandShape Shape { get; set; }
            public int ShapePoints { get; set; }
            public HandShape WinsAgainst { get; set; }
            public HandShape LosesAgainst { get; set; }
        }

        private List<Hand> PossibleHands = new List<Hand>()
        {
            new Hand(){ Shape = HandShape.Rock, ShapePoints = 1, WinsAgainst = HandShape.Siccor, LosesAgainst = HandShape.Paper },
            new Hand(){ Shape = HandShape.Paper, ShapePoints = 2, WinsAgainst = HandShape.Rock, LosesAgainst = HandShape.Siccor },
            new Hand(){ Shape = HandShape.Siccor, ShapePoints = 3, WinsAgainst = HandShape.Paper, LosesAgainst = HandShape.Rock},
        };

        private Hand ConvertToHand(char value)
        {
            return value switch
            {
                'A' => PossibleHands.First(o => o.Shape == HandShape.Rock),
                'B' => PossibleHands.First(o => o.Shape == HandShape.Paper),
                'C' => PossibleHands.First(o => o.Shape == HandShape.Siccor),
                'X' => PossibleHands.First(o => o.Shape == HandShape.Rock),
                'Y' => PossibleHands.First(o => o.Shape == HandShape.Paper),
                'Z' => PossibleHands.First(o => o.Shape == HandShape.Siccor),
                _ => new Hand() { Shape = HandShape.Invalid, ShapePoints = 0, WinsAgainst = HandShape.Invalid, LosesAgainst = HandShape.Invalid },
            };
        }        
    }
}
