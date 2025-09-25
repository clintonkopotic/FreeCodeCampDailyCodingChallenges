namespace SecondBest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-08-28"/>
/// </summary>
internal class Program
{
    static T GetLaptopCost<T>(T[] laptops, T budget) where T : INumber<T>
    {
        ArgumentNullException.ThrowIfNull(laptops);

        if (laptops.Length <= 0)
        {
            throw new ArgumentException(paramName: nameof(laptops),
                message: "Must have a length greater than zero.");
        }

        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(budget);

        // Remove duplicates, ensure each is a positive integer, and sort in
        // descending order, so its from most to least expensive.
        var laptopsSorted = laptops
            .Distinct()
            .Where(laptop => laptop > T.Zero)
            .OrderDescending()
            .ToArray();

        // Check to see if the second most expensive laptop is within budget.
        if (laptopsSorted.Length >= 2 && laptopsSorted[1] <= budget)
        {
            return laptopsSorted[1];
        }

        // Go through each laptop from most to least expensive and return the
        // first one that is within budget.
        foreach (var laptop in laptopsSorted)
        {
            if (laptop <= budget)
            {
                return laptop;
            }
        }

        // No laptops are within budget.
        return T.Zero;
    }

    static void Main()
    {
        List<(int[] laptops, int budget)> failures = [];

        foreach (var (laptops, budget, expected) in Tests)
        {
            Console.Write($"Testing {ArrayToString(laptops)} and {budget} "
                + $"(expecting {expected})...");
            var actual = GetLaptopCost(laptops, budget);
            Console.Write($"{actual}");
            var success = expected == actual;
            Console.WriteLine($" (success: {success}).");

            if (!success)
            {
                failures.Add((laptops, budget));
            }
        }

        if (failures.Count > 0)
        {
            Console.WriteLine("The following inputs failed:");

            foreach (var (laptops, budget) in failures)
            {
                Console.WriteLine($"  {ArrayToString(laptops)} and {budget}.");
            }
        }
        else
        {
            Console.WriteLine("All tests passed!");
        }
    }

    static List<(int[] laptops, int budget, int expected)> Tests =>
    [
        ([1500, 2000, 1800, 1400], 1900, 1800),
        ([1500, 2000, 2000, 1800, 1400], 1900, 1800),
        ([2099, 1599, 1899, 1499], 2200, 1899),
        ([2099, 1599, 1899, 1499], 1000, 0),
        ([1200, 1500, 1600, 1800, 1400, 2000], 1450, 1400),
    ];

    static string ArrayToString<T>(T[] t) => $"[{string.Join(", ", t)}]";
}
