namespace HexToDecimal;
using System;
using System.Collections.Generic;
using System.Numerics;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-11"/>
/// </summary>
internal class Program
{
    static T HexToDecimal<T>(string hex) where T : INumber<T>
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(hex);

        const string digits = "0123456789ABCDEF";
        T sixteen = T.CreateChecked(16);
        T digitPower = T.One;
        T result = T.Zero;

        for (var i = hex.Length - 1; i >= 0; i--)
        {
            var digit = hex[i];
            var digitsIndex = digits.IndexOf(char.ToUpper(digit));

            if (digitsIndex < 0)
            {
                throw new ArgumentException("All characters must be valid "
                    + $"hexidecimal characters, not \'{digit}\' at index {i}.");
            }

            var value = T.CreateChecked(digitsIndex);
            var digitValue = value * digitPower;
            result += digitValue;
            digitPower *= sixteen;
        }

        return result;
    }

    static void Main()
    {
        List<string> failures = [];

        foreach (var (hex, expected) in Tests)
        {
            Console.Write($"Testing \"{hex}\" (expecting {expected})...");
            var actual = HexToDecimal<double>(hex);
            Console.Write(actual);
            var success = expected == actual;
            Console.WriteLine($" (success: {success}).");

            if (!success)
            {
                failures.Add(hex);
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

    static List<(string hex, double expected)> Tests =>
    [
        ("A", 10),
        ("15", 21),
        ("2E", 46),
        ("FF", 255),
        ("A3F", 2623),
    ];
}
