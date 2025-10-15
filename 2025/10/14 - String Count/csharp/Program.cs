namespace StringCount;
using System;
using System.Collections.Generic;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-14"/>
/// </summary>
internal class Program
{
    static int Count(string text, string pattern)
    {
        ArgumentNullException.ThrowIfNull(text);
        ArgumentNullException.ThrowIfNull(pattern);

        var result = 0;

        for (var i = 0; i < text.Length; i++)
        {
            i = text.IndexOf(pattern, i);

            if (i < 0)
            {
                break;
            }

            result++;
        }

        return result;
    }

    static void Main()
    {
        List<(string text, string pattern)> failures = [];

        foreach (var (text, pattern, expected) in Tests)
        {
            Console.Write($"Testing \"{text}\" and \"{pattern}\" (expecting " + $"{expected})...");
            var actual = Count(text, pattern);
            Console.Write(actual);
            var success = expected == actual;
            Console.WriteLine($" (success: {success}).");

            if (!success)
            {
                failures.Add((text, pattern));
            }
        }

        if (failures.Count > 0)
        {
            Console.WriteLine("The following inputs failed:");

            foreach (var (text, pattern) in failures)
            {
                Console.WriteLine($"  \"{text}\" and \"{pattern}\".");
            }
        }
        else
        {
            Console.WriteLine("All tests passed!");
        }
    }

    static List<(string text, string pattern, int expected)> Tests =>
    [
        ("abcdefg", "def", 1),
        ("hello", "world", 0),
        ("mississippi", "iss", 2),
        ("she sells seashells by the seashore", "sh", 3),
        ("101010101010101010101", "101", 10),
    ];
}
