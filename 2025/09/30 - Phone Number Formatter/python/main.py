# https://www.freecodecamp.org/learn/daily-coding-challenge/2025-09-30
import re

def format_number(number):
    if not isinstance(number, str) or len(number) != 11:
        return None
    
    return f"+{number[0]} ({number[1:4]}) {number[4:7]}-{number[7:11]}"

tests = tests = [
    ["05552340182", "+0 (555) 234-0182"],
    ["15554354792", "+1 (555) 435-4792"],
]

if __name__ == "__main__":
    failures = []

    for test in tests:
        number = test[0]
        expected = test[1]
        actual = format_number(number)
        success = expected == actual
        print(f"Testing \"{number}\" (expecting \"{expected}\")...\"{actual}\" (success: {success}).")
        
        if not success:
            failures.append(number)
    
    if len(failures) > 0:
        print("The following inputs failed:")

        for failure in failures:
            print(f"  \"{failure}\".")
    else:
        print("All tests passed!")
