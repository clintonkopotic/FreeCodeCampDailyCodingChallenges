namespace Factorializer;
using System;
using System.Collections.Generic;
using System.Numerics;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-08-18"/>
/// </summary>
internal class Program
{
    static T Factorial<T>(T n) where T : INumber<T>
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(n, T.Zero);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(n, T.CreateChecked(20));

        if (T.IsZero(n))
        {
            return T.One;
        }

        return n * Factorial(n - T.One);
    }

    static void Main()
    {
        List<ulong> failures = [];

        foreach (var (n, expected) in Tests)
        {
            Console.Write($"Testing {n:N0} (expecting {expected:N0})...");
            var actual = Factorial(n);
            Console.Write($"{actual:N0}");
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
                Console.WriteLine($"  {failure:N0}.");
            }
        }
        else
        {
            Console.WriteLine("All tests passed!");
        }
    }

    static List<(ulong n, ulong expected)> Tests =>
    [
        (0, 1),
        (5, 120),
        (20, 2432902008176640000),
    ];
}
