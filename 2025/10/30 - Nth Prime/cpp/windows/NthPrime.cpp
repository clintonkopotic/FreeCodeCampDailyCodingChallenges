// NthPrime.cpp : This file contains the 'main' function. Program execution
// begins and ends there.
// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-30

#include <array>
#include <cmath>
#include <iostream>
#include <sstream>
#include <stdexcept>
#include <vector>

static double NthPrime(const double& n)
{
	if (!std::isfinite(n) || n != std::trunc(n) || n <= 0)
	{
		std::ostringstream exceptionMessage;
		exceptionMessage << "The argument \"n\" must be an integer and "
			"positive (n: " << n << ").";

		throw std::invalid_argument(exceptionMessage.str());
	}

	auto isPrime = [](double n) -> bool
		{
			if (!std::isfinite(n) || n != std::trunc(n) || n <= 1)
			{
				std::ostringstream exceptionMessage;
				exceptionMessage << "The argument \"n\" must be an integer and "
					"greater than one (n: " << n << ").";

				throw std::invalid_argument(exceptionMessage.str());
			}

			// 2 is the only even prime number.
			if (n == 2)
			{
				return true;
			}

			// Even numbers greater than 2 are not prime.
			if (std::fmod(n, 2) == 0)
			{
				return false;
			}

			// Check for divisibility by odd numbers from 3 up to the square
			// root of num.
			// We only need to check up to the square root because if a number
			// has a divisor greater than its square root, it must also have a
			// divisor smaller than its square root.
			double limit = std::sqrt(n);

			for (double i = 3; i <= limit; i += 2)
			{
				// If divisible, it's not prime.
				if (std::fmod(n, i) == 0)
				{
					return false;
				}
			}

			// If no divisors were found, it's prime.
			return true;
		};

	double prime = 2;

	for (size_t i = 1; i < n; i++)
	{
		double number = prime + 1;

		while (!isPrime(number))
		{
			number++;
		}

		prime = number;
	}

	return prime;
}

int main()
{
	try
	{
		std::vector<std::array<double, 2>> tests =
		{
			{ 5, 11 },
			{ 10, 29 },
			{ 16, 53 },
			{ 99, 523 },
			{ 1000, 7919 },
		};
		std::vector<double> failures = {};
		std::cout << std::boolalpha;

		for (const auto& test : tests)
		{
			const auto& n = test[0];
			const auto& expected = test[1];
			std::cout << "Testing " << n << " (expecting "
				<< expected << ")...";
			const auto& actual = NthPrime(n);
			std::cout << actual;
			bool success = expected == actual;
			std::cout << " (success: " << success << ")." << std::endl;

			if (!success)
			{
				failures.push_back(n);
			}
		}

		std::cout << std::noboolalpha;

		if (failures.size() > 0)
		{
			std::cout << "The following inputs failed:" << std::endl;

			for (const auto& failure : failures)
			{
				std::cout << "  " << failure << '.' << std::endl;
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
