namespace Candlelight;
using System;
using System.Collections.Generic;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-08-29"/>
/// </summary>
internal class Program
{
    static int BurnCandles(int candles, int leftoversNeeded)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(candles);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(leftoversNeeded);

        // Burn through the candles to get the first batch of leftovers.
        var candlesBurnt = candles;
        var leftovers = candles;

        // Continue to recycle and burn until there isn'int enough to recycle
        // more.
        while (leftovers >= leftoversNeeded)
        {
            // Recycle the leftovers into new candles by getting the quotient of
            // dividing the leftovers by the leftoversNeeded for each new
            // candle.
            var newCandles = leftovers / leftoversNeeded;
            
            // Keep track of how many leftovers that weren't able to be
            // recycled, or in otherwords, getting the remainder of dividing the
            // leftovers by the leftoversNeeded for each new candle.
            leftovers %= leftoversNeeded;

            // Burn through the new candles.
            candlesBurnt += newCandles;

            // Update the leftovers with the new candles burnt for the next
            // cycle.
            leftovers += newCandles;
        }

        return candlesBurnt;
    }

    static void Main()
    {
        List<(int candles, int leftoversNeeded)> failures = [];

        foreach (var (candles, leftoversNeeded, expected) in Tests)
        {
            Console.Write($"Testing {candles} and {leftoversNeeded} (expecting "
                + $"{expected})...");
            var actual = BurnCandles(candles, leftoversNeeded);
            Console.Write($"{actual}");
            var success = expected == actual;
            Console.WriteLine($" (success: {success}).");

            if (!success)
            {
                failures.Add((candles, leftoversNeeded));
            }
        }

        if (failures.Count > 0)
        {
            Console.WriteLine("The following inputs failed:");

            foreach (var (candles, leftoversNeeded) in failures)
            {
                Console.WriteLine($"  {candles} and {leftoversNeeded}.");
            }
        }
        else
        {
            Console.WriteLine("All tests passed!");
        }
    }

    static List<(int candles, int leftoversNeeded, int expected)> Tests =>
    [
        (7, 2, 13),
        (10, 5, 12),
        (20, 3, 29),
        (17, 4, 22),
        (2345, 3, 3517),
    ];
}
