namespace WordFrequency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-09-14"/>
/// </summary>
internal class Program
{
    static string[] GetWords(string paragraph)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(paragraph);
        var words = paragraph.Split(' ', StringSplitOptions.RemoveEmptyEntries
            | StringSplitOptions.TrimEntries);
        Dictionary<string, int> wordFrequency = [];

        for (var i = 0; i < words.Length; i++)
        {
            var word = words[i].Trim();

            if (word.Length <= 0)
            {
                continue;
            }

            // Change the case of the word to lowercase for easier comparison.
            word = word.ToLower();
            var lastIndex = word.Length - 1;

            // Remove any punctuation.
            if (word[lastIndex] == ',' || word[lastIndex] == '.'
                || word[lastIndex] == '!')
            {
                word = word[..lastIndex];
            }

            // Keep track of the number of times this word has occured so far in
            // the paragraph.
            var wordCount = 0;

            if (wordFrequency.TryGetValue(word, out var value))
            {
                wordCount = value;
            }

            wordFrequency[word] = wordCount + 1;
        }

        var sorted = wordFrequency.OrderByDescending(kvp => kvp.Value);
        var mostWordsSize = wordFrequency.Count >= 3 ? 3 : wordFrequency.Count;
        List<string> mostWords = [];

        foreach ((var word, _) in sorted)
        {
            mostWords.Add(word);

            if (mostWords.Count >= mostWordsSize)
            {
                return [.. mostWords];
            }
        }

        return [];
    }

    static void Main()
    {
        List<string> failures = [];

        foreach ((var paragraph, var expected) in Tests)
        {
            Console.Write($"Testing \"{paragraph}\" (expecting "
                + $"{ArrayToString(expected)})...");
            var actual = GetWords(paragraph);
            var success = expected.SequenceEqual(actual);
            Console.WriteLine($"{ArrayToString(actual)} (success: {success}).");

            if (!success)
            {
                failures.Add(paragraph);
            }
        }

        if (failures.Count <= 0)
        {
            Console.WriteLine("All tests passed!");
        }
        else
        {
            Console.WriteLine("The following inputs failed:");

            foreach (var failure in failures)
            {
                Console.WriteLine($"  \"{failure}\".");
            }
        }
    }

    static Dictionary<string, string[]> Tests => new()
    {
        {"Coding in Python is fun because coding Python allows for coding in "
            + "Python easily while coding", ["coding", "python", "in"]},
        {"I like coding. I like testing. I love debugging!",
            ["i", "like", "coding"]},
        {"Debug, test, deploy. Debug, debug, test, deploy. Debug, test, test, "
            + "deploy!", ["debug", "test", "deploy"]},
    };

    static string ArrayToString(string[] words)
    {
        var result = new StringBuilder().Append('[');

        for (var i = 0; i < words.Length; i++)
        {
            if (i > 0)
            {
                result.Append(", ");
            }

            result.Append($"\"{words[i]}\"");
        }

        return result.Append(']').ToString();
    }
}
