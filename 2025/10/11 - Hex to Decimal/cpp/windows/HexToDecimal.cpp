// HexToDecimal.cpp : This file contains the 'main' function. Program execution
// begins and ends there.
// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-11
// Using C++17.

#include <any>
#include <array>
#include <cctype>
#include <cmath>
#include <iostream>
#include <limits>
#include <vector>

static double HexToDecimal(std::string hex)
{
    if (hex.length() <= 0)
    {
        return std::numeric_limits<double>::quiet_NaN();
    }

    std::string digits = "0123456789ABCDEF";
    double exponent = 0;
    double result = 0;

    for (std::string::reverse_iterator iterator = hex.rbegin();
        iterator != hex.rend(); ++iterator)
    {
        char digit = std::toupper(*iterator);
        double value = digits.find(digit);

        if (value < 0)
        {
            return std::numeric_limits<double>::quiet_NaN();
        }

        double digitValue = value * std::pow(16, exponent);
        result += digitValue;
        exponent++;
    }

    return result;
}

int main()
{
    std::array<std::array<std::any, 2>, 5> tests =
    {
        {
            {std::string("A"), 10.0},
            {std::string("15"), 21.0},
            {std::string("2E"), 46.0},
            {std::string("FF"), 255.0},
            {std::string("A3F"), 2623.0},
        }
    };
    std::vector<std::string> failures = {};
    std::cout << std::boolalpha;

    for (size_t i = 0; i < tests.size(); i++)
    {
        std::array<std::any, 2> test = tests[i];
        std::string hex = std::any_cast<std::string>(test[0]);
        double expected = std::any_cast<double>(test[1]);
        std::cout << "Testing \"" << hex << "\" (expecting " << expected
            << ")...";
        double actual = HexToDecimal(hex);
        std::cout << actual;
        bool success = expected == actual;
        std::cout << " (success: " << success << ")." << std::endl;

        if (!success)
        {
            failures.push_back(hex);
        }
    }

    std::cout << std::noboolalpha;

    if (failures.size() > 0)
    {
        std::cout << "The following inputs failed:" << std::endl;

        for (size_t i = 0; i < failures.size(); i++)
        {
            std::cout << "  \"" << failures[i] << "\"." << std::endl;
        }
    }
    else
    {
        std::cout << "All tests passed!" << std::endl;
    }
}
