namespace MilePace;
using System;
using System.Collections.Generic;
using System.Globalization;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-08-21"/>
/// </summary>
internal class Program
{
    static string MilePace(double miles, string duration)
    {
        if (!double.IsFinite(miles))
        {
            throw new ArgumentOutOfRangeException(paramName: nameof(miles),
                actualValue: miles, message: "Must be finite.");
        }

        ArgumentOutOfRangeException.ThrowIfNegative(miles);
        var split = duration.Split(':', StringSplitOptions.TrimEntries);

        if (split.Length != 2)
        {
            throw new ArgumentException(paramName: nameof(duration),
                message: $"Invalid format, expected in: \"mm:ss\"");
        }

        if (!int.TryParse(split[0], NumberStyles.None,
            CultureInfo.InvariantCulture, out var minutes))
        {
            throw new ArgumentException(paramName: nameof(duration),
                message: $"Failed to parse the minutes of \"{split[0]}\". The "
                    + $"expected format: \"mm:ss\"");
        }

        if (!int.TryParse(split[1], NumberStyles.None,
            CultureInfo.InvariantCulture, out var seconds))
        {
            throw new ArgumentException(paramName: nameof(duration),
                message: $"Failed to parse the seconds of \"{split[1]}\". The "
                    + $"expected format: \"mm:ss\"");
        }

        TimeSpan durationSpan = new(hours: 0, minutes, seconds);

        if (durationSpan <= TimeSpan.Zero)
        {
            throw new ArgumentOutOfRangeException(paramName: nameof(duration),
                actualValue: duration, message: "Must be positive.");
        }

        var totalSeconds = durationSpan.TotalSeconds;
        var milePaceInSeconds = totalSeconds / miles;

        return TimeSpan.FromSeconds(milePaceInSeconds)
            .ToString(@"mm\:ss", CultureInfo.InvariantCulture);
    }

    static void Main()
    {
        List<(double miles, string duration)> failures = [];

        foreach (var (miles, duration, expected) in Tests)
        {
            Console.Write($"Testing {miles} and \"{duration}\" (expecting "
                + $"\"{expected}\")...");
            var actual = MilePace(miles, duration);
            Console.Write($"\"{actual}\"");
            var success = expected == actual;
            Console.WriteLine($" (success: {success}).");

            if (!success)
            {
                failures.Add((miles, duration));
            }
        }

        if (failures.Count > 0)
        {
            Console.WriteLine("The following inputs failed:");

            foreach (var (miles, duration) in failures)
            {
                Console.WriteLine($"  {miles} and \"{duration}\".");
            }
        }
        else
        {
            Console.WriteLine("All tests passed!");
        }
    }

    static List<(double miles, string duration, string expected)> Tests =>
    [
        (3, "24:00", "08:01"),
        (1, "06:45", "06:45"),
        (2, "07:00", "03:30"),
        (26.2, "120:35", "04:36"),
    ];
}
