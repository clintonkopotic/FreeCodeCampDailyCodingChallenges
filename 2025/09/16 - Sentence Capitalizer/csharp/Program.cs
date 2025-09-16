namespace SentenceCapitalizer;
using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-09-16"/>
/// </summary>
internal class Program
{
    static string Capitalize(string paragraph)
    {
        ArgumentNullException.ThrowIfNull(paragraph);
        var awaitingStartOfSentence = true;
        StringBuilder result = new();

        foreach (var @char in paragraph)
        {
            if (@char == '.' || @char == '?' || @char == '!')
            {
                result.Append(@char);
                awaitingStartOfSentence = true;
            }
            else if (@char == ' ')
            {
                result.Append(@char);
            }
            else if (awaitingStartOfSentence)
            {
                result.Append(char.ToUpper(@char));
                awaitingStartOfSentence = false;
            }
            else
            {
                result.Append(@char);
            }
        }

        return result.ToString();
    }

    static void Main()
    {
        List<string> failures = [];

        foreach ((var sentence, var expected) in Tests)
        {
            Console.Write($"Testing \"{sentence}\" (expecting "
                + $"\"{expected}\")...");
            var actual = Capitalize(sentence);
            var success = expected == actual;
            Console.WriteLine($"\"{actual}\" (success: {success}).");

            if (!success)
            {
                failures.Add(sentence);
            }
        }

        if (failures.Count <= 0)
        {
            Console.WriteLine("All tests passed!");
        }
        else
        {
            Console.WriteLine("The following inputs failed:");

            foreach (var sentence in failures)
            {
                Console.WriteLine($"  \"{sentence}\"");
            }
        }
    }

    static Dictionary<string, string> Tests => new()
    {
        { "this is a simple sentence.", "This is a simple sentence." },
        { "hello world. how are you?", "Hello world. How are you?" },
        { "i did today's coding challenge... it was fun!!", "I did today's coding challenge... It was fun!!" },
        { "crazy!!!strange???unconventional...sentences.", "Crazy!!!Strange???Unconventional...Sentences." },
        { "there's a space before this period . why is there a space before that period ?", "There's a space before this period . Why is there a space before that period ?" },
    };
}
