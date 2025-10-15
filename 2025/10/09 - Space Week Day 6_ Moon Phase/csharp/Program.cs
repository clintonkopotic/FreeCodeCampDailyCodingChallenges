namespace MoonPhase;
using System;
using System.Collections.Generic;
using System.Globalization;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-09"/>
/// </summary>
internal class Program
{
    static string MoonPhase(string dateString)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(dateString);

        var date = DateTime.ParseExact(dateString, "yyyy-MM-dd",
            CultureInfo.InvariantCulture);
        var referenceNewMoon = new DateTime(year: 2000, month: 1, day: 6);
        var diffSpan = date - referenceNewMoon;
        var diffInDays = Convert.ToInt32(
            double.Round(double.Abs(diffSpan.TotalDays)));
        var dayInLunarCycle = diffInDays % 28 + 1;

        return dayInLunarCycle switch
        {
            >= 1 and <= 7 => "New",
            >= 8 and <= 14 => "Waxing",
            >= 15 and <= 21 => "Full",
            >= 22 and <= 28 => "Waning",
            _ => throw new InvalidOperationException(),
        };
    }

    static void Main()
    {
        List<string> failures = [];

        foreach (var (dateString, expected) in Tests)
        {
            Console.Write($"Testing \"{dateString}\" (expecting "
                + $"\"{expected}\")...");
            var actual = MoonPhase(dateString);
            Console.Write($"\"{actual}\"");
            var success = expected == actual;
            Console.WriteLine($" (success: {success}).");

            if (!success)
            {
                failures.Add(dateString);
            }
        }

        if (failures.Count > 0)
        {
            Console.WriteLine("The following inputs failed:");

            foreach (var failure in failures)
            {
                Console.WriteLine($"  \"{failure}\".");
            }
        }
        else
        {
            Console.WriteLine("All tests passed!");
        }
    }

    static List<(string dateString, string expected)> Tests =>
    [
        ("2000-01-12", "New"),
        ("2000-01-13", "Waxing"),
        ("2014-10-15", "Full"),
        ("2012-10-21", "Waning"),
        ("2022-12-14", "New"),
    ];
}
