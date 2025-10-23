namespace TipCalculator;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-20"/>
/// </summary>
internal class Program
{
    static string[] CalculateTips(string mealPrice, string customTip)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(mealPrice);

        if (!mealPrice.StartsWith('$')
            || !double.TryParse(mealPrice[1..], NumberStyles.Float,
                CultureInfo.InvariantCulture, out var mealPriceValue))
        {
            throw new ArgumentException(paramName: nameof(mealPrice),
                message: "Must be of the format \"$N.NN\".");
        }

        ArgumentException.ThrowIfNullOrWhiteSpace(customTip);

        if (!customTip.EndsWith('%')
            || !int.TryParse(customTip[..^1], NumberStyles.Integer,
                CultureInfo.InvariantCulture, out var customTipValue))
        {
            throw new ArgumentException(paramName: nameof(mealPrice),
                message: "Must be of the format \"NN%\".");
        }

        return [CalculateTip(mealPriceValue, 15),
            CalculateTip(mealPriceValue, 20),
            CalculateTip(mealPriceValue, customTipValue)];

        static string CalculateTip(double priceValue, int tipValue)
        {
            if (!double.IsFinite(priceValue))
            {
                throw new ArgumentOutOfRangeException(
                    paramName: nameof(priceValue), actualValue: priceValue,
                    message: "Must be finite.");
            }

            ArgumentOutOfRangeException.ThrowIfLessThan(priceValue, 0);
            ArgumentOutOfRangeException.ThrowIfLessThan(tipValue, 0);

            return $"${priceValue * tipValue / 100d:0.00}";
        }
    }

    static void Main()
    {
        List<(string mealPrice, string customTip)> failures = [];

        foreach (var (mealPrice, customTip, expected) in Tests)
        {
            Console.Write($"Testing \"{mealPrice}\" and \"{customTip}\" "
                + $"(expecting {ArrayToString(expected)})...");
            var actual = CalculateTips(mealPrice, customTip);
            Console.Write(ArrayToString(actual));
            var success = expected.SequenceEqual(actual);
            Console.WriteLine($" (success: {success}).");

            if (!success)
            {
                failures.Add((mealPrice, customTip));
            }
        }

        if (failures.Count > 0)
        {
            Console.WriteLine("The following inputs failed:");

            foreach (var (mealPrice, customTip) in failures)
            {
                Console.WriteLine($"  \"{mealPrice}\" and \"{customTip}\".");
            }
        }
        else
        {
            Console.WriteLine("All tests passed!");
        }
    }

    static List<(string mealPrice, string customTip, string[] expected)> Tests
        =>
        [
            ("$10.00", "25%", ["$1.50", "$2.00", "$2.50"]),
            ("$89.67", "26%", ["$13.45", "$17.93", "$23.31"]),
            ("$19.85", "9%", ["$2.98", "$3.97", "$1.79"]),
        ];

    static string ArrayToString(string[] strings)
    {
        StringBuilder result = new("[");

        for (var i = 0; i < strings.Length; i++)
        {
            if (i > 0)
            {
                result.Append(", ");
            }

            result.AppendFormat("\"{0}\"", strings[i]);
        }

        return result.Append(']').ToString();
    }
}
