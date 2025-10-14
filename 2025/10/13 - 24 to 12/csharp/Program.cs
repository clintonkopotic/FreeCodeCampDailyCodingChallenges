namespace _24To12;
using System;
using System.Collections.Generic;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-13"/>
/// </summary>
internal class Program
{
    static string To12(string time)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(time, nameof(time));

        if (time.Length != 4)
        {
            throw new ArgumentException(paramName: nameof(time),
                message: "The length must be four characters.");
        }

        var hourOfDay = int.Parse(time[0..2]);

        if (hourOfDay < 0 || hourOfDay >= 24)
        {
            throw new ArgumentOutOfRangeException(paramName: nameof(time),
                actualValue: hourOfDay, message: "Invalid hour of day.");
        }

        var minuteOfHour = int.Parse(time[2..]);

        if (minuteOfHour < 0 || minuteOfHour >= 60)
        {
            throw new ArgumentOutOfRangeException(paramName: nameof(time),
                actualValue: minuteOfHour, message: "Invalid minute of hour.");
        }

        var hourOfMeridiem = hourOfDay == 0 || hourOfDay == 12 ? 12
            : hourOfDay < 12 ? hourOfDay : hourOfDay - 12;
        var meridiem = hourOfDay < 12 ? "AM" : "PM";

        return $"{hourOfMeridiem}:{minuteOfHour:D2} {meridiem}";
    }

    static void Main()
    {
        List<string> failures = [];

        foreach (var (time, expected) in Tests)
        {
            Console.Write($"Testing \"{time}\" (expecting \"{expected}\")...");
            var actual = To12(time);
            Console.Write($"\"{actual}\"");
            var success = expected == actual;
            Console.WriteLine($" (success: {success}).");

            if (!success)
            {
                failures.Add(time);
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
    }

    static List<(string time, string expected)> Tests =>
    [
        ("1124", "11:24 AM"),
        ("0900", "9:00 AM"),
        ("1455", "2:55 PM"),
        ("2346", "11:46 PM"),
        ("0030", "12:30 AM"),
    ];
}
