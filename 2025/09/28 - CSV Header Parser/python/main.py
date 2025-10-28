# https://www.freecodecamp.org/learn/daily-coding-challenge/2025-09-28

def get_headings(csv):
    if not isinstance(csv, str):
        return None
    
    headingsSplit = csv.split(',')
    headings = []

    for headingSplit in headingsSplit:
        if not isinstance(headingSplit, str):
            return None
        
        headings.append(headingSplit.strip())

    return headings

tests = tests = [
    ["name,age,city", ["name", "age", "city"]],
    ["first name,last name,phone", ["first name", "last name", "phone"]],
    ["username , email , signup date ", ["username", "email", "signup date"]],
]

if __name__ == "__main__":
    failures = []

    for test in tests:
        csv = test[0]
        expected = test[1]
        actual = get_headings(csv)
        success = expected == actual
        print(f"Testing \'{csv}\' (expecting {expected})...{actual} (success: {success}).")
        
        if not success:
            failures.append(csv)
    
    if len(failures) > 0:
        print("The following inputs failed:")

        for failure in failures:
            print(f"  \'{failure}\'.")
    else:
        print("All tests passed!")
