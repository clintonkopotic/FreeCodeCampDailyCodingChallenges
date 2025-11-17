// CountingCards.cpp : This file contains the 'main' function. Program execution
// begins and ends there.
// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-11-07

#include <array>
#include <cmath>
#include <iomanip>
#include <iostream>
#include <stdexcept>
#include <vector>

static double Combinations(double cards)
{
	const double cardsInDeck = 52;
	double integral_part;

	if (std::modf(cards, &integral_part) != 0.0)
	{
		throw std::invalid_argument("Must be an integer.");
	}
	else if (cards < 0)
	{
		throw std::invalid_argument("Must be zero or positive.");
	}
	else if (cards > cardsInDeck)
	{
		throw std::invalid_argument("Must be less than or equal to 52.");
	}
	else if (cards == 0 || cards == cardsInDeck)
	{
		return 1; // Only one way to choose 0 or all items.
	}
	else if (cards > (cardsInDeck / 2))
	{
		cards = cardsInDeck - cards; // Optimize calculation for symmetry.
	}

	double result = 1;

	for (double i = 1; i <= cards; i++)
	{
		result = result * (cardsInDeck - i + 1) / i;
	}

	return result;
}

int main()
{
	try
	{
		std::vector<std::array<double, 2>> tests =
		{
			{
				{ 52.0, 1.0 },
				{ 1.0, 52.0 },
				{ 2.0, 1326.0 },
				{ 5.0, 2598960.0 },
				{ 10.0, 15820024220.0 },
				{ 50.0, 1326.0 },
			}
		};
		std::vector<double> failures = {};
		std::cout << std::boolalpha << std::fixed << std::setprecision(0);

		for (const auto& test : tests)
		{
			const auto& cards = test[0];
			const auto& expected = test[1];
			std::cout << "Testing " << cards << " (expecting " << expected
				<< ")...";
			const auto& actual = Combinations(cards);
			std::cout << actual;
			bool success = expected == actual;
			std::cout << " (success: " << success << ")." << std::endl;

			if (!success)
			{
				failures.push_back(cards);
			}
		}

		std::cout << std::noboolalpha;

		if (failures.size() > 0)
		{
			std::cout << "The following inputs failed:" << std::endl;

			for (const auto& failure : failures)
			{
				std::cout << "  " << failure << "." << std::endl;
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
