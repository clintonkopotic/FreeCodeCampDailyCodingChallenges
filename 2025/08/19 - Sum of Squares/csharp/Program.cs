namespace SumOfSquares;
using System;
using System.Collections.Generic;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-08-19"/>
/// </summary>
internal class Program
{
    static int SumOfSquares(int n)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(n, 1);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(n, 1_000);

        var result = 0;

        for (int i = 1; i <= n; i++)
        {
            result += i * i;
        }

        return result;
    }

    static void Main()
    {
        List<int> failures = [];

        foreach (var (n, expected) in Tests)
        {
            Console.Write($"Testing {n} (expecting {expected})...");
            var actual = SumOfSquares(n);
            Console.Write(actual);
            var success = expected == actual;
            Console.WriteLine($" (success: {success}).");

            if (!success)
            {
                failures.Add(n);
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

    static List<(int n, int expected)> Tests =>
    [
        (5, 55),
        (10, 385),
        (25, 5_525),
        (500, 41_791_750),
        (1_000, 333_833_500),
    ];
}
