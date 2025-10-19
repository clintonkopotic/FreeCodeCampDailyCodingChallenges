// PhoneHome.cpp : This file contains the 'main' function. Program execution
// begins and ends there.
// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-06

#include <algorithm>
#include <array>
#include <cmath>
#include <iomanip>
#include <iostream>
#include <limits>
#include <sstream>
#include <stdexcept>
#include <vector>

static double SendMessage(std::vector<double> route)
{
	if (route.size() <= 1)
	{
		throw std::invalid_argument("Must have at least two elements.");
	}

	double distanceTraveledInKm = 0;
	// Start at negative one to not count the trip to the first satellite.
	int numberOfSatelitesPassedThrough = -1;

	for (const auto& value : route)
	{
		if (!std::isfinite(value) || value < 0.0)
		{
			throw std::invalid_argument("Each element must be finite and not "
				"negative.");
		}

		distanceTraveledInKm += value;
		numberOfSatelitesPassedThrough++;
	}

	const double messageSpeedInKmPerSecond = 300000.0;
	const double delayOfMessageThroughSatelliteInSeconds = 0.5;
	const double timeOfTravelInSeonds = (distanceTraveledInKm
		/ messageSpeedInKmPerSecond) + (delayOfMessageThroughSatelliteInSeconds
			* numberOfSatelitesPassedThrough);

	return std::round(timeOfTravelInSeonds * 10000.0) / 10000.0;
}

static std::string VectorToString(std::vector<double> vector);

int main()
{
	try
	{
		std::array<std::vector<double>, 6> tests =
		{
			{
				{ 300000, 300000, 2.5 },
				{ 384400, 384400, 3.0627 },
				{ 54600000, 54600000, 364.5 },
				{ 1000000, 500000000, 1000000, 1674.3333 },
				{ 10000, 21339, 50000, 31243, 10000, 2.4086 },
				{ 802101, 725994, 112808, 3625770, 481239, 21.1597 },
			}
		};
		std::vector<std::vector<double>> failures;
		std::cout << std::boolalpha;

		for (const auto& test : tests)
		{
			std::vector<double> route(test.begin() + 0,
				test.begin() + test.size() - 1);
			const auto& expected = test.back();
			std::cout << "Testing " << VectorToString(route) << " (expecting "
				<< expected << ")...";
			const auto& actual = SendMessage(route);
			std::cout << actual;
			const auto& success = expected == actual;
			std::cout << " (success: " << success << ")." << std::endl;

			if (!success)
			{
				failures.push_back(route);
			}
		}

		std::cout << std::noboolalpha;

		if (failures.size() > 0)
		{
			std::cout << "The following inputs failed:" << std::endl;

			for (const auto& failure : failures)
			{
				std::cout << "  " << VectorToString(failure) << '.';
			}
		}
		else
		{
			std::cout << "All tests passed!" << std::endl;
		}
	}
	catch (const std::exception& e)
	{
		std::cerr << "An unhandled exception occured: \"" << e.what() << "\""
			<< std::endl;
	}
}

static std::string VectorToString(std::vector<double> vector)
{
	std::ostringstream result;
	result << std::fixed << std::setprecision(0);
	result << '[';

	for (size_t i = 0; i < vector.size(); i++)
	{
		if (i > 0)
		{
			result << ", ";
		}

		result << vector[i];
	}

	result << ']';

	return result.str();
}
