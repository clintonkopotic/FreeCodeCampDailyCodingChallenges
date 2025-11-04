# https://www.freecodecamp.org/learn/daily-coding-challenge/2025-11-01

def verify(message, key, signature):
    if (not isinstance(message, str)) or (not isinstance(key, str)) or (not isinstance(signature, int)):
        return None
    
    def calculateSignature(string):
        if not isinstance(string, str):
            return None
        
        letters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ"
        signature = 0

        for letter in string:
            if not isinstance(letter, str):
                return None
            
            index = letters.find(letter)

            if index >= 0:
                signature += index + 1
        
        return signature
    
    calculatedSignature = calculateSignature(message) + calculateSignature(key)
    
    return signature == calculatedSignature

tests = tests = [
    ["foo", "bar", 57, True],
    ["foo", "bar", 54, False],
    ["freeCodeCamp", "Rocks", 238, True],
    ["Is this valid?", "No", 210, False],
    ["Is this valid?", "Yes", 233, True],
    ["Check out the freeCodeCamp podcast,", "in the mobile app", 514, True],
]

if __name__ == "__main__":
    failures = []

    for test in tests:
        message = test[0]
        key = test[1]
        signature = test[2]
        expected = test[3]
        actual = verify(message, key, signature)
        success = expected == actual
        print(f"Testing \"{message}\", \"{key}\", and {signature} (expecting {expected})...{actual} (success: {success}).")
        
        if not success:
            failures.append([message, key, signature])
    
    if len(failures) > 0:
        print("The following inputs failed:")

        for failure in failures:
            message = failure[0]
            key = failure[1]
            signature = failure[2]
            print(f"  \"{message}\", \"{key}\", and {signature}.")
    else:
        print("All tests passed!")
