// StellarClassification.cpp : This file contains the 'main' function. Program
// execution begins and ends there.
// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-04
// Uses C++17.

#include <any>
#include <array>
#include <iostream>
#include <sstream>
#include <stdexcept>
#include <string>
#include <vector>

static char Classification(double temp)
{
	if (!std::isfinite(temp))
	{
		throw std::invalid_argument("temp must be finite.");
	}

	if (temp < 0)
	{
		throw std::invalid_argument("temp must be zero or positive.");
	}

	if (temp >= 30000)
	{
		return 'O';
	}
	else if (temp >= 10000)
	{
		return 'B';
	}
	else if (temp >= 7500)
	{
		return 'A';
	}
	else if (temp >= 6000)
	{
		return 'F';
	}
	else if (temp >= 5200)
	{
		return 'G';
	}
	else if (temp >= 3700)
	{
		return 'K';
	}
	else
	{
		return 'M';
	}
}

static double CastTemp(std::any temp);

int main()
{
	try
	{
		std::array<std::array<std::any, 2>, 8> tests =
		{
			{
				{ 5778, 'G' },
				{ 2400, 'M' },
				{ 9999, 'A' },
				{ 3700, 'K' },
				{ 3699, 'M' },
				{ 210000, 'O'},
				{ 6000, 'F' },
				{ 11432, 'B' },
			}
		};
		std::vector<double> failures = {};
		std::cout << std::boolalpha;

		for (const auto& test : tests)
		{
			const auto& temp = CastTemp(test[0]);
			const auto& expected = std::any_cast<char>(test[1]);
			std::cout << "Testing " << temp << " (expecting \'"
				<< expected << "\')...";
			const auto& actual = Classification(temp);
			std::cout << '\'' << actual << '\'';
			bool success = expected == actual;
			std::cout << " (success: " << success << ")." << std::endl;

			if (!success)
			{
				failures.push_back(temp);
			}
		}

		std::cout << std::noboolalpha;

		if (failures.size() > 0)
		{
			std::cout << "The following inputs failed:" << std::endl;

			for (const auto& failure : failures)
			{
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

static double CastTemp(std::any temp)
{
	try
	{
		return std::any_cast<double>(temp);
	}
	catch (const std::bad_any_cast&)
	{
		try
		{
			return static_cast<double>(std::any_cast<int>(temp));
		}
		catch (const std::bad_any_cast&)
		{
			std::ostringstream result;
			result << "Failed to cast to double or int. Stored type: "
				<< temp.type().name() << '.';

			throw std::invalid_argument(result.str());
		}
	}
}
