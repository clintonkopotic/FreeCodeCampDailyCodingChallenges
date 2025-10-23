namespace HtmlAttributeExtractor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-19"/>
/// </summary>
internal partial class Program
{
    static string[] ExtractAttributes(string element)
    {
        List<string> result = [];

        // Adapted from https://stackoverflow.com/a/317081
        foreach (Match match in MatchAttributesRegex().Matches(element))
        {
            if (match.Groups.Count == 3)
            {
                result.Add($"{match.Groups[1]}, {match.Groups[2]}");
            }
        }

        return [.. result];
    }

    static void Main()
    {
        List<string> failures = [];

        foreach (var (element, expected) in Tests)
        {
            Console.Write($"Testing \"{element}\" (expecting "
                + $"{ArrayToString(expected)})...");
            var actual = ExtractAttributes(element);
            Console.Write($"{ArrayToString(actual)}");
            var success = expected.SequenceEqual(actual);
            Console.WriteLine($" (success: {success}).");

            if (!success)
            {
                failures.Add(element);
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

    static List<(string element, string[] expected)> Tests =>
    [
        ("<span class=\"red\"></span>", ["class, red"]),
        ("<meta charset=\"UTF-8\" />", ["charset, UTF-8"]),
        ("<p>Lorem ipsum dolor sit amet</p>", []),
        ("<input name=\"email\" type=\"email\" required=\"true\" />",
            ["name, email", "type, email", "required, true"]),
        ("<button id=\"submit\" class=\"btn btn-primary\">Submit</button>",
            ["id, submit", "class, btn btn-primary"]),
    ];

    static string ArrayToString<T>(T[] array)
    {
        var encloseWithDoubleQuotes = typeof(T) == typeof(string);
        StringBuilder result = new("[");

        for (var i = 0; i < array.Length; i++)
        {
            if (i > 0)
            {
                result.Append(", ");
            }

            if (encloseWithDoubleQuotes)
            {
                result.Append('\"');
            }

            result.Append(array[i]);

            if (encloseWithDoubleQuotes)
            {
                result.Append('\"');
            }
        }

        return result.Append(']').ToString();
    }

    [GeneratedRegex(@"([\w|data-]+)=[""']?((?:.(?![""']?\s+(?:\S+)=|\s*\/?[>""']))+.)[""']?")]
    private static partial Regex MatchAttributesRegex();
}
