namespace camelCase;
using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-08-25"/>
/// </summary>
internal class Program
{
    static string IsCamelCase(string s)
    {
        ArgumentNullException.ThrowIfNull(s);

        var words = s.Split([' ', '-', '_'],
            StringSplitOptions.RemoveEmptyEntries
            | StringSplitOptions.TrimEntries);

        if (words.Length <= 1)
        {
            return string.Empty;
        }

        StringBuilder result = new(words[0].ToLower());

        for (int i = 1; i < words.Length; i++)
        {
            var word = words[i].ToLower();
            word = char.ToUpper(word[0]) + word[1..];
            result.Append(word);
        }

        return result.ToString();
    }

    static void Main()
    {
        List<string> failures = [];

        foreach (var (s, expected) in Tests)
        {
            Console.Write($"Testing \"{s}\" (expecting \"{expected}\")...");
            var actual = IsCamelCase(s);
            Console.Write($"\"{actual}\"");
            var success = expected == actual;
            Console.WriteLine($" (success: {success}).");

            if (!success)
            {
                failures.Add(actual);
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

    static List<(string s, string expected)> Tests =>
    [
        ("hello world", "helloWorld"),
        ("HELLO WORLD", "helloWorld"),
        ("secret agent-X", "secretAgentX"),
        ("FREE cODE cAMP", "freeCodeCamp"),
        ("ye old-_-sea  faring_buccaneer_-_with a - peg__leg----and a_parrot_ "
            + "_named- _squawk",
        "yeOldSeaFaringBuccaneerWithAPegLegAndAParrotNamedSquawk"),
    ];
}
