namespace RomanNumeralParser;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-09-07"/>
/// </summary>
/// <remarks>Since the largest numeral in standard form is 3,999 (see
/// <see href="https://en.wikipedia.org/wiki/Roman_numerals#Standard_form">
/// Wikipedia link</see>), the <see cref="Main()"/> method counts from 1 to
/// 3,999 and converts the decimal number to a numeral with the
/// <see cref="ConvertToRomanNumeral(ushort)"/> method  and then parses it with
/// the <see cref="ParseRomanNumeral(string)"/> method (my solution to the
/// challenge).</remarks>
internal class Program
{
    public static ushort ParseRomanNumeral(string numeral)
    {
        ArgumentNullException.ThrowIfNull(numeral);
        ushort result = 0, lastValue = 0;

        for (var i = 0; i < numeral.Length; i++)
        {
            var letter = numeral[i];
            ushort value = letter switch
            {
                'I' => 1,
                'V' => 5,
                'X' => 10,
                'L' => 50,
                'C' => 100,
                'D' => 500,
                'M' => 1_000,
                _ => throw new ArgumentException(paramName: nameof(numeral),
                    message: $"The character \'{letter}\' is unexpected"),
            };

            if (i > 0)
            {
                if (lastValue < value)
                {
                    result -= lastValue;
                }
                else
                {
                    result += lastValue;
                }
            }

            lastValue = value;
        }

        result += lastValue;

        return result;
    }
    
    public static void Main()
    {
        List<string> failures = [];

        foreach ((var numeral, var expected) in Tests)
        {
            Console.Write($"Testing \"{numeral:N0}\" (expecting "
                + $"{expected:N0})...");
            var result = ParseRomanNumeral(numeral);
            var success = expected == result;
            Console.WriteLine($"{result:N0} (Success: {success}).");

            if (!success)
            {
                failures.Add(numeral);
            }
        }

        Console.WriteLine();

        if (failures.Count > 0)
        {
            Console.WriteLine("The following numerals were not parsed "
                + $"correctly: {string.Join(", ", failures)}.");

            return;
        }


        Console.WriteLine("All numerals were parsed correctly.");
        Console.WriteLine();
        ushort minNumeralNumber = 1;
        ushort maxNumeralNumber = 3_999;
        Console.WriteLine($"Counting from {minNumeralNumber:N0} to "
            + $"{maxNumeralNumber:N0}:");
        failures.Clear();
        var longestNumeral = string.Empty;
        ushort longestAsDecimal = 0;
        Console.WriteLine($"{"Numeral",15} | {"Parsed",6} "
            + $"| {"Expected",8} | {"Correct",7}");
        Console.WriteLine($"{new string('-', 15)}-+-{new string('-', 6)}-"
            + $"+-{new string('-', 8)}-+-{new string('-', 7)}");

        for (ushort number = minNumeralNumber; number <= maxNumeralNumber;
            number++)
        {
            var numeral = ConvertToRomanNumeral(number);
            var parsedValue = ParseRomanNumeral(numeral);
            var success = parsedValue == number;
            Console.WriteLine($"{numeral,15} | {parsedValue,6:N0} "
                + $"| {number,8:N0} "
                + $"| {success,7}");

            if (numeral.Length > longestNumeral.Length)
            {
                longestNumeral = numeral;
                longestAsDecimal = number;
            }

            if (!success)
            {
                failures.Add(numeral);
            }
        }

        Console.WriteLine();

        if (failures.Count > 0)
        {
            Console.WriteLine("The following numerals were not parsed "
                + $"correctly: {string.Join(", ", failures)}.");
        }
        else
        {
            Console.WriteLine("All numerals were parsed correctly.");
        }

        Console.WriteLine();
        Console.WriteLine($"Longest numeral of {longestNumeral.Length} "
            + $"characters is {longestNumeral} or in decimal "
            + $"{longestAsDecimal:N0}.");
    }

    static Dictionary<string, ushort> Tests => new()
    {
        { "III", 3 },
        { "IV", 4 },
        { "XXVI", 26 },
        { "XCIX", 99 },
        { "CDLX", 460 },
        { "DIV", 504 },
        { "MMXXV", 2025 },
    };

    public static string ConvertToRomanNumeral(ushort number)
    {
        if (number <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(number),
                $"{number:N0}", "Must be greater than zero.");
        }

        if (number >= 4_000)
        {
            throw new ArgumentOutOfRangeException(nameof(number),
                $"{number:N0}", $"Must be less than {4_000:N0}.");
        }

        var numberAsString = number.ToString(CultureInfo.InvariantCulture);
        var digitValue = Convert.ToUInt16(
            Math.Pow(10, numberAsString.Length - 1));
        StringBuilder result = new();

        for (var i = 0; i < numberAsString.Length; i++)
        {
            var digit = Convert.ToUInt16(numberAsString[i] - '0');
            var value = Convert.ToUInt16(digit * digitValue);

            if (digitValue == 1_000)
            {
                result.Append(digit switch
                {
                    1 => "M",
                    2 => "MM",
                    3 => "MMM",
                    _ => throw new InvalidOperationException("An unexpected "
                    + $"value of {value:N0}.")
                });
            }
            else if (digitValue == 100)
            {
                result.Append(digit switch
                {
                    0 => string.Empty,
                    1 => "C",
                    2 => "CC",
                    3 => "CCC",
                    4 => "CD",
                    5 => "D",
                    6 => "DC",
                    7 => "DCC",
                    8 => "DCCC",
                    9 => "CM",
                    _ => throw new InvalidOperationException("An unexpected "
                    + $"value of {value:N0}.")
                });
            }
            else if (digitValue == 10)
            {
                result.Append(digit switch
                {
                    0 => string.Empty,
                    1 => "X",
                    2 => "XX",
                    3 => "XXX",
                    4 => "XL",
                    5 => "L",
                    6 => "LX",
                    7 => "LXX",
                    8 => "LXXX",
                    9 => "XC",
                    _ => throw new InvalidOperationException("An unexpected "
                    + $"value of {value:N0}.")
                });
            }
            else if (digitValue == 1)
            {
                result.Append(digit switch
                {
                    0 => string.Empty,
                    1 => "I",
                    2 => "II",
                    3 => "III",
                    4 => "IV",
                    5 => "V",
                    6 => "VI",
                    7 => "VII",
                    8 => "VIII",
                    9 => "IX",
                    _ => throw new InvalidOperationException("An unexpected "
                    + $"value of {value:N0}.")
                });
            }

            digitValue /= 10;
        }

        return result.ToString();
    }
}
