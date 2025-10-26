// HiddenTreasure.cpp : This file contains the 'main' function. Program
// execution begins and ends there.
// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-24
// Using C++ 17.

#include <any>
#include <array>
#include <cmath>
#include <iostream>
#include <set>
#include <sstream>
#include <stdexcept>
#include <vector>

static std::string Drive(const std::vector<std::vector<char>>& map,
	const std::array<size_t, 2>& coordinates)
{
	size_t rows = map.size();

	if (rows == 0)
	{
		throw std::invalid_argument("map must have at least one row.");
	}

	// Traverse the map first to ensure all elements are members of cellValues,
	// and to ensure each vector in the vector have the same number
	// of columns in them.
	size_t columns = 0;
	size_t unfoundPartsCount = 0;
	const std::set<char> cellValues = { '-', 'O', 'X' };

	for (const auto& row : map)
	{
		if (row.size() == 0)
		{
			throw std::invalid_argument("map must have non-empty rows.");
		}
		else if (columns != 0 && columns != row.size())
		{
			throw std::invalid_argument("map must have the same number of "
				"columns in each row.");
		}

		if (columns == 0)
		{
			columns = row.size();
		}

		for (const auto& cell : row)
		{
			if (cellValues.find(cell) == cellValues.end())
			{
				throw std::invalid_argument("map must have all valid values.");
			}

			if (cell == 'O')
			{
				unfoundPartsCount++;
			}
		}
	}

	const auto& rowIndex = coordinates[0];

	if (rowIndex >= rows)
	{
		throw std::logic_error("rowIndex is greater or equal to rows.");
	}

	const auto& columnIndex = coordinates[1];

	if (columnIndex >= columns)
	{
		throw std::logic_error("columnIndex is greater or equal to columns.");
	}

	const auto& cell = map[rowIndex][columnIndex];

	if (cell == '-')
	{
		return "Empty";
	}
	else if (cell == 'O')
	{
		unfoundPartsCount--;
	}

	return unfoundPartsCount == 0 ? "Recovered" : "Found";
}

static std::string MapToString(std::vector<std::vector<char>> map);
static std::string CoordinatesToString(std::array<size_t, 2> coordinates);

int main()
{
	try
	{
		std::array<std::array<std::any, 3>, 6> tests;
		std::vector<std::vector<char>> test0map =
		{
			{ '-', 'X' },
			{ '-', 'X' },
			{ '-', 'O' },
		};
		std::array<size_t, 2> test0coordinates = { 2, 1 };
		std::string test0expected = "Recovered";
		tests[0][0] = test0map;
		tests[0][1] = test0coordinates;
		tests[0][2] = test0expected;

		std::vector<std::vector<char>> test1map =
		{
			{ '-', 'X' },
			{ '-', 'X' },
			{ '-', 'O' },
		};
		std::array<size_t, 2> test1coordinates = { 2, 0 };
		std::string test1expected = "Empty";
		tests[1][0] = test1map;
		tests[1][1] = test1coordinates;
		tests[1][2] = test1expected;

		std::vector<std::vector<char>> test2map =
		{
			{ '-', 'X' },
			{ '-', 'O' },
			{ '-', 'O' },
		};
		std::array<size_t, 2> test2coordinates = { 1, 1 };
		std::string test2expected = "Found";
		tests[2][0] = test2map;
		tests[2][1] = test2coordinates;
		tests[2][2] = test2expected;

		std::vector<std::vector<char>> test3map =
		{
			{ '-', '-', '-' },
			{ 'X', 'O', 'X' },
			{ '-', '-', '-' },
		};
		std::array<size_t, 2> test3coordinates = { 1, 2 };
		std::string test3expected = "Found";
		tests[3][0] = test3map;
		tests[3][1] = test3coordinates;
		tests[3][2] = test3expected;

		std::vector<std::vector<char>> test4map =
		{
			{ '-', '-', '-' },
			{ '-', '-', '-' },
			{ 'O', 'X', 'X' },
		};
		std::array<size_t, 2> test4coordinates = { 2, 0 };
		std::string test4expected = "Recovered";
		tests[4][0] = test4map;
		tests[4][1] = test4coordinates;
		tests[4][2] = test4expected;

		std::vector<std::vector<char>> test5map =
		{
			{ '-', '-', '-' },
			{ '-', '-', '-' },
			{ 'O', 'X', 'X' },
		};
		std::array<size_t, 2> test5coordinates = { 1, 2 };
		std::string test5expected = "Empty";
		tests[5][0] = test5map;
		tests[5][1] = test5coordinates;
		tests[5][2] = test5expected;

		std::vector<std::array<std::any, 2>> failures = {};
		std::cout << std::boolalpha;

		for (const auto& test : tests)
		{
			const auto& map = std::any_cast<std::vector<std::vector<char>>>(
				test[0]);
			const auto& coordinates = std::any_cast<std::array<size_t, 2>>(
				test[1]);
			const auto& expected = std::any_cast<std::string>(test[2]);
			std::cout << "Testing " << MapToString(map) << " and "
				<< CoordinatesToString(coordinates) << " (expecting \""
				<< expected << "\")...";
			const auto& actual = Drive(map, coordinates);
			std::cout << '\"' << actual << '\"';
			bool success = expected == actual;
			std::cout << " (success: " << success << ")." << std::endl;

			if (!success)
			{
				failures.push_back({ map, coordinates });
			}
		}

		std::cout << std::noboolalpha;

		if (failures.size() > 0)
		{
			std::cout << "The following inputs failed:" << std::endl;

			for (const auto& failure : failures)
			{
				const auto& map = std::any_cast<std::vector<std::vector<char>>>(
					failure[0]);
				const auto& coordinates = std::any_cast<std::array<size_t, 2>>(
					failure[1]);
				std::cout << "  " << MapToString(map) << " and "
					<< CoordinatesToString(coordinates) << '.' << std::endl;
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

static std::string MapToString(std::vector<std::vector<char>> map)
{
	std::ostringstream result;
	result << '[';

	for (size_t i = 0; i < map.size(); i++)
	{
		const auto& row = map[i];

		if (i > 0)
		{
			result << ", ";
		}

		result << '[';

		for (size_t j = 0; j < row.size(); j++)
		{
			if (j > 0)
			{
				result << ", ";
			}

			result << '\'' << row[j] << '\'';
		}

		result << ']';
	}

	result << ']';

	return result.str();
}

static std::string CoordinatesToString(std::array<size_t, 2> coordinates)
{
	std::ostringstream result;
	result << '[' << coordinates[0] << ", " << coordinates[1] << ']';

	return result.str();
}
