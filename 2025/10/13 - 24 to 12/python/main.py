# https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-12
def to_12(time):
    if not isinstance(time, str) or len(time) != 4:
        return None
    
    hour_of_day = int(time[0:2])
    minute_of_hour = int(time[2:])

    if (not isinstance(hour_of_day, int)) or (hour_of_day < 0) or (hour_of_day >= 24) or (not isinstance(minute_of_hour, int)) or (minute_of_hour < 0) or (minute_of_hour >= 60):
        return None
    
    hour_of_meridian = 12 if (hour_of_day == 0 or hour_of_day == 12) else (hour_of_day if hour_of_day < 12 else hour_of_day - 12)
    meridian = "AM" if hour_of_day < 12 else "PM"

    return f"{hour_of_meridian}:{minute_of_hour:02d} {meridian}"

tests = tests = [
    ["1124", "11:24 AM"],
    ["0900", "9:00 AM"],
    ["1455", "2:55 PM"],
    ["2346", "11:46 PM"],
    ["0030", "12:30 AM"],
]

if __name__ == "__main__":
    failures = []

    for test in tests:
        time = test[0]
        expected = test[1]
        actual = to_12(time)
        success = expected == actual
        print(f"Testing \"{time}\" (expecting \"{expected}\")...\"{actual}\" (success: {success}).")
        
        if not success:
            failures.append(time)
    
    if len(failures) > 0:
        print("The following inputs failed:")

        for failure in failures:
            print(f"  \"{failure}\".")
    else:
        print("All tests passed!")
