// DecimalToBinary.cpp : This file contains the 'main' function. Program
// execution begins and ends there.
// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-02
// Using C++ 17.

#include <algorithm>
#include <any>
#include <array>
#include <cmath>
#include <iostream>
#include <limits>
#include <numeric>
#include <stdexcept>
#include <string>
#include <vector>

static std::string ToBinary(const int& decimal)
{
    if (decimal < 0)
    {
        throw std::invalid_argument("decimal must be zero or positive.");
    }

    if (decimal == 0)
    {
        return "0";
    }

    auto value = decimal;
    std::vector<char> remainders = {};

    while (value > 0)
    {
        remainders.push_back(static_cast<char>('0' + (value % 2)));
        value = static_cast<int>(std::floor(static_cast<double>(value) / 2));
    }

    std::reverse(remainders.begin(), remainders.end());

    return std::accumulate(remainders.begin(), remainders.end(),
        std::string(""));
}

static int CastDecimal(const std::any& decimal);

int main()
{
    try
    {
        std::array<std::array<std::any, 2>, 4> tests =
        {
            {
                { 5, std::string("101") },
                { 12, std::string("1100") },
                { 50, std::string("110010") },
                { 99, std::string("1100011") },
            }
        };
        std::vector<int> failures = {};
        std::cout << std::boolalpha;

        for (const auto& test : tests)
        {
            const auto& decimal = CastDecimal(test[0]);
            const auto& expected = std::any_cast<std::string>(test[1]);
            std::cout << "Testing " << decimal << " (expecting \"" << expected
                << "\")...";
            const auto& actual = ToBinary(decimal);
            std::cout << '\"' << actual << '\"';
            bool success = expected == actual;
            std::cout << " (success: " << success << ")." << std::endl;

            if (!success)
            {
                failures.push_back(decimal);
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

static int CastDecimal(const std::any& decimal)
{
    try
    {
        return std::any_cast<int>(decimal);
    }
    catch (const std::exception&)
    {
        try
        {
            auto value = std::any_cast<double>(decimal);

            // Handle special floating-point values
            if (std::isinf(value) || std::isnan(value) || !std::isfinite(value))
            {
                return false; // Infinity and NaN are not considered integers
            }

            double integralPart;
            double fractionalPart = std::modf(value, &integralPart);

            // An integer has a fractional part of 0.0.
            // We use a small epsilon for comparison to account for potential
            // floating-point inaccuracies.
            if (std::fabs(fractionalPart)
                >= std::numeric_limits<double>::epsilon())
            {
                throw std::invalid_argument("decimal must be an integer.");
            }

            return static_cast<int>(value);
        }
        catch (const std::bad_any_cast&)
        {
            try
            {
                auto value = std::any_cast<float>(decimal);

                // Handle special floating-point values
                if (std::isinf(value) || std::isnan(value)
                    || !std::isfinite(value))
                { // Infinity and NaN are not considered integers
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
                    throw std::invalid_argument("decimal must be an integer.");
                }

                return static_cast<int>(value);
            }
            catch (const std::bad_any_cast&)
            {
                throw std::invalid_argument("decimal must be an integer.");
            }
        }
    }
}
