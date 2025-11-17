namespace WeekdayFinder;
using System;
using System.Collections.Generic;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-11-06"/>
/// </summary>
internal class Program
{
    static string GetWeekday(string dateString)
    {
        ArgumentNullException.ThrowIfNull(dateString);

        var date = DateOnly.ParseExact(dateString, "yyyy-MM-dd");

        return date.DayOfWeek.ToString();
    }

    static void Main()
    {
        List<string> failures = [];

        foreach (var (dateString, expected) in Tests)
        {
            Console.Write($"Testing \"{dateString}\" (expecting "
                + $"\"{expected}\")...");
            var actual = GetWeekday(dateString);
            Console.Write($"\"{actual}\"");
            var success = expected == actual;
            Console.WriteLine($" ({success}).");

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
        ("2025-11-06", "Thursday"),
        ("1999-12-31", "Friday"),
        ("1111-11-11", "Saturday"),
        ("2112-12-21", "Wednesday"),
        ("2345-10-01", "Monday"),
    ];
}
