# https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-27
import math

def sequence(n):
    if (not isinstance(n, int) and not isinstance(n, float)) or (isinstance(n, float) and not n.is_integer()) or n <= 0:
        return None
    
    result = ""

    for i in range(1, n + 1):
        result += str(i)

    return result

tests = tests = [
    [5, "12345"],
    [10, "12345678910"],
    [1, "1"],
    [27, "123456789101112131415161718192021222324252627"],
]

if __name__ == "__main__":
    failures = []

    for test in tests:
        n = test[0]
        expected = test[1]
        actual = sequence(n)
        success = expected == actual
        print(f"Testing {n} (expecting \"{expected}\")...\"{actual}\" (success: {success}).")
        
        if not success:
            failures.append(n)
    
    if len(failures) > 0:
        print("The following inputs failed:")

        for failure in failures:
            print(f"  {failure}.")
    else:
        print("All tests passed!")
