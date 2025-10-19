namespace CreditCardMasker;
using System;
using System.Collections.Generic;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-17"/>
/// </summary>
internal class Program
{
    static string Mask(string card)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(card);

        if (card.Length != 19)
        {
            throw new ArgumentOutOfRangeException(paramName: nameof(card),
                actualValue: card.Length, message: "The "
                + $"{nameof(string.Length)} must be exactly 19 characters.");
        }

        // 0         1   
        // 0123-5678-0123-5678
        return $"****{card[4]}****{card[9]}****{card[14..]}";
    }

    static void Main()
    {
        List<string> failures = [];

        foreach (var (card, expected) in Tests)
        {
            Console.Write($"Testing \"{card}\" (expecting \"{expected}\")...");
            var actual = Mask(card);
            Console.Write($"\"{actual}\"");
            var success = expected  == actual;
            Console.WriteLine($" (success: {success}).");

            if (!success)
            {
                failures.Add(card);
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

    static List<(string card, string expected)> Tests =>
    [
        ("4012-8888-8888-1881", "****-****-****-1881"),
        ("5105 1051 0510 5100", "**** **** **** 5100"),
        ("6011 1111 1111 1117", "**** **** **** 1117"),
        ("2223-0000-4845-0010", "****-****-****-0010"),
    ];
}
