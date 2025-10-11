namespace LaunchFuel;
using System;
using System.Collections.Generic;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-10"/>
/// </summary>
internal class Program
{
    static double LaunchFuel(double payload)
    {
        if (!double.IsFinite(payload))
        {
            throw new ArgumentOutOfRangeException(paramName: nameof(payload),
                actualValue: payload, message: "Must be finite.");
        }

        ArgumentOutOfRangeException.ThrowIfNegative(payload);

        // Rockets require 1 kg of fuel per 5 kg of mass they must lift.
        const double fuelInKgPerLiftMassInKg = 1d / 5d;
        var totalPayloadInKg = payload;
        var fuelToLiftInKg = totalPayloadInKg * fuelInKgPerLiftMassInKg;
        var additionalFuelInKg = fuelToLiftInKg;
        var lastFuelToLiftInKg = fuelToLiftInKg;
        var totalFuelInKg = fuelToLiftInKg;
        totalPayloadInKg += fuelToLiftInKg;

        while (additionalFuelInKg >= 1)
        {
            fuelToLiftInKg = totalPayloadInKg * fuelInKgPerLiftMassInKg;
            additionalFuelInKg = double.Abs(lastFuelToLiftInKg
                - fuelToLiftInKg);
            lastFuelToLiftInKg = fuelToLiftInKg;
            totalFuelInKg += additionalFuelInKg;
            totalPayloadInKg += additionalFuelInKg;
        }

        return double.Round(totalFuelInKg, 1);
    }

    static void Main()
    {
        List<double> failures = [];

        foreach (var (payload, expected) in Tests)
        {
            Console.Write($"Testing {payload} (expecting {expected})...");
            var actual = LaunchFuel(payload);
            Console.Write(actual);
            var success = expected == actual;
            Console.WriteLine($" (success: {success}).");

            if (!success)
            {
                failures.Add(payload);
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

    static List<(double payload, double expected)> Tests =>
    [
        (50, 12.4),
        (500, 124.8),
        (243, 60.7),
        (11000, 2749.8),
        (6214, 1553.4),
    ];
}
