// Navigator.cpp : This file contains the 'main' function. Program execution
// begins and ends there.
// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-28
// Uses C++ 17.

#include <any>
#include <array>
#include <iostream>
#include <sstream>
#include <stdexcept>
#include <string>
#include <vector>

static std::string Navigate(std::vector<std::string> commands)
{
	if (commands.size() == 0)
	{
		throw std::invalid_argument("commands must not be empty.");
	}

	const std::string visitPagePrefix = "Visit ";
	std::vector<std::string> history = { "Home" };
	size_t currentPageHistoryIndex = 0;

	for (const auto& command : commands)
	{
		if (command.compare(0, visitPagePrefix.length(), visitPagePrefix) == 0)
		{
			std::vector<std::string> newHistory(history.begin(),
				history.begin() + currentPageHistoryIndex + 1);
			history.assign(newHistory.begin(), newHistory.end());
			history.push_back(command.substr(visitPagePrefix.length()));
			currentPageHistoryIndex++;
		}
		else if (command.compare("Back") == 0)
		{
			if (currentPageHistoryIndex > 0)
			{
				currentPageHistoryIndex--;
			}
		}
		else if (command.compare("Forward") == 0)
		{
			if (currentPageHistoryIndex != history.size() - 1)
			{
				currentPageHistoryIndex++;
			}
		}
		else
		{
			std::ostringstream result;
			result << "Invalid command of \"" << command << "\".";

			throw std::invalid_argument(result.str());
		}
	}

	return history[currentPageHistoryIndex];
}

static std::string OutputToString(std::vector<std::string> output);

int main()
{
	try
	{
		std::vector<std::array<std::any, 2>> tests;

		std::vector<std::string> test0commands =
		{
			"Visit About Us",
			"Back",
			"Forward",
		};
		std::array<std::any, 2> test0;
		test0[0] = test0commands;
		test0[1] = std::string("About Us");
		tests.push_back(test0);

		std::vector<std::string> test1commands =
		{
			"Forward",
		};
		std::array<std::any, 2> test1;
		test1[0] = test1commands;
		test1[1] = std::string("Home");
		tests.push_back(test1);

		std::vector<std::string> test2commands =
		{
			"Back",
		};
		std::array<std::any, 2> test2;
		test2[0] = test2commands;
		test2[1] = std::string("Home");
		tests.push_back(test2);

		std::vector<std::string> test3commands =
		{
			"Visit About Us",
			"Visit Gallery",
		};
		std::array<std::any, 2> test3;
		test3[0] = test3commands;
		test3[1] = std::string("Gallery");
		tests.push_back(test3);

		std::vector<std::string> test4commands =
		{
			"Visit About Us",
			"Visit Gallery",
			"Back",
			"Back",
		};
		std::array<std::any, 2> test4;
		test4[0] = test4commands;
		test4[1] = std::string("Home");
		tests.push_back(test4);

		std::vector<std::string> test5commands =
		{
			"Visit About",
			"Visit Gallery",
			"Back",
			"Visit Contact",
			"Forward",
		};
		std::array<std::any, 2> test5;
		test5[0] = test5commands;
		test5[1] = std::string("Contact");
		tests.push_back(test5);

		std::vector<std::string> test6commands =
		{
			"Visit About Us",
			"Visit Visit Us",
			"Forward",
			"Visit Contact Us",
			"Back",
		};
		std::array<std::any, 2> test6;
		test6[0] = test6commands;
		test6[1] = std::string("Visit Us");
		tests.push_back(test6);

		std::vector<std::vector<std::string>> failures = {};
		std::cout << std::boolalpha;

		for (const auto& test : tests)
		{
			const auto& commands = std::any_cast<std::vector<std::string>>(
				test[0]);
			const auto& expected = std::any_cast<std::string>(test[1]);
			std::cout << "Testing " << OutputToString(commands)
				<< " (expecting \"" << expected << "\")...";
			const auto& actual = Navigate(commands);
			std::cout << '\"' << actual << '\"';
			bool success = expected == actual;
			std::cout << " (success: " << success << ")." << std::endl;

			if (!success)
			{
				failures.push_back(commands);
			}
		}

		std::cout << std::noboolalpha;

		if (failures.size() > 0)
		{
			std::cout << "The following inputs failed:" << std::endl;

			for (const auto& failure : failures)
			{
				std::cout << "  " << OutputToString(failure) << '.'
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

static std::string OutputToString(std::vector<std::string> output)
{
	std::ostringstream result;
	result << '[';

	for (size_t i = 0; i < output.size(); i++)
	{
		if (i > 0)
		{
			result << ", ";
		}

		result << '\"' << output[i] << '\"';
	}

	result << ']';

	return result.str();
}
