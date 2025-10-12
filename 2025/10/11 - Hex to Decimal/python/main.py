# https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-11
def hex_to_decimal(hex):
    digits = "0123456789ABCDEF"
    exponent = 0
    result = 0

    for digit in reversed(hex):
        value = digits.find(digit)

        if value < 0:
            return None
        
        digitValue = value * pow(16, exponent)
        result += digitValue
        exponent += 1

    return result

tests = tests = [
    ["A", 10],
    ["15", 21],
    ["2E", 46],
    ["FF", 255],
    ["A3F", 2623],
]

if __name__ == "__main__":
    failures = []

    for test in tests:
        hex = test[0]
        expected = test[1]
        actual = hex_to_decimal(hex)
        success = expected == actual
        print(f"Testing \"{hex}\" (expecting {expected})...{actual} (success: {success}).")
        
        if not success:
            failures.append(hex)
    
    if len(failures) > 0:
        print("The following inputs failed:")

        for failure in failures:
            print(f"  \"{failure}\".")
    else:
        print("All tests passed!")
