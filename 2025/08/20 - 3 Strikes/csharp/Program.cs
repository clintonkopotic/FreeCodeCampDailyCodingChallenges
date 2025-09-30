namespace _3Strikes;
using System;
using System.Collections.Generic;

internal class Program
{
    static int SquaresWithThree(int n)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(n, 1);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(n, 10_000);

        var result = 0;

        for (var i = 1; i < n; i++)
        {
            var squareOfI = i * i;

            if (squareOfI.ToString().Contains('3'))
            {
                result++;
            }
        }

        return result;
    }

    static void Main()
    {
        List<int> failures = [];

        foreach (var (n, expected) in Tests)
        {
            Console.Write($"Testing {n} (expecting {expected})...");
            var actual = SquaresWithThree(n);
            Console.Write(actual);
            var success = expected == actual;
            Console.WriteLine($" (success: {success}).");

            if (!success)
            {
                failures.Add(n);
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

    static List<(int n, int expected)> Tests =>
    [
        (1, 0),
        (10, 1),
        (100, 19),
        (1_000, 326),
        (10_000, 4531),
    ];
}
