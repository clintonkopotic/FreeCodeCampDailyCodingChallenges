// MatrixBuilder.cpp : This file contains the 'main' function. Program execution
// begins and ends there.
// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-11-05
// Uses C++ 17.

#include <any>
#include <array>
#include <iostream>
#include <sstream>
#include <stdexcept>
#include <vector>

static std::vector<std::vector<int>> BuildMatrix(const size_t& rows,
	const size_t& columns)
{
	std::vector<std::vector<int>> matrix = {};
	std::vector<int> row = {};

	for (size_t j = 0; j < columns; j++)
	{
		row.push_back(0);
	}

	for (size_t i = 0; i < rows; i++)
	{
		matrix.push_back(row);
	}

	return matrix;
}

static std::string MatrixToString(std::vector<std::vector<int>> matrix);

int main()
{
	try
	{
		std::vector<std::array<std::any, 3>> tests;

		std::vector<std::vector<int>> test0expected
			= { { { 0, 0, 0 }, { 0, 0, 0 } }, };
		tests.push_back({ 2, 3, test0expected });

		std::vector<std::vector<int>> test1expected
			= { { { 0, 0 }, { 0, 0 }, { 0, 0 } }, };
		tests.push_back({ 3, 2, test1expected });

		std::vector<std::vector<int>> test2expected
			= { { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } }, };
		tests.push_back({ 4, 3, test2expected });

		std::vector<std::vector<int>> test3expected
			= { { { 0 } }, { { 0 } }, { { 0 } }, { { 0 } }, { { 0 } },
				{ { 0 } }, { { 0 } }, { { 0 } }, { { 0 } }, };
		tests.push_back({ 9, 1, test3expected });

		std::vector<std::array<int, 2>> failures = {};
		std::cout << std::boolalpha;

		for (const auto& test : tests)
		{
			const auto& rows = std::any_cast<int>(test[0]);
			const auto& columns = std::any_cast<int>(test[1]);
			const auto& expected = std::any_cast<std::vector<std::vector<int>>>(
				test[2]);
			std::cout << "Testing " << rows << " and " << columns
				<< " (expecting " << MatrixToString(expected) << ")...";
			const auto& actual = BuildMatrix(rows, columns);
			std::cout << MatrixToString(actual);
			bool success = expected == actual;
			std::cout << " (success: " << success << ")." << std::endl;

			if (!success)
			{
				failures.push_back({ rows, columns });
			}
		}

		std::cout << std::noboolalpha;

		if (failures.size() > 0)
		{
			std::cout << "The following inputs failed:" << std::endl;

			for (const auto& failure : failures)
			{
				const auto& rows = failure[0];
				const auto& columns = failure[1];
				std::cout << "  " << rows << " and " << columns << '.'
					<< std::endl;
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

static std::string MatrixToString(std::vector<std::vector<int>> matrix)
{
	std::ostringstream result;
	result << '[';

	for (size_t i = 0; i < matrix.size(); i++)
	{
		const auto& row = matrix[i];

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

			result << row[j];
		}

		result << ']';
	}

	result << ']';

	return result.str();
}
