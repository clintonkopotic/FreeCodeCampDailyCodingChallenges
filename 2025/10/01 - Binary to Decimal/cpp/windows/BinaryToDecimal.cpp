// BinaryToDecimal.cpp : This file contains the 'main' function. Program
// execution begins and ends there.
// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-01
// Uses C++ 17.

#include <any>
#include <array>
#include <cmath>
#include <iostream>
#include <stdexcept>
#include <string>
#include <vector>

static double ToDecimal(const std::string& binary)
{
    if (binary.length() == 0)
    {
        throw std::invalid_argument("Cannot be empty.");
    }

    double result = 0;

    for (size_t i = binary.length() - 1, j = 0; i < binary.length() && i >= 0;
        i--, j++)
    {
        const auto& digit = binary[i];

        if (digit == '1')
        {
            result += std::pow(2, j);
        }
        else if (digit != '0')
        {
            throw std::invalid_argument("Must only contain \'0\' and \'1\' "
                "characters.");
        }
    }

    return result;
}

static double CastExpected(const std::any& expected);

int main()
{
    try
    {
        std::array<std::array<std::any, 2>, 4> tests =
        {
            {
                { std::string("101"), 5 },
                { std::string("1010"), 10 },
                { std::string("10010"), 18 },
                { std::string("1010101"), 85 },
            }
        };
        std::vector<std::string> failures = {};
        std::cout << std::boolalpha;

        for (const auto& test : tests)
        {
            const auto& binary = std::any_cast<std::string>(test[0]);
            const auto& expected = CastExpected(test[1]);
            std::cout << "Testing \"" << binary << "\" (expecting " << expected
                << ")...";
            const auto& actual = ToDecimal(binary);
            std::cout << actual;
            bool success = expected == actual;
            std::cout << " (success: " << success << ")." << std::endl;

            if (!success)
            {
                failures.push_back(binary);
            }
        }

        std::cout << std::noboolalpha;

        if (failures.size() > 0)
        {
            std::cout << "The following inputs failed:" << std::endl;

            for (const auto& failure : failures)
            {
                std::cout << "  \"" << failure << "\"." << std::endl;
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

static double CastExpected(const std::any& expected)
{
    try
    {
        return std::any_cast<double>(expected);
    }
    catch (const std::exception&)
    {
        try
        {
            auto value = std::any_cast<int>(expected);

            return static_cast<int>(value);
        }
        catch (const std::bad_any_cast&)
        {
            try
            {
                auto value = std::any_cast<float>(expected);

                return static_cast<double>(value);
            }
            catch (const std::bad_any_cast&)
            {
                throw std::invalid_argument("expected must be a number.");
            }
        }
    }
}
