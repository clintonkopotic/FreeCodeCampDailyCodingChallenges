# https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-04
import math

def classification(temp):
    if (not isinstance(temp, int) and not isinstance(temp, float)) or not math.isfinite(temp) or temp < 0:
        return None
    
    if temp >= 30_000:
        return "O"
    elif temp >= 10_000:
        return "B"
    elif temp >= 7_500:
        return "A"
    elif temp >= 6_000:
        return "F"
    elif temp >= 5_200:
        return "G"
    elif temp >= 3_700:
        return "K"
    else:
        return "M"

tests = tests = [
    [5_778, "G"],
    [2_400, "M"],
    [9_999, "A"],
    [3_700, "K"],
    [3_699, "M"],
    [210_000, "O"],
    [6_000, "F"],
    [11_432, "B"],
]

if __name__ == "__main__":
    failures = []

    for test in tests:
        temp = test[0]
        expected = test[1]
        actual = classification(temp)
        success = expected == actual
        print(f"Testing {temp} (expecting \"{expected}\")...\"{actual}\" (success: {success}).")
        
        if not success:
            failures.append(temp)
    
    if len(failures) > 0:
        print("The following inputs failed:")

        for failure in failures:
            print(f"  {failure}.")
    else:
        print("All tests passed!")
