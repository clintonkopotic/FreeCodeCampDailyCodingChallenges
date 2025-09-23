namespace DigitsVsLetters;
using System;
using System.Collections.Generic;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-09-22"/>
/// </summary>
internal class Program
{
    static string DigitsOrLetters(string str)
    {
        ArgumentNullException.ThrowIfNull(str);

        var numberOfDigits = 0;
        var numberOfLetters = 0;

        foreach (var @char in str)
        {
            if (char.IsDigit(@char))
            {
                numberOfDigits++;
            }
            else if (char.IsLetter(@char))
            {
                numberOfLetters++;
            }
        }

        if (numberOfDigits > numberOfLetters)
        {
            return "digits";
        }
        else if (numberOfLetters > numberOfDigits)
        {
            return "letters";
        }

        return "tie";
    }

    static void Main()
    {
        List<string> failures = [];

        foreach (var (str, expected) in Tests)
        {
            Console.Write($"Testing \"{str}\" (expecting \"{expected}\")...");
            var actual = DigitsOrLetters(str);
            Console.Write($"\"{actual}\"");
            var success = expected == actual;
            Console.WriteLine($" (success: {success}).");

            if (!success)
            {
                failures.Add(str);
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

    static List<(string str, string expected)> Tests =>
    [
        ("abc123", "tie"),
        ("a1b2c3d", "letters"),
        ("1a2b3c4", "digits"),
        ("abc123!@#DEF", "letters"),
        ("H3110 W0R1D", "digits"),
        ("P455W0RD", "tie"),
    ];
}
