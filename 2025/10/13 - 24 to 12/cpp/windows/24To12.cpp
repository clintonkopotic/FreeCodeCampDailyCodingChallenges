// 24To12.cpp : This file contains the 'main' function. Program execution begins
// and ends there.
//

#include <array>
#include <iomanip>
#include <iostream>
#include <sstream>
#include <stdexcept>
#include <string>
#include <vector>

static std::string To12(std::string time)
{
	if (time.length() != 4)
	{
		throw std::invalid_argument("The time argument must have a length of "
			"four.");
	}

	int hourOfDay = std::stoi(time.substr(0, 2));

	if (hourOfDay < 0 || hourOfDay >= 24)
	{
		throw std::invalid_argument("The time argument must have a valid "
			"hour.");
	}

	int minuteOfHour = std::stoi(time.substr(2));

	if (minuteOfHour < 0 || minuteOfHour >= 60)
	{
		throw std::invalid_argument("The time argument must have a valid "
			"minute.");
	}

	int hourOfMeridiem = hourOfDay == 0 || hourOfDay == 12 ? 12
		: hourOfDay < 12 ? hourOfDay : hourOfDay - 12;
	std::string meridiem = hourOfDay < 12 ? "AM" : "PM";

	std::stringstream result;
	result << hourOfMeridiem << ':' << std::setw(2) << std::setfill('0')
		<< minuteOfHour << ' ' << meridiem;

	return result.str();
}

int main()
{
	try
	{
		std::array<std::array<std::string, 2>, 5> tests =
		{
			{
				{"1124", "11:24 AM"},
				{"0900", "9:00 AM"},
				{"1455", "2:55 PM"},
				{"2346", "11:46 PM"},
				{"0030", "12:30 AM"},
			}
		};
		std::vector<std::string> failures = {};
		std::cout << std::boolalpha;

		for (size_t i = 0; i < tests.size(); i++)
		{
			std::array<std::string, 2> test = tests[i];
			std::string ourTeam = test[0];
			std::string expected = test[1];
			std::cout << "Testing \"" << ourTeam << "\" (expecting \""
				<< expected << "\")...";
			std::string actual = To12(ourTeam);
			std::cout << "\"" << actual << "\"";
			bool success = expected == actual;
			std::cout << " (success: " << success << ")." << std::endl;

			if (!success)
			{
				failures.push_back(ourTeam);
			}
		}

		std::cout << std::noboolalpha;

		if (failures.size() > 0)
		{
			std::cout << "The following inputs failed:" << std::endl;

			for (size_t i = 0; i < failures.size(); i++)
			{
				std::string failure = failures[i];
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
