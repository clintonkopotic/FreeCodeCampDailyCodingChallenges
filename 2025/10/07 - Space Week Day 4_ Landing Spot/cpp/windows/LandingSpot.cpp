// LandingSpot.cpp : This file contains the 'main' function. Program execution
// begins and ends there.
// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-07
// Uses C++ 17

#include <any>
#include <array>
#include <cmath>
#include <iostream>
#include <sstream>
#include <stdexcept>
#include <vector>

static std::array<size_t, 2> FindLandingSpot(
	const std::vector<std::vector<double>>& matrix)
{
	size_t rows = matrix.size();

	if (rows == 0)
	{
		throw std::invalid_argument("matrix must have at least one row.");
	}

	auto IsInteger = [](const double value) -> bool
		{
			double integral_part;
			return std::modf(value, &integral_part) == 0.0;
		};

	// Traverse the matix first to ensure all elements are numbers between 0 and
	// 9, inclusive, and to ensure each array in the array have the same number
	// of columns in them.
	size_t columns = 0;

	for (const auto& row : matrix)
	{
		if (row.size() == 0)
		{
			throw std::invalid_argument("matrix must have non-empty rows.");
		}
		else if (columns > 0 && columns != row.size())
		{
			throw std::invalid_argument("matrix must have the same number of "
				"columns in each row.");
		}

		if (columns == 0)
		{
			columns = row.size();
		}

		for (const double value : row)
		{
			if (!IsInteger(value))
			{
				throw std::invalid_argument("matrix must have all integers as "
					"values.");
			}
			else if (value < 0 || value > 9)
			{
				throw std::invalid_argument("matrix must have integer values "
					"between zero and nine, inclusive.");
			}
		}
	}

	// Now traverse the matix to find the landing spot.
	bool haveSetMin = false;
	double minNeighborTotal = 0;
	size_t landingSpotRowIndex = -1;
	size_t landingSpotColumnIndex = -1;

	for (size_t i = 0; i < rows; i++)
	{
		for (size_t j = 0; j < columns; j++)
		{
			if (matrix[i][j] != 0)
			{
				continue;
			}

			double northNeighborValue = i > 0 ? matrix[i - 1][j] : 0;
			double eastNeighborValue = j < (columns - 1) ? matrix[i][j + 1] : 0;
			double southNeighborValue = i < (rows - 1) ? matrix[i + 1][j] : 0;
			double westNeighborsValue = j > 0 ? matrix[i][j - 1] : 0;
			double neighborTotal = northNeighborValue + eastNeighborValue
				+ southNeighborValue + westNeighborsValue;

			if (!haveSetMin || neighborTotal < minNeighborTotal)
			{
				minNeighborTotal = neighborTotal;
				landingSpotRowIndex = i;
				landingSpotColumnIndex = j;
				haveSetMin = true;
			}
		}
	}

	return { landingSpotRowIndex, landingSpotColumnIndex };
}

static std::string MatrixToString(std::vector<std::vector<double>> matrix);
static std::string OutputToString(std::array<size_t, 2> output);

int main()
{
	try
	{
		std::array<std::array<std::any, 2>, 4> tests;
		std::vector<std::vector<double>> test0inputs =
		{
			{ 1, 0 },
			{ 2, 0 }
		};
		std::array<size_t, 2> test0expected = { 0, 1 };
		tests[0][0] = test0inputs;
		tests[0][1] = test0expected;

		std::vector<std::vector<double>> test1inputs =
		{
			{ 9, 0, 3 },
			{ 7, 0, 4 },
			{ 8, 0, 5 }
		};
		std::array<size_t, 2> test1expected = { 1, 1 };
		tests[1][0] = test1inputs;
		tests[1][1] = test1expected;

		std::vector<std::vector<double>> test2inputs =
		{
			{ 1, 2, 1},
			{ 0, 0, 2 },
			{ 3, 0, 0 }
		};
		std::array<size_t, 2> test2expected = { 2, 2 };
		tests[2][0] = test2inputs;
		tests[2][1] = test2expected;

		std::vector<std::vector<double>> test3inputs =
		{
			{ 9, 6, 0, 8 },
			{ 7, 1, 1, 0 },
			{ 3, 0, 3, 9 },
			{ 8, 6, 0, 9 }
		};
		std::array<size_t, 2> test3expected = { 2, 1 };
		tests[3][0] = test3inputs;
		tests[3][1] = test3expected;

		std::vector<std::vector<std::vector<double>>> failures = {};
		std::cout << std::boolalpha;

		for (size_t i = 0; i < tests.size(); i++)
		{
			std::array<std::any, 2> test = tests[i];
			std::vector<std::vector<double>> matrix
				= std::any_cast<std::vector<std::vector<double>>>(test[0]);
			std::array<size_t, 2> expected
				= std::any_cast<std::array<size_t, 2>>(test[1]);
			std::cout << "Testing " << MatrixToString(matrix) << " (expecting "
				<< OutputToString(expected) << ")...";
			std::array<size_t, 2> actual = FindLandingSpot(matrix);
			std::cout << OutputToString(actual);
			bool success = expected == actual;
			std::cout << " (success: " << success << ")." << std::endl;

			if (!success)
			{
				failures.push_back(matrix);
			}
		}

		std::cout << std::noboolalpha;

		if (failures.size() > 0)
		{
			std::cout << "The following inputs failed:" << std::endl;

			for (size_t i = 0; i < failures.size(); i++)
			{
				std::vector<std::vector<double>> failure = failures[i];
				std::cout << "  " << MatrixToString(failure) << '.'
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

static std::string MatrixToString(std::vector<std::vector<double>> matrix)
{
	std::ostringstream result;
	result << '[';

	for (size_t i = 0; i < matrix.size(); i++)
	{
		std::vector<double> row = matrix[i];

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

static std::string OutputToString(std::array<size_t, 2> output)
{
	std::ostringstream result;
	result << '[' << output[0] << ", " << output[1] << ']';

	return result.str();
}
