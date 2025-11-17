// CharacterLimit.cpp : This file contains the 'main' function. Program
// execution begins and ends there.

#include <array>
#include <iostream>
#include <string>
#include <vector>

static std::string CanPost(std::string message)
{
	if (message.length() <= 40)
	{
		return "short post";
	}
	else if (message.length() <= 80)
	{
		return "long post";
	}
	else
	{
		return "invalid post";
	}
}

int main()
{
	try
	{
		std::vector<std::array<std::string, 2>> tests =
		{
			{
				{ "Hello world", "short post" },
				{ "This is a longer message but still under eighty characters.",
					"long post"},
				{ "This message is too long to fit into either of the "
					"character limits for a social media post.",
					"invalid post" },
			}
		};
		std::vector<std::string> failures = {};
		std::cout << std::boolalpha;

		for (const auto& test : tests)
		{
			const auto& message = test[0];
			const auto& expected = test[1];
			std::cout << "Testing \"" << message << "\" (expecting \""
				<< expected << "\")...";
			const auto& actual = CanPost(message);
			std::cout << '\"' << actual << '\"';
			bool success = expected == actual;
			std::cout << " (success: " << success << ")." << std::endl;

			if (!success)
			{
				failures.push_back(message);
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
