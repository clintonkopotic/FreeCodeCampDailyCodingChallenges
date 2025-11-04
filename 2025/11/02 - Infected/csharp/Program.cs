namespace Infected;
using System;
using System.Collections.Generic;
using System.Numerics;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-11-02"/>
/// </summary>
internal class Program
{
    static T Infected<T>(T days) where T : IFloatingPointIeee754<T>
    {
        if (!T.IsInteger(days))
        {
            throw new ArgumentOutOfRangeException(paramName: nameof(days),
                actualValue: days, message: "Must be an integer.");
        }

        ArgumentOutOfRangeException.ThrowIfNegative(days);
        var twentyPercent = T.CreateChecked(0.2);
        var two = T.CreateChecked(2);
        var three = T.CreateChecked(3);
        var numberOfComputersInfected = T.One;

        for (var day = T.One; day <= days; day++)
        {
            numberOfComputersInfected *= two;

            if (day % three == T.Zero)
            {
                var patched = T.Ceiling(numberOfComputersInfected
                    * twentyPercent);
                numberOfComputersInfected -= patched;
            }
        }

        return numberOfComputersInfected;
    }

    static void Main()
    {
        List<double> failures = [];

        foreach (var (days, expected) in Tests)
        {
            Console.Write($"Testing {days} (expecting {expected})...");
            var actual = Infected(days);
            Console.Write(actual);
            var success = expected == actual;
            Console.WriteLine($" (success: {success}).");

            if (!success)
            {
                failures.Add(days);
            }
        }

        if (failures.Count > 0)
        {
            Console.WriteLine("The following inputs failed:");

            foreach (var failure in failures)
            {
                Console.WriteLine($"  {failure}.");
            }
        }
        else
        {
            Console.WriteLine("All tests passed!");
        }
    }

    static List<(double days, double expected)> Tests =>
    [
        (1, 2),
        (3, 6),
        (8, 152),
        (17, 39808),
        (25, 5217638),
    ];
}
