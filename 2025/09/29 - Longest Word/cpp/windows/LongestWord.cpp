// LongestWord.cpp : This file contains the 'main' function. Program execution
// begins and ends there.
// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-09-29

#include <array>
#include <iostream>
#include <sstream>
#include <stdexcept>
#include <string>
#include <vector>

static std::string GetLongestWord(const std::string& sentence)
{
	auto splitIntoWords = [](const std::string& text)
		-> std::vector<std::string>
		{
			std::vector<std::string> words;
			std::istringstream iss(text);
			std::string word;

			while (iss >> word)
			{
				// Extracts words separated by whitespace
				words.push_back(word);
			}

			return words;
		};

	const auto& words = splitIntoWords(sentence);
	std::string result = "";

	for (std::string word : words)
	{
		if (word[word.length() - 1] == '.')
		{
			word = word.substr(0, word.length() - 1);
		}

		if (word.length() > result.length())
		{
			result = word;
		}
	}

	return result;
}

int main()
{
	try
	{
		std::array<std::array<std::string, 2>, 3> tests =
		{
			{
				{ "coding is fun", "coding" },
				{ "Coding challenges are fun and educational.", "educational" },
				{ "This sentence has multiple long words.", "sentence" },
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
			const auto& actual = GetLongestWord(number);
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
