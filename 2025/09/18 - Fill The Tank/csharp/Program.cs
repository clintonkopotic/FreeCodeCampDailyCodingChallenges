namespace FillTheTank;
using System;
using System.Collections.Generic;

internal class Program
{
    static string CostToFill(double tankSize, double fuelLevel,
        double pricePerGallon)
    {
        if (!double.IsFinite(tankSize) || double.IsNegative(tankSize))
        {
            throw new ArgumentOutOfRangeException(paramName: nameof(tankSize),
                actualValue: tankSize,
                message: "Must be finite and not negative.");
        }

        if (!double.IsFinite(fuelLevel) || double.IsNegative(fuelLevel))
        {
            throw new ArgumentOutOfRangeException(paramName: nameof(fuelLevel),
                actualValue: fuelLevel,
                message: "Must be finite and not negative.");
        }

        if (!double.IsFinite(pricePerGallon)
            || double.IsNegative(pricePerGallon))
        {
            throw new ArgumentOutOfRangeException(
                paramName: nameof(pricePerGallon),
                actualValue: pricePerGallon,
                message: "Must be finite and not negative.");
        }

        var gallonsToFill = tankSize - fuelLevel;
        var costToFill = gallonsToFill * pricePerGallon;

        return $"${costToFill:F2}";
    }

    static void Main()
    {
        List<Test> failures = [];

        foreach (var test in Tests)
        {
            Console.Write($"Testing {test}...");
            var actual = CostToFill(test.TankSize, test.FuelLevel,
                test.PricePerGallon);
            var success = test.Expected == actual;
            Console.WriteLine($"\"{actual}\" (success: {success}).");

            if (!success)
            {
                failures.Add(test);
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

    static List<Test> Tests => [
        new(20, 0, 4, "$80.00"),
        new(15, 10, 3.50, "$17.50"),
        new(18, 9, 3.25, "$29.25"),
        new(12, 12, 4.99, "$0.00"),
        new(15, 9.5, 3.98, "$21.89"),
    ];

    record Test(double TankSize, double FuelLevel, double PricePerGallon,
        string Expected)
    {
        public override string ToString() => $"{{ "
            + $"{nameof(TankSize)} = {TankSize}, "
            + $"{nameof(FuelLevel)} = {FuelLevel}, "
            + $"{nameof(PricePerGallon)} = {PricePerGallon}, "
            + $"{nameof(Expected)} = \"{Expected}\""
            + $" }}";
    }
}
