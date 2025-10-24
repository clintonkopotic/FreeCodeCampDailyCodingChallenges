// ExoplanetSearch.cpp : This file contains the 'main' function. Program
// execution begins and ends there.
// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-05
// Uses C++ 17.

#include <any>
#include <array>
#include <iostream>
#include <stdexcept>
#include <string>
#include <vector>

static bool HasExoplanets(std::string readings)
{
	const std::string values = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
	size_t totalNumberOfReadings = 0;
	size_t sumOfReadings = 0;
	std::vector<size_t> readingValues = {};

	for (const auto& readingCharacter : readings)
	{
		const auto& value = values.find(readingCharacter);

		if (value == std::string::npos)
		{
			throw std::logic_error("Invalid reading.");
		}

		totalNumberOfReadings++;
		sumOfReadings += value;
		readingValues.push_back(value);
	}

	if (totalNumberOfReadings == 0)
	{
		throw std::logic_error("No readings");
	}

	const double averageReading = static_cast<double>(sumOfReadings)
		/ static_cast<double>(totalNumberOfReadings);

	if (!std::isfinite(averageReading))
	{
		throw std::logic_error("averageReading is not finite.");
	}

	const double exoplanetMaxThresholdReading = 0.8 * averageReading;

	for (const auto& readingValue : readingValues)
	{
		if (static_cast<double>(readingValue) <= exoplanetMaxThresholdReading)
		{
			return true;
		}
	}

	return false;
}

int main()
{
	try
	{
		std::array<std::array<std::any, 2>, 6> tests =
		{
			{
				{ std::string("665544554"), false },
				{ std::string("FGFFCFFGG"), true },
				{ std::string("MONOPLONOMONPLNOMPNOMP"), false },
				{ std::string("FREECODECAMP"), true },
				{ std::string("9AB98AB9BC98A"), false },
				{ std::string("ZXXWYZXYWYXZEGZXWYZXYGEE"), true },
			}
		};
		std::vector<std::string> failures = {};
		std::cout << std::boolalpha;

		for (const auto& test : tests)
		{
			const auto& readings = std::any_cast<std::string>(test[0]);
			const auto& expected = std::any_cast<bool>(test[1]);
			std::cout << "Testing \"" << readings << "\" (expecting "
				<< expected << ")...";
			const auto& actual = HasExoplanets(readings);
			std::cout << actual;
			bool success = expected == actual;
			std::cout << " (success: " << success << ")." << std::endl;

			if (!success)
			{
				failures.push_back(readings);
			}
		}

		std::cout << std::noboolalpha;

		if (failures.size() > 0)
		{
			std::cout << "The following inputs failed:" << std::endl;

			for (const auto& failure : failures)
			{
				std::cout << "  \"" << failure << "\"." << std::endl;
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
