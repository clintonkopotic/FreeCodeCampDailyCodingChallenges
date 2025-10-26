namespace HiddenTreasure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-24"/>
/// </summary>
internal class Program
{
    static string Dive(char[][] map, int[] coordinates)
    {
        ArgumentNullException.ThrowIfNull(map);

        var rows = map.Length;

        if (rows == 0)
        {
            throw new ArgumentOutOfRangeException(paramName: nameof(map),
                actualValue: rows, message: "There must be at least one row.");
        }

        var columns = 0;
        var unfoundPartsCount = 0;
        HashSet<char> cellValues = ['-', 'O', 'X'];
        char cell;

        for (var i = 0; i < map.Length; i++)
        {
            var row = map[i] ?? throw new ArgumentException(
                paramName: nameof(map),
                message: $"Each row must not be null (rowIndex: {i}).");

            if (row.Length == 0)
            {
                throw new ArgumentException(
                    paramName: nameof(map),
                    message: $"Each row must not be empty (rowIndex: {i}).");
            }

            if (columns != 0 && columns != row.Length)
            {
                throw new ArgumentException(
                    paramName: nameof(map),
                    message: $"Each row must have the same number of columns "
                        + $"(rowIndex: {i}).");
            }

            if (columns == 0)
            {
                columns = row.Length;
            }

            for (var j = 0; j < columns; j++)
            {
                cell = row[j];

                if (!cellValues.Contains(cell))
                {
                    throw new ArgumentOutOfRangeException(
                        paramName: nameof(map),
                        actualValue: cell,
                        message:"Each cell must be a valid value (rowIndex: "
                            + $"{i}, columnIndex: {j}).");
                }

                if (cell == 'O')
                {
                    unfoundPartsCount++;
                }
            }
        }

        ArgumentNullException.ThrowIfNull(coordinates);

        if (coordinates.Length != 2)
        {
            throw new ArgumentOutOfRangeException(
                paramName: nameof(coordinates),
                actualValue: coordinates.Length,
                message: "There must be only two coordinates.");
        }

        var rowIndex = coordinates[0];

        if (rowIndex < 0 || rowIndex >= rows)
        {
            throw new ArgumentOutOfRangeException(
                paramName: nameof(coordinates),
                actualValue: rowIndex,
                message: "Must be a valid rowIndex.");
        }

        var columnIndex = coordinates[1];

        if (columnIndex < 0 || columnIndex >= columns)
        {
            throw new ArgumentOutOfRangeException(
                paramName: nameof(coordinates),
                actualValue: columnIndex,
                message: "Must be a valid columnIndex.");
        }

        cell = map[rowIndex][columnIndex];

        if (cell == '-')
        {
            return "Empty";
        }
        else if (cell == 'O')
        {
            unfoundPartsCount--;
        }

        return unfoundPartsCount == 0 ? "Recovered" : "Found";
    }

    static void Main()
    {
        List<(char[][] map, int[] coordinates)> failures = [];

        foreach (var (map, coordinates, expected) in Tests)
        {
            Console.Write($"Testing {ValueToString(map)} and "
                + $"{ValueToString(coordinates)} (expecting "
                + $"{ValueToString(expected)})...");
            var actual = Dive(map, coordinates);
            Console.Write(ValueToString(actual));
            var success = expected == actual;
            Console.WriteLine($" (success: {success}).");

            if (!success)
            {
                failures.Add((map, coordinates));
            }
        }

        if (failures.Count > 0)
        {
            Console.WriteLine("The following inputs failed:");

            foreach (var (map, coordinates) in failures)
            {
                Console.WriteLine($"  {ValueToString(map)} and "
                    + $"{ValueToString(coordinates)}.");
            }
        }
        else
        {
            Console.WriteLine("All tests passed!");
        }
    }

    static List<(char[][] map, int[] coordinates, string expected)> Tests =>
    [
        ([['-', 'X'], ['-', 'X'], ['-', 'O']], [2, 1], "Recovered"),
        ([['-', 'X'], ['-', 'X'], ['-', 'O']], [2, 0], "Empty"),
        ([['-', 'X'], ['-', 'O'], ['-', 'O']], [1, 1], "Found"),
        ([['-', '-', '-'], ['X', 'O', 'X'], ['-', '-', '-']], [1, 2], "Found"),
        ([['-', '-', '-'], ['-', '-', '-'], ['O', 'X', 'X']], [2, 0],
            "Recovered"),
        ([['-', '-', '-'], ['-', '-', '-'], ['O', 'X', 'X']], [1, 2], "Empty"),
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
        else if (value is char charValue)
        {
            return $"\'{charValue}\'";
        }
        else if (value is string stringValue)
        {
            return $"\"{stringValue}\"";
        }
        else
        {
            return value?.ToString() ?? "null";
        }
    }
}
