namespace VideoStorage;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-09-21"/>
/// </summary>
internal class Program
{
    static bool NumberOfVideos(double videoSize, string videoUnit,
        double driveSize, string driveUnit, out int numberOfVideos,
        [NotNullWhen(false)] out string? message)
    {
        numberOfVideos = 0;
        message = default;
        HashSet<string> allowedVideoUnits = ["KB", "MB", "GB"];
        HashSet<string> allowedDriveUnits = ["GB", "TB"];
        Dictionary<string, double> bytes = new()
        {
            { "B", 1d },
            { "KB", 1_000d },
            { "MB", 1_000_000d },
            { "GB", 1_000_000_000d },
            { "TB", 1_000_000_000_000d },
        };
        ThrowIfNotFiniteAndNotPositive(videoSize);
        ArgumentException.ThrowIfNullOrWhiteSpace(videoUnit);
        videoUnit = videoUnit.Trim().ToUpper();

        if (!bytes.TryGetValue(videoUnit, out var videoUnitB)
            || !allowedVideoUnits.Contains(videoUnit))
        {
            message = "Invalid video unit";

            return false;
        }

        ThrowIfNotFiniteAndNegative(driveSize);
        ArgumentException.ThrowIfNullOrWhiteSpace(driveUnit);
        driveUnit = driveUnit.Trim().ToUpper();

        if (!bytes.TryGetValue(driveUnit, out var driveUnitB)
            || !allowedDriveUnits.Contains(driveUnit))
        {
            message = "Invalid drive unit";

            return false;
        }

        var driveSizeB = driveSize * driveUnitB;
        var fileSizeB = videoSize * videoUnitB;
        numberOfVideos = Convert.ToInt32(double.Floor(driveSizeB / fileSizeB));

        return true;

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
            var actualValid = NumberOfVideos(test.FileSize, test.FileUnit,
                test.DriveSize, test.DriveUnit, out var actualNumber,
                out var actualMessage);

            bool success;

            if (actualValid)
            {
                Console.Write($"{actualNumber:N0}");
                success = test.ExpectedNumberOfVideos == actualNumber;
            }
            else
            {
                Console.Write($"\"{actualMessage}\"");
                success = test.ExpectedMessage == actualMessage;
            }

            Console.WriteLine($" (success: {success}).");

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
        new(500, "MB", 100, "GB", true, 200),
        new(2_000, "B", 1, "TB", false, -1, "Invalid video unit"),
        new(2_000, "MB", 100_000, "MB", false, -1, "Invalid drive unit"),
        new(500_000, "KB", 2, "TB", true, 4_000),
        new(1.5, "GB", 2.2, "TB", true, 1_466),
    ];

    record Failure(double FileSize, string FileUnit, double DriveSize,
        string DriveUnit)
    {
        public Failure(Test test)
            : this(test.FileSize, test.FileUnit, test.DriveSize, test.DriveUnit)
        { }

        public override string ToString() =>
            new StringBuilder("{ ")
                .AppendFormat("{0} = {1:#,##0.##}, ", nameof(FileSize),
                    FileSize)
                .AppendFormat("{0} = {1}, ", nameof(FileUnit),
                    FileUnit is null ? "null" : $"\"{FileUnit}\"")
                .AppendFormat("{0} = {1:#,##0.##}", nameof(DriveSize),
                    DriveSize)
                .AppendFormat("{0} = {1}", nameof(DriveUnit),
                    DriveUnit is null ? "null" : $"\"{DriveUnit}\"")
                .Append(" }")
                .ToString();
    }

    record Test(double FileSize, string FileUnit, double DriveSize,
        string DriveUnit, bool ExpectedValidNumberOfVideos,
        int ExpectedNumberOfVideos, string? ExpectedMessage = null)
    {
        public override string ToString()
        {
            var result = new StringBuilder("{ ")
                .AppendFormat("{0} = {1:#,##0.##}, ", nameof(FileSize),
                    FileSize)
                .AppendFormat("{0} = {1}, ", nameof(FileUnit),
                    FileUnit is null ? "null" : $"\"{FileUnit}\"")
                .AppendFormat("{0} = {1:#,##0.##}, ", nameof(DriveSize),
                    DriveSize)
                .AppendFormat("{0} = {1}, ", nameof(DriveUnit),
                    DriveUnit is null ? "null" : $"\"{DriveUnit}\"")
                .Append("Expected = ");

            if (ExpectedValidNumberOfVideos)
            {
                _ = result.AppendFormat("{0:N0}", ExpectedNumberOfVideos);
            }
            else
            {
                _ = result.AppendFormat("{0}, ", ExpectedMessage is null ?
                    "null" : $"\"{ExpectedMessage}\"");
            }

            return result.Append(" }").ToString();
        }
    }
}
