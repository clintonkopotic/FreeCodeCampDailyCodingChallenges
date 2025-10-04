namespace DecimalToBinary;
using System;
using System.Collections.Generic;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-02"/>
/// </summary>
internal class Program
{
    static string ToBinary(double @decimal)
    {
        if (!double.IsInteger(@decimal))
        {
            throw new ArgumentOutOfRangeException(paramName: nameof(@decimal),
                actualValue: @decimal, message: "Must be an integer.");
        }

        ArgumentOutOfRangeException.ThrowIfNegative(@decimal);

        if (@decimal == 0d)
        {
            return "0";
        }

        var value = @decimal;
        List<double> remainders = [];

        while (value > 0d)
        {
            remainders.Add(value % 2d);
            value = double.Floor(value / 2d);
        }

        remainders.Reverse();

        return string.Join(string.Empty, remainders);
    }

    static void Main()
    {
        List<double> failures = [];

        foreach (var (@decimal, expected) in Tests)
        {
            Console.Write($"Testing {@decimal} (expecting \"{expected}\")...");
            var actual = ToBinary(@decimal);
            Console.Write($"\"{actual}\"");
            var success = expected == actual;
            Console.WriteLine($" (success: {success}).");

            if (!success)
            {
                failures.Add(@decimal);
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

    static List<(double @decimal, string expected)> Tests =>
    [
        (5, "101"),
        (12, "1100"),
        (50, "110010"),
        (99, "1100011"),
    ];
}
