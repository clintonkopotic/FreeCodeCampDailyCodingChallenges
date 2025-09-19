namespace PhotoStorage;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-09-19"/>
/// </summary>
internal class Program
{
    static int NumberOfPhotos(double photoSizeMb, double hardDriveSizeGb)
    {
        ThrowIfNotFiniteOrNotPositive(photoSizeMb);
        ThrowIfNotFiniteOrNotPositive(hardDriveSizeGb);
        var hardDriveSizeMb = 1_000d * hardDriveSizeGb;
        var numberOfPhotos = Convert.ToInt32(
            double.Floor(hardDriveSizeMb / photoSizeMb));

        return numberOfPhotos;

        static void ThrowIfNotFiniteOrNotPositive(double @double,
            [CallerArgumentExpression(nameof(@double))] string? paramName
                = null)
        {
            if (!double.IsFinite(@double) || @double == 0d
                || !double.IsPositive(@double))
            {
                throw new ArgumentOutOfRangeException(paramName,
                    @double, "Must be finite and positive.");
            }
        }
    }

    static void Main()
    {
        List<(double photoSizeMb, double hardDriveSizeGb)> failures = [];

        foreach ((var photoSizeMb, var hardDriveSizeGb, var expected) in Tests)
        {
            Console.Write($"Testing {photoSizeMb} and {hardDriveSizeGb} "
                + $"(expecting {expected})...");
            var actual = NumberOfPhotos(photoSizeMb, hardDriveSizeGb);
            var success = expected == actual;
            Console.WriteLine($"{actual} (success: {success}).");

            if (!success)
            {
                failures.Add((photoSizeMb, hardDriveSizeGb));
            }
        }

        if (failures.Count > 0)
        {
            Console.WriteLine("The following inputs failed:");

            foreach ((var photoSizeMb, var hardDriveSizeGb) in failures)
            {
                Console.WriteLine($"  {photoSizeMb} and {hardDriveSizeGb}.");
            }
        }
        else
        {
            Console.WriteLine("All tests past.");
        }
    }

    static List<(double photoSizeMb, double hardDriveSizeGb, int expected)>
        Tests =>
        [
            (1, 1, 1_000),
            (2, 1, 500),
            (4, 256, 64_000),
            (3.5, 750, 214_285),
            (3.5, 5.5, 1_571),
        ];
}
