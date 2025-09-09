namespace AcronymBuilder;
using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// This is the Daily Coding Challenge for Mon 08 Sep 2025:
/// <see href="https://www.linkedin.com/posts/free-code-camp_freecodecamp-now-has-daily-coding-challenges-activity-7370822135944187905-0YDw">LinkedIn Post</see>.
/// </summary>
internal class Program
{
    public static string BuildAcronym(string wordsToAcronym)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(wordsToAcronym);
        StringBuilder result = new();
        HashSet<string> wordsToIgnore = ["a", "for", "an", "and", "by", "of"];
        var words = wordsToAcronym.Split(separator: ' ',
            options: StringSplitOptions.RemoveEmptyEntries
            | StringSplitOptions.TrimEntries);

        foreach (var word in words)
        {
            if (!string.IsNullOrWhiteSpace(word) && word.Length > 0
                && char.IsUpper(word[0])
                && !wordsToIgnore.Contains(word.ToLowerInvariant()))
            {
                result.Append(word[0]);
            }
        }

        return result.ToString();
    }

    static void Main()
    {
        List<string> failures = [];

        foreach ((var expected, var words) in Tests)
        {
            Console.Write($"Building acronym from \"{words}\" (expecting "
                + $"\"{expected}\")...");

            try
            {
                var acronym = BuildAcronym(words);
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

        if (failures.Count > 0)
        {
            Console.WriteLine($"The number of failures: {failures.Count}.");
        }
    }

    static readonly Dictionary<string, string> Tests = new()
    {
        { "NASA", "National Aeronautics and Space Administration" },
        { "YAML", "YAML Ain't Markup Language" },
        { "DRY", "Don't Repeat Yourself" },
        { "KISS", "Keep It Simple, Silly" },
        { "ASAP", "As Soon As Possible" },
        { "FAQ", "Frequently Asked Questions" },
        { "PTO", "Paid Time Off" },
        { "SME", "Subject Matter Expert" },
        { "ETA", "Estimated Time of Arrival" },
        { "TBD", "To Be Determined" },
        { "DIY", "Do It Yourself" },
        { "CB", "Close Of Business" },
    };
}
