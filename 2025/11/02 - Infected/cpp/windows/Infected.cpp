// Infected.cpp : This file contains the 'main' function. Program execution
// begins and ends there.
// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-11-02

#include <array>
#include <cmath>
#include <iomanip>
#include <iostream>
#include <sstream>
#include <stdexcept>
#include <vector>

static double Infected(const double& days)
{
	if (!std::isfinite(days) || days != std::trunc(days) || days <= 0)
	{
		std::ostringstream exceptionMessage;
		exceptionMessage << "The argument \"n\" must be an integer and not "
			"negative (n: " << days << ").";

		throw std::invalid_argument(exceptionMessage.str());
	}

	double numberOfComputersInfected = 1;

	for (double day = 1; day <= days; day++)
	{
		numberOfComputersInfected *= 2;

		if (fmod(day, 3) == 0)
		{
			double patched = std::ceil(numberOfComputersInfected * 0.2);
			numberOfComputersInfected -= patched;
		}
	}

	return numberOfComputersInfected;
}

int main()
{
	try
	{
		std::vector<std::array<double, 2>> tests =
		{
			{ 1, 2 },
			{ 3, 6 },
			{ 8, 152 },
			{ 17, 39808 },
			{ 25, 5217638 },
		};
		std::vector<double> failures = {};
		std::cout << std::boolalpha << std::fixed << std::setprecision(0);

		for (const auto& test : tests)
		{
			const auto& days = test[0];
			const auto& expected = test[1];
			std::cout << "Testing " << days << " (expecting "
				<< expected << ")...";
			const auto& actual = Infected(days);
			std::cout << actual;
			bool success = expected == actual;
			std::cout << " (success: " << success << ")." << std::endl;

			if (!success)
			{
				failures.push_back(days);
			}
		}

		std::cout << std::noboolalpha;

		if (failures.size() > 0)
		{
			std::cout << "The following inputs failed:" << std::endl;

			for (const auto& failure : failures)
			{
				std::cout << "  " << failure << '.' << std::endl;
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
