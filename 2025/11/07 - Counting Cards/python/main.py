# https://www.freecodecamp.org/learn/daily-coding-challenge/2025-11-07
from datetime import date

def combinations(cards):
    if not isinstance(cards, int) and not isinstance(cards, float):
        return None
    
    parsed_date = date.fromisoformat(cards)
    
    return parsed_date.strftime("%A")

tests = tests = [
    ["2025-11-06", "Thursday"],
    ["1999-12-31", "Friday"],
    ["1111-11-11", "Saturday"],
    ["2112-12-21", "Wednesday"],
    ["2345-10-01", "Monday"],
]

if __name__ == "__main__":
    failures = []

    for test in tests:
        date_string = test[0]
        expected = test[1]
        actual = combinations(date_string)
        success = expected == actual
        print(f"Testing \"{date_string}\" (expecting \"{expected}\")...\"{actual}\" (success: {success}).")
        
        if not success:
            failures.append(date_string)
    
    if len(failures) > 0:
        print("The following inputs failed:")

        for failure in failures:
            print(f"  \"{failure}\".")
    else:
        print("All tests passed!")
