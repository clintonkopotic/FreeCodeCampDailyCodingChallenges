namespace PhoneNumberFormatter;
using System;
using System.Collections.Generic;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-09-30"/>
/// </summary>
internal class Program
{
    static string FormatNumber(string number)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(number);

        if (number.Length != 11)
        {
            throw new ArgumentException("The length must be 11.");
        }

        return $"+{number[0]} ({number[1..4]}) {number[4..7]}-{number[7..11]}";
    }

    static void Main()
    {
        List<string> failures = [];

        foreach (var (number, expected) in Tests)
        {
            Console.Write($"Testing \"{number}\" (expecting "
                + $"\"{expected}\")...");
            var actual = FormatNumber(number);
            Console.Write($"\"{actual}\"");
            var success = expected == actual;
            Console.WriteLine($" (success: {success}).");

            if (!success)
            {
                failures.Add(number);
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

    static List<(string number, string expected)> Tests =>
    [
        ("05552340182", "+0 (555) 234-0182"),
        ("15554354792", "+1 (555) 435-4792"),
    ];
}
