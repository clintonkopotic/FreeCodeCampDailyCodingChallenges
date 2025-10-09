namespace GoldilocksZone;
using System;
using System.Collections.Generic;
using System.Numerics;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-08"/>
/// </summary>
internal class Program
{
    static (T startOfZone, T endOfZone) GoldilocksZone<T>(T mass)
        where T : IFloatingPointIeee754<T>
    {
        if (!T.IsFinite(mass))
        {
            throw new ArgumentOutOfRangeException(paramName: nameof(mass),
                actualValue: mass, message: "Must be finite.");
        }

        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(mass, T.Zero);
        var luminosityExponent = T.CreateChecked(3.5d);
        var startOfZoneCoefficient = T.CreateChecked(0.95d);
        var endOfZoneCoefficient = T.CreateChecked(1.37d);
        var luminosityOfStar = T.Pow(mass, luminosityExponent);
        var squareRootOfLuminosityOfStar = T.Sqrt(luminosityOfStar);
        var startOfZone = startOfZoneCoefficient * squareRootOfLuminosityOfStar;
        var endOfZone = endOfZoneCoefficient * squareRootOfLuminosityOfStar;

        return (T.Round(startOfZone, 2), T.Round(endOfZone, 2));
    }

    static void Main()
    {
        List<double> failures = [];

        foreach (var (mass, expected) in Tests)
        {
            Console.Write($"Testing {mass} (expecting {expected})...");
            var actual = GoldilocksZone(mass);
            Console.Write(actual);
            var success = expected == actual;
            Console.WriteLine($" (success: {success}).");

            if (!success)
            {
                failures.Add(mass);
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

    static List<(double mass, (double startOfZone, double endOfZone) expected)>
        Tests =>
        [
            (1, (0.95, 1.37)),
            (0.5, (0.28, 0.41)),
            (6, (21.85, 31.51)),
            (3.7, (9.38, 13.52)),
            (20, (179.69, 259.13)),
        ];
}
