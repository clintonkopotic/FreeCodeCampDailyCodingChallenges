// WordCounter.cpp : This file contains the 'main' function. Program execution
// begins and ends there.
// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-11-03
// Uses C++ 17.

#include <any>
#include <array>
#include <iostream>
#include <sstream>
#include <string>
#include <vector>

static size_t CountWords(const std::string& sentence)
{
	std::vector<std::string> words;
	std::istringstream iss(sentence);
	std::string word;

	while (iss >> word) {
		words.push_back(word);
	}

	return words.size();
}

int main()
{
	try
	{
		std::vector<std::array<std::any, 2>> tests =
		{
			{
				{ std::string("Hello world"), 2ULL },
				{ std::string("The quick brown fox jumps over the lazy dog."),
					9ULL },
				{ std::string("I like coding challenges!"), 4ULL },
				{ std::string("Complete the challenge in JavaScript and "
					"Python."), 7ULL },
				{ std::string("The missing semi-colon crashed the entire "
					"internet."), 7ULL },
			}
		};
		std::vector<std::string> failures = {};
		std::cout << std::boolalpha;

		for (const auto& test : tests)
		{
			const auto& boo = std::any_cast<std::string>(test[0]);
			const auto& expected = std::any_cast<size_t>(test[1]);
			std::cout << "Testing \"" << boo << "\" (expecting \""
				<< expected << "\")...";
			const auto& actual = CountWords(boo);
			std::cout << "\"" << actual << "\"";
			bool success = expected == actual;
			std::cout << " (success: " << success << ")." << std::endl;

			if (!success)
			{
				failures.push_back(boo);
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
