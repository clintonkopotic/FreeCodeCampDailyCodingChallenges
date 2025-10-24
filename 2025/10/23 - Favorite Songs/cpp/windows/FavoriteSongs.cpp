// FavoriteSongs.cpp : This file contains the 'main' function. Program execution
// begins and ends there.
// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-23
// Uses C++ 17

#include <any>
#include <algorithm>
#include <array>
#include <iostream>
#include <sstream>
#include <vector>

class Song
{
public:
	// Default constructor is automatically generated.
	// The members will be initialized with their default values.

	// Optional: a custom constructor
	Song(std::string newTitle, int initialPlays)
		: title(newTitle), plays(initialPlays)
	{
	}

	void play() { plays++; }

	std::string getTitle() const { return title; }
	int getPlays() const { return plays; }

	std::string toString() const
	{
		std::ostringstream result;
		result << "{ " << "Title: \"" << getTitle() << "\", Plays: "
			<< getPlays() << " }";

		return result.str();
	}

private:
	std::string title; // Default-initialized to an empty string
	int plays = 0;     // Default-initialized to 0
};

static std::vector<std::string> FavoriteSongs(const std::vector<Song>& playlist)
{
	std::vector<Song> sorted = playlist;
	std::sort(sorted.begin(), sorted.end(),
		[](const Song a, const Song b) -> bool
		{
			return a.getPlays() > b.getPlays();
		});

	std::vector<std::string> result = {};

	for (const auto& song : sorted)
	{
		result.push_back(song.getTitle());

		if (result.size() >= 2)
		{
			break;
		}
	}

	return result;
}

static std::string PlaylistToString(std::vector<Song> playlist);
static std::string OutputToString(std::vector<std::string> output);

int main()
{
	try
	{
		std::array<std::array<std::any, 2>, 3> tests;
		std::vector<Song> test0playlist =
		{
			Song("Sync or Swim", 3),
			Song("Byte Me", 1),
			Song("Earbud Blues", 2),
		};
		std::vector<std::string> test0expected =
		{
			"Sync or Swim",
			"Earbud Blues",
		};
		tests[0][0] = test0playlist;
		tests[0][1] = test0expected;

		std::vector<Song> test1playlist =
		{
			Song("Skip Track", 98),
			Song("99 Downloads", 99),
			Song("Clickwheel Love", 100),
		};
		std::vector<std::string> test1expected =
		{
			"Clickwheel Love",
			"99 Downloads",
		};
		tests[1][0] = test1playlist;
		tests[1][1] = test1expected;

		std::vector<Song> test2playlist =
		{
			Song("Song A", 42),
			Song("Song B", 99),
			Song("Song C", 74),
		};
		std::vector<std::string> test2expected =
		{
			"Song B",
			"Song C",
		};
		tests[2][0] = test2playlist;
		tests[2][1] = test2expected;

		std::vector<std::vector<Song>> failures = {};
		std::cout << std::boolalpha;

		for (const auto& test : tests)
		{
			const auto& playlist = std::any_cast<std::vector<Song>>(test[0]);
			const auto& expected = std::any_cast<std::vector<std::string>>(
				test[1]);
			std::cout << "Testing " << PlaylistToString(playlist)
				<< " (expecting " << OutputToString(expected) << ")...";
			const auto& actual = FavoriteSongs(playlist);
			std::cout << OutputToString(actual);
			bool success = expected == actual;
			std::cout << " (success: " << success << ")." << std::endl;

			if (!success)
			{
				failures.push_back(playlist);
			}
		}

		std::cout << std::noboolalpha;

		if (failures.size() > 0)
		{
			std::cout << "The following inputs failed:" << std::endl;

			for (const auto& failure : failures)
			{
				std::cout << "  " << PlaylistToString(failure) << '.'
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

static std::string PlaylistToString(std::vector<Song> playlist)
{
	std::ostringstream result;
	result << '[';

	for (size_t i = 0; i < playlist.size(); i++)
	{
		if (i > 0)
		{
			result << ", ";
		}

		result << playlist[i].toString();
	}

	result << ']';

	return result.str();
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
