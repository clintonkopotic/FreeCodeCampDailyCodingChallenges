// ComplementaryDNA.cpp : This file contains the 'main' function. Program
// execution begins and ends there.
// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-25

#include <array>
#include <iostream>
#include <sstream>
#include <string>
#include <vector>

static std::string ComplementaryDNA(const std::string& strand)
{
	std::ostringstream result;

	for (const auto& letter : strand)
	{
		if (letter == 'A')
		{
			result << 'T';
		}
		else if (letter == 'T')
		{
			result << 'A';
		}
		else if (letter == 'C')
		{
			result << 'G';
		}
		else if (letter == 'G')
		{
			result << 'C';
		}
		else
		{
			throw std::invalid_argument("strand must only contain the letters: "
				"\'A\', \'C\', \'G\', and \'T\'.");
		}
	}

	return result.str();
}

int main()
{
	try
	{
		std::array<std::array<std::string, 2>, 4> tests =
		{
			{
				{ "ACGT", "TGCA" },
				{ "ATGCGTACGTTAGC", "TACGCATGCAATCG" },
				{ "GGCTTACGATCGAAG", "CCGAATGCTAGCTTC" },
				{ "GATCTAGCTAGGCTAGCTAG", "CTAGATCGATCCGATCGATC" },
			}
		};
		std::vector<std::string> failures = {};
		std::cout << std::boolalpha;

		for (const auto& test : tests)
		{
			const auto& sentence = test[0];
			const auto& expected = test[1];
			std::cout << "Testing \"" << sentence << "\" (expecting \""
				<< expected << "\")...";
			std::string actual = ComplementaryDNA(sentence);
			std::cout << "\"" << actual << "\"";
			bool success = expected == actual;
			std::cout << " (success: " << success << ")." << std::endl;

			if (!success)
			{
				failures.push_back(sentence);
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
