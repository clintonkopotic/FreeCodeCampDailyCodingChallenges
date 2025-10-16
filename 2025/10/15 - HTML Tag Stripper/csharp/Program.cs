namespace HtmlTagStripper;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-15"/>
/// </summary>
internal partial class Program
{
    static string StripTags(string html)
    {
        ArgumentNullException.ThrowIfNull(html);

        return StripTagsRegex().Replace(html, string.Empty);
    }

    static void Main()
    {
        List<string> failures = [];

        foreach (var (html, expected) in Tests)
        {
            Console.Write($"Testing \"{html}\" (expecting \"{expected}\")...");
            var actual = StripTags(html);
            Console.Write($"\"{actual}\"");
            var success = expected == actual;
            Console.WriteLine($" (success: {success}).");

            if (!success)
            {
                failures.Add(html);
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

    static List<(string html, string expected)> Tests =>
    [
        ("<a href=\"#\">Click here</a>", "Click here"),
        ("<p class=\"center\">Hello <b>World</b>!</p>", "Hello World!"),
        ("<img src=\"cat.jpg\" alt=\"Cat\">", ""),
        ("<main id=\"main\"><section class=\"section\">section</section>"
            + "<section class=\"section\">section</section></main>",
            "sectionsection"),
    ];

    [GeneratedRegex(@"<[^>]+>")]
    private static partial Regex StripTagsRegex();
}
