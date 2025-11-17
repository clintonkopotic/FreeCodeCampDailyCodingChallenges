namespace MatrixBuilder;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-11-05"/>
/// </summary>
internal class Program
{
    static T[][] BuildMatrix<T>(T rows, T columns) where T : INumber<T>
    {
        if (!T.IsInteger(rows))
        {
            throw new ArgumentOutOfRangeException(paramName: nameof(rows),
                actualValue: rows, message: "Must be an integer.");
        }

        if (!T.IsInteger(columns))
        {
            throw new ArgumentOutOfRangeException(paramName: nameof(columns),
                actualValue: columns, message: "Must be an integer.");
        }

        ArgumentOutOfRangeException.ThrowIfNegative(rows);
        ArgumentOutOfRangeException.ThrowIfNegative(columns);

        List<T[]> matrix = [];
        List<T> rowList = [];

        for (var j = T.Zero; j < columns; j++)
        {
            rowList.Add(T.Zero);
        }

        T[] row = [.. rowList];

        for (var i = T.Zero; i < rows; i++)
        {
            matrix.Add(row);
        }

        return [.. matrix];
    }

    static void Main()
    {
        List<(int rows, int columns)> failures = [];
        var rowEqualityComparer = EqualityComparer<IEnumerable<int>>.Create(
            (rowA, rowB) =>
            {
                ArgumentNullException.ThrowIfNull(rowA);
                ArgumentNullException.ThrowIfNull(rowB);

                return rowA.SequenceEqual(rowB);
            });

        foreach (var (rows, columns, expected) in Tests)
        {
            Console.Write($"Testing {rows} and {columns} (expecting "
                + $"{ValueToString(expected)})...");
            var actual = BuildMatrix(rows, columns);
            Console.Write(ValueToString(actual));
            var success = expected.SequenceEqual(actual, rowEqualityComparer);
            Console.WriteLine($" (success: {success}).");

            if (!success)
            {
                failures.Add((rows, columns));
            }
        }

        if (failures.Count > 0)
        {
            Console.WriteLine("The following inputs have failed:");

            foreach (var (rows, columns) in failures)
            {
                Console.WriteLine($"  {rows} and {columns}.");
            }
        }
        else
        {
            Console.WriteLine("All tests passed!");
        }
    }

    static List<(int rows, int columns, int[][] expected)> Tests =>
    [
        (2, 3, [[0, 0, 0], [0, 0, 0]]),
        (3, 2, [[0, 0], [0, 0], [0, 0]]),
        (4, 3, [[0, 0, 0], [0, 0, 0], [0, 0, 0], [0, 0, 0]]),
        (9, 1, [[0], [0], [0], [0], [0], [0], [0], [0], [0]]),
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
