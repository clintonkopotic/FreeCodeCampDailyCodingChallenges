namespace ImageSearch;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-11-04"/>
/// </summary>
internal class Program
{
    static string[] ImageSearch(string[] images, string term)
    {
        ArgumentNullException.ThrowIfNull(images);
        ArgumentNullException.ThrowIfNull(term);

        List<string> foundImages = [];

        foreach (var image in images)
        {
            if (image.Contains(term,
                StringComparison.InvariantCultureIgnoreCase))
            {
                foundImages.Add(image);
            }
        }

        return [.. foundImages];
    }

    static void Main()
    {
        List<(string[] images, string term)> failures = [];

        foreach (var (images, term, expected) in Tests)
        {
            Console.Write($"Testing {ValueToString(images)} and "
                + $"{ValueToString(term)} (expecting "
                + $"{ValueToString(expected)})...");
            var actual = ImageSearch(images, term);
            Console.Write(ValueToString(actual));
            var success = expected.SequenceEqual(actual);
            Console.WriteLine($" (success: {success}).");

            if (!success)
            {
                failures.Add((images, term));
            }
        }

        if (failures.Count > 0)
        {
            Console.WriteLine("The following inputs failed:");

            foreach (var (images, term) in failures)
            {
                Console.WriteLine($"  {ValueToString(images)} and "
                    + $"{ValueToString(term)}.");
            }
        }
        else
        {
            Console.WriteLine("All tests passed");
        }
    }

    static List<(string[] images, string term, string[] expected)> Tests =>
    [
        (["dog.png", "cat.jpg", "parrot.jpeg"], "dog", ["dog.png"]),
        (["Sunset.jpg", "Beach.png", "sunflower.jpeg"], "sun", ["Sunset.jpg",
            "sunflower.jpeg"]),
        (["Moon.png", "sun.jpeg", "stars.png"], "PNG", ["Moon.png",
            "stars.png"]),
        (["cat.jpg", "dogToy.jpeg", "kitty-cat.png", "catNip.jpeg",
            "franken_cat.gif"], "Cat", ["cat.jpg", "kitty-cat.png",
                "catNip.jpeg", "franken_cat.gif"]),
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
