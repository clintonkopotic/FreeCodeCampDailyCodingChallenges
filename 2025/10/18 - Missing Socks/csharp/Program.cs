namespace MissingSocks;
using System;
using System.Collections.Generic;
using System.Numerics;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-18"/>
/// </summary>
internal class Program
{
    static T SockPairs<T>(T pairs, T cycles) where T : IFloatingPointIeee754<T>
    {
        if (!T.IsInteger(pairs))
        {
            throw new ArgumentOutOfRangeException(paramName: nameof(pairs),
                actualValue: pairs, message: "Must be an integer.");
        }

        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(pairs, T.Zero);

        if (!T.IsInteger(cycles))
        {
            throw new ArgumentOutOfRangeException(paramName: nameof(cycles),
                actualValue: cycles, message: "Must be an integer.");
        }

        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(cycles, T.Zero);

        var two = T.One + T.One;
        var three = two + T.One;
        var five = three + two;
        var ten = two * five;
        var numberOfSocks = pairs * two;

        for (var cycle = T.One; cycle <= cycles; cycle++)
        {
            // Every 2 wash cycles, you lose a single sock.
            if (cycle % two == T.Zero)
            {
                numberOfSocks--;
            }

            // Every 3 wash cycles, you find a single missing sock.
            if (cycle % three == T.Zero)
            {
                numberOfSocks++;
            }

            // Every 5 wash cycles, a single sock is worn out and must be thrown
            // away.
            if (cycle % five == T.Zero)
            {
                numberOfSocks--;
            }

            // Every 10 wash cycles, you buy a pair of socks.
            if (cycle % ten == T.Zero)
            {
                numberOfSocks += two;
            }

            // You can never have less than zero total socks.
            if (numberOfSocks < T.Zero)
            {
                numberOfSocks = T.Zero;
            }
        }

        return T.Floor(numberOfSocks / two);
    }

    static void Main()
    {
        List<(double pairs, double cycles)> failures = [];

        foreach (var (pairs, cycles, expected) in Tests)
        {
            Console.Write($"Testing {pairs} and {cycles} (expecting "
                + $"{expected})...");
            var actual = SockPairs(pairs, cycles);
            Console.Write(actual);
            var success = expected == actual;
            Console.WriteLine($" (success: {success}).");

            if (!success)
            {
                failures.Add((pairs, cycles));
            }
        }

        if (failures.Count > 0)
        {
            Console.WriteLine("The following inputs failed:");

            foreach (var (pairs, cycles) in failures)
            {
                Console.WriteLine($"  {pairs} and {cycles}.");
            }
        }
    }

    static List<(double pairs, double cycles, double expected)> Tests =
    [
        (2, 5, 1),
        (1, 2, 0),
        (5, 11, 4),
        (6, 25, 3),
        (1, 8, 0),
    ];
}
