# https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-08
import math

def goldilocksZone(mass):
    if not math.isfinite(mass) or mass <= 0:
        return None
    
    luminosityOfStar = math.pow(mass, 3.5)
    squareRootOfLuminosityOfStar = math.sqrt(luminosityOfStar)
    startOfZone = 0.95 * squareRootOfLuminosityOfStar
    endOfZone = 1.37 * squareRootOfLuminosityOfStar

    return [round(startOfZone, 2), round(endOfZone, 2)]

tests = tests = [
    [1, [0.95, 1.37]],
    [0.5, [0.28, 0.41]],
    [6, [21.85, 31.51]],
    [3.7, [9.38, 13.52]],
    [20, [179.69, 259.13]],
]

if __name__ == "__main__":
    failures = []

    for test in tests:
        mass = test[0]
        expected = test[1]
        actual = goldilocksZone(mass)
        success = expected == actual
        print(f"Testing {mass} (expecting {expected})...{actual} (success: {success}).")
        
        if not success:
            failures.append(mass)
    
    if len(failures) > 0:
        print("The following inputs failed:")

        for failure in failures:
            print(f"  {failure}.")
    else:
        print("All tests passed!")
