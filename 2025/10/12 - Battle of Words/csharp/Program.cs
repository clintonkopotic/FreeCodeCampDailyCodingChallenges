namespace BattleOfWords;
using System;
using System.Collections.Generic;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-12"/>
/// </summary>
internal class Program
{
    static string Battle(string ourTeam, string opponent)
    {
        ArgumentNullException.ThrowIfNull(ourTeam);
        ArgumentNullException.ThrowIfNull(opponent);

        var ourTeamWords = ourTeam.Split(' ',
            StringSplitOptions.RemoveEmptyEntries
            | StringSplitOptions.TrimEntries);
        var opponentWords = opponent.Split(' ',
            StringSplitOptions.RemoveEmptyEntries
            | StringSplitOptions.TrimEntries);

        if (ourTeamWords.Length != opponentWords.Length)
        {
            throw new ArgumentException(paramName: opponent,
                message: "Must have the same number of words as "
                    + $"{nameof(ourTeam)} ({ourTeamWords.Length} not "
                    + $"{opponentWords.Length}).");
        }

        var ourTeamWins = 0;
        var opponentWins = 0;

        for (var i = 0; i < ourTeamWords.Length; i++)
        {
            var ourTeamWordValue = CalculateWordValue(ourTeamWords[i]);
            var opponentWordValue = CalculateWordValue(opponentWords[i]);

            if (ourTeamWordValue > opponentWordValue)
            {
                ourTeamWins++;
            }
            else if (ourTeamWordValue < opponentWordValue)
            {
                opponentWins++;
            }
        }

        return (ourTeamWins - opponentWins) switch
        {
            > 0 => "We win",
            < 0 => "We lose",
            _ => "Draw",
        };

        static int CalculateWordValue(string word)
        {
            ArgumentNullException.ThrowIfNull(word);

            const string values = " abcdefghijklmnopqrstuvwxyz";
            var result = 0;

            for (var i = 0; i < word.Length; i++)
            {
                char letter = word[i];
                if (char.IsAsciiLetterLower(letter))
                {
                    result += values.IndexOf(letter);
                }
                else if (char.IsAsciiLetterUpper(letter))
                {
                    result += values.IndexOf(char.ToLower(letter)) * 2;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(
                        paramName: nameof(word), actualValue: letter,
                        message: $"Must be an ASCII letter (index: {i}).");
                }
            }

            return result;
        }
    }

    static void Main()
    {
        List<(string ourTeam, string opponent)> failures = [];

        foreach (var (ourTeam, opponent, expected) in Tests)
        {
            Console.Write($"Testing \"{ourTeam}\" and \"{opponent}\" "
                + $"(expecting \"{expected}\")...");
            var actual = Battle(ourTeam, opponent);
            Console.Write($"\"{actual}\"");
            var success = expected == actual;
            Console.WriteLine($" (success: {success}).");

            if (!success)
            {
                failures.Add((ourTeam, opponent));
            }
        }

        if (failures.Count > 0)
        {
            Console.WriteLine("The following inputs failed:");

            foreach (var (ourTeam, opponent) in failures)
            {
                Console.WriteLine($"  \"{ourTeam}\" and \"{opponent}\".");
            }
        }
        else
        {
            Console.WriteLine("All tests passed!");
        }
    }

    static List<(string ourTeam, string opponent, string expected)> Tests =>
    [
        ("hello world", "hello word", "We win"),
        ("Hello world", "hello world", "We win"),
        ("lorem ipsum", "kitty ipsum", "We lose"),
        ("hello world", "world hello", "Draw"),
        ("git checkout", "git switch", "We win"),
        ("Cheeseburger with fries", "Cheeseburger with Fries", "We lose"),
        ("We must never surrender", "Our team must win", "Draw"),
    ];
}
