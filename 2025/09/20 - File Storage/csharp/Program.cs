namespace FileStorage;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-09-21"/>
/// </summary>
internal class Program
{
    static int NumberOfFiles(double fileSize, string fileUnit,
        double driveSizeGb)
    {
        Dictionary<string, double> bytes = new()
        {
            {"B", 1d },
            {"KB", 1_000d },
            {"MB", 1_000_000d },
            {"GB", 1_000_000_000d },
        };
        ThrowIfNotFiniteAndNotPositive(fileSize);
        ArgumentException.ThrowIfNullOrWhiteSpace(fileUnit);

        if (!bytes.TryGetValue(fileUnit.ToUpper(), out var fileUnitB))
        {
            throw new ArgumentOutOfRangeException(paramName: nameof(fileUnit),
                actualValue:  fileUnit, message: "Invalid unit.");
        }

        ThrowIfNotFiniteAndNegative(driveSizeGb);
        var driveSizeB = driveSizeGb * bytes["GB"];
        var fileSizeB = fileSize * fileUnitB;
        var numberOfFiles = Convert.ToInt32(
            double.Floor(driveSizeB / fileSizeB));

        return numberOfFiles;

        static void ThrowIfNotFinite(double @double,
            [CallerArgumentExpression(nameof(@double))] string?
            paramName = null)
        {
            if (!double.IsFinite(@double))
            {
                throw new ArgumentOutOfRangeException(paramName: paramName,
                    actualValue: @double,
                    message: "Must be finite.");
            }
        }

        static void ThrowIfNotFiniteAndNegative(double @double,
            [CallerArgumentExpression(nameof(@double))] string?
            paramName = null)
        {
            ThrowIfNotFinite(@double, paramName);

            if (double.IsNegative(@double))
            {
                throw new ArgumentOutOfRangeException(paramName: paramName,
                    actualValue: @double,
                    message: "Cannot be negative.");
            }
        }

        static void ThrowIfNotFiniteAndNotPositive(double @double,
            [CallerArgumentExpression(nameof(@double))] string?
            paramName = null)
        {
            ThrowIfNotFiniteAndNegative(@double, paramName);

            if (@double <= 0d || !double.IsPositive(@double))
            {
                throw new ArgumentOutOfRangeException(paramName: paramName,
                    actualValue: @double,
                    message: "Must be positive.");
            }
        }
    }

    static void Main()
    {
        List<Failure> failures = [];

        foreach (var test in Tests)
        {
            Console.Write($"Testing {test}...");
            var actual = NumberOfFiles(test.FileSize, test.FileUnit,
                test.DriveSizeGb);
            var success = test.Expected == actual;
            Console.WriteLine($"{actual:N0} (success: {success}).");

            if (!success)
            {
                failures.Add(new Failure(test));
            }
        }

        if (failures.Count > 0)
        {
            Console.WriteLine("The following inputs failed:");

            foreach (var failure in failures)
            {
                Console.WriteLine($"  {failure}.");
            }
        }
        else
        {
            Console.WriteLine("All tests passed!");
        }
    }

    static List<Test> Tests =>
    [
        new(500d, "KB", 1d, 2_000),
        new(50_000d, "B", 1d, 20_000),
        new(4_096d, "B", 1.5d, 366_210),
        new(220.5d, "KB", 100d, 453_514),
        new(4.5d, "MB", 750d, 166_666),
    ];

    record Failure(double FileSize, string FileUnit, double DriveSizeGb)
    {
        public Failure(Test test)
            : this(test.FileSize, test.FileUnit, test.DriveSizeGb)
        { }

        public override string ToString() =>
            new StringBuilder("{ ")
                .AppendFormat("{0} = {1:#,##0.##}, ", nameof(FileSize),
                    FileSize)
                .AppendFormat("{0} = {1}, ", nameof(FileUnit),
                    FileUnit is null ? "null" : $"\"{FileUnit}\"")
                .AppendFormat("{0} = {1:#,##0.##}", nameof(DriveSizeGb),
                    DriveSizeGb)
                .Append(" }")
                .ToString();
    }

    record Test(double FileSize, string FileUnit, double DriveSizeGb,
        int Expected)
    {
        public override string ToString() =>
            new StringBuilder("{ ")
                .AppendFormat("{0} = {1:#,##0.##}, ", nameof(FileSize),
                    FileSize)
                .AppendFormat("{0} = {1}, ", nameof(FileUnit),
                    FileUnit is null ? "null" : $"\"{FileUnit}\"")
                .AppendFormat("{0} = {1:#,##0.##}, ", nameof(DriveSizeGb),
                    DriveSizeGb)
                .AppendFormat("{0} = {1:N0}", nameof(Expected), Expected)
                .Append(" }")
                .ToString();
    }
}
