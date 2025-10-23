# https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-21

def adjust_thermostat(currentF, targetC):
    if ((not isinstance(currentF, float)) and (not isinstance(currentF, int))) or ((not isinstance(targetC, float)) and (not isinstance(targetC, int))):
        return None
    
    targetF = targetC * 1.8 + 32
    differenceF = round(currentF - targetF, 1)

    if differenceF == 0:
        return "Hold"
    elif differenceF < 0:
        return f"Heat: {abs(differenceF):.1f} degrees Fahrenheit"
    elif differenceF > 0:
        return f"Cool: {differenceF:.1f} degrees Fahrenheit"
    
    return None

tests = tests = [
    [32, 0, "Hold"],
    [70, 25, "Heat: 7.0 degrees Fahrenheit"],
    [72, 18, "Cool: 7.6 degrees Fahrenheit"],
    [212, 100, "Hold"],
    [59, 22, "Heat: 12.6 degrees Fahrenheit"],
]

if __name__ == "__main__":
    failures = []

    for test in tests:
        currentF = test[0]
        targetC = test[1]
        expected = test[2]
        actual = adjust_thermostat(currentF, targetC)
        success = expected == actual
        print(f"Testing {currentF} and {targetC} (expecting {expected})...{actual} (success: {success}).")
        
        if not success:
            failures.append([currentF, targetC])
    
    if len(failures) > 0:
        print("The following inputs failed:")

        for failure in failures:
            currentF = failure[0]
            targetC = failure[1]
            print(f"  {currentF} and {targetC}.")
    else:
        print("All tests passed!")
