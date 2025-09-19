namespace Pangram;
using System;
using System.Collections.Generic;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-09-03"/>
/// </summary>
internal class Program
{
    static bool IsPangram(string sentence, string letters)
    {
        ArgumentNullException.ThrowIfNull(sentence);
        ArgumentNullException.ThrowIfNull(letters);

        return AllLettersUsed(sentence, letters)
            && AllLettersUsed(letters, sentence);

        static bool AllLettersUsed(string @string, string other)
        {
            ArgumentNullException.ThrowIfNull(@string);
            ArgumentNullException.ThrowIfNull(other);

            foreach (var character in @string)
            {
                if (char.IsLetter(character)
                    && !other.Contains(character,
                    StringComparison.InvariantCultureIgnoreCase))
                {
                    return false;
                }
            }

            return true;
        }
    }

    static void Main()
    {
        List<(string sentence, string letters)> failures = [];

        foreach ((var sentence, var letters, var expected) in Tests)
        {
            Console.Write($"Testing \"{sentence}\" and \"{letters}\" "
                + $"(expecting {expected})...");
            var actual = IsPangram(sentence, letters);
            var success = expected == actual;
            Console.WriteLine($"{actual} (success: {success}).");

            if (!success)
            {
                failures.Add((sentence, letters));
            }
        }

        if (failures.Count > 0)
        {
            Console.WriteLine("The following inputs failed:");

            foreach ((var sentence, var letters) in failures)
            {
                Console.WriteLine($"  \"{sentence}\" and \"{letters}\".");
            }
        }
        else
        {
            Console.WriteLine("All tests passed!");
        }
    }

    static List<(string sentence, string letters, bool expected)> Tests =>
    [
        ("hello", "helo", true),
        ("hello", "hel", false),
        ("hello", "helow", false),
        ("hello world", "helowrd", true),
        ("Hello World!", "helowrd", true),
        ("Hello World!", "heliowrd", false),
        ("freeCodeCamp", "frcdmp", false),
        ("The quick brown fox jumps over the lazy dog.",
            "abcdefghijklmnopqrstuvwxyz", true),
    ];
}
