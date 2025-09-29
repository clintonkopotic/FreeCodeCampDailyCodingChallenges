namespace CsvHeaderParse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-09-28"/>
/// </summary>
internal class Program
{
    static string[] GetHeadings(string csv)
    {
        ArgumentNullException.ThrowIfNull(csv);

        return csv.Split(',', StringSplitOptions.RemoveEmptyEntries
            | StringSplitOptions.TrimEntries);
    }

    static void Main()
    {
        List<string> failures = [];

        foreach (var (csv, expected) in Tests)
        {
            Console.Write($"Testing \"{csv}\" (expecting "
                + $"{ArrayToString(expected)})...");
            var actual = GetHeadings(csv);
            Console.Write(ArrayToString(actual));
            var success = expected.SequenceEqual(actual);
            Console.WriteLine($" (success: {success}).");

            if (!success)
            {
                failures.Add(csv);
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

    static List<(string csv, string[] expected)> Tests =>
    [
        ("name,age,city", ["name", "age", "city"]),
        ("first name,last name,phone", ["first name", "last name", "phone"]),
        ("username , email , signup date ",
            ["username", "email", "signup date"]),
    ];

    static string ArrayToString(string[] strings)
    {
        ArgumentNullException.ThrowIfNull(strings);

        StringBuilder stringBuilder = new("[");

        for (var i = 0; i < strings.Length; i++)
        {
            if (i > 0)
            {
                stringBuilder.Append(", ");
            }

            stringBuilder.Append($"\"{strings[i]}\"");
        }

        return stringBuilder.Append(']').ToString();
    }
}
