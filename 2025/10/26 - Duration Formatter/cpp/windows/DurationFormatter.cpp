// DurationFormatter.cpp : This file contains the 'main' function. Program
// execution begins and ends there.
// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-26
// Uses C++ 17.

#include <any>
#include <array>
#include <iomanip>
#include <iostream>
#include <stdexcept>
#include <sstream>
#include <string>
#include <vector>

static std::string Format(const int& seconds)
{
    if (seconds < 0)
    {
        throw std::invalid_argument("seconds must be zero or positive.");
    }

    const int secondsPerMinute = 60;
    const int minutesPerHour = 60;
    const int secondsPerHour = secondsPerMinute * minutesPerHour;
    auto remaining = seconds;
    int hours = 0;
    std::ostringstream result;

    if (remaining >= secondsPerHour)
    {
        hours = remaining / secondsPerHour;
        remaining %= secondsPerHour;

        if (hours > 0)
        {
            result << hours << ':';
        }
    }

    int minutes = 0;

    if (remaining >= secondsPerMinute)
    {
        minutes = remaining / secondsPerMinute;
        remaining %= secondsPerMinute;
    }

    if (hours > 0)
    {
        result << std::setfill('0') << std::setw(2) << minutes << ':';
    }
    else
    {
        result << minutes << ':' << std::setfill('0') << std::setw(2);
    }

    result << remaining;

    return result.str();
}

static int CastSeconds(const std::any& seconds);

int main()
{
    try
    {
        std::array<std::array<std::any, 2>, 5> tests =
        {
            {
                { 500, std::string("8:20") },
                { 4000, std::string("1:06:40") },
                { 1, std::string("0:01") },
                { 5555, std::string("1:32:35") },
                { 99999, std::string("27:46:39") },
            }
        };
        std::vector<int> failures = {};
        std::cout << std::boolalpha;

        for (const auto& test : tests)
        {
            const auto& decimal = CastSeconds(test[0]);
            const auto& expected = std::any_cast<std::string>(test[1]);
            std::cout << "Testing " << decimal << " (expecting \"" << expected
                << "\")...";
            const auto& actual = Format(decimal);
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

static int CastSeconds(const std::any& seconds)
{
    try
    {
        return std::any_cast<int>(seconds);
    }
    catch (const std::exception&)
    {
        try
        {
            auto value = std::any_cast<double>(seconds);

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
                auto value = std::any_cast<float>(seconds);

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
