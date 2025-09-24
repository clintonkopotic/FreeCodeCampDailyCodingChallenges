namespace PerfectSquare;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-09-24"/>
/// </summary>
internal class Program
{
    static bool IsPerfectSquare(double number,
        [CallerArgumentExpression(nameof(number))] string? paramName = null)
    {
        ArgumentNullException.ThrowIfNull(number, paramName);

        if (!double.IsInteger(number))
        {
            throw new ArgumentException(paramName: paramName,
                message: "Must be an integer.");
        }

        return double.IsInteger(double.Sqrt(number));
    }

    static void Main()
    {
        List<double> failures = [];

        foreach (var (number, expected) in Tests)
        {
            Console.Write($"Testing {number} (expecting {expected})...");
            var actual = IsPerfectSquare(number);
            Console.Write($"{actual}");
            var success = expected == actual;
            Console.WriteLine($" (success: {success}).");

            if (!success)
            {
                failures.Add(number);
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

    static List<(double number, bool expected)> Tests =>
    [
        (9, true),
        (49, true),
        (1, true),
        (2, false),
        (99, false),
        (-9, false),
        (0, true),
        (25281, true),
    ];
}
