namespace UniqueCharacters;
using System;
using System.Collections.Generic;

internal class Program
{
    public static bool AllUnique(string str)
    {
        ArgumentException.ThrowIfNullOrEmpty(str);

        HashSet<char> uniqueChars = [];

        foreach (var @char in str)
        {
            if (!uniqueChars.Add(@char))
            {
                return false;
            }
        }

        return true;
    }

    static void Main()
    {
        List<string> failures = [];

        foreach ((var test, var expected) in Tests)
        {
            Console.Write($"Testing \"{test}\" (expecting {expected})...");
            var result = AllUnique(test);
            var success = expected == result;
            Console.WriteLine($"{result} (Success: {success}).");

            if (!success)
            {
                failures.Add(test);
            }
        }

        if (failures.Count <= 0)
        {
            Console.WriteLine("All tests passed!");
        }
        else
        {
            Console.Write("The following inputs failed: ");

            for (var i = 0; i < failures.Count; i++)
            {
                if (i > 0)
                {
                    Console.Write(", ");
                }

                Console.Write($"\"{failures[i]}\"");
            }

            Console.WriteLine('.');
        }
    }

    static Dictionary<string, bool> Tests => new()
    {
        { "abc", true },
        { "aA", true },
        { "QwErTy123!@", true },
        { "~!@#$%^&*()_+", true },
        { "hello", false },
        { "freeCodeCamp", false },
        { "!@#*$%^&*()aA", false },
    };
}
