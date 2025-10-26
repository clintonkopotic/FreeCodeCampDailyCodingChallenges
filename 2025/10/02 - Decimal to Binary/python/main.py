# https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-02
import math

def to_binary(decimal):
    if (not isinstance(decimal, int) and not isinstance(decimal, float)) or (isinstance(decimal, float) and not decimal.is_integer()) or decimal < 0:
        return None
    
    if decimal == 0:
        return "0"
    
    value = int(decimal)
    remainders = []

    while value > 0:
        remainders.append(str(value % 2))
        value = math.floor(value / 2)
    
    remainders.reverse()

    return "".join(remainders)

tests = tests = [
    [5, "101"],
    [12, "1100"],
    [50, "110010"],
    [99, "1100011"],
]

if __name__ == "__main__":
    failures = []

    for test in tests:
        decimal = test[0]
        expected = test[1]
        actual = to_binary(decimal)
        success = expected == actual
        print(f"Testing {decimal} (expecting \"{expected}\")...\"{actual}\" (success: {success}).")
        
        if not success:
            failures.append(decimal)
    
    if len(failures) > 0:
        print("The following inputs failed:")

        for failure in failures:
            print(f"  {failure}.")
    else:
        print("All tests passed!")
