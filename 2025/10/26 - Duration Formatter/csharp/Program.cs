namespace DurationFormatter;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-26"/>
/// </summary>
internal class Program
{
    static string Format<T>(T seconds) where T : INumber<T>
    {
        if (!T.IsInteger(seconds))
        {
            throw new ArgumentException(paramName: nameof(seconds),
                message: "Must be an integer.");
        }

        ArgumentOutOfRangeException.ThrowIfNegative(seconds);

        var duration = TimeSpan.FromSeconds(Convert.ToDouble(seconds));
        var hours = Convert.ToInt32(double.Floor(duration.TotalHours));
        var minutes = duration.Minutes;
        StringBuilder result = new();

        if (hours > 0)
        {
            result.AppendFormat("{0}:{1:D2}:", hours, minutes);
        }
        else
        {
            result.AppendFormat("{0}:", minutes);
        }

        return result.AppendFormat("{0:D2}", duration.Seconds).ToString();
    }

    static void Main()
    {
        List<int> failures = [];

        foreach (var (seconds, expected) in Tests)
        {
            Console.Write($"Testing {seconds} (expecting \"{expected}\")...");
            var actual = Format(seconds);
            Console.Write($"\"{actual}\"");
            var sucess = expected == actual;
            Console.WriteLine($" (success: {sucess}).");

            if (!sucess)
            {
                failures.Add(seconds);
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

    static List<(int seconds, string expected)> Tests =>
    [
        (500, "8:20"),
        (4000, "1:06:40"),
        (1, "0:01"),
        (5555, "1:32:35"),
        (99999, "27:46:39"),
    ];
}
