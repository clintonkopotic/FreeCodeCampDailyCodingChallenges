namespace UnnaturalPrime;
using System;
using System.Collections.Generic;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-08-23"/>
/// </summary>
internal class Program
{
    static bool IsUnnaturalPrime(int n)
    {
        var absN = int.Abs(n);

        // Numbers less than or equal to 1 are not prime
        if (absN <= 1)
        {
            return false;
        }

        // 2 is the only even prime number
        if (absN == 2)
        {
            return true;
        }

        // Even numbers greater than 2 are not prime
        if (absN % 2 == 0)
        {
            return false;
        }

        // Check for divisibility by odd numbers from 3 up to the square root
        // of num. We only need to check up to the square root because if a
        // number has a divisor greater than its square root, it must also have
        // a divisor smaller than its square root.
        var limit = Convert.ToInt32(double.Ceiling(double.Sqrt(absN)));

        for (var i = 3; i <= limit; i += 2)
        {
            if (absN % i == 0)
            {
                return false; // If divisible, it's not prime
            }
        }

        return true; // If no divisors were found, it's prime
    }

    static void Main()
    {
        List<int> failures = [];

        foreach (var (n, expected) in Tests)
        {
            Console.Write($"Testing {n:N0} (expecting {expected})...");
            var actual = IsUnnaturalPrime(n);
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
                Console.WriteLine($"  {failure:N0}.");
            }
        }
        else
        {
            Console.WriteLine("All tests passed!");
        }
    }

    static List<(int n, bool expected)> Tests =>
    [
        (1, false),
        (-1, false),
        (19, true),
        (-23, true),
        (0, false),
        (97, true),
        (-61, true),
        (99, false),
        (-44, false),
    ];
}
