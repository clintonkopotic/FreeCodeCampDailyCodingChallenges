namespace ReverseParenthesis;
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-08-26"/>
/// </summary>
internal class Program
{
    static string Decode(string s)
    {
        ArgumentNullException.ThrowIfNull(s);
        var result = s;
        var lastOpenIndex = -1;
        var i = 0;

        while (i < result.Length && result.Contains('('))
        {
            var character = result[i];

            if (character == ')')
            {
                if (lastOpenIndex < 0 || lastOpenIndex >= result.Length
                    || lastOpenIndex >= i)
                {
                    throw new InvalidOperationException(
                        $"{nameof(lastOpenIndex)} has the unexpected value of "
                        + $"{lastOpenIndex}.");
                }

                var before = result[..lastOpenIndex];
                var decoded = new string(
                    [.. result[(lastOpenIndex + 1)..i].Reverse()]);
                var after = result[(i + 1)..];

                result = before + decoded + after;
                lastOpenIndex = -1;
                i = 0;

                continue;
            }
            else if (character == '(')
            {
                lastOpenIndex = i;
            }

            i++;
        }

        return result;
    }

    static void Main()
    {
        List<string> failures = [];

        foreach (var (s, expected) in Tests)
        {
            Console.Write($"Testing \"{s}\" (expecting \"{expected}\")...");
            var actual = Decode(s);
            Console.Write($"\"{actual}\"");
            var success = expected == actual;
            Console.WriteLine($" (success: {success}).");
            
            if (!success)
            {
                failures.Add(s);
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

    static List<(string s, string expected)> Tests =>
    [
        ("(f(b(dc)e)a)", "abcdef"),
        ("((is?)(a(t d)h)e(n y( uo)r)aC)", "Can you read this?"),
        ("f(Ce(re))o((e(aC)m)d)p", "freeCodeCamp"),
    ];
}
