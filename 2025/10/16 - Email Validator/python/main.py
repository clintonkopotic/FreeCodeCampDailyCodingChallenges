# https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-16
import re

def validate(email):
    if not isinstance(email, str):
        return None
    
    parts = email.split('@')

    if not isinstance(parts, list) or len(parts) != 2:
        return False
    
    localPart = parts[0]

    if not re.fullmatch(r"^[0-9a-zA-Z._-]+$", localPart) or localPart.startswith('.') or localPart.endswith('.') or ".." in localPart:
        return False
    
    domainPart = parts[1]

    if domainPart.startswith('.') or domainPart.endswith('.') or ".." in domainPart:
        return False
    
    domainDotParts = domainPart.split('.')

    if not isinstance(domainDotParts, list) or len(domainDotParts) < 2:
        return False
    
    for domainDotPart in domainDotParts:
        if not isinstance(domainDotPart, str) or len(domainDotPart) == 0:
            return False
    
    lastDomainDotPart = domainDotParts[len(domainDotParts) - 1]

    return lastDomainDotPart.isalpha() and len(lastDomainDotPart) >= 2

tests = tests = [
    ["a@b.cd", True],
    ["hell.-w.rld@example.com", True],
    [".b@sh.rc", False],
    ["example@test.c0", False],
    ["freecodecamp.org", False],
    ["develop.ment_user@c0D!NG.R.CKS", True],
    ["hello.@wo.rld", False],
    ["hello@world..com", False],
    ["git@commit@push.io", False],
]

if __name__ == "__main__":
    failures = []

    for test in tests:
        email = test[0]
        expected = test[1]
        actual = validate(email)
        success = expected == actual
        print(f"Testing \"{email}\" (expecting {expected})...{actual} (success: {success}).")
        
        if not success:
            failures.append(email)
    
    if len(failures) > 0:
        print("The following inputs failed:")

        for failure in failures:
            print(f"  \"{failure}\".")
    else:
        print("All tests passed!")
