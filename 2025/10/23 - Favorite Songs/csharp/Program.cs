namespace FavoriteSongs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-23"/>
/// </summary>
internal class Program
{
    static string[] FavoriteSongs(Song[] playlist)
    {
        ArgumentNullException.ThrowIfNull(playlist);

        List<string> result = [];

        foreach (var song in new SortedSet<Song>(playlist,
            Comparer<Song>.Create((a, b) => b.Plays - a.Plays)))
        {
            result.Add(song.Title);

            if (result.Count >= 2)
            {
                break;
            }
        }

        return [.. result];
    }

    static void Main()
    {
        List<Song[]> failures = [];

        foreach (var (playlist, expected) in Tests)
        {
            Console.Write($"Testing {ValueToString(playlist)} (expecting "
                + $"{ValueToString(expected)})...");
            var actual = FavoriteSongs(playlist);
            Console.Write(ValueToString(actual));
            var success = expected.SequenceEqual(actual);
            Console.WriteLine($" (success: {success}).");

            if (!success)
            {
                failures.Add(playlist);
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

    static List<(Song[] playlist, string[] expected)> Tests =>
    [
        (playlist: [
            new(Title: "Sync or Swim", Plays: 3),
            new(Title: "Byte Me", Plays: 1),
            new(Title : "Earbud Blues", Plays : 2),
        ],
        expected: [ "Sync or Swim", "Earbud Blues" ]),
        (playlist: [
            new(Title: "Skip Track", Plays: 98),
            new(Title: "99 Downloads", Plays: 99),
            new(Title : "Clickwheel Love", Plays : 100),
        ],
        expected: [ "Clickwheel Love", "99 Downloads" ]),
        (playlist: [
            new(Title: "Song A", Plays: 42),
            new(Title: "Song B", Plays: 99),
            new(Title : "Song C", Plays : 75),
        ],
        expected: [ "Song B", "Song C" ]),
    ];

    static string ValueToString(object? value)
    {
        if (value is string stringValue)
        {
            return $"\"{stringValue}\"";
        }
        else if (value is IList listValues)
        {
            StringBuilder result = new("[");

            for (var i = 0; i < listValues.Count; i++)
            {
                if (i > 0)
                {
                    result.Append(", ");
                }

                result.Append(ValueToString(listValues[i]));
            }

            return result.Append(']').ToString();
        }
        else
        {
            return value?.ToString() ?? "null";
        }
    }
}

record Song(string Title, int Plays)
{
    public override string? ToString()
        => $"{{ {nameof(Title)}: \"{Title}\", {nameof(Plays)}: {Plays} }}";
}
