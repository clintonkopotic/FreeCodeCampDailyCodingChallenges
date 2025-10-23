// HtmlAttributeExtractor.cpp : This file contains the 'main' function. Program
// execution begins and ends there.
// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-19
// Uses C++ 17.

#include <any>
#include <array>
#include <iostream>
#include <regex>
#include <sstream>
#include <stdexcept>
#include <string>
#include <vector>

static std::vector<std::string> ExtractAttributes(std::string element)
{
	std::vector<std::string> result;

	// Adapted from https://stackoverflow.com/a/317081
	std::regex attribute_regex(
		R"(([\w|data-]+)=["']?((?:.(?!["']?\s+(?:\S+)=|\s*\/?[>"']))+.)["']?)");
	std::smatch match;

	// Search for the first attribute within the tag
	std::string::const_iterator search_start(element.cbegin());

	while (std::regex_search(search_start, element.cend(), match,
		attribute_regex))
	{
		std::string attribute_name = match[1].str();
		std::string attribute_value;

		// Determine which capturing group holds the value (double quotes,
		// single quotes, or unquoted)
		if (match[2].matched) // Double-quoted value
		{
			attribute_value = match[2].str();
		}
		else if (match[3].matched) // Single-quoted value
		{
			attribute_value = match[3].str();
		}
		else if (match[4].matched) // Unquoted value
		{
			attribute_value = match[4].str();
		}
		else // No match
		{
			throw std::logic_error("No attribute_value found.");
		}

		result.push_back(attribute_name + ", " + attribute_value);

		// Advance the search start to continue finding more attributes
		search_start = match.suffix().first;
	}

	return result;
}

static std::string OutputToString(std::vector<std::string> output);

int main()
{
	try
	{
		std::array<std::array<std::any, 2>, 5> tests;

		std::string test0input = "<span class=\"red\"></span>";
		std::vector<std::string> test0expected = { "class, red" };
		tests[0][0] = test0input;
		tests[0][1] = test0expected;

		std::string test1input = "<meta charset=\"UTF-8\" />";
		std::vector<std::string> test1expected = { "charset, UTF-8" };
		tests[1][0] = test1input;
		tests[1][1] = test1expected;

		std::string test2input = "<p>Lorem ipsum dolor sit amet</p>";
		std::vector<std::string> test2expected = { };
		tests[2][0] = test2input;
		tests[2][1] = test2expected;

		std::string test3input = "<input name=\"email\" type=\"email\" "
			"required=\"true\" />";
		std::vector<std::string> test3expected =
		{
			"name, email",
			"type, email",
			"required, true"
		};
		tests[3][0] = test3input;
		tests[3][1] = test3expected;

		std::string test4input = "<button id=\"submit\" "
			"class=\"btn btn-primary\">Submit</button>";
		std::vector<std::string> test4expected =
		{
			"id, submit",
			"class, btn btn-primary"
		};
		tests[4][0] = test4input;
		tests[4][1] = test4expected;

		std::vector<std::string> failures = {};
		std::cout << std::boolalpha;

		for (const auto& test : tests)
		{
			const auto& element = std::any_cast<std::string>(test[0]);
			const auto& expected = std::any_cast<std::vector<std::string>>(
				test[1]);
			std::cout << "Testing \"" << element << "\" (expecting "
				<< OutputToString(expected) << ")...";
			const auto& actual = ExtractAttributes(element);
			std::cout << OutputToString(actual);
			bool success = expected == actual;
			std::cout << " (success: " << success << ")." << std::endl;

			if (!success)
			{
				failures.push_back(element);
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
