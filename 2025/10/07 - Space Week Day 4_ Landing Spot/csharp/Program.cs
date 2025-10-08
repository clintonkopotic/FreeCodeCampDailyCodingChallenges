namespace LandingSpot;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-07"/>
/// </summary>
internal class Program
{
    static (int rowIndex, int columnIndex) FindLandingSpot<T>(T[][] matrix)
        where T : INumber<T>
    {
        ArgumentNullException.ThrowIfNull(matrix);
        var rows = matrix.Length;

        if (rows <= 0)
        {
            throw new ArgumentException(paramName: nameof(matrix),
                message: "Must have at least one row.");
        }

        // Traverse the matix first to ensure all elements are numbers between
        // 0 and 9, inclusive, and to ensure each array in the array have the
        // same number of columns in them.
        var nine = T.CreateChecked(9);
        var columns = 0;

        foreach (var row in matrix)
        {
            if (row is null)
            {
                throw new ArgumentException(paramName: nameof(matrix),
                    message: "A row cannot be null.");
            }
            else if (row.Length == 0)
            {
                throw new ArgumentException(paramName: nameof(matrix),
                    message: "A row cannot be empty.");
            }
            else if (columns > 0 && columns != row.Length)
            {
                throw new ArgumentException(paramName: nameof(matrix),
                    message: "The columns must be of the same size.");
            }

            if (columns <= 0)
            {
                columns = row.Length;
            }

            foreach (var value in row)
            {
                if (!T.IsInteger(value))
                {
                    throw new ArgumentException(paramName: nameof(matrix),
                        message: "All items must be integers.");
                }
                else if (value < T.Zero || value > nine)
                {
                    throw new ArgumentException(paramName: nameof(matrix),
                        message: "All items must be between zero and nine, "
                            + "inclusive.");
                }
            }
        }

        // Now traverse the matix to find the landing spot.
        var haveSetMin = false;
        var minNeighborTotal = T.Zero;
        (int rowIndex, int columnIndex) landingSpot = (-1, -1);

        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < columns; j++)
            {
                if (matrix[i][j] != T.Zero)
                {
                    continue;
                }

                var northNeighborValue = i > 0
                    ? matrix[i - 1][j] : T.Zero;
                var eastNeighborValue = j < (columns - 1)
                    ? matrix[i][j + 1] : T.Zero;
                var southNeighborValue = i < (rows - 1)
                    ? matrix[i + 1][j] : T.Zero;
                var westNeighborsValue = j > 0
                    ? matrix[i][j - 1] : T.Zero;
                var neighborTotal = northNeighborValue + eastNeighborValue
                    + southNeighborValue + westNeighborsValue;
                
                if (!haveSetMin || neighborTotal < minNeighborTotal)
                {
                    minNeighborTotal = neighborTotal;
                    landingSpot = (i, j);
                    haveSetMin = true;
                }
            }
        }

        return landingSpot;
    }

    static void Main()
    {
        List<double[][]> failures = [];

        foreach (var (matrix, expected) in Tests)
        {
            Console.Write($"Testing {MatrixToString(matrix)} (expecting " + $"{expected})...");
            var actual = FindLandingSpot(matrix);
            Console.Write(actual);
            var success = expected == actual;
            Console.WriteLine($" (success: {success}).");

            if (!success)
            {
                failures.Add(matrix);
            }
        }

        if (failures.Count > 0)
        {
            Console.WriteLine("The following inputs failed:");

            foreach (var failure in failures)
            {
                Console.WriteLine($"  {MatrixToString(failure)}.");
            }
        }
        else
        {
            Console.WriteLine("All tests passed!");
        }
    }

    static List<(double[][] matrix, (int rowIndex, int columnIndex) expected)>
        Tests =>
        [
            ([[1, 0], [2, 0]], (0, 1)),
            ([[9, 0, 3], [7, 0, 4], [8, 0, 5]], (1, 1)),
            ([[1, 2, 1], [0, 0, 2], [3, 0, 0]], (2, 2)),
            ([[9, 6, 0, 8], [7, 1, 1, 0], [3, 0, 3, 9], [8, 6, 0, 9]], (2, 1)),
        ];

    static string MatrixToString<T>(T[][] matrix)
    {
        ArgumentNullException.ThrowIfNull(matrix);

        StringBuilder result = new("[");

        for (var i = 0; i < matrix.Length; i++)
        {
            var row = matrix[i]
                ?? throw new ArgumentException(paramName: nameof(matrix),
                    message: $"No rows can be null. Row index: {i}.");

            if (i > 0)
            {
                result.Append(", ");
            }

            result.Append('[');

            for (var j = 0; j < row.Length; j++)
            {
                var value = row[j] ?? throw new ArgumentException(
                    paramName: nameof(matrix),
                    message: $"No values can be null. ({i}, {j}).");

                if (j > 0)
                {
                    result.Append(", ");
                }

                result.Append(value);
            }

            result.Append(']');
        }

        return result.Append(']').ToString();
    }
}
