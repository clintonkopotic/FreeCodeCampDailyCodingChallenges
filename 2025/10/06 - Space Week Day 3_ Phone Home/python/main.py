# https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-06
import math

def send_message(route):
    if not isinstance(route, list) or len(route) <= 1:
        return None
    
    distanceTraveledInKm = 0
    # Start at negative one to not count the trip to the first satellite.
    numberOfSatelitesPassedThrough = -1

    for value in route:
        if not math.isfinite(value) or value < 0:
            return None
        
        distanceTraveledInKm += value
        numberOfSatelitesPassedThrough += 1

    messageSpeedInKmPerSecond = 300_000
    delayOfMessageThroughSatelliteInSeconds = 0.5
    timeOfTravelInSeonds = (distanceTraveledInKm / messageSpeedInKmPerSecond) + (delayOfMessageThroughSatelliteInSeconds * numberOfSatelitesPassedThrough)
    
    return round(timeOfTravelInSeonds, 4)

tests = tests = [
    [[300000, 300000], 2.5],
    [[384400, 384400], 3.0627],
    [[54600000, 54600000], 364.5],
    [[1000000, 500000000, 1000000], 1674.3333],
    [[10000, 21339, 50000, 31243, 10000], 2.4086],
    [[802101, 725994, 112808, 3625770, 481239], 21.1597],
]

if __name__ == "__main__":
    failures = []

    for test in tests:
        route = test[0]
        expected = test[1]
        actual = send_message(route)
        success = expected == actual
        print(f"Testing {route} (expecting {expected})...{actual} (success: {success}).")
        
        if not success:
            failures.append(route)
    
    if len(failures) > 0:
        print("The following inputs failed:")

        for failure in failures:
            print(f"  {failure}.")
    else:
        print("All tests passed!")
