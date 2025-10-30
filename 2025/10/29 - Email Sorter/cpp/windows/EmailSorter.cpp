// EmailSorter.cpp : This file contains the 'main' function. Program execution
// begins and ends there.
// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-29

#include <algorithm>
#include <array>
#include <cctype>
#include <iostream>
#include <sstream>
#include <stdexcept>
#include <string>
#include <vector>

static std::vector<std::string> Sort(const std::vector<std::string>& emails)
{
	std::vector<std::string> sortedEmails = emails;

	auto parseEmail = [](const std::string& email) -> std::array<std::string, 2>
		{
			std::string emailLowerCase = email;
			std::transform(emailLowerCase.begin(), emailLowerCase.end(),
				emailLowerCase.begin(), [](unsigned char character) -> int
				{
					return std::tolower(character);
				});

			std::istringstream emailPartsStream(emailLowerCase);
			std::vector<std::string> emailParts;
			std::string emailPart;

			while (std::getline(emailPartsStream, emailPart, '@'))
			{
				emailParts.push_back(emailPart);
			}

			if (emailParts.size() != 2)
			{
				throw std::invalid_argument("email address is invalid format.");
			}

			return { emailParts[1], emailParts[0] };
		};

	std::sort(sortedEmails.begin(), sortedEmails.end(),
		[parseEmail](const std::string& email1, const std::string& email2)
		-> bool
		{
			const auto& parsedEmail1 = parseEmail(email1);
			const auto& parsedEmail2 = parseEmail(email2);
			const auto& domainCompare = parsedEmail1[0].compare(
				parsedEmail2[0]);

			if (domainCompare != 0)
			{
				return domainCompare < 0;
			}

			return parsedEmail1[1] < parsedEmail2[1];
		});

	return sortedEmails;
}

static std::string OutputToString(std::vector<std::string> output);

int main()
{
	try
	{
		std::vector<std::array<std::vector<std::string>, 2>> tests =
		{
			{{{ "jill@mail.com", "john@example.com", "jane@example.com" },
			{ "jane@example.com", "john@example.com", "jill@mail.com" }}},
			{{{ "bob@mail.com", "alice@zoo.com", "carol@mail.com" },
			{ "bob@mail.com", "carol@mail.com", "alice@zoo.com" }}},
			{{{ "user@z.com", "user@y.com", "user@x.com" },
			{ "user@x.com", "user@y.com", "user@z.com" }}},
			{{{ "sam@MAIL.com", "amy@mail.COM", "bob@Mail.com" },
			{ "amy@mail.COM", "bob@Mail.com", "sam@MAIL.com" }}},
			{{{ "simon@beta.com", "sammy@alpha.com", "Sarah@Alpha.com",
				"SAM@ALPHA.com", "Simone@Beta.com", "sara@alpha.com" },
			{ "SAM@ALPHA.com", "sammy@alpha.com", "sara@alpha.com",
				"Sarah@Alpha.com", "simon@beta.com", "Simone@Beta.com" }}},
		};
		std::vector<std::vector<std::string>> failures = {};
		std::cout << std::boolalpha;

		for (const auto& test : tests)
		{
			const auto& emails = test[0];
			const auto& expected = test[1];
			std::cout << "Testing " << OutputToString(emails) << " (expecting "
				<< OutputToString(expected) << ")...";
			const auto& actual = Sort(emails);
			std::cout << OutputToString(actual);
			const auto& success = expected == actual;
			std::cout << " (success: " << success << ")." << std::endl;

			if (!success)
			{
				failures.push_back(emails);
			}
		}

		std::cout << std::noboolalpha;

		if (failures.size() > 0)
		{
			std::cout << "The following inputs failed:" << std::endl;

			for (const auto& failure : failures)
			{
				std::cout << "  " << OutputToString(failure) << '.'
					<< std::endl;
			}
		}
		else
		{
			std::cout << "All tests passed!";
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
