# https://www.freecodecamp.org/learn/daily-coding-challenge/2025-11-08
from datetime import date

def can_post(message):
    if not isinstance(message, str):
        return None
    
    if len(message) <= 40:
        return "short post"
    elif len(message) <= 80:
        return "long post"
    else:
        return "invalid post"

tests = tests = [
    ["Hello world", "short post"],
    ["This is a longer message but still under eighty characters.", "long post"],
    ["This message is too long to fit into either of the character limits for a social media post.", "invalid post"],
]

if __name__ == "__main__":
    failures = []

    for test in tests:
        message = test[0]
        expected = test[1]
        actual = can_post(message)
        success = expected == actual
        print(f"Testing \"{message}\" (expecting \"{expected}\")...\"{actual}\" (success: {success}).")
        
        if not success:
            failures.append(message)
    
    if len(failures) > 0:
        print("The following inputs failed:")

        for failure in failures:
            print(f"  \"{failure}\".")
    else:
        print("All tests passed!")
