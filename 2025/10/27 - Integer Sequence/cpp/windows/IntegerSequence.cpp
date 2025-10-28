// IntegerSequence.cpp : This file contains the 'main' function. Program
// execution begins and ends there.
// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-27
// Uses C++ 17.

#include <any>
#include <array>
#include <iostream>
#include <stdexcept>
#include <sstream>
#include <string>
#include <vector>

static std::string Sequence(const int& n)
{
    if (n <= 0)
    {
        throw std::invalid_argument("seconds must be positive.");
    }

    std::ostringstream result;

    for (int i = 1; i <= n; i++)
    {
        result << i;
    }

    return result.str();
}

static int CastN(const std::any& n);

int main()
{
    try
    {
        std::array<std::array<std::any, 2>, 4> tests =
        {
            {
                { 5, std::string("12345") },
                { 10, std::string("12345678910") },
                { 1, std::string("1") },
                { 27, std::string("123456789101112131415161718192021222324252627") },
            }
        };
        std::vector<int> failures = {};
        std::cout << std::boolalpha;

        for (const auto& test : tests)
        {
            const auto& decimal = CastN(test[0]);
            const auto& expected = std::any_cast<std::string>(test[1]);
            std::cout << "Testing " << decimal << " (expecting \"" << expected
                << "\")...";
            const auto& actual = Sequence(decimal);
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

static int CastN(const std::any& n)
{
    try
    {
        return std::any_cast<int>(n);
    }
    catch (const std::exception&)
    {
        try
        {
            auto value = std::any_cast<double>(n);

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
                throw std::invalid_argument("n must be an integer.");
            }

            return static_cast<int>(value);
        }
        catch (const std::bad_any_cast&)
        {
            try
            {
                auto value = std::any_cast<float>(n);

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
                    throw std::invalid_argument("n must be an integer.");
                }

                return static_cast<int>(value);
            }
            catch (const std::bad_any_cast&)
            {
                throw std::invalid_argument("n must be an integer.");
            }
        }
    }
}
