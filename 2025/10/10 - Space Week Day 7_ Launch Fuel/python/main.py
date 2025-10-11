# https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-10

def launch_fuel(payload):
    # Rockets require 1 kg of fuel per 5 kg of mass they must lift.
    fuelInKgPerLiftMassInKg = 1 / 5
    totalPayloadInKg = payload
    fuelToLiftInKg = totalPayloadInKg * fuelInKgPerLiftMassInKg
    additionalFuelInKg = fuelToLiftInKg
    lastFuelToLiftInKg = fuelToLiftInKg
    totalFuelInKg = fuelToLiftInKg
    totalPayloadInKg += fuelToLiftInKg

    while additionalFuelInKg >= 1:
        fuelToLiftInKg = totalPayloadInKg * fuelInKgPerLiftMassInKg
        additionalFuelInKg = abs(lastFuelToLiftInKg - fuelToLiftInKg)
        lastFuelToLiftInKg = fuelToLiftInKg
        totalFuelInKg += additionalFuelInKg
        totalPayloadInKg += additionalFuelInKg

    return round(totalFuelInKg, 1)

tests = tests = [
    [50, 12.4],
    [500, 124.8],
    [243, 60.7],
    [11000, 2749.8],
    [6214, 1553.4],
]

if __name__ == "__main__":
    failures = []

    for test in tests:
        payload = test[0]
        expected = test[1]
        actual = launch_fuel(payload)
        success = expected == actual
        print(f"Testing {payload} (expecting {expected})...{actual} (success: {success}).")
        
        if not success:
            failures.append(payload)
    
    if len(failures) > 0:
        print("The following inputs failed:")

        for failure in failures:
            print(f"  {failure}.")
    else:
        print("All tests passed!")
