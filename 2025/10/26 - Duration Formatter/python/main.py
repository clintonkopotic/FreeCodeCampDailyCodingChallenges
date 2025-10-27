# https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-26
import math

def format(seconds):
    if (not isinstance(seconds, int) and not isinstance(seconds, float)) or (isinstance(seconds, float) and not seconds.is_integer()) or seconds < 0:
        return None
    
    secondsPerMinute = 60
    minutesPerHour = 60
    secondsPerHour = secondsPerMinute * minutesPerHour
    remaining = seconds
    hours = 0
    result = ""

    if remaining >= secondsPerHour:
        hours = math.floor(remaining / secondsPerHour)
        remaining %= secondsPerHour

        if hours > 0:
            result += f"{hours}:"

    minutes = 0

    if remaining >= secondsPerMinute:
        minutes = math.floor(remaining / secondsPerMinute)
        remaining %= secondsPerMinute
    
    if hours > 0:
        result += f"{minutes:02d}:"
    else:
        result += f"{minutes}:"
    
    result += f"{remaining:02d}"

    return result

tests = tests = [
    [500, "8:20"],
    [4000, "1:06:40"],
    [1, "0:01"],
    [5555, "1:32:35"],
    [99999, "27:46:39"],
]

if __name__ == "__main__":
    failures = []

    for test in tests:
        seconds = test[0]
        expected = test[1]
        actual = format(seconds)
        success = expected == actual
        print(f"Testing {seconds} (expecting \"{expected}\")...\"{actual}\" (success: {success}).")
        
        if not success:
            failures.append(seconds)
    
    if len(failures) > 0:
        print("The following inputs failed:")

        for failure in failures:
            print(f"  {failure}.")
    else:
        print("All tests passed!")
