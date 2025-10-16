// HtmlTagStripper.cpp : This file contains the 'main' function. Program
// execution begins and ends there.
// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-15

#include <array>
#include <iostream>
#include <regex>
#include <string>
#include <vector>

static std::string StripTags(const std::string& html)
{
	const std::regex pattern("\\<.*?\\>");

	return std::regex_replace(html, pattern, "");
}

int main()
{
	try
	{
		std::array<std::array<std::string, 2>, 4> tests =
		{
			{
				{ "<a href=\"#\">Click here</a>", "Click here" },
				{ "<p class=\"center\">Hello <b>World</b>!</p>",
					"Hello World!" },
				{ "<img src=\"cat.jpg\" alt=\"Cat\">", "" },
				{ "<main id=\"main\"><section class=\"section\">section"
					"</section><section class=\"section\">section</section>"
					"</main>", "sectionsection" },
			}
		};
		std::vector<std::string> failures = {};
		std::cout << std::boolalpha;

		for (size_t i = 0; i < tests.size(); i++)
		{
			std::array<std::string, 2> test = tests[i];
			std::string html = test[0];
			std::string expected = test[1];
			std::cout << "Testing \"" << html << "\" (expecting \""
				<< expected << "\")...";
			std::string actual = StripTags(html);
			std::cout << "\"" << actual << "\"";
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
