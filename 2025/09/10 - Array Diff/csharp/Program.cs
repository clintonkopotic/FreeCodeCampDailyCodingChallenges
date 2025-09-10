namespace ArrayDiff;
using System;
using System.Collections.Generic;

/// <summary>
/// This is the Daily Coding Challenge for Wed 10 Sep 2025:
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-09-10">Website Link</see>.
/// </summary>
internal class Program
{
    public static string[] ArrayDiff(string[] arr1, string[] arr2)
    {
        SortedSet<string> result = [];

        foreach (var item in arr1)
        {
            if (Array.IndexOf(arr2, item) < 0)
            {
                _ = result.Add(item);
            }
        }

        foreach (var item in arr2)
        {
            if (Array.IndexOf(arr1, item) < 0)
            {
                _ = result.Add(item);
            }
        }

        return [.. result];
    }

    static void Main()
    {
        List<(string[] arr1, string[] arr2)> failures = [];

        foreach ((var arr1, var arr2, var expected) in Tests)
        {
            Console.Write($"Testing {nameof(ArrayDiff)}("
                + $"{ArrayToString(arr1)}, {ArrayToString(arr2)}) (expecting "
                + $"{ArrayToString(expected)})...");
            var actual = ArrayDiff(arr1, arr2);
            var success = false;

            if (expected.Length == actual.Length)
            {
                success = true;

                for (var i = 0; i < actual.Length; i++)
                {
                    if (expected[i] != actual[i])
                    {
                        success = false;

                        break;
                    }
                }
            }

            Console.WriteLine($"{ArrayToString(actual)} (Success: "
                + $"{success}).");
            
            if (!success)
            {
                failures.Add((arr1, arr2));
            }
        }

        if (failures.Count <= 0)
        {
            Console.WriteLine("All tests passed!");
        }
        else
        {
            Console.WriteLine("The following inputs failed:");

            foreach ((var arr1, var arr2) in failures)
            {
                Console.WriteLine($"  ({ArrayToString(arr1)}, "
                    + $"{ArrayToString(arr2)})");
            }
        }
    }

    static List<(string[] arr1, string[] arr2, string[] expected)> Tests =>
    [
        (["apple", "banana"], ["apple", "banana", "cherry"], ["cherry"]),
        (["apple", "banana", "cherry"], ["apple", "banana"], ["cherry"]),
        (["one", "two", "three", "four", "six"], ["one", "three", "eight"],
            ["eight", "four", "six", "two"]),
        (["two", "four", "five", "eight"], ["one", "two", "three", "four",
            "seven", "eight"], ["five", "one", "seven", "three"]),
        (["I", "like", "freeCodeCamp"], ["I", "like", "rocks"],
            ["freeCodeCamp", "rocks"]),
    ];

    static string ArrayToString(string[] strings)
        => $"[{string.Join(", ", strings)}]";
}
