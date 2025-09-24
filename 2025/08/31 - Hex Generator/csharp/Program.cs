namespace HexGenerator;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-08-31"/>
/// </summary>
internal class Program
{
    static string GenerateHex(string color)
    {
        ArgumentNullException.ThrowIfNull(color);
        color = color.Trim();

        var dominateColorNumber = Convert.ToByte(Random.Shared.Next(156, 256));
        var dominateColor = dominateColorNumber.ToString("X2");
        var secondColor = Convert.ToByte(
            Random.Shared.Next(dominateColorNumber)).ToString("X2");
        var thirdColor = Convert.ToByte(
            Random.Shared.Next(dominateColorNumber)).ToString("X2");

        if (color.Equals("red", StringComparison.InvariantCultureIgnoreCase))
        {
            return $"{dominateColor}{secondColor}{thirdColor}";
        }
        else if (color.Equals("green",
            StringComparison.InvariantCultureIgnoreCase))
        {
            return $"{secondColor}{dominateColor}{thirdColor}";
        }
        else if (color.Equals("blue",
            StringComparison.InvariantCultureIgnoreCase))
        {
            return $"{secondColor}{thirdColor}{dominateColor}";
        }

        return "Invalid color";
    }

    static void Main()
    {
        List<string> failures = [];

        // Test 1. GenerateHex("yellow") should return "Invalid color".
        var color1 = "yellow";
        var expected1 = "Invalid color";
        Console.Write($"Testing \"{color1}\" (expecting \"{expected1}\")...");
        var actual1 = GenerateHex(color1);
        Console.Write($"\"{actual1}\"");
        var success1 = expected1 == actual1;
        Console.WriteLine($" (success: {success1}).");

        if (!success1)
        {
            failures.Add(color1);
        }

        // Test 2. GenerateHex("red") should return a six-character string.
        var color2 = "red";
        var expected2 = 6;
        Console.Write($"Testing \"{color2}\" (expecting six-character "
            + "string)...");
        var actual2 = GenerateHex(color2);
        Console.Write($"\"{actual2}\"");
        var actual2Length = actual2.Length;
        var success2 = expected2 == actual2Length;
        Console.WriteLine($" (length: {actual2Length}; success: {success2}).");

        if (!success2)
        {
            failures.Add(color2);
        }

        // Test 3. GenerateHex("red") should return a valid six-character hex
        // color code.
        var color3 = "red";
        var expected3 = 6;
        Console.Write($"Testing \"{color3}\" (expecting valid six-character "
            + "hex color code)...");
        var actual3 = GenerateHex(color3);
        Console.Write($"\"{actual3}\"");
        var actual3Length = actual3.Length;
        var validLength3 = expected3 == actual3Length;
        var validHexColorCode3 = ValidSixCharacterHexColorCode(actual3);
        var success3 = validLength3 && validHexColorCode3;
        Console.WriteLine($" (length: {actual3Length}; valid hex color code: "
            + $"{validHexColorCode3}; success: {success3}).");

        if (!success3)
        {
            failures.Add(color3);
        }

        // Test 4. GenerateHex("red") should return a valid hex color with a
        // higher red value than other colors.
        var color4 = "red";
        Console.Write($"Testing \"{color4}\" (expecting valid hex color with a "
            + $"higher {color4} value than other colors)...");
        var actual4 = GenerateHex(color4);
        Console.Write($"\"{actual4}\"");
        var (red4, green4, blue4) = ParseIntoRgb(actual4);
        var success4 = red4 > green4 && red4 > blue4;
        Console.WriteLine($" (red: {red4}; green: {green4}; blue: {blue4}; "
            + $"success: {success4}).");

        if (!success4)
        {
            failures.Add(color4);
        }

        // Test 5. Calling GenerateHex("red") twice should return two different
        // hex color values where red is dominant.
        var color5 = "red";
        Console.WriteLine($"Testing \"{color5}\" (expecting two different hex "
            + $"color values where {color5} is dominant):");

        var actual51 = GenerateHex(color5);
        var (red51, green51, blue51) = ParseIntoRgb(actual51);
        var success51 = red51 > green51 && red51 > blue51;
        Console.WriteLine($"  \"{actual51}\" (red: {red51}; green: {green51}; "
            + $"blue: {blue51}; success: {success51}).");

        var actual52 = GenerateHex(color5);
        var (red52, green52, blue52) = ParseIntoRgb(actual52);
        var success52 = red52 > green52 && red52 > blue52;
        Console.WriteLine($"  \"{actual52}\" (red: {red52}; green: {green52}; "
            + $"blue: {blue52}; success: {success52}).");

        var success53 = actual51 != actual52;
        Console.WriteLine($"  Different hex color values: {success53}.");

        var success5 = success51 && success52 && success53;

        if (!success5)
        {
            failures.Add(color5);
        }

        // Test 6. Calling GenerateHex("green") twice should return two
        // different hex color values where green is dominant.
        var color6 = "green";
        Console.WriteLine($"Testing \"{color6}\" (expecting two different hex "
            + $"color values where {color6} is dominant):");

        var actual61 = GenerateHex(color6);
        var (red61, green61, blue61) = ParseIntoRgb(actual61);
        var success61 = green61 > red61 && green61 > blue61;
        Console.WriteLine($"  \"{actual61}\" (red: {red61}; green: {green61}; "
            + $"blue: {blue61}; success: {success61}).");

        var actual62 = GenerateHex(color6);
        var (red62, green62, blue62) = ParseIntoRgb(actual62);
        var success62 = green62 > red62 && green62 > blue62;
        Console.WriteLine($"  \"{actual62}\" (red: {red62}; green: {green62}; "
            + $"blue: {blue62}; success: {success62}).");

        var success63 = actual61 != actual62;
        Console.WriteLine($"  Different hex color values: {success63}.");

        var success6 = success61 && success62 && success63;

        if (!success6)
        {
            failures.Add(color6);
        }

        // Test 7. Calling GenerateHex("blue") twice should return two
        // different hex color values where blue is dominant.
        var color7 = "blue";
        Console.WriteLine($"Testing \"{color7}\" (expecting two different hex "
            + $"color values where {color7} is dominant):");

        var actual71 = GenerateHex(color7);
        var (red71, green71, blue71) = ParseIntoRgb(actual71);
        var success71 = blue71 > red71 && blue71 > green71;
        Console.WriteLine($"  \"{actual71}\" (red: {red71}; green: {green71}; "
            + $"blue: {blue71}; success: {success71}).");

        var actual72 = GenerateHex(color7);
        var (red72, green72, blue72) = ParseIntoRgb(actual72);
        var success72 = blue72 > red72 && blue72 > green72;
        Console.WriteLine($"  \"{actual72}\" (red: {red72}; green: {green72}; "
            + $"blue: {blue72}; success: {success72}).");

        var success73 = actual71 != actual72;
        Console.WriteLine($"  Different hex color values: {success73}.");

        var success7 = success71 && success72 && success73;

        if (!success7)
        {
            failures.Add(color7);
        }

        // Done.
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

    static bool ValidSixCharacterHexColorCode(string str,
        [CallerArgumentExpression(nameof(str))] string? paramName = null)
    {
        ArgumentNullException.ThrowIfNull(str, paramName);

        if (str.Length != 6)
        {
            return false;
        }

        foreach (char character in str)
        {
            if (!char.IsAsciiHexDigit(character))
            {
                return false;
            }
        }

        return true;
    }

    static (byte red, byte green, byte blue) ParseIntoRgb(string str,
        [CallerArgumentExpression(nameof(str))] string? paramName = null)
    {
        if (!ValidSixCharacterHexColorCode(str, paramName))
        {
            throw new ArgumentException(paramName: paramName,
                message: "Inavlid six-character hex color code.");
        }

        var red = byte.Parse(str[0..2], NumberStyles.HexNumber);
        var green = byte.Parse(str[2..4], NumberStyles.HexNumber);
        var blue = byte.Parse(str[4..6], NumberStyles.HexNumber);

        return (red, green, blue);
    }
}
