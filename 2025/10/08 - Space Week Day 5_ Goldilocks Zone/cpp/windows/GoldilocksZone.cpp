// GoldilocksZone.cpp : This file contains the 'main' function. Program
// execution begins and ends there.
// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-08

#include <array>
#include <cmath>
#include <iostream>
#include <sstream>
#include <stdexcept>
#include <vector>

static std::array<double, 2> GoldilocksZone(double mass)
{
	if (!std::isfinite(mass) || mass < 0)
	{
		std::ostringstream exceptionMessage;
		exceptionMessage << "The argument \"mass\" must be finite and not "
			<< "negative (mass: " << mass << ").";

		throw std::invalid_argument(exceptionMessage.str());
	}

	double luminosityOfStar = std::pow(mass, 3.5);
	double squareRootOfLuminosityOfStar = std::sqrt(luminosityOfStar);
	double startOfZone = 0.95 * squareRootOfLuminosityOfStar;
	double endOfZone = 1.37 * squareRootOfLuminosityOfStar;

	return { std::round(startOfZone * 100) / 100,
		std::round(endOfZone * 100) / 100 };
}

int main()
{
	try
	{
		auto toString = [](std::array<double, 2> array)
			-> std::string
			{
				std::ostringstream result;
				result << "[" << array[0] << ", " << array[1] << "]";

				return result.str();
			};

		std::array<std::array<double, 3>, 5> tests =
		{
			{
				{1, 0.95, 1.37},
				{0.5, 0.28, 0.41},
				{6, 21.85, 31.51},
				{3.7, 9.38, 13.52},
				{20, 179.69, 259.13},
			}
		};
		std::vector<double> failures = {};
		std::cout << std::boolalpha;

		for (size_t i = 0; i < tests.size(); i++)
		{
			std::array<double, 3> test = tests[i];
			double mass = test[0];
			std::array<double, 2> expected = { test[1], test[2] };
			std::cout << "Testing " << mass << " (expecting "
				<< toString(expected) << ")...";
			std::array<double, 2> actual = GoldilocksZone(mass);
			std::cout << toString(actual);
			bool success = expected == actual;
			std::cout << " (success: " << success << ")." << std::endl;

			if (!success)
			{
				failures.push_back(mass);
			}
		}

		std::cout << std::noboolalpha;

		if (failures.size() > 0)
		{
			std::cout << "The following inputs failed:" << std::endl;

			for (size_t i = 0; i < failures.size(); i++)
			{
				double failure = failures[i];
				std::cout << "  " << failure << "." << std::endl;
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
