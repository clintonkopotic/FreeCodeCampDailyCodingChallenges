namespace NthPrime;
using System;
using System.Collections.Generic;
using System.Numerics;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-30"/>
/// </summary>
internal class Program
{
    static T NthPrime<T>(T n) where T : INumber<T>
    {
        if (!T.IsInteger(n))
        {
            throw new ArgumentOutOfRangeException(paramName: nameof(n),
                actualValue: n, message: "Must be an integer.");
        }

        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(n, T.Zero);
        T prime = T.One + T.One;

        for (T i = T.One; i < n; i++)
        {
            T number = prime + T.One;

            while (!IsPrime(number))
            {
                number++;
            }

            prime = number;
        }

        return prime;

        static bool IsPrime<U>(U n) where U : INumber<U>
        {
            if (!U.IsInteger(n))
            {
                throw new ArgumentOutOfRangeException(paramName: nameof(n),
                    actualValue: n,
                    message: "Must be an integer.");
            }

            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(n, U.One);
            U two = U.One + U.One;

            // 2 is the only even prime number
            if (n == two)
            {
                return true;
            }

            // Even numbers greater than 2 are not prime
            if (n % two == U.Zero)
            {
                return false;
            }

            // Check for divisibility by odd numbers from 3 up to half of n.
            // We only need to check up to half because if a number has a
            // divisor greater than half, it must also have a divisor smaller
            // than half.
            U limit = n / two;

            for (U i = (two + U.One); i < limit; i += two)
            {
                // If divisible, it's not prime.
                if (n % i == U.Zero)
                {
                    return false;
                }
            }

            // If no divisors were found, it's prime.
            return true;
        }
    }

    static void Main()
    {
        List<double> failures = [];

        foreach (var (n, expected) in Tests)
        {
            Console.Write($"Testing {n} (expecting {expected})...");
            var actual = NthPrime(n);
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

    static List<(double n, double expected)> Tests =>
    [
        (5, 11),
        (10, 29),
        (16, 53),
        (99, 523),
        (1000, 7919),
    ];
}
