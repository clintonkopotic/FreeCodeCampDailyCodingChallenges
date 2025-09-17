namespace SlugGenerator;
using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-09-17"/>
/// </summary>
internal class Program
{
    static string GenerateSlug(string str)
    {
        ArgumentNullException.ThrowIfNull(str);

        str = str.Trim();
        var lastCharacterWasASpace = false;
        StringBuilder result = new();

        foreach (var @char in str)
        {
            if (@char == ' ')
            {
                if (!lastCharacterWasASpace)
                {
                    result.Append("%20");
                    lastCharacterWasASpace = true;
                }
            }
            else if (char.IsNumber(@char))
            {
                result.Append(@char);
                lastCharacterWasASpace = false;
            }
            else if (char.IsLetter(@char))
            {
                result.Append(char.ToLower(@char));
                lastCharacterWasASpace = false;
            }
        }

        return result.ToString();
    }

    static void Main()
    {
        List<string> failures = [];

        foreach ((var str, var expected) in Tests)
        {
            Console.Write($"Testing \"{str}\" (expecting "
                + $"\"{expected}\")...");
            var actual = GenerateSlug(str);
            var success = expected == actual;
            Console.WriteLine($"\"{actual}\" (success: {success}).");

            if (!success)
            {
                failures.Add(str);
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
        { "helloWorld", "helloworld" },
        {"hello world!", "hello%20world" },
        {" hello-world ", "helloworld" },
        {"hello  world", "hello%20world" },
        {"  ?H^3-1*1]0! W[0%R#1]D  ", "h3110%20w0r1d" },
    };
}
