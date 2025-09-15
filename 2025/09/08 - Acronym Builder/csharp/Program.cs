namespace AcronymBuilder;
using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-09-08"/>
/// </summary>
internal class Program
{
    public static string BuildAcronym(string str)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(str);
        HashSet<string> wordsToIgnore
            = new(StringComparer.InvariantCultureIgnoreCase)
            { "a", "for", "an", "and", "by", "of" };
        StringBuilder result = new();
        var words = str.Split(separator: ' ',
            options: StringSplitOptions.RemoveEmptyEntries
            | StringSplitOptions.TrimEntries);

        for (var i = 0; i < words.Length; i++)
        {
            var word = words[i];

            if (!string.IsNullOrWhiteSpace(word) && word.Length > 0
                && (i <= 0 || !wordsToIgnore.Contains(word)))
            {
                result.Append(char.ToUpper(word[0]));
            }
        }

        return result.ToString();
    }

    static void Main()
    {
        List<string> failures = [];

        foreach ((var str, var expected) in Tests)
        {
            Console.Write($"Building acronym from \"{str}\" (expecting "
                + $"\"{expected}\")...");

            try
            {
                var acronym = BuildAcronym(str);
                var success = expected.Equals(acronym);
                Console.WriteLine($"Acronym: \"{acronym}\"; Success: "
                    + $"{success}.");

                if (!success)
                {
                    failures.Add(expected);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}.");
                failures.Add(expected);
            }
        }

        if (failures.Count <= 0)
        {
            Console.WriteLine("All tests passed!");
        }
        else
        {
            Console.WriteLine($"The number of failures: {failures.Count}.");
        }
    }

    static readonly Dictionary<string, string> Tests = new()
    {
        { "Search Engine Optimization", "SEO" },
        { "Frequently Asked Questions", "FAQ" },
        { "National Aeronautics and Space Administration", "NASA" },
        { "Federal Bureau of Investigation", "FBI"},
        {"For your information", "FYI"},
        {"By the way", "BTW"},
        { "An unstoppable herd of waddling penguins overtakes the icy "
            + "mountains and sings happily", "AUHWPOTIMSH"},
    };
}
