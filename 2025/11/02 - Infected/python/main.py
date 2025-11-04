# https://www.freecodecamp.org/learn/daily-coding-challenge/2025-11-02
import math

def infected(days):
    if ((not isinstance(days, int)) and (not isinstance(days, float))) or days < 0:
        return None
    numberOfComputersInfected = 1

    for day in range(1, days + 1):
        numberOfComputersInfected *= 2

        if day % 3 == 0:
            patched = math.ceil(numberOfComputersInfected * 0.2)
            numberOfComputersInfected -= patched

    return numberOfComputersInfected

tests = tests = [
    [1, 2],
    [3, 6],
    [8, 152],
    [17, 39808],
    [25, 5217638],
]

if __name__ == "__main__":
    failures = []

    for test in tests:
        days = test[0]
        expected = test[1]
        actual = infected(days)
        success = expected == actual
        print(f"Testing {days} (expecting {expected})...{actual} (success: {success}).")
        
        if not success:
            failures.append(days)
    
    if len(failures) > 0:
        print("The following inputs failed:")

        for failure in failures:
            print(f"  {failure}.")
    else:
        print("All tests passed!")
