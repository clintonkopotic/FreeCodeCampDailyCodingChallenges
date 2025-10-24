# https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-05
import math

def has_exoplanet(readings):
    if not isinstance(readings, str):
        return None
    
    values = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ"
    totalNumberOfReadings = 0
    sumOfReadings = 0
    readingValues = []

    for readingCharacter in readings:
        value = values.index(readingCharacter)

        if value < 0 or value >= len(values):
            return None
        
        totalNumberOfReadings += 1
        sumOfReadings += value
        readingValues.append(value)

    if totalNumberOfReadings == 0:
        return None
    
    averageReading = sumOfReadings / totalNumberOfReadings

    if not math.isfinite(averageReading):
        return None
    
    exoplanetMaxThresholdReading = 0.8 * averageReading

    for readingValue in readingValues:
        if readingValue <= exoplanetMaxThresholdReading:
            return True
    
    return False

tests = tests = [
    ["665544554", False],
    ["FGFFCFFGG", True],
    ["MONOPLONOMONPLNOMPNOMP", False],
    ["FREECODECAMP", True],
    ["9AB98AB9BC98A", False],
    ["ZXXWYZXYWYXZEGZXWYZXYGEE", True],
]

if __name__ == "__main__":
    failures = []

    for test in tests:
        readings = test[0]
        expected = test[1]
        actual = has_exoplanet(readings)
        success = expected == actual
        print(f"Testing \"{readings}\" (expecting {expected})...{actual} (success: {success}).")
        
        if not success:
            failures.append(readings)
    
    if len(failures) > 0:
        print("The following inputs failed:")

        for failure in failures:
            print(f"  \"{failure}\".")
    else:
        print("All tests passed!")
