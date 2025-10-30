namespace Navigator;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-28"/>
/// </summary>
internal class Program
{
    static string Navigate(string[] commands)
    {
        ArgumentNullException.ThrowIfNull(commands);

        if (commands.Length <= 0)
        {
            throw new ArgumentOutOfRangeException(paramName: nameof(commands),
                actualValue: commands.Length, message: "Must not be empty.");
        }

        const string visitPagePrefix = "Visit ";
        List<string> history = ["Home"];
        var currentPageHistoryIndex = 0;

        foreach (var command in commands)
        {
            if (command.StartsWith(visitPagePrefix))
            {
                history = history[0..(currentPageHistoryIndex + 1)];
                history.Add(command[visitPagePrefix.Length..]);
                currentPageHistoryIndex++;
            }
            else if (command == "Back")
            {
                if (currentPageHistoryIndex > 0)
                {
                    currentPageHistoryIndex--;
                }
            }
            else if (command == "Forward")
            {
                if (currentPageHistoryIndex != history.Count - 1)
                {
                    currentPageHistoryIndex++;
                }
            }
            else
            {
                throw new ArgumentException(paramName: nameof(commands),
                    message: $"Invalid command of \"{command}\".");
            }
        }

        return history[currentPageHistoryIndex];
    }

    static void Main()
    {
        List<string[]> failures = [];

        foreach (var (commands, expected) in Tests)
        {
            Console.Write($"Testing {ValueToString(commands)} (expecting "
                + $"{ValueToString(expected)})...");
            var actual = Navigate(commands);
            Console.Write(ValueToString(actual));
            var success = expected == actual;
            Console.WriteLine($" (success: {success}).");

            if (!success)
            {
                failures.Add(commands);
            }
        }

        if (failures.Count > 0)
        {
            Console.WriteLine("The following inputs failed:");

            foreach (var failure in failures)
            {
                Console.WriteLine($"  {ValueToString(failure)}.");
            }
        }
        else
        {
            Console.WriteLine("All tests passed!");
        }
    }

    static List<(string[] commands, string expected)> Tests =>
    [
        (["Visit About Us", "Back", "Forward"], "About Us"),
        (["Forward"], "Home"),
        (["Back"], "Home"),
        (["Visit About Us", "Visit Gallery"], "Gallery"),
        (["Visit About Us", "Visit Gallery", "Back", "Back"], "Home"),
        (["Visit About", "Visit Gallery", "Back", "Visit Contact", "Forward"],
            "Contact"),
        (["Visit About Us", "Visit Visit Us", "Forward", "Visit Contact Us",
            "Back"], "Visit Us"),
    ];

    static string ValueToString(object? value)
    {
        if (value is IList values)
        {
            StringBuilder result = new("[");

            for (var i = 0; i < values.Count; i++)
            {
                if (i > 0)
                {
                    result.Append(", ");
                }

                result.Append(ValueToString(values[i]));
            }

            return result.Append(']').ToString();
        }
        else if (value is string valueString)
        {
            return $"\"{valueString}\"";
        }
        else
        {
            return value?.ToString() ?? "null";
        }
    }
}
