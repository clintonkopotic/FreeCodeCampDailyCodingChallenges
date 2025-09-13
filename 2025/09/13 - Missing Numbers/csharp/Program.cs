namespace MissingNumbers;
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-09-13"/>
/// </summary>
internal class Program
{
    static int[] FindMissingNumbers(int[] arr)
    {
        ArgumentNullException.ThrowIfNull(arr);

        if (arr.Length <= 0)
        {
            return arr;
        }

        // Find the largest number in arr.
        var n = arr.Max();

        // Now count from 1 to n-1 and keep track any numbers not contained in
        // arr.
        List<int> missingNumbers = [];

        for (var i = 1; i < n; i++)
        {
            if (!arr.Contains(i))
            {
                missingNumbers.Add(i);
            }
        }

        return [.. missingNumbers];
    }

    static void Main()
    {
        List<int[]> failures = [];

        foreach ((var hours, var expected) in Tests)
        {
            Console.Write($"Testing {ArrayToString(hours)} (expecting "
                + $"{ArrayToString(expected)})...");
            var actual = FindMissingNumbers(hours);
            var success = expected.SequenceEqual(actual);
            Console.WriteLine($"{ArrayToString(actual)} (success: {success}).");

            if (!success)
            {
                failures.Add(hours);
            }
        }

        if (failures.Count <= 0)
        {
            Console.WriteLine("All tests passed!");
        }
        else
        {
            Console.WriteLine("The following inputs failed:");

            foreach (var failure in failures)
            {
                Console.WriteLine($"  {ArrayToString(failure)}.");
            }
        }
    }

    static List<(int[], int[])> Tests =>
    [
        ([1, 3, 5], [2, 4]),
        ([1, 2, 3, 4, 5], []),
        ([1, 10], [2, 3, 4, 5, 6, 7, 8, 9]),
        ([10, 1, 10, 1, 10, 1], [2, 3, 4, 5, 6, 7, 8, 9]),
        ([3, 1, 4, 1, 5, 9], [2, 6, 7, 8]),
        ([1, 2, 3, 4, 5, 7, 8, 9, 10, 12, 6, 8, 9, 3, 2, 10, 7, 4], [11]),
    ];

    static string ArrayToString(int[] hours) => $"[{string.Join(", ", hours)}]";
}
