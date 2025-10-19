// CreditCardMasker.cpp : This file contains the 'main' function. Program
// execution begins and ends there.
// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-17

#include <array>
#include <iostream>
#include <sstream>
#include <stdexcept>
#include <string>
#include <vector>

static std::string Mask(const std::string& card)
{
	if (card.length() != 19)
	{
		throw std::invalid_argument("card must have a length of exactly 19 "
			"characters.");
	}

	std::ostringstream result;
	result << "****" << card[4] << "****" << card[9] << "****"
		<< card.substr(14);
	
	return result.str();
}

int main()
{
	try
	{
		std::array<std::array<std::string, 2>, 4> tests =
		{
			{
				{"4012-8888-8888-1881", "****-****-****-1881"} ,
				{"5105 1051 0510 5100", "**** **** **** 5100"},
				{"6011 1111 1111 1117", "**** **** **** 1117"},
				{"2223-0000-4845-0010", "****-****-****-0010"},
			}
		};
		std::vector<std::string> failures = {};
		std::cout << std::boolalpha;

		for (std::array<std::string, 2> test : tests)
		{
			std::string card = test[0];
			std::string expected = test[1];
			std::cout << "Testing \"" << card << "\" (expecting \""
				<< expected << "\")...";
			std::string actual = Mask(card);
			std::cout << "\"" << actual << "\"";
			bool success = expected == actual;
			std::cout << " (success: " << success << ")." << std::endl;

			if (!success)
			{
				failures.push_back(card);
			}
		}

		std::cout << std::noboolalpha;

		if (failures.size() > 0)
		{
			std::cout << "The following inputs failed:" << std::endl;

			for (std::string failure : failures)
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
