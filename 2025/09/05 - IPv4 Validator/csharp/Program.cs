namespace IPv4Validator;
using System;
using System.Collections.Generic;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-09-05"/>
/// </summary>
internal class Program
{
    static bool IsValidIPv4(string ipv4)
    {
        ArgumentNullException.ThrowIfNull(ipv4);

        var octets = ipv4.Split('.');

        if (octets.Length != 4)
        {
            return false;
        }

        foreach (var octet in octets)
        {
            if (octet.Length <= 0 || octet.Length > 3
                || (octet[0] == '0' && octet.Length != 1)
                || !int.TryParse(octet, out var octetValue)
                || octetValue < 0 || octetValue > 255)
            {
                return false;
            }
        }

        return true;
    }

    static void Main()
    {
        List<string> failures = [];

        foreach ((var test, var expected) in Tests)
        {
            Console.Write($"Testing \"{test}\" (expecting {expected})...");
            var result = IsValidIPv4(test);
            var success = expected == result;
            Console.WriteLine($"{result} (Success: {success}).");

            if (!success)
            {
                failures.Add(test);
            }
        }

        if (failures.Count <= 0)
        {
            Console.WriteLine("All tests passed!");
        }
        else
        {
            Console.Write("The following inputs failed: ");

            for (var i = 0; i < failures.Count; i++)
            {
                if (i > 0)
                {
                    Console.Write(", ");
                }

                Console.Write($"\"{failures[i]}\"");
            }

            Console.WriteLine('.');
        }
    }

    static Dictionary<string, bool> Tests => new()
    {
        { "192.168.1.1", true },
        { "0.0.0.0", true },
        { "255.01.50.111", false },
        { "255.00.50.111", false },
        { "256.101.50.115", false },
        { "192.168.101.", false },
        { "192168145213", false },
    };
}
