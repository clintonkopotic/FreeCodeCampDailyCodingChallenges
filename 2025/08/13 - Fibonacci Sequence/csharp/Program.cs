namespace FibonacciSequence;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-08-13"/>
/// </summary>
internal class Program
{
    static T[] FibonacciSequence<T>(T[] startSequence, int length)
        where T : INumber<T>
    {
        ArgumentNullException.ThrowIfNull(startSequence);

        if (startSequence.Length != 2)
        {
            throw new ArgumentException(paramName: nameof(startSequence),
                message: "Must have exactly two elements. Length: "
                    + $"{startSequence.Length}.");
        }

        ArgumentOutOfRangeException.ThrowIfNegative(length);

        List<T> result = [];

        for (var i = 0; i < length; i++)
        {
            if (i < 2)
            {
                result.Add(startSequence[i]);
            }
            else
            {
                result.Add(result[i - 2] + result[i - 1]);
            }
        }

        return [.. result];
    }

    static void Main()
    {
        List<(double[] startSequence, int length)> failures = [];

        foreach (var (startSequence, length, expected) in Tests)
        {
            Console.Write($"Testing {ValueToString(startSequence)} and "
                + $"{ValueToString(length)} (expecting "
                + $"{ValueToString(expected)})...");
            var actual = FibonacciSequence(startSequence, length);
            Console.Write(ValueToString(actual));
            var success = expected.SequenceEqual(actual);
            Console.WriteLine($" (success: {success}).");

            if (!success)
            {
                failures.Add((startSequence, length));
            }
        }

        if (failures.Count > 0)
        {
            Console.WriteLine("The following inputs failed:");

            foreach (var (startSequence, length) in failures)
            {
                Console.WriteLine($"  {ValueToString(startSequence)} and "
                    + $"{ValueToString(length)}.");
            }
        }
        else
        {
            Console.WriteLine("All tests passed!");
        }
    }

    static List<(double[] startSequence, int length, double[] expected)> Tests =>
    [
        ([0, 1], 20, [0, 1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144, 233, 377,
            610, 987, 1597, 2584, 4181]),
        ([21, 32], 1, [21]),
        ([0, 1], 0, []),
        ([10, 20], 2, [10, 20]),
        ([123456789, 987654321], 5, [123456789, 987654321, 1111111110,
            2098765431, 3209876541]),
    ];

    static string ValueToString(object? value)
    {
        const string @null = "null";

        if (value is null)
        {
            return @null;
        }
        else if (value is string)
        {
            return $"\"{value}\"";
        }
        else if (value is IList list)
        {
            StringBuilder result = new("[");

            for (int i = 0; i < list.Count; i++)
            {
                if (i > 0)
                {
                    result.Append(", ");
                }

                result.Append(ValueToString(list[i]));
            }

            return result.Append(']').ToString();
        }

        return value.ToString() ?? @null;
    }
}
