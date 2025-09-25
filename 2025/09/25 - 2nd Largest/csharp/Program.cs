namespace _2ndLargest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;

internal class Program
{
    static T SecondLargest<T>(T[] arr,
        [CallerArgumentExpression(nameof(arr))] string? paramName = null)
        where T : INumber<T>
    {
        ArgumentNullException.ThrowIfNull(arr, paramName);
        
        if (arr.Length < 2)
        {
            throw new ArgumentException(paramName: paramName,
                message: "Must have a length greater or equal to 2.");
        }

        var uniqueArraySorted = arr.Distinct().OrderDescending().ToArray();

        if (uniqueArraySorted.Length < 2)
        {
            throw new ArgumentException(paramName: paramName,
                message: "Must have at least 2 unique elements.");
        }

        return uniqueArraySorted[1];
    }

    static void Main()
    {
        List<double[]> failures = [];

        foreach (var (arr, expected) in Tests)
        {
            Console.Write($"Testing {ArrayToString(arr)} (expecting "
                + $"{expected})...");
            var actual = SecondLargest(arr);
            Console.Write($"{actual}");
            var success = expected == actual;
            Console.WriteLine($" (success: {success}).");

            if (!success)
            {
                failures.Add(arr);
            }
        }

        if (failures.Count > 0)
        {
            Console.WriteLine("The following inputs failed:");

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

    static List<(double[] arr, double expected)> Tests =>
    [
        ([1, 2, 3, 4], 3),
        ([20, 139, 94, 67, 31], 94),
        ([2, 3, 4, 6, 6], 4),
        ([10, -17, 55.5, 44, 91, 0], 55.5),
        ([1, 0, -1, 0, 1, 0, -1, 1, 0], 0),
    ];

    static string ArrayToString<T>(T[] t) => $"[{string.Join(", ", t)}]";
}
