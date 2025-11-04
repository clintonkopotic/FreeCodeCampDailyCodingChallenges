namespace WordCounter;
using System;
using System.Collections.Generic;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-11-03"/>
/// </summary>
internal class Program
{
    static int CountWords(string sentence)
    {
        ArgumentNullException.ThrowIfNull(sentence);

        return sentence.Split(' ', StringSplitOptions.RemoveEmptyEntries
            | StringSplitOptions.TrimEntries).Length;
    }

    static void Main()
    {
        List<string> failures = [];

        foreach (var (sentence, expected) in Tests)
        {
            Console.Write($"Testing \"{sentence}\" (expecting {expected})...");
            var actual = CountWords(sentence);
            Console.Write(actual);
            var success = expected == actual;
            Console.WriteLine($" (success: {success}).");

            if (!success)
            {
                failures.Add(sentence);
            }
        }

        if (failures.Count > 0)
        {
            Console.WriteLine("The following inputs failed:");

            foreach (var failure in failures)
            {
                Console.WriteLine($"  \"{failure}\".");
            }
        }
        else
        {
            Console.WriteLine("All tests passed!");
        }
    }

    static List<(string sentence, int expected)> Tests =>
    [
        ("Hello world", 2),
        ("The quick brown fox jumps over the lazy dog.", 9),
        ("I like coding challenges!", 4),
        ("Complete the challenge in JavaScript and Python.", 7),
        ("The missing semi-colon crashed the entire internet.", 7),
    ];
}
