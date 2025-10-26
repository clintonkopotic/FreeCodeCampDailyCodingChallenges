namespace ComplementaryDNA;
using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-25"/>
/// </summary>
internal class Program
{
    static string ComplementaryDNA(string strand)
    {
        ArgumentNullException.ThrowIfNull(strand);

        StringBuilder result = new();

        foreach (var letter in strand)
        {
            result.Append(letter switch
            {
                'A' => 'T',
                'T' => 'A',
                'C' => 'G',
                'G' => 'C',
                _ => throw new ArgumentException(paramName: nameof(strand),
                    message: "Must only contain letters: \'A\', \'C\', \'G\', "
                    + "and \'T\'")
            });
        }

        return result.ToString();
    }
    static void Main()
    {
        List<string> failures = [];

        foreach (var (strand, expected) in Tests)
        {
            Console.Write($"Testing \"{strand}\" (expecting "
                + $"\"{expected}\")...");
            var actual = ComplementaryDNA(strand);
            Console.Write($"\"{actual}\"");
            var success = expected == actual;
            Console.WriteLine($" (success: {success}).");

            if (!success)
            {
                failures.Add(strand);
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

    static List<(string strand, string expected)> Tests =>
    [
        ("ACGT", "TGCA"),
        ("ATGCGTACGTTAGC", "TACGCATGCAATCG"),
        ("GGCTTACGATCGAAG", "CCGAATGCTAGCTTC"),
        ("GATCTAGCTAGGCTAGCTAG", "CTAGATCGATCCGATCGATC"),
    ];
}
