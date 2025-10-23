namespace SpeakWiselyYouMust;
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-22"/>
/// </summary>
internal class Program
{
    static string WiseSpeak(string sentence)
    {
        ArgumentNullException.ThrowIfNull(sentence);

        var words = sentence.Split(' ', StringSplitOptions.RemoveEmptyEntries
            | StringSplitOptions.TrimEntries);
        
        if (words.Length <= 0)
        {
            return string.Empty;
        }

        // All given sentences will end with a single punctuation mark. Keep the
        // original punctuation of the sentence and move it to the end of the
        // new sentence.
        var lastWord = words[^1];
        var sentencePunctuationMark = lastWord[^1];

        // Find the first occurrence of one of the following words in the
        // sentence: "have", "must", "are", "will", "can".
        string[] newEndingWords = ["have", "must", "are", "will", "can"];
        var newEndingWordIndex = Array.FindIndex(words,
            word => newEndingWords.Contains(word));

        if (newEndingWordIndex < 0 || newEndingWordIndex >= words.Length)
        {
            throw new ArgumentException(paramName: nameof(sentence),
                message: "Does not contain a word of the following: "
                    + $"[{string.Join(", ", newEndingWords)}].");
        }

        List<string> newWordsOrder = [];
        var newFirstWord = words[newEndingWordIndex + 1];
        newWordsOrder.Add(
            $"{char.ToUpper(newFirstWord[0])}{newFirstWord[1..]}");
        newWordsOrder.AddRange(
            words[(newEndingWordIndex + 2)..(words.Length - 1)]);
        newWordsOrder.Add($"{lastWord[0..(lastWord.Length - 1)]},");
        newWordsOrder.Add(words[0].ToLower());
        newWordsOrder.AddRange(words[1..newEndingWordIndex]);
        newWordsOrder.Add(
            $"{words[newEndingWordIndex]}{sentencePunctuationMark}");

        return string.Join(' ', newWordsOrder);
    }

    static void Main()
    {
        List<string> failures = [];

        foreach (var (sentence, expected) in Tests)
        {
            Console.Write($"Testing \"{sentence}\" (expecting "
                + $"\"{expected}\")...");
            var actual = WiseSpeak(sentence);
            Console.Write($"\"{actual}\"");
            var success = expected == actual;
            Console.WriteLine($" (success: {success}).");

            if (!success)
            {
                failures.Add(sentence);
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

    static List<(string sentence, string expected)> Tests =>
    [
        ("You must speak wisely.", "Speak wisely, you must."),
        ("You can do it!", "Do it, you can!"),
        ("Do you think you will complete this?",
            "Complete this, do you think you will?"),
        ("All your base are belong to us.", "Belong to us, all your base are."),
        ("You have much to learn.", "Much to learn, you have."),
    ];
}
