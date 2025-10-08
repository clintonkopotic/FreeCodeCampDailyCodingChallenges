namespace VowelBalance;
using System;
using System.Collections.Generic;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-08-11"/>
/// </summary>
internal class Program
{
    static bool IsBalanced(string s)
    {
        ArgumentNullException.ThrowIfNull(s);

        const string vowels = "aeiou";
        var totalCharacters = s.Length;
        var numberOfCharactersPerHalf = Convert.ToInt32(
            double.Floor(totalCharacters / 2d));
        var lastIndex1stHalf = numberOfCharactersPerHalf - 1;
        var startIndex2ndHalf = numberOfCharactersPerHalf
            + (totalCharacters % 2);
        var numberOfVowels = 0;

        for (var i = 0; i < totalCharacters; i++)
        {
            var isVowel = vowels.Contains(value: s[i],
                comparisonType: StringComparison.InvariantCultureIgnoreCase);

            if (!isVowel)
            {
                continue;
            }

            if (i <= lastIndex1stHalf)
            {
                numberOfVowels++;
            }
            else if (i >= startIndex2ndHalf)
            {
                numberOfVowels--;
            }
        }

        return numberOfVowels == 0;
    }

    static void Main()
    {
        List<string> failures = [];

        foreach (var (s, expected) in Tests)
        {
            Console.Write($"Testing \"{s}\" (expecting {expected})...");
            var actual = IsBalanced(s);
            Console.Write($"{actual}");
            var success = expected == actual;
            Console.WriteLine($" (success: {success}).");

            if (!success)
            {
                failures.Add(s);
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

    static List<(string s, bool expected)> Tests =>
    [
        ("racecar", true),
        ("Lorem Ipsum", true),
        ("Kitty Ipsum", false),
        ("string", false),
        (" ", true),
        ("abcdefghijklmnopqrstuvwxyz", false),
        ("123A#b!E&*456-o.U", true),
    ];
}
