namespace S_P_A_C_E_J_A_M;
using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-08-14"/>
/// </summary>
internal class Program
{
    static string SpaceJam(string s)
    {
        ArgumentNullException.ThrowIfNull(s);
        var insertSpaces = false;
        StringBuilder result = new();

        foreach (var character in s)
        {
            if (character == ' ')
            {
                continue;
            }
            else if (insertSpaces)
            {
                result.Append("  ");
            }

            result.Append(char.ToUpper(character));
            insertSpaces = true;
        }

        return result.ToString();
    }

    static void Main()
    {
        List<string> failures = [];

        foreach (var (s, expected) in Tests)
        {
            Console.Write($"Testing \"{s}\" (expecting \"{expected}\")...");
            var actual = SpaceJam(s);
            Console.Write($"\"{actual}\"");
            var success = expected == actual;
            Console.WriteLine($" (success: {success}).");

            if (!success)
            {
                failures.Add(s);
            }
        }

        if (failures.Count > 0)
        {
            Console.WriteLine("The following inputs failed:");

            foreach (var failure in failures)
            {
                Console.WriteLine($"  \"{failure}\".");
            }
        }
        else
        {
            Console.WriteLine("All tests passed!");
        }
    }

    static List<(string s, string expected)> Tests =>
    [
        ("freeCodeCamp", "F  R  E  E  C  O  D  E  C  A  M  P"),
        ("   free   Code   Camp   ", "F  R  E  E  C  O  D  E  C  A  M  P"),
        ("Hello World?!", "H  E  L  L  O  W  O  R  L  D  ?  !"),
        ("C@t$ & D0g$", "C  @  T  $  &  D  0  G  $"),
        ("allyourbase", "A  L  L  Y  O  U  R  B  A  S  E"),
    ];
}
