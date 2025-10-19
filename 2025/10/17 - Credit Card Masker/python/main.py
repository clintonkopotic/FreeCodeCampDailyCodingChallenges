# https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-15
import re

def mask(card):
    if not isinstance(card, str):
        return None
    
    return f"****{card[4]}****{card[9]}****{card[14:]}"

tests = tests = [
    ["4012-8888-8888-1881", "****-****-****-1881"],
    ["5105 1051 0510 5100", "**** **** **** 5100"],
    ["6011 1111 1111 1117", "**** **** **** 1117"],
    ["2223-0000-4845-0010", "****-****-****-0010"],
]

if __name__ == "__main__":
    failures = []

    for test in tests:
        card = test[0]
        expected = test[1]
        actual = mask(card)
        success = expected == actual
        print(f"Testing \"{card}\" (expecting \"{expected}\")...\"{actual}\" (success: {success}).")
        
        if not success:
            failures.append(card)
    
    if len(failures) > 0:
        print("The following inputs failed:")

        for failure in failures:
            print(f"  \"{failure}\".")
    else:
        print("All tests passed!")
