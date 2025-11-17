// ImageSearch.cpp : This file contains the 'main' function. Program execution
// begins and ends there.
// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-11-04
// Uses C++ 17

#include <any>
#include <algorithm>
#include <array>
#include <cctype>
#include <iostream>
#include <sstream>
#include <string>
#include <vector>

static std::vector<std::string> ImageSearch(
	const std::vector<std::string>& images, const std::string& term)
{
	auto containsCaseInsensitive = [](const std::string& text,
		const std::string& substring) -> bool
		{
			return std::search(text.begin(), text.end(),
				substring.begin(), substring.end(),
				[](unsigned char c1, unsigned char c2) -> bool
				{
					return std::tolower(c1) == std::tolower(c2);
				}) != text.end();
		};

	std::vector<std::string> result = {};

	for (const auto& image : images)
	{
		if (containsCaseInsensitive(image, term))
		{
			result.push_back(image);
		}
	}

	return result;
}

static std::string OutputToString(std::vector<std::string> output);

int main()
{
	try
	{
		std::vector<std::array<std::any, 3>> tests;

		std::vector<std::string> test0images = { "dog.png", "cat.jpg",
			"parrot.jpeg" };
		std::string test0term = "dog";
		std::vector<std::string> test0expected = { "dog.png" };
		tests.push_back({ test0images, test0term, test0expected });

		std::vector<std::string> test1images = { "Sunset.jpg", "Beach.png",
			"sunflower.jpeg" };
		std::string test1term = "sun";
		std::vector<std::string> test1expected = { "Sunset.jpg",
			"sunflower.jpeg" };
		tests.push_back({ test1images, test1term, test1expected });

		std::vector<std::string> test2images = { "Moon.png", "sun.jpeg",
			"stars.png" };
		std::string test2term = "PNG";
		std::vector<std::string> test2expected = { "Moon.png", "stars.png" };
		tests.push_back({ test2images, test2term, test2expected });

		std::vector<std::string> test3images = { "cat.jpg", "dogToy.jpeg",
			"kitty-cat.png", "catNip.jpeg", "franken_cat.gif" };
		std::string test3term = "Cat";
		std::vector<std::string> test3expected = { "cat.jpg", "kitty-cat.png",
			"catNip.jpeg", "franken_cat.gif" };
		tests.push_back({ test3images, test3term, test3expected });

		std::vector<std::array<std::any, 2>> failures = {};
		std::cout << std::boolalpha;

		for (const auto& test : tests)
		{
			const auto& images = std::any_cast<std::vector<std::string>>(
				test[0]);
			const auto& term = std::any_cast<std::string>(test[1]);
			const auto& expected = std::any_cast<std::vector<std::string>>(
				test[2]);
			std::cout << "Testing " << OutputToString(images)
				<< " and \"" << term << "\" (expecting "
				<< OutputToString(expected) << ")...";
			const auto& actual = ImageSearch(images, term);
			std::cout << OutputToString(actual);
			bool success = expected == actual;
			std::cout << " (success: " << success << ")." << std::endl;

			if (!success)
			{
				failures.push_back({ images, term });
			}
		}

		std::cout << std::noboolalpha;

		if (failures.size() > 0)
		{
			std::cout << "The following inputs failed:" << std::endl;

			for (const auto& failure : failures)
			{
				const auto& images = std::any_cast<std::vector<std::string>>(
					failure[0]);
				const auto& term = std::any_cast<std::string>(failure[1]);

				std::cout << "  " << OutputToString(images) << " and \"" << term
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
