namespace TargetedSum;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-08-17"/>
/// </summary>
internal class Program
{
    static (int index0, int index1, bool found, string? messsage) FindTarget<T>(
        T[] arr, T target) where T : INumber<T>
    {
        ArgumentNullException.ThrowIfNull(arr);

        if (arr.Length <= 0)
        {
            throw new ArgumentOutOfRangeException(paramName: nameof(arr),
                actualValue: arr.Length,
                message: "The length must be positive.");
        }

        if (!T.IsInteger(target))
        {
            throw new ArgumentOutOfRangeException(paramName: nameof(target),
                actualValue: target, message: "Must be an integer.");
        }

        for (var i = 0; i < arr.Length; i++)
        {
            var number0 = arr[i];

            if (!T.IsInteger(number0))
            {
                throw new ArgumentOutOfRangeException(paramName: nameof(arr),
                    actualValue: number0,
                    message: $"All items must be integers. Index: {i}.");
            }

            for (var j = 0; j < arr.Length; j++)
            {
                if (i == j)
                {
                    continue;
                }

                var number1 = arr[j];

                if (!T.IsInteger(number1))
                {
                    throw new ArgumentOutOfRangeException(
                        paramName: nameof(arr),
                        actualValue: number1,
                        message: $"All items must be integers. Index: {j}.");
                }

                T sum;

                checked
                {
                    sum = number0 + number1;
                }

                if (sum == target)
                {
                    return (i, j, true, null);
                }
            }
        }

        return (-1, -1, false, "Target not found");
    }

    static void Main()
    {
        List<(int[] arr, int target)> failures = [];

        foreach (var (arr, target, expected) in Tests)
        {
            Console.Write($"Testing {ValueToString(arr)} and "
                + $"{ValueToString(target)} (expecting "
                + $"{ValueToString(expected)})...");
            var actual = FindTarget(arr, target);
            Console.Write($"{ValueToString(actual)}");
            var success = expected.found
                ? expected.index0 == actual.index0
                    && expected.index1 == actual.index1
                : expected.message == actual.messsage;
            Console.WriteLine($" (success: {ValueToString(success)}).");

            if (!success)
            {
                failures.Add((arr, target));
            }
        }

        if (failures.Count > 0)
        {
            Console.WriteLine("The following inputs failed:");

            foreach (var (arr, target) in failures)
            {
                Console.WriteLine($"  {ValueToString(arr)} and "
                    + $"{ValueToString(target)}.");
            }
        }
        else
        {
            Console.WriteLine("All tests passed!");
        }
    }

    static List<(int[] arr, int target, (int index0, int index1, bool found,
        string? message) expected)> Tests =>
        [
            ([2, 7, 11, 15], 9, (0, 1, true, null)),
            ([3, 2, 4, 5], 6, (1, 2, true, null)),
            ([1, 3, 5, 6, 7, 8], 15, (4, 5, true, null)),
            ([1, 3, 5, 7], 14, (-1, -1, false, "Target not found")),
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
        else if (value is ValueTuple<int, int, bool, string?> expected)
        {
            (var index0, var index1, var found, var message) = expected;

            if (found)
            {
                return $"({ValueToString(index0)}, {ValueToString(index1)})";
            }

            return ValueToString(message);
        }

        return value.ToString() ?? @null;
    }
}
