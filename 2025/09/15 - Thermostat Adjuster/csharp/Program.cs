namespace ThermostatAdjuster;
using System;
using System.Collections.Generic;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-09-15"/>
/// </summary>
internal class Program
{
    static string AdjustThermostat(double temp, double target)
    {
        if (!double.IsFinite(temp))
        {
            throw new ArgumentException(paramName: nameof(temp),
                message: "Must be finite.");
        }

        if (!double.IsFinite(target))
        {
            throw new ArgumentException(paramName: nameof(target),
                message: "Must be finite.");
        }

        var adjust = "hold";

        if (temp < target)
        {
            adjust = "heat";
        }
        else if (temp > target)
        {
            adjust = "cool";
        }

        return adjust;
    }

    static void Main()
    {
        List<(double temp, double target)> failures = [];

        foreach ((var temp, var target, var expected) in Tests)
        {
            Console.Write($"Testing {temp} & {target} (expecting "
                + $"\"{expected}\")...");
            var actual = AdjustThermostat(temp, target);
            var success = expected == actual;
            Console.WriteLine($"\"{actual}\" (success: {success}).");

            if (!success)
            {
                failures.Add((temp, target));
            }
        }

        if (failures.Count <= 0)
        {
            Console.WriteLine("All tests passed!");
        }
        else
        {
            Console.WriteLine("The following inputs failed:");

            foreach ((var temp, var target) in failures)
            {
                Console.WriteLine($"  {temp} & {target}.");
            }
        }
    }

    static List<(double temp, double target, string expected)> Tests =>
        [
            (68d, 72d, "heat"),
            (75d, 72d, "cool"),
            (72d, 72d, "hold"),
            (-20.5d, -10.1d, "heat"),
            (100d, 99.9d, "cool"),
            (0.0d, 0.0d, "hold"),
        ];
}
