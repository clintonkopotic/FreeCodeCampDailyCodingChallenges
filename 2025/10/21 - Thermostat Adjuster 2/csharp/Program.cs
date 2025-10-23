namespace ThermostatAdjuster2;
using System;
using System.Collections.Generic;
using System.Numerics;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-21"/>
/// </summary>
internal class Program
{
    static string AdjustThermostat<T>(T currentF, T targetC)
        where T : IFloatingPointIeee754<T>
    {
        if (!T.IsFinite(currentF))
        {
            throw new ArgumentOutOfRangeException(paramName: nameof(currentF),
                actualValue: currentF, message: "Must be finite.");
        }

        if (!T.IsFinite(targetC))
        {
            throw new ArgumentOutOfRangeException(paramName: nameof(targetC),
                actualValue: targetC, message: "Must be finite.");
        }

        T targetF = targetC * T.CreateChecked(1.8) + T.CreateChecked(32);
        T differenceF = T.Round(currentF - targetF, 1);

        if (differenceF == T.Zero)
        {
            return "Hold";
        }
        else if (differenceF < T.Zero)
        {
            return $"Heat: {T.Abs(differenceF):0.0} degrees Fahrenheit";
        }
        else if (differenceF > T.Zero)
        {
            return $"Cool: {differenceF:0.0} degrees Fahrenheit";
        }

        throw new InvalidOperationException("An unexpected value for "
            + $"{nameof(differenceF)} with {differenceF} occurred.");
    }

    static void Main()
    {
        List<(double currentF, double targetC)> failures = [];

        foreach (var (currentF, targetC, expected) in Tests)
        {
            Console.Write($"Testing {currentF} and {targetC} (expecting "
                + $"\"{expected}\")...");
            var actual = AdjustThermostat(currentF, targetC);
            Console.Write($"\"{actual}\"");
            var success = expected == actual;
            Console.WriteLine($" (success: {success}).");

            if (!success)
            {
                failures.Add((currentF, targetC));
            }
        }

        if (failures.Count > 0)
        {
            Console.WriteLine("The following inputs failed:");

            foreach (var (currentF, targetC) in failures)
            {
                Console.WriteLine($"  {currentF} and {targetC}.");
            }
        }
        else
        {
            Console.WriteLine("All tests passed!");
        }
    }

    static List<(double currentF, double targetC, string expected)> Tests =>
    [
        (32, 0, "Hold"),
        (70, 25, "Heat: 7.0 degrees Fahrenheit"),
        (72, 18, "Cool: 7.6 degrees Fahrenheit"),
        (212, 100, "Hold"),
        (59, 22, "Heat: 12.6 degrees Fahrenheit"),
    ];
}
