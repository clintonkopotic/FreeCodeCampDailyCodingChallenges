// SpeakWiselyYouMust.cpp : This file contains the 'main' function. Program
// execution begins and ends there.
// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-22

#include <algorithm>
#include <array>
#include <cctype>
#include <iostream>
#include <sstream>
#include <string>
#include <vector>

static std::string WiseSpeak(const std::string& sentence)
{
	if (sentence.length() == 0)
	{
		return "";
	}

	std::vector<std::string> words;
	std::stringstream ss(sentence);
	std::string word;

	while (ss >> word) { // Extracts words separated by whitespace
		words.push_back(word);
	}

	if (words.size() == 0)
	{
		"";
	}

	// All given sentences will end with a single punctuation mark. Keep the
	// original punctuation of the sentence and move it to the end of the new
	// sentence.
	std::string lastWord = words[words.size() - 1];
	char sentencePunctuationMark = lastWord[lastWord.length() - 1];

	// Find the first occurrence of one of the following words in the sentence:
	// "have", "must", "are", "will", "can".
	const std::array<std::string, 5> newEndingWords
		= { "have", "must", "are", "will", "can" };
	size_t newEndingWordIndex = std::distance(words.begin(), std::find_first_of(
		words.begin(), words.end(), newEndingWords.begin(),
		newEndingWords.end()));

	if (newEndingWordIndex >= words.size())
	{
		throw std::invalid_argument("sentence does not contain a word in "
			"newEndingWords.");
	}

	std::vector<std::string> newWordsOrder = {};
	std::string newFirstWord = words[newEndingWordIndex + 1];
	newWordsOrder.push_back(std::string(1, std::toupper(newFirstWord[0]))
		+ newFirstWord.substr(1));

	if (newEndingWordIndex + 2 < words.size() - 1)
	{
		std::vector<std::string> sliced_vector(
			words.begin() + newEndingWordIndex + 2,
			words.end() - 1);

		for (const auto& item : sliced_vector)
		{
			newWordsOrder.push_back(item);
		}
	}

	newWordsOrder.push_back(lastWord.substr(0, lastWord.length() - 1) + ",");
	std::string firstWord = words[0];
	std::transform(firstWord.begin(), firstWord.end(), firstWord.begin(),
		std::tolower);
	newWordsOrder.push_back(firstWord);

	if (newEndingWordIndex > 1)
	{
		std::vector<std::string> sliced_vector(words.begin() + 1,
			words.begin() + newEndingWordIndex);

		for (const auto& item : sliced_vector)
		{
			newWordsOrder.push_back(item);
		}
	}

	newWordsOrder.push_back(words[newEndingWordIndex]
		+ sentencePunctuationMark);

	std::ostringstream result;
	
	for (size_t i = 0; i < newWordsOrder.size(); i++)
	{
		if (i > 0)
		{
			result << ' ';
		}

		result << newWordsOrder[i];
	}

	return result.str();
}

int main()
{
	try
	{
		std::array<std::array<std::string, 2>, 6> tests =
		{
			{
				{ "You must speak wisely.", "Speak wisely, you must." },
				{ "You can do it!", "Do it, you can!" },
				{ "Do you think you will complete this?",
					"Complete this, do you think you will?" },
				{ "All your base are belong to us.",
					"Belong to us, all your base are."},
				{ "You have much to learn.", "Much to learn, you have." },
			}
		};
		std::vector<std::string> failures = {};
		std::cout << std::boolalpha;

		for (const auto& test : tests)
		{
			std::string sentence = test[0];
			std::string expected = test[1];
			std::cout << "Testing \"" << sentence << "\" (expecting \""
				<< expected << "\")...";
			std::string actual = WiseSpeak(sentence);
			std::cout << "\"" << actual << "\"";
			bool success = expected == actual;
			std::cout << " (success: " << success << ")." << std::endl;

			if (!success)
			{
				failures.push_back(sentence);
			}
		}

		std::cout << std::noboolalpha;

		if (failures.size() > 0)
		{
			std::cout << "The following inputs failed:" << std::endl;

			for (size_t i = 0; i < failures.size(); i++)
			{
				std::string failure = failures[i];
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
