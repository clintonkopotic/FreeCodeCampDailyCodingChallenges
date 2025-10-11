// LaunchFuel.cpp : This file contains the 'main' function. Program execution
// begins and ends there.
// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-10

#include <array>
#include <cmath>
#include <iostream>
#include <limits>
#include <vector>

static double LaunchFuel(double payload)
{
	if (!std::isfinite(payload) || payload < 0.0)
	{
		return std::numeric_limits<double>::quiet_NaN();
	}

	// Rockets require 1 kg of fuel per 5 kg of mass they must lift.
	const double fuelInKgPerLiftMassInKg = 1.0 / 5.0;
	double totalPayloadInKg = payload;
	double fuelToLiftInKg = totalPayloadInKg * fuelInKgPerLiftMassInKg;
	double additionalFuelInKg = fuelToLiftInKg;
	double lastFuelToLiftInKg = fuelToLiftInKg;
	double totalFuelInKg = fuelToLiftInKg;
	totalPayloadInKg += fuelToLiftInKg;

	while (additionalFuelInKg >= 1)
	{
		fuelToLiftInKg = totalPayloadInKg * fuelInKgPerLiftMassInKg;
		additionalFuelInKg = std::abs(lastFuelToLiftInKg - fuelToLiftInKg);
		lastFuelToLiftInKg = fuelToLiftInKg;
		totalFuelInKg += additionalFuelInKg;
		totalPayloadInKg += additionalFuelInKg;
	}

	return std::round(totalFuelInKg * 10.0) / 10.0;
}

int main()
{
	std::array<std::array<double, 2>, 5> tests =
	{
		{
			{50, 12.4},
			{500, 124.8},
			{243, 60.7},
			{11000, 2749.8},
			{6214, 1553.4},
		}
	};
	std::vector<double> failures;
	std::cout << std::boolalpha;

	for (size_t i = 0; i < tests.size(); i++)
	{
		std::array<double, 2> test = tests[i];
		double payload = test[0];
		double expected = test[1];
		std::cout << "Testing " << payload << " (expecting " << expected
			<< ")...";
		double actual = LaunchFuel(payload);
		std::cout << actual;
		bool success = expected == actual;
		std::cout << " (success: " << success << ")." << std::endl;

		if (!success)
		{
			failures.push_back(payload);
		}
	}

	std::cout << std::noboolalpha;

	if (failures.size() > 0)
	{
		std::cout << "The following inputs failed:" << std::endl;

		for (size_t i = 0; i < failures.size(); i++)
		{
			std::cout << "  " << failures[i] << "." << std::endl;
		}
	}
	else
	{
		std::cout << "All tests passed!" << std::endl;
	}
}
