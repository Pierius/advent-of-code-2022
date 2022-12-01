using AdventOfCode2022.Code.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Code.Converters
{
    internal class DayConverter
    {
        private const int MINIMUM_DAY = 1;
        private const int MAXIMUM_DAY = 25;
        
        public static Day ConvertToDay(string? value)
        {
            if (string.IsNullOrWhiteSpace(value) || !Int32.TryParse(value, out int day))
            {
                return Day.Invalid;
            }

            if (day < MINIMUM_DAY || day > MAXIMUM_DAY)
            {
                return Day.Invalid;
            }

            return (Day)day;
        }

        public static int ConvertToInt(Day day)
        {
            return (int)day;
        }
    }
}
