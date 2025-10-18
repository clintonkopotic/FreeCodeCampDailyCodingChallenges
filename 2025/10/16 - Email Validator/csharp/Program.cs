namespace EmailValidator;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-16"/>
/// </summary>
internal partial class Program
{
    static bool Validate(string email)
    {
        ArgumentNullException.ThrowIfNull(email);

        var parts = email.Split('@');

        if (parts.Length != 2)
        {
            return false;
        }

        var localPart = parts[0];

        if (!LocalPartRegex().IsMatch(localPart) || localPart.StartsWith('.')
            || localPart.EndsWith('.') || localPart.Contains(".."))
        {
            return false;
        }

        var domainPart = parts[1];

        if (domainPart.StartsWith('.') || domainPart.EndsWith('.')
            || domainPart.Contains(".."))
        {
            return false;
        }

        var domainDotParts = domainPart.Split('.');

        if (domainDotParts.Length < 2)
        {
            return false;
        }

        foreach (var domainDotPart in domainDotParts)
        {
            if (domainDotPart.Length == 0)
            {
                return false;
            }
        }

        var letterCount = 0;

        foreach (var lastDomainDotPartCharacter in domainDotParts[^1])
        {
            if (!char.IsAsciiLetter(lastDomainDotPartCharacter))
            {
                return false;
            }

            letterCount++;
        }

        return letterCount >= 2;
    }

    static void Main()
    {
        List<string> failures = [];

        foreach (var (email, expected) in Tests)
        {
            Console.Write($"Testing \"{email}\" (expecting {expected})...");
            var actual = Validate(email);
            Console.Write(actual);
            var success = expected == actual;
            Console.WriteLine($" (success: {success}).");

            if (!success)
            {
                failures.Add(email);
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

    static List<(string email, bool expected)> Tests =>
    [
        ("a@b.cd", true),
        ("hell.-w.rld@example.com", true),
        (".b@sh.rc", false),
        ("example@test.c0", false),
        ("freecodecamp.org", false),
        ("develop.ment_user@c0D!NG.R.CKS", true),
        ("hello.@wo.rld", false),
        ("hello@world..com", false),
        ("git@commit@push.io", false),
    ];
    
    [GeneratedRegex(@"^[0-9a-zA-Z._-]+$")]
    private static partial Regex LocalPartRegex();
}
