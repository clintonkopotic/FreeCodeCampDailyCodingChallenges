# https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-03
import re

def check_strength(password):
    if not isinstance(password, str):
        return None
    
    rules_meet = 0

    # Rule 1. At least 8 characters long.
    if len(password) >= 8:
        rules_meet += 1
    
    # Rule 2. Contains both uppercase and lowercase letters.
    if re.search(r"[A-Z]", password) and re.search(r"[a-z]", password):
        rules_meet += 1
    
    # Rule 3. Contains at least one number.
    if re.search(r"[0-9]", password):
        rules_meet += 1
    
    # Rule 4. Contains at least one special character from this set: !, @, #, $, %, ^, &, or *.
    if re.search(r"[!@#$%^&*]", password):
        rules_meet += 1
    
    if rules_meet < 2:
        return "weak"
    elif rules_meet < 4:
        return "medium"
    else:
        return "strong"

tests = tests = [
    ["123456", "weak"],
    ["pass!!!", "weak"],
    ["Qwerty", "weak"],
    ["PASSWORD", "weak"],
    ["PASSWORD!", "medium"],
    ["PassWord%^!", "medium"],
    ["qwerty12345", "medium"],
    ["PASSWORD!", "medium"],
    ["PASSWORD!", "medium"],
    ["S3cur3P@ssw0rd", "strong"],
    ["C0d3&Fun!", "strong"],
]

if __name__ == "__main__":
    failures = []

    for test in tests:
        password = test[0]
        expected = test[1]
        actual = check_strength(password)
        success = expected == actual
        print(f"Testing \"{password}\" (expecting \"{expected}\")...\"{actual}\" (success: {success}).")
        
        if not success:
            failures.append(password)
    
    if len(failures) > 0:
        print("The following inputs failed:")

        for failure in failures:
            print(f"  \"{failure}\".")
    else:
        print("All tests passed!")
