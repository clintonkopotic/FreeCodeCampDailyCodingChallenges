namespace BinaryToDecimal;
using System;
using System.Collections.Generic;
using System.Numerics;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-01"/>
/// </summary>
internal class Program
{
    static T ToDecimal<T>(string binary) where T : INumber<T>
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(binary);

        T two = T.One + T.One;
        T result = T.Zero;

        checked
        {
            T lastPowerOf2Exponent = T.Zero;
            T lastPowerOf2Value = T.One;
            T exponent = T.Zero;

            for (var i = (binary.Length - 1); i >= 0; i--)
            {
                var digit = binary[i];

                if (digit == '1')
                {
                    while (lastPowerOf2Exponent < exponent)
                    {
                        lastPowerOf2Value *= two;
                        lastPowerOf2Exponent++;
                    }

                    result += lastPowerOf2Value;
                }
                else if (digit != '0')
                {
                    throw new ArgumentException(paramName: nameof(binary),
                        message: "Not a binary number, encountered character "
                            + $"\'{digit}\' (index: {i}).");
                }

                exponent++;
            }
        }

        return result;
    }

    static void Main()
    {
        List<string> failures = [];

        foreach (var (binary, expected) in Tests)
        {
            Console.Write($"Testing \"{binary}\" (expecting {expected:N0})...");
            var actual = ToDecimal<double>(binary);
            Console.Write($"{actual:N0}");
            var success = expected == actual;
            Console.WriteLine($" (success: {success}).");

            if (!success)
            {
                failures.Add(binary);
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

    static List<(string binary, double expected)> Tests =>
    [
        ("101", 5),
        ("1010", 10),
        ("10010", 18),
        ("1010101", 85),
    ];
}
