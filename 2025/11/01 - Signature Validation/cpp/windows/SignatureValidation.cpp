// SignatureValidation.cpp : This file contains the 'main' function. Program
// execution begins and ends there.
// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-11-01
// Uses C++ 17.

#include <any>
#include <array>
#include <iostream>
#include <stdexcept>
#include <sstream>
#include <string>
#include <vector>

static bool Verify(const std::string& message, const std::string& key,
	size_t signature)
{
	auto calculateSignature = [](const std::string& string)
		{
			const std::string letters
				= "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
			size_t signature = 0;

			for (const auto& character : string)
			{
				const auto& index = letters.find(character);

				if (index != std::string::npos)
				{
					signature += index + 1;
				}
			}

			return signature;
		};

	const auto& calculatedSignature = calculateSignature(message)
		+ calculateSignature(key);

	return signature == calculatedSignature;
}

static size_t CastSignature(const std::any& signature);

int main()
{
	try
	{
		std::vector<std::array<std::any, 4>> tests =
		{
			{
				{ std::string("foo"), std::string("bar"), 57, true },
				{ std::string("foo"), std::string("bar"), 54, false },
				{ std::string("freeCodeCamp"), std::string("Rocks"), 238,
					true },
				{ std::string("Is this valid?"), std::string("No"), 210,
					false },
				{ std::string("Is this valid?"), std::string("Yes"), 233,
					true },
				{ std::string("Check out the freeCodeCamp podcast,"),
					std::string("in the mobile app"), 514, true },
			}
		};
		std::vector<std::array<std::any, 3>> failures = {};
		std::cout << std::boolalpha;

		for (const auto& test : tests)
		{
			const auto& message = std::any_cast<std::string>(test[0]);
			const auto& key = std::any_cast<std::string>(test[1]);
			const auto& signature = CastSignature(test[2]);
			const auto& expected = std::any_cast<bool>(test[3]);
			std::cout << "Testing \"" << message << "\", \"" << key
				<< "\", and " << signature << " (expecting " << expected
				<< ")...";
			const auto& actual = Verify(message, key, signature);
			std::cout << actual;
			bool success = expected == actual;
			std::cout << " (success: " << success << ")." << std::endl;

			if (!success)
			{
				failures.push_back({ message, key, signature });
			}
		}

		std::cout << std::noboolalpha;

		if (failures.size() > 0)
		{
			std::cout << "The following inputs failed:" << std::endl;

			for (const auto& failure : failures)
			{
				const auto& message = std::any_cast<std::string>(failure[0]);
				const auto& key = std::any_cast<std::string>(failure[1]);
				const auto& signature = CastSignature(failure[2]);
				std::cout << "  \"" << message << "\", \"" << key
					<< "\", and " << signature << '.' << std::endl;
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

static size_t CastSignature(const std::any& signature)
{
	try
	{
		return std::any_cast<size_t>(signature);
	}
	catch (const std::exception&)
	{
		try
		{
			auto value = std::any_cast<int>(signature);

			return static_cast<size_t>(value);
		}
		catch (const std::exception&)
		{
			try
			{
				auto value = std::any_cast<double>(signature);

				// Handle special floating-point values
				if (std::isinf(value) || std::isnan(value)
					|| !std::isfinite(value))
				{ // Infinity and NaN are not considered integers.
					return false;
				}

				double integralPart;
				double fractionalPart = std::modf(value, &integralPart);

				// An integer has a fractional part of 0.0.
				// We use a small epsilon for comparison to account for
				// potential floating-point inaccuracies.
				if (std::fabs(fractionalPart)
					>= std::numeric_limits<double>::epsilon())
				{
					throw std::invalid_argument("n must be an integer.");
				}

				return static_cast<size_t>(value);
			}
			catch (const std::bad_any_cast&)
			{
				try
				{
					auto value = std::any_cast<float>(signature);

					// Handle special floating-point values
					if (std::isinf(value) || std::isnan(value)
						|| !std::isfinite(value))
					{ // Infinity and NaN are not considered integers.
						return false;
					}

					float integralPart;
					float fractionalPart = std::modf(value, &integralPart);

					// An integer has a fractional part of 0.0.
					// We use a small epsilon for comparison to account for
					// potential floating-point inaccuracies.
					if (std::fabs(fractionalPart)
						>= std::numeric_limits<float>::epsilon())
					{
						throw std::invalid_argument("n must be an integer.");
					}

					return static_cast<size_t>(value);
				}
				catch (const std::bad_any_cast&)
				{
					throw std::invalid_argument("n must be an integer.");
				}
			}
		}
	}
}
