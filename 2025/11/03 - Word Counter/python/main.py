# https://www.freecodecamp.org/learn/daily-coding-challenge/2025-11-03

def count_words(sentence):
    if not isinstance(sentence, str):
        return None
    
    return len(sentence.split(' '))

tests = tests = [
    ["Hello world", 2],
    ["The quick brown fox jumps over the lazy dog.", 9],
    ["I like coding challenges!", 4],
    ["Complete the challenge in JavaScript and Python.", 7],
    ["The missing semi-colon crashed the entire internet.", 7],
]

if __name__ == "__main__":
    failures = []

    for test in tests:
        sentence = test[0]
        expected = test[1]
        actual = count_words(sentence)
        success = expected == actual
        print(f"Testing \"{sentence}\" (expecting {expected})...{actual} (success: {success}).")
        
        if not success:
            failures.append(sentence)
    
    if len(failures) > 0:
        print("The following inputs failed:")

        for failure in failures:
            print(f"  \"{failure}\".")
    else:
        print("All tests passed!")
