namespace P_ssw0rdStr3ngth_;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-03"/>
/// </summary>
internal partial class Program
{
    static string CheckStrength(string password)
    {
        ArgumentNullException.ThrowIfNull(password);
        var rulesMeet = 0;

        // Rule 1. At least 8 characters long.
        if (password.Length >= 8)
        {
            rulesMeet++;
        }

        // Rule 2. Contains both uppercase and lowercase letters.
        if (HasUpperCaseCharacter().IsMatch(password)
            && HasLowerCaseCharacter().IsMatch(password))
        {
            rulesMeet++;
        }

        // Rule 3. Contains at least one number.
        if (HasNumberCharacter().IsMatch(password))
        {
            rulesMeet++;
        }

        // Rule 4. Contains at least one special character from this set:
        // !, @, #, $, %, ^, &, or *.
        if (HasSpecialCharacter().IsMatch(password))
        {
            rulesMeet++;
        }

        return rulesMeet switch
        {
            0 or 1 => "weak",
            2 or 3 => "medium",
            4 => "strong",
            _ => throw new InvalidOperationException(),
        };
    }

    static void Main()
    {
        List<string> failures = [];

        foreach (var (password, expected) in Tests)
        {
            Console.Write($"Testing \"{password}\" (expecting "
                + $"\"{expected}\")...");
            var actual = CheckStrength(password);
            Console.Write($"\"{actual}\"");
            var success = expected == actual;
            Console.WriteLine($" (success: {success}).");

            if (!success)
            {
                failures.Add(password);
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

    static List<(string password, string expected)> Tests =>
    [
        ("123456", "weak"),
        ("pass!!!", "weak"),
        ("Qwerty", "weak"),
        ("PASSWORD", "weak"),
        ("PASSWORD!", "medium"),
        ("PassWord%^!", "medium"),
        ("qwerty12345", "medium"),
        ("PASSWORD!", "medium"),
        ("PASSWORD!", "medium"),
        ("S3cur3P@ssw0rd", "strong"),
        ("C0d3&Fun!", "strong"),
    ];

    [GeneratedRegex(@"[A-Z]")]
    private static partial Regex HasUpperCaseCharacter();

    [GeneratedRegex(@"[a-z]")]
    private static partial Regex HasLowerCaseCharacter();

    [GeneratedRegex(@"[0-9]")]
    private static partial Regex HasNumberCharacter();

    [GeneratedRegex(@"[!@#$%^&*]")]
    private static partial Regex HasSpecialCharacter();
}
