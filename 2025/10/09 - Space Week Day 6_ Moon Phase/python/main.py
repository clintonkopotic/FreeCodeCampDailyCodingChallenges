# https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-09
from datetime import datetime

def moon_phase(date_string):
    date = datetime.fromisoformat(date_string)
    reference_new_moon = datetime.fromisoformat("2000-01-06")
    diff_in_days = (date - reference_new_moon).days
    day_in_lunar_cycle = diff_in_days % 28 + 1

    if day_in_lunar_cycle >= 1 and day_in_lunar_cycle <=7:
        return "New"
    elif day_in_lunar_cycle >= 8 and day_in_lunar_cycle <=14:
        return "Waxing"
    elif day_in_lunar_cycle >= 15 and day_in_lunar_cycle <=21:
        return "Full"
    elif day_in_lunar_cycle >= 22 and day_in_lunar_cycle <=28:
        return "Waning"

    return None

tests = tests = [
    ["2000-01-12", "New"],
    ["2000-01-13", "Waxing"],
    ["2014-10-15", "Full"],
    ["2012-10-21", "Waning"],
    ["2022-12-14", "New"],
]

if __name__ == "__main__":
    failures = []

    for test in tests:
        date_string = test[0]
        expected = test[1]
        actual = moon_phase(date_string)
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
