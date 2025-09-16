namespace MatrixRotate;
using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-09-06"/>
/// </summary>
internal class Program
{
    static T[,] Rotate<T>(T[,] matrix)
    {
        ArgumentNullException.ThrowIfNull(matrix);
        var rows = matrix.GetLength(0);
        var columns = matrix.GetLength(1);
        var rotated = new T[rows, columns];

        for (var columnIndex = 0; columnIndex < columns; columnIndex++)
        {
            for (int readRowIndex = rows - 1, writeColumnIndex = 0;
                readRowIndex >= 0; readRowIndex--, writeColumnIndex++)
            {
                rotated[columnIndex, writeColumnIndex]
                    = matrix[readRowIndex, columnIndex];
            }
        }

        return rotated;
    }

    static void Main()
    {
        List<int[,]> failures = [];

        foreach ((var matrix, var expected) in Tests)
        {
            Console.Write($"Testing {MatrixToString(matrix)} (expecting "
                + $"{MatrixToString(expected)})...");
            var actual = Rotate(matrix);
            var success = AreMatricesIdentical(expected, actual);
            Console.WriteLine($"{MatrixToString(actual)} (Success: "
                + $"{success}).");

            if (!success)
            {
                failures.Add(matrix);
            }
        }

        if (failures.Count <= 0)
        {

            Console.WriteLine("All tests passed!");
        }
        else
        {
            Console.WriteLine("The following inputs failed:");

            foreach (var failure in failures)
            {
                Console.WriteLine($"  {MatrixToString(failure)}");
            }
        }
    }

    static List<(int[,], int[,])> Tests =>
    [
        (new int[,] {{ 1 }},
         new int[,] {{ 1 }}),
        (new int[,] {{ 1, 2 },
                     { 3, 4 }},
         new int[,] {{ 3, 1 },
                     { 4, 2 }}),
        (new int[,] {{ 1, 2, 3 },
                     { 4, 5, 6 },
                     { 7, 8, 9 }},
         new int[,] {{ 7, 4, 1 },
                     { 8, 5, 2 },
                     { 9, 6, 3 }}),
        (new int[,] {{ 0, 1, 0 },
                     { 1, 0, 1 },
                     { 0, 0, 0 }},
         new int[,] {{ 0, 1, 0 },
                     { 0, 0, 1 },
                     { 0, 1, 0 }}),
    ];

    static bool AreMatricesIdentical<T>(T[,] matrix1, T[,] matrix2)
    {
        ArgumentNullException.ThrowIfNull(matrix1);
        ArgumentNullException.ThrowIfNull(matrix2);
        var matrix1Rows = matrix1.GetLength(0);
        var matrix1Columns = matrix1.GetLength(1);
        var matrix2Rows = matrix2.GetLength(0);
        var matrix2Columns = matrix2.GetLength(1);

        if (matrix1Rows != matrix2Rows || matrix1Columns != matrix2Columns)
        {
            return false;
        }

        for (var rowIndex = 0; rowIndex < matrix1Rows; rowIndex++)
        {
            for (var columnIndex = 0; columnIndex < matrix1Columns;
                columnIndex++)
            {
                var matrix1Element = matrix1[rowIndex, columnIndex];
                var matrix2Element = matrix2[rowIndex, columnIndex];

                if ((matrix1Element is null && matrix2Element is not null)
                    || (matrix1Element is not null && matrix2Element is null)
                    || (matrix1Element is not null && matrix2Element is not null
                        && !matrix1Element.Equals(matrix2Element)))
                {
                    return false;
                }
            }
        }

        return true;
    }

    static string MatrixToString<T>(T[,] matrix)
    {
        ArgumentNullException.ThrowIfNull(matrix);
        var rows = matrix.GetLength(0);
        var columns = matrix.GetLength(1);
        StringBuilder result = new("[");

        for (var rowsIndex = 0; rowsIndex < rows; rowsIndex++)
        {
            if (rowsIndex > 0)
            {
                result.Append(", ");
            }

            result.Append('[');

            for (var columnsIndex = 0; columnsIndex < columns; columnsIndex++)
            {
                if (columnsIndex > 0)
                {
                    result.Append(", ");
                }

                result.Append(matrix[rowsIndex, columnsIndex]);
            }

            result.Append(']');
        }

        return result.Append(']').ToString();
    }
}
