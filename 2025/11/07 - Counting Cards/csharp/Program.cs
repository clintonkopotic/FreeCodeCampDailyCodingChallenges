namespace CountingCards;
using System;
using System.Collections.Generic;
using System.Numerics;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-11-07"/>
/// </summary>
internal class Program
{
    static T Combinations<T>(T cards) where T : INumber<T>
    {
        if (!T.IsInteger(cards))
        {
            throw new ArgumentOutOfRangeException(paramName: nameof(cards),
                actualValue: cards, message: "Must be an integer.");
        }

        T cardsInDeck = T.CreateChecked(52);
        ArgumentOutOfRangeException.ThrowIfNegative(cards);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(cards, cardsInDeck);

        if (cards == T.Zero || cards == cardsInDeck)
        {
            return T.One; // Only one way to choose 0 or all items.
        }
        else if (cards > (cardsInDeck / (T.One + T.One)))
        {
            cards = cardsInDeck - cards; // Optimize calculation for symmetry.
        }

        T result = T.One;

        for (T i = T.One; i <= cards; i++)
        {
            result = result * (cardsInDeck - i + T.One) / i;
        }

        return result;
    }

    static void Main()
    {
        List<ulong> failures = [];

        foreach (var (cards, expected) in Tests)
        {
            Console.Write($"Testing {cards} (expecting {expected})...");
            var actual = Combinations(cards);
            Console.Write(actual);
            var success = expected == actual;
            Console.WriteLine($" (success: {success}).");

            if (!success)
            {
                failures.Add(cards);
            }
        }

        if (failures.Count > 0)
        {
            Console.WriteLine("The following inputs failed:");

            foreach (var failure in failures)
            {
                Console.WriteLine($"  {failure}.");
            }
        }
        else
        {
            Console.WriteLine("All tests passed!");
        }
    }

    static List<(ulong cards, ulong expected)> Tests =>
    [
        (52, 1),
        (1, 52),
        (2, 1326),
        (5, 2598960),
        (10, 15820024220),
        (50, 1326),
    ];
}
