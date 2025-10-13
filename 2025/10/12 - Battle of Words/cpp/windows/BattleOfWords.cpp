// BattleOfWords.cpp : This file contains the 'main' function. Program execution
// begins and ends there.
// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-12

#include <array>
#include <cctype>
#include <iostream>
#include <sstream>
#include <stdexcept>
#include <string>
#include <vector>

static std::string Battle(std::string ourTeam, std::string opponent)
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

	std::vector<std::string> ourTeamWords = splitIntoWords(ourTeam);
	std::vector<std::string> opponentWords = splitIntoWords(opponent);

	if (ourTeamWords.size() != opponentWords.size())
	{
		throw std::invalid_argument("Each argument must have the same number "
			"of words.");
	}

	auto calculateWordValue = [](const std::string word) -> size_t
		{
			const std::string values = " abcdefghijklmnopqrstuvwxyz";
			size_t result = 0;

			for (char letter : word)
			{
				if (islower(letter))
				{
					result += values.find(letter);
				}
				else if (isupper(letter))
				{
					result += values.find(tolower(letter)) * 2;
				}
				else
				{
					throw std::invalid_argument("All characters must be "
						"letters.");
				}
			}

			return result;
		};

	size_t ourTeamWins = 0;
	size_t opponentWins = 0;

	for (size_t i = 0; i < ourTeamWords.size(); i++)
	{
		size_t ourTeamWordValue = calculateWordValue(ourTeamWords[i]);
		size_t opponentWordValue = calculateWordValue(opponentWords[i]);

		if (ourTeamWordValue > opponentWordValue)
		{
			ourTeamWins++;
		}
		else if (ourTeamWordValue < opponentWordValue)
		{
			opponentWins++;
		}
	}

	if (ourTeamWins > opponentWins)
	{
		return "We win";
	}
	else if (ourTeamWins < opponentWins)
	{
		return "We lose";
	}
	else
	{
		return "Draw";
	}
}

int main()
{
	try
	{
		std::array<std::array<std::string, 3>, 7> tests =
		{
			{
				{"hello world", "hello word", "We win"},
				{"Hello world", "hello world", "We win"},
				{"lorem ipsum", "kitty ipsum", "We lose"},
				{"hello world", "world hello", "Draw"},
				{"git checkout", "git switch", "We win"},
				{"Cheeseburger with fries", "Cheeseburger with Fries",
					"We lose"},
				{"We must never surrender", "Our team must win", "Draw"},
			}
		};
		std::vector<std::array<std::string, 2>> failures = {};
		std::cout << std::boolalpha;

		for (size_t i = 0; i < tests.size(); i++)
		{
			std::array<std::string, 3> test = tests[i];
			std::string ourTeam = test[0];
			std::string opponent = test[1];
			std::string expected = test[2];
			std::cout << "Testing \"" << ourTeam << "\" and \"" << opponent
				<< "\" (expecting \"" << expected << "\")...";
			std::string actual = Battle(ourTeam, opponent);
			std::cout << "\"" << actual << "\"";
			bool success = expected == actual;
			std::cout << " (success: " << success << ")." << std::endl;

			if (!success)
			{
				failures.push_back({ ourTeam, opponent });
			}
		}

		std::cout << std::noboolalpha;

		if (failures.size() > 0)
		{
			std::cout << "The following inputs failed:" << std::endl;

			for (size_t i = 0; i < failures.size(); i++)
			{
				std::array<std::string, 2> failure = failures[i];
				std::string ourTeam = failure[0];
				std::string opponent = failure[1];
				std::cout << "  \"" << ourTeam << "\" and \"" << opponent
					<< "\"." << std::endl;
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
