namespace ReverseSentence;
using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-09-11"/>
/// </summary>
internal class Program
{
    static string ReverseSentence(string sentence)
    {
        var words = sentence.Split(' ', StringSplitOptions.RemoveEmptyEntries
            | StringSplitOptions.TrimEntries);
        StringBuilder result = new();

        for (var i = words.Length - 1; i >= 0; i--)
        {
            if (i < (words.Length - 1))
            {
                result.Append(' ');
            }

            result.Append(words[i]);
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
            var actual = ReverseSentence(sentence);
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
        { "world hello", "hello world" },
        { "push commit git", "git commit push" },
        { "npm  install  sudo", "sudo install npm" },
        { "import    default   function  export",
            "export function default import" },
    };
}
