namespace CaughtSpeeding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-09-26"/>
/// </summary>
internal class Program
{
    static T[] Speeding<T>(T[] speeds, T limit) where T : INumber<T>
    {
        ArgumentNullException.ThrowIfNull(speeds);

        if (!T.IsFinite(limit))
        {
            throw new ArgumentOutOfRangeException(paramName: nameof(limit),
                actualValue: limit,
                message: "Must be finite.");
        }

        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(limit);

        var numberSpeeding = T.Zero;
        var averageSpeedOver = T.Zero;

        foreach (var speed in speeds)
        {
            if (T.IsFinite(speed) && speed > limit)
            {
                averageSpeedOver += speed - limit;
                numberSpeeding++;
            }
        }

        if (numberSpeeding > T.Zero)
        {
            averageSpeedOver /= numberSpeeding;
        }

        return [numberSpeeding, averageSpeedOver];
    }

    static void Main()
    {
        List<(double[] speeds, double limit)> failures = [];

        foreach (var (speeds, limit, expected) in Tests)
        {
            Console.Write($"Testing {ArrayToString(speeds)} and {limit} "
                + $"(expecting {ArrayToString(expected)})...");
            var actual = Speeding(speeds, limit);
            Console.Write(ArrayToString(actual));
            var success = expected.SequenceEqual(actual);
            Console.WriteLine($" (success: {success}).");

            if (!success)
            {
                failures.Add((speeds, limit));
            }
        }

        if (failures.Count > 0)
        {
            Console.WriteLine("The following inputs failed:");

            foreach (var (speeds, limit) in failures)
            {
                Console.WriteLine($"  {ArrayToString(speeds)} and {limit}.");
            }
        }
        else
        {
            Console.WriteLine("All tests passed!");
        }
    }

    static List<(double[] speeds, double limit, double[] expected)> Tests =>
    [
        ([50, 60, 55], 60, [0, 0]),
        ([58, 50, 60, 55], 55, [2, 4]),
        ([61, 81, 74, 88, 65, 71, 68], 70, [4, 8.5]),
        ([100, 105, 95, 102], 100, [2, 3.5]),
        ([40, 45, 44, 50, 112, 39], 55, [1, 57]),
    ];

    static string ArrayToString<T>(T[] t) => $"[{string.Join(", ", t)}]";
}
