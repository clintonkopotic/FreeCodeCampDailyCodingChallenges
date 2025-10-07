namespace PhoneHome;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-06"/>
/// </summary>
internal class Program
{
    static double SendMessage(double[] route)
    {
        ArgumentNullException.ThrowIfNull(route);

        if (route.Length < 2)
        {
            throw new ArgumentException(paramName: nameof(route),
                message: "Must have at least two elements. Length: "
                    + $"{route.Length}.");
        }

        var distanceTraveledInKm = 0d;
        // Start at negative one to not count the trip to the first satellite.
        var numberOfSatelitesPassedThrough = -1;

        for (var i = 0; i < route.Length; i++)
        {
            var value = route[i];

            if (!double.IsFinite(value))
            {
                throw new ArgumentOutOfRangeException(paramName: nameof(route),
                    actualValue: value, message: "All elements must be finite. "
                        + $"Index: {i}.");
            }
            else if (double.IsNegative(value))
            {
                throw new ArgumentOutOfRangeException(paramName: nameof(route),
                    actualValue: value, message: "All elements must not be "
                        + $"negative. Index: {i}.");
            }

            distanceTraveledInKm += value;
            numberOfSatelitesPassedThrough++;
        }

        var messageSpeedInKmPerSecond = 300_000d;
        var delayOfMessageThroughSatelliteInSeconds = 0.5d;
        var timeOfTravelInSeonds = (distanceTraveledInKm
            / messageSpeedInKmPerSecond)
            + (delayOfMessageThroughSatelliteInSeconds
            * numberOfSatelitesPassedThrough);

        return double.Round(timeOfTravelInSeonds, 4);
    }

    static void Main()
    {
        List<double[]> failures = [];

        foreach (var (route, expected) in Tests)
        {
            Console.Write($"Testing {ValueToString(route)} (expecting "
                + $"{ValueToString(expected)})...");
            var actual = SendMessage(route);
            Console.Write(ValueToString(actual));
            var success = expected == actual;
            Console.WriteLine($" (success: {ValueToString(success)}).");

            if (!success)
            {
                failures.Add(route);
            }
        }

        if (failures.Count > 0)
        {
            Console.WriteLine("The following inputs failed:");

            foreach (var failure in failures)
            {
                Console.WriteLine($"  {ValueToString(failure)}.");
            }
        }
        else
        {
            Console.WriteLine("All tests passed!");
        }
    }

    static List<(double[] route, double expected)> Tests =>
    [
        ([300000, 300000], 2.5),
        ([384400, 384400], 3.0627),
        ([54600000, 54600000], 364.5),
        ([1000000, 500000000, 1000000], 1674.3333),
        ([10000, 21339, 50000, 31243, 10000], 2.4086),
        ([802101, 725994, 112808, 3625770, 481239], 21.1597),
    ];

    static string ValueToString(object? value)
    {
        const string @null = "null";

        if (value is null)
        {
            return @null;
        }
        else if (value is string)
        {
            return $"\"{value}\"";
        }
        else if (value is IList list)
        {
            StringBuilder result = new("[");

            for (int i = 0; i < list.Count; i++)
            {
                if (i > 0)
                {
                    result.Append(", ");
                }

                result.Append(ValueToString(list[i]));
            }

            return result.Append(']').ToString();
        }

        return value.ToString() ?? @null;
    }
}
