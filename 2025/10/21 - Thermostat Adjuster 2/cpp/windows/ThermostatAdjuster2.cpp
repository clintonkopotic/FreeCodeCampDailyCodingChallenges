// ThermostatAdjuster2.cpp : This file contains the 'main' function. Program
// execution begins and ends there.
// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-21
// Uses C++ 17

#include <any>
#include <array>
#include <iomanip>
#include <iostream>
#include <sstream>
#include <stdexcept>
#include <string>
#include <vector>

static std::string AdjustThermostat(double currentF, double targetC)
{
	if (!std::isfinite(currentF))
	{
		throw std::invalid_argument("currentF must be finite.");
	}

	if (!std::isfinite(targetC))
	{
		throw std::invalid_argument("targetC must be finite.");
	}

	double targetF = targetC * 1.8 + 32;
	double differenceF = std::round((currentF - targetF) * 10) / 10;

	if (differenceF == 0)
	{
		return "Hold";
	}
	else if (differenceF < 0) {
		std::ostringstream result;
		result << std::fixed << std::setprecision(1);
		result << "Heat: " << std::abs(differenceF) << " degrees Fahrenheit";

		return result.str();
	}
	else if (differenceF > 0) {
		std::ostringstream result;
		result << std::fixed << std::setprecision(1);
		result << "Cool: " << differenceF << " degrees Fahrenheit";

		return result.str();
	}

	throw std::logic_error("Unexpected differenceF value.");
}

int main()
{
	try
	{
		std::array<std::array<std::any, 3>, 5> tests =
		{
			{
				{ 32.0, 0.0, std::string("Hold") },
				{ 70.0, 25.0, std::string("Heat: 7.0 degrees Fahrenheit") },
				{ 72.0, 18.0, std::string("Cool: 7.6 degrees Fahrenheit") },
				{ 212.0, 100.0, std::string("Hold") },
				{ 59.0, 22.0, std::string("Heat: 12.6 degrees Fahrenheit") },
			},
		};

		std::vector<std::array<double, 2>> failures = {};
		std::cout << std::boolalpha;

		for (const auto& test : tests)
		{
			const auto& currentF = std::any_cast<double>(test[0]);
			const auto& targetC = std::any_cast<double>(test[1]);
			const auto& expected = std::any_cast<std::string>(test[2]);
			std::cout << "Testing " << currentF << " and " << targetC
				<< " (expecting \"" << expected << "\")...";
			const auto& actual = AdjustThermostat(currentF, targetC);
			std::cout << '\"' << actual << '\"';
			bool success = expected == actual;
			std::cout << " (success: " << success << ")." << std::endl;

			if (!success)
			{
				failures.push_back({ currentF, targetC });
			}
		}

		std::cout << std::noboolalpha;

		if (failures.size() > 0)
		{
			std::cout << "The following inputs failed:" << std::endl;

			for (const auto& failure : failures)
			{
				const auto& currentF = failure[0];
				const auto& targetC = failure[1];
				std::cout << "  " << currentF << " and " << targetC << '.'
					<< std::endl;
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
