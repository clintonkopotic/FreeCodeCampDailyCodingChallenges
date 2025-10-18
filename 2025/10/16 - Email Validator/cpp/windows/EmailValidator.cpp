// EmailValidator.cpp : This file contains the 'main' function. Program
// execution begins and ends there.
// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-16
// Uses C++17

#include <any>
#include <array>
#include <cctype>
#include <iostream>
#include <regex>
#include <sstream>
#include <string>
#include <vector>

static bool Validate(const std::string& email)
{
	auto splitString = [](const std::string& s, char delimiter)
		-> std::vector<std::string>
		{
			std::vector<std::string> tokens;
			std::string token;

			// Create a stringstream from the input string.
			std::stringstream ss(s);

			// Read tokens from the stringstream until the delimiter is
			// encountered.,
			while (std::getline(ss, token, delimiter))
			{
				tokens.push_back(token);
			}

			return tokens;
		};

	std::vector<std::string> parts = splitString(email, '@');

	if (parts.size() != 2)
	{
		return false;
	}

	std::string localPart = parts[0];
	const std::regex localPartRegexPattern("^[A-Za-z0-9._-]+$");

	if (localPart.length() == 0
		|| !std::regex_match(localPart, localPartRegexPattern)
		|| localPart.front() == '.' || localPart.back() == '.'
		|| localPart.find("..") != std::string::npos)
	{
		return false;
	}

	std::string domainPart = parts[1];

	if (domainPart.length() == 0
		|| domainPart.front() == '.' || domainPart.back() == '.'
		|| domainPart.find("..") != std::string::npos)
	{
		return false;
	}

	std::vector<std::string> domainDotParts = splitString(domainPart, '.');

	if (domainDotParts.size() < 2)
	{
		return false;
	}

	for (size_t i = 0; i < domainDotParts.size(); i++)
	{
		std::string domainDotPart = domainDotParts[i];

		if (domainDotPart.length() == 0)
		{
			return false;
		}
	}

	int letterCount = 0;
	std::string lastDomainDotPart = domainDotParts.back();

	for (size_t i = 0; i < lastDomainDotPart.length(); i++)
	{
		char lastDomainDotPartCharacter = lastDomainDotPart[i];

		if (std::isalpha(lastDomainDotPartCharacter) == 0)
		{
			return false;
		}

		letterCount++;
	}

	return letterCount >= 2;
}

int main()
{
	try
	{
		std::array<std::array<std::any, 2>, 9> tests =
		{
			{
				{"a@b.cd", true},
				{"hell.-w.rld@example.com", true},
				{".b@sh.rc", false},
				{"example@test.c0", false},
				{"freecodecamp.org", false},
				{"develop.ment_user@c0D!NG.R.CKS", true},
				{"hello.@wo.rld", false},
				{"hello@world..com", false},
				{"git@commit@push.io", false},
			}
		};
		std::vector<std::string> failures = {};
		std::cout << std::boolalpha;

		for (size_t i = 0; i < tests.size(); i++)
		{
			std::array<std::any, 2> test = tests[i];
			std::string html = std::string(std::any_cast<const char*>(test[0]));
			bool expected = std::any_cast<bool>(test[1]);
			std::cout << "Testing \"" << html << "\" (expecting "
				<< expected << ")...";
			bool actual = Validate(html);
			std::cout << actual;
			bool success = expected == actual;
			std::cout << " (success: " << success << ")." << std::endl;

			if (!success)
			{
				failures.push_back(html);
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
