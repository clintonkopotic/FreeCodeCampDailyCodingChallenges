// P@ssw0rdStr3ngth!.cpp : This file contains the 'main' function. Program
// execution begins and ends there.
// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-03

#include <array>
#include <iostream>
#include <regex>
#include <string>
#include <vector>

static std::string CheckStrength(const std::string& password)
{
	const std::regex hasUpperCaseCharacterPattern("[A-Z]");
	const std::regex hasLowerCaseCharacterPattern("[a-z]");
	const std::regex hasNumberCharacterPattern("[0-9]");
	const std::regex hasSpecialCharacterPattern("[!@#$%^&*]");

	size_t rulesMeet = 0;

	// Rule 1. At least 8 characters long.
	if (password.length() >= 8)
	{
		rulesMeet++;
	}

	// Rule 2. Contains both uppercase and lowercase letters.
	if (std::regex_search(password, hasUpperCaseCharacterPattern)
		&& std::regex_search(password, hasLowerCaseCharacterPattern))
	{
		rulesMeet++;
	}

	// Rule 3. Contains at least one number.
	if (std::regex_search(password, hasNumberCharacterPattern))
	{
		rulesMeet++;
	}

	// Rule 4. Contains at least one special character from this set:
	// !, @, #, $, %, ^, &, or *.
	if (std::regex_search(password, hasSpecialCharacterPattern))
	{
		rulesMeet++;
	}

	if (rulesMeet < 2)
	{
		return "weak";
	}
	else if (rulesMeet < 4)
	{
		return "medium";
	}
	else
	{
		return "strong";
	}
}

int main()
{
	try
	{
		std::array<std::array<std::string, 2>, 11> tests =
		{
			{
				{ "123456", "weak" },
				{ "pass!!!", "weak" },
				{ "Qwerty", "weak" },
				{ "PASSWORD", "weak" },
				{ "PASSWORD!", "medium" },
				{ "PassWord%^!", "medium" },
				{ "qwerty12345", "medium" },
				{ "PASSWORD!", "medium" },
				{ "PASSWORD!", "medium" },
				{ "S3cur3P@ssw0rd", "strong" },
				{ "C0d3&Fun!", "strong" },
			}
		};
		std::vector<std::string> failures = {};
		std::cout << std::boolalpha;

		for (const auto& test : tests)
		{
			std::string password = test[0];
			std::string expected = test[1];
			std::cout << "Testing \"" << password << "\" (expecting \""
				<< expected << "\")...";
			std::string actual = CheckStrength(password);
			std::cout << "\"" << actual << "\"";
			bool success = expected == actual;
			std::cout << " (success: " << success << ")." << std::endl;

			if (!success)
			{
				failures.push_back(password);
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
