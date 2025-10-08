namespace BaseCheck;
using System;
using System.Collections.Generic;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-08-12"/>
/// </summary>
internal class Program
{
    static bool IsValidNumber(string n, int @base)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(n);
        ArgumentOutOfRangeException.ThrowIfLessThan(@base, 2);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(@base, 36);

        var baseCharcters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ"[0..@base];

        foreach (var character in n)
        {
            if (!baseCharcters.Contains(char.ToUpper(character)))
            {
                return false;
            }
        }

        return true;
    }

    static void Main()
    {
        List<(string n, int @base)> failures = [];

        foreach (var (n, @base, expected) in Tests)
        {
            Console.Write($"Testing \"{n}\" and {@base} (expecting "
                + $"{expected})...");
            var actual = IsValidNumber(n, @base);
            Console.Write(actual);
            var success = expected == actual;
            Console.WriteLine($" (success: {success}).");

            if (!success)
            {
                failures.Add((n, @base));
            }
        }

        if (failures.Count > 0)
        {
            Console.WriteLine("The following inputs failed:");

            foreach (var (n, @base) in failures)
            {
                Console.WriteLine($"  \"{n}\" and {@base}.");
            }
        }
        else
        {
            Console.WriteLine("All tests passed!");
        }
    }

    static List<(string n, int @base, bool expected)> Tests => new()
    {
        ("10101", 2, true),
        ("10201", 2, false),
        ("76543210", 8, true),
        ("9876543210", 8, false),
        ("9876543210", 10, true),
        ("ABC", 10, false),
        ("ABC", 16, true),
        ("Z", 36, true),
        ("ABC", 20, true),
        ("4B4BA9", 16, true),
        ("5G3F8F", 16, false),
        ("5G3F8F", 17, true),
        ("abc", 10, false),
        ("abc", 16, true),
        ("AbC", 16, true),
        ("z", 36, true),
    };
}
