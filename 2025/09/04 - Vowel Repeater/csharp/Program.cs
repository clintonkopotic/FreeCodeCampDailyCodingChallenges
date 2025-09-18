namespace VowelRepeater;
using System;
using System.Collections.Generic;
using System.Text;

internal class Program
{
    static string RepeatVowels(string str)
    {
        ArgumentNullException.ThrowIfNull(str);
        var vowelCount = 0;
        StringBuilder result = new();

        foreach (var character in str)
        {
            result.Append(character);

            if (Vowels.Contains(character))
            {
                vowelCount++;
                
                if (vowelCount > 1)
                {
                    result.Append(char.ToLowerInvariant(character),
                        repeatCount: vowelCount - 1);
                }
            }
        }

        return result.ToString();
    }

    static HashSet<char> Vowels
        => ['a', 'A', 'e', 'E', 'i', 'I', 'o', 'O', 'u', 'U',];

    static void Main()
    {
        List<string> failures = [];

        foreach ((var str, var expected) in Tests)
        {
            Console.Write($"Testing \"{str}\" (expecting "
                + $"\"{expected}\")...");
            var actual = RepeatVowels(str);
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
        { "hello world", "helloo wooorld" },
        { "freeCodeCamp", "freeeCooodeeeeCaaaaamp" },
        { "AEIOU", "AEeIiiOoooUuuuu" },
        { "I like eating ice cream in Iceland",
            "I liikeee eeeeaaaaatiiiiiing iiiiiiiceeeeeeee "
            + "creeeeeeeeeaaaaaaaaaam iiiiiiiiiiin "
            + "Iiiiiiiiiiiiceeeeeeeeeeeeelaaaaaaaaaaaaaand" },
    };
}
