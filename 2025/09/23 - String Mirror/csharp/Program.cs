namespace StringMirror;
using System;
using System.Collections.Generic;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-09-23"/>
/// </summary>
internal class Program
{
    static bool IsMirror(string str1, string str2)
    {
        ArgumentNullException.ThrowIfNull(str1);
        ArgumentNullException.ThrowIfNull(str2);

        for (int i = 0, j = (str2.Length - 1);
            (i < str1.Length) && (j >= 0); i++, j--)
        {
            var endOf1 = false;

            while (!char.IsLetter(str1[i]))
            {
                i++;

                if (i >= str1.Length)
                {
                    endOf1 = true;
                    
                    break;
                }
            }

            var endOf2 = false;

            while (!char.IsLetter(str2[j]))
            {
                j--;

                if (j < 0)
                {
                    endOf2 = true;

                    break;
                }
            }

            if (endOf1 && endOf2)
            {
                break;
            }
            else if ((endOf1 || endOf2) || (str1[i] != str2[j]))
            {
                return false;
            }
        }

        return true;
    }

    static void Main()
    {
        List<(string str1, string str2)> failures = [];

        foreach (var (str1, str2, expected) in Tests)
        {
            Console.Write($"Testing \"{str1}\" and \"{str2}\" (expecting "
                + $"{expected})...");
            var actual = IsMirror(str1, str2);
            Console.Write($"\"{actual}\"");
            var success = expected == actual;
            Console.WriteLine($" (success: {success}).");

            if (!success)
            {
                failures.Add((str1, str2));
            }
        }

        if (failures.Count > 0)
        {
            Console.WriteLine("The following inputs failed:");

            foreach (var (str1, str2) in failures)
            {
                Console.WriteLine($"  \"{str1}\" and \"{str2}\".");
            }
        }
        else
        {
            Console.WriteLine("All tests passed!");
        }
    }

    static List<(string str1, string str2, bool expected)> Tests =>
    [
        ("helloworld", "helloworld", false),
        ("Hello World", "dlroW olleH", true),
        ("RaceCar", "raCecaR", true),
        ("RaceCar", "RaceCar", false),
        ("Mirror", "rorrim", false),
        ("Hello World", "dlroW-olleH", true),
        ("Hello World", "!dlroW !olleH", true),
    ];
}
