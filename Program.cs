// See https://aka.ms/new-console-template for more information

using AdventOfCode2022.Code.Converters;
using AdventOfCode2022.Code.Days;
using AdventOfCode2022.Code.Enums;
using System.Runtime.Remoting;

int MINIMUM_DAY = 1;
int MAXIMUM_DAY = 25;
string errorMessage = string.Empty;

while (true)
{
    Console.Clear();

    if (errorMessage != string.Empty)
    {
        Console.WriteLine($"Error: {errorMessage}\n");
        errorMessage = string.Empty;
    }

    Console.WriteLine("Welcome to Advent of Code!");
    Console.Write($"Choose your day ({MINIMUM_DAY}-{MAXIMUM_DAY}):");

    string? userInput = Console.ReadLine();

    if (userInput != null &&
        (userInput.ToLower() == "q" || userInput.ToLower() == "quit"))
    {
        break; // Quit program
    }

    Console.WriteLine();            

    Day day = DayConverter.ConvertToDay(userInput);

    if (day == Day.Invalid)
    {
        errorMessage = $"Only days between {MINIMUM_DAY} and {MAXIMUM_DAY} are valid!";
        continue;
    }

    int dayNumber = DayConverter.ConvertToInt(day);

    try
    {
        ObjectHandle handle = Activator.CreateInstance("AdventOfCode2022", $"AdventOfCode2022.Code.Days.Day{dayNumber}");
        ((AbstractDay)handle.Unwrap()).Execute();
    }
    catch(Exception e)
    {
        errorMessage = $"Ooops something went wrong: \n{e.Message}";
        continue;
    }

    Console.Write("\nPress any key to continue . . .");

    Console.ReadKey();
}