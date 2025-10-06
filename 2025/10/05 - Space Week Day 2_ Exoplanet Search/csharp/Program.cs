namespace ExoplanetSearch;
using System;
using System.Collections.Generic;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-05"/>
/// </summary>
internal class Program
{
    static bool HasExoplanet(string readings)
    {
        ArgumentNullException.ThrowIfNull(readings);

        const string values = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        var totalNumberOfReadings = 0;
        var sumOfReadings = 0;
        List<int> readingValues = [];

        foreach (var character in readings)
        {
            var value = values.IndexOf(character);

            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(
                    paramName: nameof(readings), actualValue: character,
                    message: "Invalid reading character.");
            }

            totalNumberOfReadings++;
            sumOfReadings += value;
            readingValues.Add(value);
        }

        if (totalNumberOfReadings == 0)
        {
            throw new ArithmeticException("No readings.");
        }

        var averageReading = Convert.ToDouble(sumOfReadings)
            / Convert.ToDouble(totalNumberOfReadings);

        if (!double.IsFinite(averageReading))
        {
            throw new InvalidOperationException($"{nameof(averageReading)} "
                + $"must be finite, not {averageReading}.");
        }

        var exoplanetMaxThresholdReading = 0.8d * averageReading;

        foreach (var readingValue in readingValues)
        {
            if (readingValue <= exoplanetMaxThresholdReading)
            {
                return true;
            }
        }

        return false;
    }

    static void Main()
    {
        List<string> failures = [];

        foreach (var (readings, expected) in Tests)
        {
            Console.Write($"Testing \"{readings}\" (expecting {expected})...");
            var actual = HasExoplanet(readings);
            Console.Write($"{actual}");
            var success = expected == actual;
            Console.WriteLine($" (success: {success}).");
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

    static List<(string readings, bool expected)> Tests =>
    [
        ("665544554", false),
        ("FGFFCFFGG", true),
        ("MONOPLONOMONPLNOMPNOMP", false),
        ("FREECODECAMP", true),
        ("9AB98AB9BC98A", false),
        ("ZXXWYZXYWYXZEGZXWYZXYGEE", true),
    ];
}
