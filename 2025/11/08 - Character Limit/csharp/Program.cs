namespace CharacterLimit;
using System;
using System.Collections.Generic;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-11-08"/>
/// </summary>
internal class Program
{
    static string CanPost(string message)
    {
        ArgumentNullException.ThrowIfNull(message);

        return message.Length switch
        {
            <= 40 => "short post",
            <= 80 => "long post",
            _ => "invalid post",
        };
    }

    static void Main()
    {
        List<string> failures = [];

        foreach (var (message, expected) in Tests)
        {
            Console.Write($"Testing \"{message}\" (expecting "
                + $"\"{expected}\")...");
            var actual = CanPost(message);
            Console.Write($"\"{actual}\"");
            var success = expected == actual;
            Console.WriteLine($" (success: {success}).");

            if (!success)
            {
                failures.Add(message);
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

    static List<(string message, string expected)> Tests =>
    [
        ("Hello world", "short post"),
        ("This is a longer message but still under eighty characters.",
            "long post"),
        ("This message is too long to fit into either of the character limits "
            + "for a social media post.", "invalid post"),
    ];
}
