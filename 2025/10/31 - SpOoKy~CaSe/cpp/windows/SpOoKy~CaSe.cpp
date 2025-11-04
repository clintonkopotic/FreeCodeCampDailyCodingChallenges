// SpOoKy~CaSe.cpp : This file contains the 'main' function. Program execution
// begins and ends there.
// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-31

#include <array>
#include <cctype>
#include <iostream>
#include <sstream>
#include <string>
#include <vector>

static std::string Spookify(const std::string& boo)
{
	const char tilde = '~';
	std::string spookyTilde = boo;

	for (auto& character : spookyTilde)
	{
		if (character == '_' || character == '-')
		{
			character = tilde;
		}
	}

	std::ostringstream result;
	bool capitalizeLetter = true;

	for (const auto& character : spookyTilde)
	{
		result << static_cast<char>(capitalizeLetter ? std::toupper(character)
			: std::tolower(character));

		if (character != tilde)
		{
			capitalizeLetter = !capitalizeLetter;
		}
	}

	return result.str();
}

int main()
{
	try
	{
		std::vector<std::array<std::string, 2>> tests =
		{
			{
				{ "hello_world", "HeLlO~wOrLd" },
				{ "Spooky_Case", "SpOoKy~CaSe" },
				{ "TRICK-or-TREAT", "TrIcK~oR~tReAt" },
				{ "c_a-n_d-y_-b-o_w_l", "C~a~N~d~Y~~b~O~w~L" },
				{ "thE_hAUntEd-hOUsE-Is-fUll_Of_ghOsts",
					"ThE~hAuNtEd~HoUsE~iS~fUlL~oF~gHoStS" },
			}
		};
		std::vector<std::string> failures = {};
		std::cout << std::boolalpha;

		for (const auto& test : tests)
		{
			const auto& boo = test[0];
			const auto& expected = test[1];
			std::cout << "Testing \"" << boo << "\" (expecting \""
				<< expected << "\")...";
			const auto& actual = Spookify(boo);
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
