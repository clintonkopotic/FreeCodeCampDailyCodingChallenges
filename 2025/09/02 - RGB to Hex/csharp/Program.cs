namespace RgbToHex;
using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-09-02"/>
/// </summary>
internal class Program
{
    static string RgbToHex(string rgb)
    {
        ArgumentNullException.ThrowIfNull(rgb);

        var prefix = "rgb(";
        var suffix = ")";
        var delimiter = ',';

        if (string.IsNullOrWhiteSpace(rgb) || !rgb.StartsWith(prefix)
            || !rgb.EndsWith(suffix))
        {
            ThrowUnexpectedFormat(nameof(rgb));
        }

        var valueStrings = rgb[prefix.Length..(rgb.Length - suffix.Length)]
            .Split(delimiter, StringSplitOptions.TrimEntries);

        if (valueStrings.Length != 3)
        {
            ThrowUnexpectedFormat(nameof(rgb));
        }

        StringBuilder result = new("#");

        foreach (var valueString in valueStrings)
        {
            if (!byte.TryParse(valueString, out var value))
            {
                ThrowUnexpectedFormat(nameof(rgb));
            }

            result.AppendFormat("{0:x2}", value);
        }

        return result.ToString();

        static void ThrowUnexpectedFormat(string? paramName = null)
        {
            throw new ArgumentException(paramName: paramName,
                message: "Unexpected format.");
        }
    }

    static void Main()
    {
        List<string> failures = [];

        foreach ((var rgb, var expected) in Tests)
        {
            Console.Write($"Testing \"{rgb}\" (expecting \"{expected}\")...");
            var actual = RgbToHex(rgb);
            var success = expected == actual;
            Console.WriteLine($"{actual} (success: {success}).");

            if (!success)
            {
                failures.Add(rgb);
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
    }

    static List<(string rgb, string expected)> Tests =>
    [
        ("rgb(255, 255, 255)", "#ffffff"),
        ("rgb(1, 11, 111)", "#010b6f"),
        ("rgb(173, 216, 230)", "#add8e6"),
        ("rgb(79, 123, 201)", "#4f7bc9"),
    ];
}
