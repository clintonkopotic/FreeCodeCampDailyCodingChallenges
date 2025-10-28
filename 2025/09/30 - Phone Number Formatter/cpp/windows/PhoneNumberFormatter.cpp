// PhoneNumberFormatter.cpp : This file contains the 'main' function. Program
// execution begins and ends there.
// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-09-30

#include <array>
#include <iostream>
#include <regex>
#include <sstream>
#include <stdexcept>
#include <string>
#include <vector>

static std::string FormatNumber(const std::string& number)
{
	if (number.length() != 11)
	{
		throw std::invalid_argument("number be 11 characters in length.");
	}

	std::ostringstream result;
	result << '+' << number[0] << " (" << number.substr(1, 3) << ") "
		<< number.substr(4, 3) << '-' << number.substr(7, 4);

	return result.str();
}

int main()
{
	try
	{
		std::array<std::array<std::string, 2>, 2> tests =
		{
			{
				{ "05552340182", "+0 (555) 234-0182" },
				{ "15554354792", "+1 (555) 435-4792" },
			}
		};
		std::vector<std::string> failures = {};
		std::cout << std::boolalpha;

		for (const auto& test : tests)
		{
			const auto& number = test[0];
			const auto& expected = test[1];
			std::cout << "Testing \"" << number << "\" (expecting \""
				<< expected << "\")...";
			const auto& actual = FormatNumber(number);
			std::cout << '\"' << actual << '\"';
			bool success = expected == actual;
			std::cout << " (success: " << success << ")." << std::endl;

			if (!success)
			{
				failures.push_back(number);
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
