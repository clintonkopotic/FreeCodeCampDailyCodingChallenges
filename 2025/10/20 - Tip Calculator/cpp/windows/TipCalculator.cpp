// TipCalculator.cpp : This file contains the 'main' function. Program execution
// begins and ends there.
// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-20
// Uses C++ 17.

#include <any>
#include <array>
#include <charconv>
#include <iomanip>
#include <iostream>
#include <regex>
#include <sstream>
#include <stdexcept>
#include <string>
#include <vector>

static std::array<std::string, 3> CalculateTips(std::string mealPrice,
	std::string customTip)
{
	if (mealPrice.length() == 0 || mealPrice[0] != '$')
	{
		throw std::invalid_argument("mealPrice must be of format \"$N.NN\".");
	}

	double mealPriceValue;
	auto mealPriceParseResult = std::from_chars(mealPrice.data() + 1,
		mealPrice.data() + mealPrice.size(), mealPriceValue);

	if (mealPriceParseResult.ec == std::errc::invalid_argument)
	{
		throw std::logic_error("Error: Invalid argument (not a valid double "
			"format) for mealPrice.");
	}
	else if (mealPriceParseResult.ec == std::errc::result_out_of_range)
	{
		throw std::logic_error("Error: Result out of range for mealPrice.");
	}
	else if (mealPriceParseResult.ec != std::errc{})
	{
		throw std::logic_error("Error: Unknown error during parsing for "
			"mealPrice.");
	}

	if (customTip.length() == 0 || customTip[customTip.length() - 1] != '%')
	{
		throw std::invalid_argument("customTip must be of format \"NN%\".");
	}

	double customTipValue;
	auto customTipParseResult = std::from_chars(customTip.data(),
		customTip.data() + customTip.size() - 1, customTipValue);

	if (customTipParseResult.ec == std::errc::invalid_argument)
	{
		throw std::logic_error("Error: Invalid argument (not a valid double "
			"format) for customTip.");
	}
	else if (customTipParseResult.ec == std::errc::result_out_of_range)
	{
		throw std::logic_error("Error: Result out of range for customTip.");
	}
	else if (customTipParseResult.ec != std::errc{})
	{
		throw std::logic_error("Error: Unknown error during parsing for "
			"customTip.");
	}

	auto CalculateTip = [](double priceValue, double tipValue) -> std::string
		{
			if (!std::isfinite(priceValue))
			{
				throw std::invalid_argument("priceValue must be finite.");
			}

			if (priceValue < 0)
			{
				throw std::invalid_argument("priceValue must be zero or "
					"positive.");
			}

			if (tipValue < 0)
			{
				throw std::invalid_argument("tipValue must be zero or "
					"positive.");
			}

			std::ostringstream result;
			result << std::fixed << std::setprecision(2);
			result << '$' << (priceValue * tipValue / 100);

			return result.str();
		};

	return
	{
		CalculateTip(mealPriceValue, 15),
		CalculateTip(mealPriceValue, 20),
		CalculateTip(mealPriceValue, customTipValue),
	};
}

static std::string OutputToString(std::array<std::string, 3> output);

int main()
{
	try
	{
		std::array<std::array<std::any, 3>, 3> tests;

		std::array<std::string, 3> test0expected =
		{
			"$1.50",
			"$2.00",
			"$2.50"
		};
		tests[0][0] = std::string("$10.00");
		tests[0][1] = std::string("25%");
		tests[0][2] = test0expected;

		std::array<std::string, 3> test1expected =
		{
			"$13.45",
			"$17.93",
			"$23.31"
		};
		tests[1][0] = std::string("$89.67");
		tests[1][1] = std::string("26%");
		tests[1][2] = test1expected;

		std::array<std::string, 3> test2expected =
		{
			"$2.98",
			"$3.97",
			"$1.79"
		};
		tests[2][0] = std::string("$19.85");
		tests[2][1] = std::string("9%");
		tests[2][2] = test2expected;

		std::vector<std::array<std::string, 2>> failures = {};
		std::cout << std::boolalpha;

		for (const auto& test : tests)
		{
			const auto& mealPrice = std::any_cast<std::string>(test[0]);
			const auto& customTip = std::any_cast<std::string>(test[1]);
			const auto& expected = std::any_cast<std::array<std::string, 3>>(
				test[2]);
			std::cout << "Testing \"" << mealPrice << "\" and \"" << customTip
				<< "\" (expecting " << OutputToString(expected) << ")...";
			const auto& actual = CalculateTips(mealPrice, customTip);
			std::cout << OutputToString(actual);
			bool success = expected == actual;
			std::cout << " (success: " << success << ")." << std::endl;

			if (!success)
			{
				failures.push_back({ mealPrice, customTip });
			}
		}

		std::cout << std::noboolalpha;

		if (failures.size() > 0)
		{
			std::cout << "The following inputs failed:" << std::endl;

			for (const auto& failure : failures)
			{
				const auto& mealPrice = failure[0];
				const auto& customTip = failure[1];
				std::cout << "  \"" << mealPrice << "\" and \"" << customTip
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

static std::string OutputToString(std::array<std::string, 3> output)
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
