namespace ArrayDuplicates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-08-30"/>
/// </summary>
internal class Program
{
    static T[] FindDuplicates<T>(T[] arr,
        [CallerArgumentExpression(nameof(arr))] string? paramName = null)
    {
        ArgumentNullException.ThrowIfNull(arr, paramName);
        SortedSet<T> result = [];
        List<T> appeared = [];
        
        foreach (var item in arr)
        {
            if (!appeared.Contains(item))
            {
                appeared.Add(item);
            }
            else if (!result.Contains(item))
            {
                _ = result.Add(item);
            }
        }

        return [.. result];
    }

    static void Main()
    {
        List<int[]> failures = [];

        foreach ((var arr, var expected) in Tests)
        {
            Console.Write($"Testing {ArrayToString(arr)} (expecting "
                + $"{ArrayToString(expected)})...");
            var actual = FindDuplicates(arr);
            Console.Write(ArrayToString(actual));
            var success = expected.SequenceEqual(actual);
            Console.WriteLine($" (success: {success}).");

            if (!success)
            {
                failures.Add(arr);
            }
        }

        if (failures.Count > 0)
        {
            Console.WriteLine("The following input failed:");

            foreach (var failure in failures)
            {
                Console.WriteLine($"  {ArrayToString(failure)}.");
            }
        }
        else
        {
            Console.WriteLine("All tests passed!");
        }
    }

    static List<(int[] arr, int[] expected)> Tests =>
    [
        ([1, 2, 3, 4, 5], []),
        ([1, 2, 3, 4, 1, 2], [1, 2]),
        ([2, 34, 0, 1, -6, 23, 5, 3, 2, 5, 67, -6, 23, 2, 43, 2, 12, 0, 2, 4,
            4], [-6, 0, 2, 4, 5, 23]),
    ];

    static string ArrayToString<T>(T[] array)
        => $"[{string.Join(", ", array)}]";
}
