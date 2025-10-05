namespace AnagramChecker;
using System;
using System.Collections.Generic;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-08-16"/>
/// </summary>
internal class Program
{
    static bool AreAnagrams(string str1, string str2)
    {
        ArgumentNullException.ThrowIfNull(str1);
        ArgumentNullException.ThrowIfNull(str2);

        List<char> str1characters = [];

        foreach (var str1character in str1)
        {
            if (!char.IsWhiteSpace(str1character))
            {
                str1characters.Add(char.ToLower(str1character));
            }
        }

        foreach (var str2character in str2)
        {
            if (char.IsWhiteSpace(str2character))
            {
                continue;
            }

            var indexInStr1Characters = str1characters.IndexOf(
                char.ToLower(str2character));

            if (indexInStr1Characters < 0)
            {
                return false;
            }

            str1characters.RemoveAt(indexInStr1Characters);
        }

        return str1characters.Count == 0;
    }

    static void Main()
    {
        List<(string str1, string str2)> failures = [];

        foreach (var (str1, str2, expected) in Tests)
        {
            Console.Write($"Testing \"{str1}\" and \"{str2}\" (expecting "
                + $"{expected})...");
            var actual = AreAnagrams(str1, str2);
            Console.Write(actual);
            var success = expected == actual;
            Console.WriteLine($" (success: {success}).");

            if (!success)
            {
                failures.Add((str1, str2));
            }
        }

        if (failures.Count > 0)
        {
            Console.WriteLine("The following inputs failed:");

            foreach (var (str1, str2) in failures)
            {
                Console.WriteLine($"  \"{str1}\" and \"{str2}\".");
            }
        }
        else
        {
            Console.WriteLine("All tests passed!");
        }
    }

    static List<(string str1, string str2, bool expected)> Tests =>
    [
        ("listen", "silent", true),
        ("School master", "The classroom", true),
        ("A gentleman", "Elegant man", true),
        ("Hello", "World", false),
        ("apple", "banana", false),
        ("cat", "dog", false),
    ];
}
