namespace JbelmudText;
using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-08-15"/>
/// </summary>
internal class Program
{
    static string Jbelmu(string text)
    {
        ArgumentNullException.ThrowIfNull(text);

        var words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries
            | StringSplitOptions.TrimEntries);
        var wordCount = 0;
        StringBuilder result = new();

        foreach (var word in words)
        {
            if (wordCount > 0)
            {
                result.Append(' ');
            }

            if (word.Length <= 3)
            {
                result.Append(word);
            }
            else
            {
                var middle = word.ToCharArray(startIndex: 1,
                    length: word.Length - 2);
                Array.Sort(middle);
                var jumbledWord = word[0] + new string(middle) + word[^1];
                result.Append(jumbledWord);
            }

            wordCount++;
        }

        return result.ToString();
    }

    static void Main()
    {
        List<string> failures = [];

        foreach (var (text, expected) in Tests)
        {
            Console.Write($"Testing \"{text}\" (expecting \"{expected}\")...");
            var actual = Jbelmu(text);
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

    static List<(string text, string expected)> Tests =>
    [
        ("hello world", "hello wlord"),
        ("i love jumbled text", "i love jbelmud text"),
        ("freecodecamp is my favorite place to learn to code",
            "faccdeeemorp is my faiortve pacle to laern to cdoe"),
        ("the quick brown fox jumps over the lazy dog",
            "the qciuk borwn fox jmpus oevr the lazy dog"),
    ];
}
