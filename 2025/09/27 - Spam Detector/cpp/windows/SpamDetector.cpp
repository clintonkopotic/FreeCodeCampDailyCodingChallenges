// SpamDetector.cpp : This file contains the 'main' function. Program execution
// begins and ends there.
// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-09-27
// Uses C++ 17.

#include <any>
#include <array>
#include <charconv>
#include <iostream>
#include <regex>
#include <sstream>
#include <stdexcept>
#include <string>
#include <vector>

static bool IsSpam(const std::string& number)
{
	const std::regex pattern(R"(^\+(\d+)\s\((\d{3})\)\s(\d{3})\-(\d{4})$)");
	std::smatch matches;

	if (!std::regex_search(number, matches, pattern)
		|| matches.size() != 5)
	{
		throw std::invalid_argument("number is in invalid format.");
	}

	auto parseInt = [](std::string s) -> int
		{
			int value;
			const auto& parseResult = std::from_chars(s.data(),
				s.data() + s.size(), value);

			if (parseResult.ec == std::errc())
			{
				return value;
			}
			else if (parseResult.ec == std::errc::invalid_argument)
			{
				throw std::invalid_argument("Failed to parse, not a valid "
					"number.");
			}
			else if (parseResult.ec == std::errc::result_out_of_range)
			{
				throw std::invalid_argument("Faild to parse, result out of "
					"range.");
			}

			throw std::invalid_argument("Failed to parse, unknown.");
		};

	const auto& countryCodeString = matches[1].str();
	const auto& countryCodeNumber = parseInt(countryCodeString);

	// The country code is greater than 2 digits long or doesn't begin with a
	// zero (0).
	if (countryCodeString.length() > 2 || countryCodeString[0] != '0') {
		return true;
	}

	const auto& areaCodeString = matches[2].str();
	const auto& areaCodeNumber = parseInt(areaCodeString);

	// The area code is greater than 900 or less than 200.
	if (areaCodeNumber > 900 || areaCodeNumber < 200) {
		return true;
	}

	const auto& prefixString = matches[3].str();
	const auto& prefixNumber = parseInt(prefixString);

	int sumOfPrefixDigits = 0;

	for (const auto& prefixDigitChar : prefixString)
	{
		const auto& areaCodeDigitNumber = prefixDigitChar - '0';

		if (areaCodeDigitNumber < 0 || areaCodeDigitNumber > 9)
		{
			throw std::invalid_argument("number has invalid area code digit.");
		}

		sumOfPrefixDigits += areaCodeDigitNumber;
	}

	const auto& sumOfPrefixDigitsString = std::to_string(sumOfPrefixDigits);

	const auto& suffixString = matches[4].str();
	const auto& suffixNumber = parseInt(suffixString);

	// The sum of first three digits of the local number appears within last
	// four digits of the local number.
	if (suffixString.find(sumOfPrefixDigitsString) != std::string::npos)
	{
		return true;
	}

	const auto& numberNoFormattingChars = countryCodeString + areaCodeString
		+ prefixString + suffixString;

	if (numberNoFormattingChars.length() < 4)
	{
		throw std::logic_error("numberNoFormattingChars must have a length of "
			"at least 4.");
	};

	// The number has the same digit four or more times in a row (ignoring the
	// formatting characters).
	for (size_t i = 3; i < numberNoFormattingChars.length(); i++)
	{
		if (numberNoFormattingChars[i - 3] == numberNoFormattingChars[i]
			&& numberNoFormattingChars[i - 2] == numberNoFormattingChars[i]
			&& numberNoFormattingChars[i - 1] == numberNoFormattingChars[i])
		{
			return true;
		}
	}

	return false;
}

int main()
{
	try
	{
		std::vector<std::array<std::any, 2>> tests =
		{
			{ std::string("+0 (200) 234-0182"), false },
			{ std::string("+091 (555) 309-1922"), true },
			{ std::string("+1 (555) 435-4792"), true },
			{ std::string("+0 (955) 234-4364"), true },
			{ std::string("+0 (155) 131-6943"), true },
			{ std::string("+0 (555) 135-0192"), true },
			{ std::string("+0 (555) 564-1987"), true },
			{ std::string("+00 (555) 234-0182"), false },
		};

		std::vector<std::string> failures = {};
		std::cout << std::boolalpha;

		for (const auto& test : tests)
		{
			const auto& number = std::any_cast<std::string>(test[0]);
			const auto& expected = std::any_cast<bool>(test[1]);
			std::cout << "Testing \"" << number << "\" (expecting "
				<< expected << ")...";
			const auto& actual = IsSpam(number);
			std::cout << actual;
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
