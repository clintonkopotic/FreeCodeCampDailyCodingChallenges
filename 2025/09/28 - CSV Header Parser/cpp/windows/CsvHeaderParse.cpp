// CsvHeaderParse.cpp : This file contains the 'main' function. Program
// execution begins and ends there.
// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-09-28
// Uses C++ 17.

#include <algorithm>
#include <any>
#include <array>
#include <cctype>
#include <iostream>
#include <sstream>
#include <stdexcept>
#include <string>
#include <vector>

static std::vector<std::string> GetHeadings(const std::string& csv)
{
	auto trim = [](const std::string& s) -> std::string
		{
			std::string copyOfS = s;

			// Trim the ending first.
			copyOfS.erase(std::find_if(copyOfS.rbegin(), copyOfS.rend(),
				[](unsigned char ch) {
					return !std::isspace(ch);
				}).base(), copyOfS.end());

			// Then trim the beginning.
			copyOfS.erase(copyOfS.begin(), std::find_if(copyOfS.begin(),
				copyOfS.end(), [](unsigned char ch) {
					return !std::isspace(ch);
				}));

			return copyOfS;
		};
	auto splitString = [](const std::string& s, char delimiter)
		-> std::vector<std::string>
		{
			std::vector<std::string> tokens;
			std::string token;
			size_t start = 0;
			size_t end = s.find(delimiter);

			while (end != std::string::npos)
			{
				tokens.push_back(s.substr(start, end - start));
				start = end + 1;
				end = s.find(delimiter, start);
			}

			tokens.push_back(s.substr(start)); // Add the last token
			return tokens;
		};

	const auto& headingsSplit = splitString(csv, ',');
	std::vector<std::string> headings = {};

	for (std::string headingSplit : headingsSplit)
	{
		headings.push_back(trim(headingSplit));
	}

	return headings;
}

static std::string VectorToString(const std::vector<std::string>& strings);

int main()
{
	try
	{
		std::vector<std::array<std::any, 2>> tests;

		std::array<std::any, 2> test0;
		test0[0] = std::string("name,age,city");
		std::vector<std::string> test0expected = { "name", "age", "city", };
		test0[1] = test0expected;
		tests.push_back(test0);

		std::array<std::any, 2> test1;
		test1[0] = std::string("first name,last name,phone");
		std::vector<std::string> test1expected = { "first name", "last name",
			"phone", };
		test1[1] = test1expected;
		tests.push_back(test1);

		std::array<std::any, 2> test2;
		test2[0] = std::string("username , email , signup date ");
		std::vector<std::string> test2expected = { "username", "email",
			"signup date", };
		test2[1] = test2expected;
		tests.push_back(test2);

		std::vector<std::string> failures = {};
		std::cout << std::boolalpha;

		for (const auto& test : tests)
		{
			const auto& csv = std::any_cast<std::string>(test[0]);
			const auto& expected = std::any_cast<std::vector<std::string>>(
				test[1]);
			std::cout << "Testing \"" << csv << "\" (expecting "
				<< VectorToString(expected) << ")...";
			const auto& actual = GetHeadings(csv);
			std::cout << VectorToString(actual);
			bool success = expected == actual;
			std::cout << " (success: " << success << ")." << std::endl;

			if (!success)
			{
				failures.push_back(csv);
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

static std::string VectorToString(const std::vector<std::string>& strings)
{
	std::ostringstream result;
	result << '[';

	for (size_t i = 0; i < strings.size(); i++)
	{
		if (i > 0)
		{
			result << ", ";
		}

		result << '\"' << strings[i] << '\"';
	}

	result << ']';

	return result.str();
}
