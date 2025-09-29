namespace CharacterBattle;
using System;
using System.Collections.Generic;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-08-24"/>
/// </summary>
internal class Program
{
    static string Battle(string myArmy, string opposingArmy)
    {
        ArgumentNullException.ThrowIfNull(myArmy);
        ArgumentNullException.ThrowIfNull(opposingArmy);

        if (myArmy.Length > opposingArmy.Length)
        {
            return "Opponent retreated";
        }
        else if (myArmy.Length < opposingArmy.Length)
        {
            return "We retreated";
        }

        var battlesWon = 0;

        for (var i = 0; i < myArmy.Length; i++)
        {
            var myCharacter = myArmy[i];
            var myValue = GetValue(myCharacter);
            var opposingCharacter = opposingArmy[i];
            var opposingValue = GetValue(opposingCharacter);

            if (myValue > opposingValue)
            {
                battlesWon++;
            }
            else if (myValue < opposingValue)
            {
                battlesWon--;
            }
        }

        if (battlesWon > 0)
        {
            return "We won";
        }
        else if (battlesWon < 0)
        {
            return "We lost";
        }

        return "It was a tie";

        static int GetValue(char @char)
        {
            if (@char >= 'a' && @char <= 'z')
            {
                return @char - 'a' + 1;
            }
            else if (@char >= 'A' && @char <= 'Z')
            {
                return @char - 'A' + 27;
            }
            else if (@char >= '0' && @char <= '9')
            {
                return @char - '0';
            }

            return 0;
        }
    }

    static void Main()
    {
        List<(string myArmy, string opposingArmy)> failures = [];

        foreach (var (myArmy, opposingArmy, expected) in Tests)
        {
            Console.Write($"Testing \"{myArmy}\" and \"{opposingArmy}\" "
                + $"(expecting \"{expected}\")...");
            var actual = Battle(myArmy, opposingArmy);
            Console.Write($"\"{actual}\"");
            var success = expected == actual;
            Console.WriteLine($" (success: {success}).");

            if (!success)
            {
                failures.Add((myArmy, opposingArmy));
            }
        }

        if (failures.Count > 0)
        {
            Console.WriteLine("The following inputs failed:");

            foreach (var (myArmy, opposingArmy) in failures)
            {
                Console.WriteLine($"  \"{myArmy}\" and \"{opposingArmy}\".");
            }
        }
        else
        {
            Console.WriteLine("All tests passed!");
        }
    }

    static List<(string myArmy, string opposingArmy, string expected)> Tests =>
    [
        ("Hello", "World", "We lost"),
        ("pizza", "salad", "We won"),
        ("C@T5", "D0G$", "We won"),
        ("kn!ght", "orc", "Opponent retreated"),
        ("PC", "Mac", "We retreated"),
        ("Wizards", "Dragons", "It was a tie"),
        ("Mr. Smith", "Dr. Jones", "It was a tie"),
    ];
}
