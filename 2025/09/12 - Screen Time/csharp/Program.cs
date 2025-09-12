namespace ScreenTime;
using System;
using System.Collections.Generic;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-09-12"/>
/// </summary>
internal class Program
{
    static bool TooMuchScreenTime(int[] hours)
    {
        ArgumentNullException.ThrowIfNull(hours);

        if (hours.Length != 7)
        {
            throw new ArgumentException(paramName: nameof(hours),
                message: $"The {nameof(Array.Length)} must be 7.");
        }

        var sumOf7 = 0;
        var sumOf02 = 0;
        var sumOf13 = 0;
        var sumOf24 = 0;
        var sumOf35 = 0;
        var sumOf46 = 0;

        for (var i = 0; i < hours.Length; i++)
        {
            var dayHours = hours[i];

            if (dayHours >= 10)
            {
                return true;
            }

            sumOf7 += dayHours;

            if (i >= 0 && i <= 2)
            {
                sumOf02 += dayHours;

                if (i == 2 && ((sumOf02 / 3d) >= 8d))
                {
                    return true;
                }
            }

            if (i >= 1 && i <= 3)
            {
                sumOf13 += dayHours;

                if (i == 3 && ((sumOf13 / 3d) >= 8d))
                {
                    return true;
                }
            }

            if (i >= 2 && i <= 4)
            {
                sumOf24 += dayHours;

                if (i == 4 && ((sumOf24 / 3d) >= 8d))
                {
                    return true;
                }
            }

            if (i >= 3 && i <= 5)
            {
                sumOf35 += dayHours;

                if (i == 5 && ((sumOf35 / 3d) >= 8d))
                {
                    return true;
                }
            }

            if (i >= 4 && i <= 6)
            {
                sumOf46 += dayHours;

                if (i == 6 && ((sumOf46 / 3d) >= 8d))
                {
                    return true;
                }
            }
        }

        return (sumOf7 / 7d) >= 6d;
    }

    static void Main()
    {
        List<int[]> failures = [];

        foreach ((var hours, var expected) in Tests)
        {
            Console.Write($"Testing {ArrayToString(hours)} (expecting "
                + $"{expected})...");
            var actual = TooMuchScreenTime(hours);
            var success = expected == actual;
            Console.WriteLine($"{actual} (success: {success}).");

            if (!success)
            {
                failures.Add(hours);
            }
        }

        if (failures.Count <= 0)
        {
            Console.WriteLine("All tests passed!");
        }
        else
        {
            Console.WriteLine("The following inputs failed:");

            foreach (var failure in failures)
            {
                Console.WriteLine($"  {ArrayToString(failure)}.");
            }
        }
    }

    static List<(int[], bool)> Tests =>
    [
        ([1, 2, 3, 4, 5, 6, 7], false),
        ([7, 8, 8, 4, 2, 2, 3], false),
        ([5, 6, 6, 6, 6, 6, 6], false),
        ([1, 2, 3, 11, 1, 3, 4], true),
        ([1, 2, 3, 10, 2, 1, 0], true),
        ([3, 3, 5, 8, 8, 9, 4], true),
        ([3, 9, 4, 8, 5, 7, 6], true),
    ];

    static string ArrayToString(int[] hours) => $"[{string.Join(", ", hours)}]";
}
