namespace SignatureValidation;
using System;
using System.Collections.Generic;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-11-01"/>
/// </summary>
internal class Program
{
    static bool Verify(string message, string key, int signature)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(key);
        ArgumentOutOfRangeException.ThrowIfNegative(signature);

        var calculatedSignature = CalculateSignature(message)
            + CalculateSignature(key);

        return signature == calculatedSignature;

        static int CalculateSignature(string @string)
        {
            ArgumentNullException.ThrowIfNull(@string);

            const string letters
                = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var signature = 0;

            foreach (var character in @string)
            {
                var index = letters.IndexOf(character);

                if (index >= 0)
                {
                    signature += index + 1;
                }
            }

            return signature;
        }
    }

    static void Main()
    {
        List<(string message, string key, int signature)> failrues = [];

        foreach (var (message, key, signature, expected) in Tests)
        {
            Console.Write($"Testing \"{message}\", \"{key}\", and {signature} "
                + $"(expecting {expected})...");
            var actual = Verify(message, key, signature);
            Console.Write(actual);
            var success = expected == actual;
            Console.WriteLine($" (success: {success}).");

            if (!success)
            {
                failrues.Add((message, key, signature));
            }
        }

        if (failrues.Count > 0)
        {
            Console.WriteLine("The following inputs failed:");

            foreach (var (message, key, signature) in failrues)
            {
                Console.WriteLine($"  \"{message}\", \"{key}\" and "
                    + $"{signature}.");
            }
        }
        else
        {
            Console.WriteLine("All tests passed!");
        }
    }

    static List<(string message, string key, int signature, bool expected)>
        Tests =>
        [
            ("foo", "bar", 57, true),
            ("foo", "bar", 54, false),
            ("freeCodeCamp", "Rocks", 238, true),
            ("Is this valid?", "No", 210, false),
            ("Is this valid?", "Yes", 233, true),
            ("Check out the freeCodeCamp podcast,", "in the mobile app", 514,
                true),
        ];
}
