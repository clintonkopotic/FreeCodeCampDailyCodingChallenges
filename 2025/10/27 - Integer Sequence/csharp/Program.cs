namespace IntegerSequence;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-27"/>
/// </summary>
internal class Program
{
    static string Sequence<T>(T n) where T : INumber<T>
    {
        if (!T.IsInteger(n))
        {
            throw new ArgumentException(paramName: nameof(n),
                message: "Must be an integer.");
        }

        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(n);
        StringBuilder result = new();

        for (T i = T.One; i <= n; i++)
        {
            result.Append(i);
        }

        return result.ToString();
    }

    static void Main()
    {
        List<int> failures = [];

        foreach (var (n, expected) in Tests)
        {
            Console.Write($"Testing {n} (expecting \"{expected}\")...");
            var actual = Sequence(n);
            Console.Write($"\"{actual}\"");
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

    static List<(int n, string expected)> Tests =>
    [
        (5, "12345"),
        (10, "12345678910"),
        (1, "1"),
        (27, "123456789101112131415161718192021222324252627"),
    ];
}
