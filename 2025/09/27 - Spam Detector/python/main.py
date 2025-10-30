# https://www.freecodecamp.org/learn/daily-coding-challenge/2025-09-27
import re

def is_spam(number):
    if not isinstance(number, str):
        return None
    
    # Adapted from https://stackoverflow.com/a/317081
    regex = r"^\+(\d+)\s\((\d{3})\)\s(\d{3})\-(\d{4})$"
    match = re.fullmatch(regex, number)

    if len(match.groups()) != 4:
        return None
    
    countryCodeString = match.group(1)
    countryCodeNumber = int(countryCodeString)

    # The country code is greater than 2 digits long or doesn't begin with a zero (0).
    if len(countryCodeString) > 2 or countryCodeString[0] != '0':
        return True
    
    areaCodeString = match.group(2)
    areaCodeNumber = int(areaCodeString)

    # The area code is greater than 900 or less than 200.
    if areaCodeNumber > 900 or areaCodeNumber < 200:
        return True
    
    prefixString = match.group(3)
    prefixNumber = int(prefixString)

    sumOfPrefixDigits = 0

    for prefixChar in prefixString:
        prefixDigit = int(prefixChar)

        if isinstance(prefixDigit, int):
            sumOfPrefixDigits += prefixDigit
    
    suffixString = match.group(4)
    suffixNumber = int(suffixString)

    if suffixString.find(str(sumOfPrefixDigits)) >= 0:
        return True
    
    numberNoFormattingChars = f"{countryCodeString}{areaCodeString}{prefixString}{suffixString}"

    if len(numberNoFormattingChars) < 4:
        return None
    
    # The number has the same digit four or more times in a row (ignoring the formatting characters).
    for i in range(3, len(numberNoFormattingChars)):
        if numberNoFormattingChars[i - 3] == numberNoFormattingChars[i] and numberNoFormattingChars[i - 2] == numberNoFormattingChars[i] and numberNoFormattingChars[i - 1] == numberNoFormattingChars[i]:
            return True
    
    return False

tests = tests = [
    ["+0 (200) 234-0182", False],
    ["+091 (555) 309-1922", True],
    ["+1 (555) 435-4792", True],
    ["+0 (955) 234-4364", True],
    ["+0 (155) 131-6943", True],
    ["+0 (555) 135-0192", True],
    ["+0 (555) 564-1987", True],
    ["+00 (555) 234-0182", False],
]

if __name__ == "__main__":
    failures = []

    for test in tests:
        number = test[0]
        expected = test[1]
        actual = is_spam(number)
        success = expected == actual
        print(f"Testing \'{number}\' (expecting {expected})...{actual} (success: {success}).")
        
        if not success:
            failures.append(number)
    
    if len(failures) > 0:
        print("The following inputs failed:")

        for failure in failures:
            print(f"  \'{failure}\'.")
    else:
        print("All tests passed!")
