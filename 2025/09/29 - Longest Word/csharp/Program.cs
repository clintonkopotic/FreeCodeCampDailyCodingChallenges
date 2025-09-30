namespace LongestWord;
using System;
using System.Collections.Generic;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-09-29"/>
/// </summary>
internal class Program
{
    static string GetLongestWord(string sentence)
    {
        ArgumentNullException.ThrowIfNull(sentence);

        var words = sentence.Split(' ', StringSplitOptions.RemoveEmptyEntries
            | StringSplitOptions.TrimEntries);
        var result = string.Empty;

        for (var i = 0; i < words.Length; i++)
        {
            var word = words[i];

            if (word.EndsWith('.'))
            {
                word = word[0..(word.Length - 1)];
            }

            if (word.Length > result.Length)
            {
                result = word;
            }
        }

        return result;
    }
    static void Main()
    {
        List<string> failures = [];

        foreach (var (sentence, expected) in Tests)
        {
            Console.Write($"Testing \"{sentence}\" (expecting "
                + $"\"{expected}\")...");
            var actual = GetLongestWord(sentence);
            Console.Write($"\"{actual}\"");
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

    static List<(string sentence, string expected)> Tests =>
    [
        ("coding is fun", "coding"),
        ("Coding challenges are fun and educational.", "educational"),
        ("This sentence has multiple long words.", "sentence"),
    ];
}
