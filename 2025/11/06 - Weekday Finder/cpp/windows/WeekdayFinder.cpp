// WeekdayFinder.cpp : This file contains the 'main' function. Program execution
// begins and ends there.
// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-11-06
// Uses C++ 20.

#include <array>
#include <chrono>
#include <format>
#include <iomanip>
#include <iostream>
#include <sstream>
#include <stdexcept>
#include <string>
#include <vector>

static std::string GetWeekday(const std::string& dateString)
{
	std::istringstream stringStream(dateString);
	std::chrono::year_month_day date;
	stringStream >> std::chrono::parse("%Y-%m-%d", date);
	
	if (stringStream.fail())
	{
		throw std::invalid_argument("Invalid date format.");
	}

	return std::format("{:%A}", date);
}

int main()
{
	try
	{
		std::vector<std::array<std::string, 2>> tests =
		{
			{
				{ "2025-11-06", "Thursday" },
				{ "1999-12-31", "Friday" },
				{ "1111-11-11", "Saturday" },
				{ "2112-12-21", "Wednesday" },
				{ "2345-10-01", "Monday" },
			}
		};
		std::vector<std::string> failures = {};
		std::cout << std::boolalpha;

		for (const auto& test : tests)
		{
			std::string dateString = test[0];
			std::string expected = test[1];
			std::cout << "Testing \"" << dateString << "\" (expecting \""
				<< expected << "\")...";
			std::string actual = GetWeekday(dateString);
			std::cout << "\"" << actual << "\"";
			bool success = expected == actual;
			std::cout << " (success: " << success << ")." << std::endl;

			if (!success)
			{
				failures.push_back(dateString);
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
