namespace EmailSorter;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-29"/>
/// </summary>
internal class Program
{
    static string[] Sort(string[] emails)
    {
        ArgumentNullException.ThrowIfNull(emails);
        var sortedEmails = new string[emails.Length];
        emails.CopyTo(sortedEmails, 0);
        Array.Sort(sortedEmails, (email1, email2) =>
        {
            var (email1domain, email1username) = ParseEmail(email1);
            var (email2domain, email2username) = ParseEmail(email2);

            var domainComparison = email1domain.CompareTo(email2domain);

            if (domainComparison != 0)
            {
                return domainComparison;
            }

            return email1username.CompareTo(email2username);
        });

        return sortedEmails;

        static (string domain, string username) ParseEmail(string email)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(email);

            var emailParts = email.Split('@');

            if (emailParts.Length != 2)
            {
                throw new ArgumentException(paramName: nameof(email),
                    message: "Invalid email format.");
            }

            return (emailParts[1].ToLowerInvariant(),
                emailParts[0].ToLowerInvariant());
        }
    }

    static void Main()
    {
        List<string[]> failures = [];

        foreach (var (emails, expected) in Tests)
        {
            Console.Write($"Testing {ValueToString(emails)} (expecting "
                + $"{ValueToString(expected)})...");
            var actual = Sort(emails);
            Console.Write(ValueToString(actual));
            var success = expected.SequenceEqual(actual);
            Console.WriteLine($" (success: {success}).");

            if (!success)
            {
                failures.Add(emails);
            }
        }

        if (failures.Count > 0)
        {
            Console.WriteLine("The following inputs failed:");

            foreach (var failure in failures)
            {
                Console.WriteLine($"  {ValueToString(failure)}.");
            }
        }
        else
        {
            Console.WriteLine("All tests passed!");
        }
    }

    static List<(string[] emails, string[] expected)> Tests =>
    [
        (["jill@mail.com", "john@example.com", "jane@example.com"],
            ["jane@example.com", "john@example.com", "jill@mail.com"]),
        (["bob@mail.com", "alice@zoo.com", "carol@mail.com"],
            ["bob@mail.com", "carol@mail.com", "alice@zoo.com"]),
        (["user@z.com", "user@y.com", "user@x.com"],
            ["user@x.com", "user@y.com", "user@z.com"]),
        (["sam@MAIL.com", "amy@mail.COM", "bob@Mail.com"],
            ["amy@mail.COM", "bob@Mail.com", "sam@MAIL.com"]),
        (["simon@beta.com", "sammy@alpha.com", "Sarah@Alpha.com",
            "SAM@ALPHA.com", "Simone@Beta.com", "sara@alpha.com"],
            ["SAM@ALPHA.com", "sammy@alpha.com", "sara@alpha.com",
                "Sarah@Alpha.com", "simon@beta.com", "Simone@Beta.com"]),
    ];

    static string ValueToString(object? value)
    {
        if (value is IList values)
        {
            StringBuilder result = new("[");

            for (var i = 0; i < values.Count; i++)
            {
                if (i > 0)
                {
                    result.Append(", ");
                }

                result.Append(ValueToString(values[i]));
            }

            return result.Append(']').ToString();
        }
        else if (value is string valueString)
        {
            return $"\"{valueString}\"";
        }
        else
        {
            return value?.ToString() ?? "null";
        }
    }
}
