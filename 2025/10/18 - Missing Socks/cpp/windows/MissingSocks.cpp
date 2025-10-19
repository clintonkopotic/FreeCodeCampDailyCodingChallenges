// MissingSocks.cpp : This file contains the 'main' function. Program execution
// begins and ends there.
// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-18

#include <array>
#include <cmath>
#include <iostream>
#include <limits>
#include <vector>

static double SockPairs(double pairs, double cycles)
{
	auto IsInteger = [](const double value) -> bool
		{
			double integral_part;
			return std::modf(value, &integral_part) == 0.0;
		};

	if (!IsInteger(pairs) || pairs < 0.0 || !IsInteger(cycles) || cycles < 0.0)
	{
		return std::numeric_limits<double>::quiet_NaN();
	}

	double numberOfSocks = pairs * 2;

	for (int cycle = 1; cycle <= cycles; cycle++) {
		// Every 2 wash cycles, you lose a single sock.
		if (cycle % 2 == 0) {
			numberOfSocks--;
		}

		// Every 3 wash cycles, you find a single missing sock.
		if (cycle % 3 == 0) {
			numberOfSocks++;
		}

		// Every 5 wash cycles, a single sock is worn out and must be thrown
		// away.
		if (cycle % 5 == 0) {
			numberOfSocks--;
		}

		// Every 10 wash cycles, you buy a pair of socks.
		if (cycle % 10 == 0) {
			numberOfSocks += 2;
		}

		// You can never have less than zero total socks.
		if (numberOfSocks < 0) {
			numberOfSocks = 0;
		}
	}

	// Return the number of complete pairs of socks.
	return std::floor(numberOfSocks / 2);
}

int main()
{
	std::array<std::array<double, 3>, 5> tests =
	{
		{
			{2, 5, 1} ,
			{1, 2, 0},
			{5, 11, 4},
			{6, 25, 3},
			{1, 8, 0},
		}
	};
	std::vector<std::array<double, 2>> failures;
	std::cout << std::boolalpha;

	for (const auto& test : tests)
	{
		const auto& pairs = test[0];
		const auto& cycles = test[1];
		const auto& expected = test[2];
		std::cout << "Testing " << pairs << " and " << cycles << " (expecting "
			<< expected << ")...";
		const auto& actual = SockPairs(pairs, cycles);
		std::cout << actual;
		const auto& success = expected == actual;
		std::cout << " (success: " << success << ")." << std::endl;

		if (!success)
		{
			failures.push_back({ pairs, cycles });
		}
	}

	std::cout << std::noboolalpha;

	if (failures.size() > 0)
	{
		std::cout << "The following inputs failed:" << std::endl;

		for (const auto& failure : failures)
		{
			const auto& pairs = failure[0];
			const auto& cycles = failure[1];
			std::cout << "  " << pairs << " and " << cycles << '.';
		}
	}
	else
	{
		std::cout << "All tests passed!" << std::endl;
	}
}
