# https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-01

def to_decimal(binary):
    if not isinstance(binary, str) or len(binary) <= 0:
        return None
    
    result = 0

    for i, j in zip(range(len(binary) - 1, -1, -1), range(len(binary))):
        digit = binary[i]

        if digit == '1':
            result += 2 ** j
        elif digit != '0':
            return None

    return result

tests = tests = [
    ["101", 5],
    ["1010", 10],
    ["10010", 18],
    ["1010101", 85],
]

if __name__ == "__main__":
    failures = []

    for test in tests:
        binary = test[0]
        expected = test[1]
        actual = to_decimal(binary)
        success = expected == actual
        print(f"Testing \"{binary}\" (expecting {expected})...{actual} (success: {success}).")
        
        if not success:
            failures.append(binary)
    
    if len(failures) > 0:
        print("The following inputs failed:")

        for failure in failures:
            print(f"  \"{failure}\".")
    else:
        print("All tests passed!")
