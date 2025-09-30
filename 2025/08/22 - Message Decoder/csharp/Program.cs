namespace MessageDecoder;
using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-08-22"/>
/// </summary>
internal class Program
{
    static string Decode(string message, int shift)
    {
        ArgumentNullException.ThrowIfNull(message);
        StringBuilder result = new();

        foreach (char @char in message)
        {
            if (@char >= 'A' && @char <= 'Z')
            {
                result.Append(DecodeLetterChar(@char, 'A', shift));
            }
            else if (@char >= 'a' && @char <= 'z')
            {
                result.Append(DecodeLetterChar(@char, 'a', shift));
            }
            else
            {
                result.Append(@char);
            }
        }

        return result.ToString();

        static char DecodeLetterChar(char @char, char aChar, int shift)
        {
            var charValue = Convert.ToInt32(@char);
            var aCharValue = Convert.ToInt32(aChar);
            charValue -= aCharValue;
            charValue -= shift;
            charValue %= 26;

            if (charValue < 0)
            {
                charValue += 26;
            }

            charValue += aCharValue;

            return Convert.ToChar(charValue);
        }
    }

    static void Main()
    {
        List<(string message, int shift)> failures = [];

        foreach (var (message, shift, expected) in Tests)
        {
            Console.Write($"Testing \"{message}\" and {shift} (expecting "
                + $"\"{expected}\"...");
            var actual = Decode(message, shift);
            Console.Write($"\"{actual}\"");
            var success = expected == actual;
            Console.WriteLine($" (success: {success}).");

            if (!success)
            {
                failures.Add((message, shift));
            }
        }

        if (failures.Count > 0)
        {
            Console.WriteLine("The following inputs failed:");

            foreach (var (message, shift) in failures)
            {
                Console.WriteLine($"  \"{message}\" and {shift}.");
            }
        }
        else
        {
            Console.WriteLine("All tests passed!");
        }
    }

    static List<(string message, int shift, string expected)> Tests =>
    [
        ("Xlmw mw e wigvix qiwweki.", 4, "This is a secret message."),
        ("Byffi Qilfx!", 20, "Hello World!"),
        ("Zqd xnt njzx?", -1, "Are you okay?"),
        ("oannLxmnLjvy", 9, "freeCodeCamp"),
    ];
}
