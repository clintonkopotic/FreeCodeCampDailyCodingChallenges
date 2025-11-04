namespace SpOoKy_CaSe;
using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-31"/>
/// </summary>
internal class Program
{
    static string Spookify(string boo)
    {
        ArgumentNullException.ThrowIfNull(boo);

        const char tilde = '~';
        var spookyTilde = boo.Replace('_', tilde).Replace('-', tilde);
        StringBuilder spookyCase = new();
        var capitalizeLetter = true;

        foreach (var character in spookyTilde)
        {
            spookyCase.Append(capitalizeLetter
                ? char.ToUpper(character) : char.ToLower(character));

            if (character != tilde)
            {
                capitalizeLetter = !capitalizeLetter;
            }
        }

        return spookyCase.ToString();
    }

    static void Main()
    {
        List<string> failures = [];

        foreach (var (boo, expected) in Tests)
        {
            Console.Write($"Testing \"{boo}\" (expecting \"{expected}\")...");
            var actual = Spookify(boo);
            Console.Write($"\"{actual}\"");
            var success = expected == actual;
            Console.WriteLine($" (success: {success}).");

            if (!success)
            {
                failures.Add(boo);
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

    static List<(string boo, string expected)> Tests =>
    [
        ("hello_world", "HeLlO~wOrLd"),
        ("Spooky_Case", "SpOoKy~CaSe"),
        ("TRICK-or-TREAT", "TrIcK~oR~tReAt"),
        ("c_a-n_d-y_-b-o_w_l", "C~a~N~d~Y~~b~O~w~L"),
        ("thE_hAUntEd-hOUsE-Is-fUll_Of_ghOsts",
            "ThE~hAuNtEd~HoUsE~iS~fUlL~oF~gHoStS"),
    ];
}
