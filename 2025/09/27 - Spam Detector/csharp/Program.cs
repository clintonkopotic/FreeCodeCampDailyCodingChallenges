namespace SpamDetector;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-09-27"/>
/// </summary>
internal class Program
{
    static bool IsSpam(string number)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(number);

        // Parse number into country code, area code, prefix, and suffix.
        var pattern = @"^\+(?<countryCode>\d+)\s\((?<areaCode>\d{3})\)\s(?<prefix>\d{3})\-(?<suffix>\d{4})$";
        var match = Regex.Match(number, pattern);

        if (match is null || !match.Success)
        {
            throw new ArgumentException(paramName: nameof(number),
                message: "Invalid phone number format.");
        }

        var countryCodeGroup = match.Groups["countryCode"];

        if (countryCodeGroup is null || !countryCodeGroup.Success)
        {
            throw new ArgumentException(paramName: nameof(number),
                message: "Failed to parse the country code.");
        }

        var countryCodeString = countryCodeGroup.Value;
        _ = int.Parse(countryCodeString);

        // The country code is greater than 2 digits long or doesn't begin with
        // a zero (0).
        if (countryCodeString.Length > 2 || countryCodeString[0] != '0')
        {
            return true;
        }

        var areaCodeGroup = match.Groups["areaCode"];

        if (areaCodeGroup is null || !areaCodeGroup.Success)
        {
            throw new ArgumentException(paramName: nameof(number),
                message: "Failed to parse the area code.");
        }

        var areaCodeString = areaCodeGroup.Value;
        var areaCodeNumber = int.Parse(areaCodeString);

        // The area code is greater than 900 or less than 200.
        if (areaCodeNumber > 900 || areaCodeNumber < 200)
        {
            return true;
        }

        var prefixGroup = match.Groups["prefix"];

        if (prefixGroup is null || !prefixGroup.Success)
        {
            throw new ArgumentException(paramName: nameof(number),
                message: "Failed to parse the prefix.");
        }

        var prefixString = prefixGroup.Value;
        _ = int.Parse(prefixString);
        var sumOfPrefixDigits = 0;

        foreach (var character in prefixString)
        {
            if (char.IsDigit(character))
            {
                sumOfPrefixDigits += character - '0';
            }
        }

        var suffixGroup = match.Groups["suffix"];

        if (suffixGroup is null || !suffixGroup.Success)
        {
            throw new ArgumentException(paramName: nameof(number),
                message: "Failed to parse the suffix.");
        }

        var suffixString = suffixGroup.Value;
        _ = int.Parse(suffixString);

        // The sum of first three digits of the local number appears within last
        // four digits of the local number.
        if (suffixString.Contains(sumOfPrefixDigits.ToString()))
        {
            return true;
        }

        var numberNoFormattingChars = $"{countryCodeString}{areaCodeString}"
            + $"{prefixString}{suffixString}";

        if (numberNoFormattingChars.Length < 4)
        {
            throw new ArgumentException(paramName: nameof(number),
                message: "Invalid phone number format.");
        }

        // The number has the same digit four or more times in a row (ignoring
        // the formatting characters).
        for (var i = 3; i < numberNoFormattingChars.Length; i++)
        {
            if (numberNoFormattingChars[i - 3] == numberNoFormattingChars[i]
                && numberNoFormattingChars[i - 2] == numberNoFormattingChars[i]
                && numberNoFormattingChars[i - 1] == numberNoFormattingChars[i])
            {
                return true;
            }
        }


        return false;
    }

    static void Main()
    {
        List<string> failures = [];

        foreach (var (number, expected) in Tests)
        {
            Console.Write($"Testing \"{number}\" (expecting {expected})...");
            var actual = IsSpam(number);
            Console.Write(actual);
            var success = expected == actual;
            Console.WriteLine($" (success: {success}).");

            if (!success)
            {
                failures.Add(number);
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

    static List<(string number, bool expected)> Tests =>
    [
        ("+0 (200) 234-0182", false),
        ("+091 (555) 309-1922", true),
        ("+1 (555) 435-4792", true),
        ("+0 (955) 234-4364", true),
        ("+0 (155) 131-6943", true),
        ("+0 (555) 135-0192", true),
        ("+0 (555) 564-1987", true),
        ("+00 (555) 234-0182", false),
    ];
}
