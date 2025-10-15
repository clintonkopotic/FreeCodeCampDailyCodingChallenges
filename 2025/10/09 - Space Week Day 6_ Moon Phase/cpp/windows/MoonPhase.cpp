// MoonPhase.cpp : This file contains the 'main' function. Program execution
// begins and ends there.
// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-09

#include <array>
#include <chrono>
#include <ctime>
#include <iomanip>
#include <iostream>
#include <sstream>
#include <stdexcept>
#include <string>
#include <vector>

static std::string MoonPhase(std::string dateString)
{
	auto parseDateString = [](const std::string& dateString) -> std::time_t
		{
			std::tm tm_struct = {};
			std::istringstream ss(dateString);
			ss.exceptions(std::istream::failbit | std::istream::badbit);
			ss >> std::get_time(&tm_struct, "%Y-%m-%d");

			return std::mktime(&tm_struct);
		};
	auto to_time_t = [](int year, int month, int day) -> std::time_t
		{
			std::tm t = {};				// Initialize to all zeros
			t.tm_year = year - 1900;	// Years since 1900
			t.tm_mon = month - 1;		// Months since January (0-11)
			t.tm_mday = day;			// Day of the month (1-31)
			t.tm_isdst = -1;			// Let mktime determine DST

			return std::mktime(&t);
		};

	std::time_t date = parseDateString(dateString);
	std::time_t referenceNewMoon = to_time_t(2000, 1, 6);
	double diffInSeconds = std::difftime(date, referenceNewMoon);
	long long diffInDays = static_cast<long long>(diffInSeconds
		/ (60 * 60 * 24));
	long long dayInLunarCycle = diffInDays % 28 + 1;

	if (dayInLunarCycle >= 1 && dayInLunarCycle <= 7) {
		return "New";
	}
	else if (dayInLunarCycle >= 8 && dayInLunarCycle <= 14) {
		return "Waxing";
	}
	else if (dayInLunarCycle >= 15 && dayInLunarCycle <= 21) {
		return "Full";
	}
	else if (dayInLunarCycle >= 22 && dayInLunarCycle <= 28) {
		return "Waning";
	}

	std::ostringstream errorMessage;
	errorMessage << "Invalid dayInLunarCycle (" << dayInLunarCycle << ").";

	throw std::logic_error(errorMessage.str());
}

int main()
{
	try
	{
		std::array<std::array<std::string, 2>, 5> tests =
		{
			{
				{"2000-01-12", "New"},
				{"2000-01-13", "Waxing"},
				{"2014-10-15", "Full"},
				{"2012-10-21", "Waning"},
				{"2022-12-14", "New"},
			}
		};
		std::vector<std::string> failures = {};
		std::cout << std::boolalpha;

		for (size_t i = 0; i < tests.size(); i++)
		{
			std::array<std::string, 2> test = tests[i];
			std::string dateString = test[0];
			std::string expected = test[1];
			std::cout << "Testing \"" << dateString << "\" (expecting \""
				<< expected << "\")...";
			std::string actual = MoonPhase(dateString);
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
