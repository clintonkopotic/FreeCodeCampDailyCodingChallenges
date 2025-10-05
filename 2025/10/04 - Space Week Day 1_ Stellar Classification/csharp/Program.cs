namespace StellarClassification;
using System;
using System.Collections.Generic;
using System.Numerics;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-04"/>
/// </summary>
internal class Program
{
    static char Classification<T>(T temp) where T : INumber<T>
    {
        if (!T.IsFinite(temp))
        {
            throw new ArgumentOutOfRangeException(paramName: nameof(temp),
                actualValue: temp, message: "Must be finite.");
        }

        ArgumentOutOfRangeException.ThrowIfNegative(temp);

        if (temp >= T.CreateChecked(30_000))
        {
            return 'O';
        }
        else if (temp >= T.CreateChecked(10_000))
        {
            return 'B';
        }
        else if (temp >= T.CreateChecked(7_500))
        {
            return 'A';
        }
        else if (temp >= T.CreateChecked(6_000))
        {
            return 'F';
        }
        else if (temp >= T.CreateChecked(5_200))
        {
            return 'G';
        }
        else if (temp >= T.CreateChecked(3_700))
        {
            return 'K';
        }
        else
        {
            return 'M';
        }
    }

    static void Main()
    {
        List<double> failures = [];

        foreach (var (temp, expected) in Tests)
        {
            Console.Write($"Testing {temp:N0} (expecting \'{expected}\')...");
            var actual = Classification(temp);
            Console.Write($"\'{actual}\'");
            var success = expected == actual;
            Console.WriteLine($" (success: {success}).");

            if (!success)
            {
                failures.Add(temp);
            }
        }

        if (failures.Count > 0)
        {
            Console.WriteLine("The following inputs failed:");

            foreach (var failure in failures)
            {
                Console.WriteLine($"  {failure:N0}.");
            }
        }
        else
        {
            Console.WriteLine("All tests passed!");
        }
    }

    static List<(double temp, char expected)> Tests =>
    [
        (5_778, 'G'),
        (2_400, 'M'),
        (9_999, 'A'),
        (3_700, 'K'),
        (3_699, 'M'),
        (210_000, 'O'),
        (6_000, 'F'),
        (11_432, 'B'),
    ];
}
