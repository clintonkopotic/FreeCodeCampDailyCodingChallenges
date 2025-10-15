// StringCount.cpp : This file contains the 'main' function. Program execution
// begins and ends there.
// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-14
// Uses C++17.

#include <any>
#include <array>
#include <iostream>
#include <stdexcept>
#include <string>
#include <vector>

static int Count(std::string text, std::string pattern)
{
	int result = 0;

	for (size_t i = 0; i < text.length(); i++)
	{
		i = text.find(pattern, i);

		if (i == std::string::npos)
		{
			break;
		}

		result++;
	}

	return result;
}

int main()
{
	try
	{
		std::array<std::array<std::any, 3>, 5> tests =
		{
			{
				{"abcdefg", "def", 1} ,
				{"hello", "world", 0},
				{"mississippi", "iss", 2},
				{"she sells seashells by the seashore", "sh", 3},
				{"101010101010101010101", "101", 10},
			}
		};
		std::vector<std::array<std::string, 2>> failures = {};
		std::cout << std::boolalpha;

		for (size_t i = 0; i < tests.size(); i++)
		{
			std::array<std::any, 3> test = tests[i];
			std::string text = std::string(std::any_cast<const char*>(test[0]));
			std::string pattern = std::string(std::any_cast<const char*>(
				test[1]));
			int expected = std::any_cast<int>(test[2]);
			std::cout << "Testing \"" << text << "\" and \"" << pattern
				<< "\" (expecting " << expected << ")...";
			int actual = Count(text, pattern);
			std::cout << actual;
			bool success = expected == actual;
			std::cout << " (success: " << success << ")." << std::endl;

			if (!success)
			{
				failures.push_back({ text, pattern });
			}
		}

		std::cout << std::noboolalpha;

		if (failures.size() > 0)
		{
			std::cout << "The following inputs failed:" << std::endl;

			for (size_t i = 0; i < failures.size(); i++)
			{
				std::array<std::string, 2> failure = failures[i];
				std::string text = failure[0];
				std::string pattern = failure[1];
				std::cout << "  \"" << text << "\" and \"" << pattern << "\"."
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
