# https://www.freecodecamp.org/learn/daily-coding-challenge/2025-11-06
from datetime import date

def get_weekday(date_string):
    if not isinstance(date_string, str) or len(date_string) != 10:
        return None
    
    parsed_date = date.fromisoformat(date_string)
    
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
        actual = get_weekday(date_string)
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
