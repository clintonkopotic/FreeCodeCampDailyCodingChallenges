namespace UnorderOfperations;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-08-27"/>
/// </summary>
internal class Program
{
    static T Evaluate<T>(T[] numbers, string[] operators) where T : INumber<T>
    {
        ArgumentNullException.ThrowIfNull(numbers);

        if (numbers.Length < 2)
        {
            throw new ArgumentException(paramName: nameof(numbers),
                message: "Must have a length of 2 or more.");
        }

        if (!T.IsFinite(numbers[0]))
        {
            throw new ArgumentOutOfRangeException(paramName: nameof(numbers),
                actualValue: numbers[0],
                message: "Each element must be finite.");
        }

        ArgumentNullException.ThrowIfNull(operators);

        if (operators.Length <= 0)
        {
            throw new ArgumentException(paramName: nameof(operators),
                message: "Must have a length of 1 or more.");
        }

        T result = numbers[0];

        for (int i = 1, j = 0; i < numbers.Length;
            i++, j = (i - 1) % operators.Length)
        {
            var number = numbers[i];

            if (!T.IsFinite(number))
            {
                throw new ArgumentOutOfRangeException(
                    paramName: nameof(numbers),
                    actualValue: number,
                    message: "Each element must be finite.");
            }

            var @operator = operators[j];

            if (@operator == "+")
            {
                result += number;
            }
            else if (@operator == "-")
            {
                result -= number;
            }
            else if (@operator == "*")
            {
                result *= number;
            }
            else if (@operator == "/")
            {
                result /= number;
            }
            else if (@operator == "%")
            {
                result %= number;
            }
            else
            {
                throw new ArgumentOutOfRangeException(
                    paramName: nameof(operators),
                    actualValue: @operator,
                    message: $"Invalid operator at index {j}.");
            }
        }

        return result;
    }

    static void Main()
    {
        List<(double[] numbers, string[] operators)> failures = [];

        foreach (var (numbers, operators, expected) in Tests)
        {
            Console.Write($"Testing {ArrayToString(numbers)} and "
                + $"{ArrayToString(operators)} (expecting {expected})...");
            var actual = Evaluate(numbers, operators);
            Console.Write(actual);
            var success = expected == actual;
            Console.WriteLine($" (success: {success}).");

            if (!success)
            {
                failures.Add((numbers, operators));
            }
        }

        if (failures.Count > 0)
        {
            Console.WriteLine("The following inputs failed:");

            foreach (var (numbers, operators) in failures)
            {
                Console.WriteLine($" {ArrayToString(numbers)} and "
                    + $"{ArrayToString(operators)}.");
            }
        }
        else
        {
            Console.WriteLine("All tests passed!");
        }
    }

    static List<(double[] numbers, string[] operators, double expected)> Tests =>
    [
        ([5, 6, 7, 8, 9], ["+", "-"], 3),
        ([17, 61, 40, 24, 38, 14], ["+", "%"], 38),
        ([20, 2, 4, 24, 12, 3], ["*", "/"], 60),
        ([11, 4, 10, 17, 2], ["*", "*", "%"], 30),
        ([33, 11, 29, 13], ["/", "-"], -2),
    ];

    static string ArrayToString(string[] strings)
    {
        ArgumentNullException.ThrowIfNull(strings);

        StringBuilder stringBuilder = new("[");

        for (var i = 0; i < strings.Length; i++)
        {
            if (i > 0)
            {
                stringBuilder.Append(", ");
            }

            stringBuilder.Append($"\"{strings[i]}\"");
        }

        return stringBuilder.Append(']').ToString();
    }

    static string ArrayToString<T>(T[] t) => $"[{string.Join(", ", t)}]";
}
