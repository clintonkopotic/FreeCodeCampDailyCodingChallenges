# https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-23

class Song:
    def __init__(self, title, plays):
        self.title = title
        self.plays = plays
    
    def __repr__(self):
        return f"{{ title: \"{self.title}\", plays: {self.plays} }}"

def favorite_songs(playlist):
    if not isinstance(playlist, list) or len(playlist) == 0:
        return None
    
    sorted_playlist = sorted(playlist, key=lambda song: song.plays, reverse=True)

    result = []

    for song in sorted_playlist:
        result.append(song.title)

        if len(result) >= 2:
            break

    return result

tests = tests = [
    [[Song("Sync or Swim", 3), Song("Byte Me", 1), Song("Earbud Blues", 2)], ["Sync or Swim", "Earbud Blues"]],
    [[Song("Skip Track", 98), Song("99 Downloads", 99), Song("Clickwheel Love", 100)], ["Clickwheel Love", "99 Downloads"]],
    [[Song("Song A", 42), Song("Song B", 99), Song("Song C", 75)], ["Song B", "Song C"]],
]

if __name__ == "__main__":
    failures = []

    for test in tests:
        playlist = test[0]
        expected = test[1]
        actual = favorite_songs(playlist)
        success = expected == actual
        print(f"Testing {playlist} (expecting {expected})...{actual} (success: {success}).")
        
        if not success:
            failures.append(playlist)
    
    if len(failures) > 0:
        print("The following inputs failed:")

        for failure in failures:
            print(f"  {failure}.")
    else:
        print("All tests passed!")
