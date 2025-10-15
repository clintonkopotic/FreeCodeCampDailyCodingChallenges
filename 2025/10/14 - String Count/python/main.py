# https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-14
def count(text, pattern):
    if not isinstance(text, str) or not isinstance(pattern, str):
        return None
    
    result = 0
    i = 0

    while i < len(text):
        i = text.find(pattern, i)

        if i < 0:
            break

        result += 1
        i += 1
    
    return result

tests = tests = [
    ['abcdefg', 'def', 1],
    ['hello', 'world', 0],
    ['mississippi', 'iss', 2],
    ['she sells seashells by the seashore', 'sh', 3],
    ['101010101010101010101', '101', 10],
]

if __name__ == "__main__":
    failures = []

    for test in tests:
        text = test[0]
        pattern = test[1]
        expected = test[2]
        actual = count(text, pattern)
        success = expected == actual
        print(f"Testing \"{text}\" and \"{pattern}\" (expecting {expected})...{actual} (success: {success}).")
        
        if not success:
            failures.append([text, pattern])
    
    if len(failures) > 0:
        print("The following inputs failed:")

        for failure in failures:
            text = failure[0]
            pattern = failure[1]
            print(f"  \"{text}\" and \"{pattern}\".")
    else:
        print("All tests passed!")
