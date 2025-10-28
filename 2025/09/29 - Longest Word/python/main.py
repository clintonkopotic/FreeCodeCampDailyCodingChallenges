# https://www.freecodecamp.org/learn/daily-coding-challenge/2025-09-29

def get_longest_word(sentence):
    if not isinstance(sentence, str):
        return None
    
    words = sentence.split()
    result = ""

    for word in words:
        if word.endswith('.'):
            word = word[0:(len(word) - 1)]
        
        if len(word) > len(result):
            result = word
    
    return result

tests = tests = [
    ["coding is fun", "coding"],
    ["Coding challenges are fun and educational.", "educational"],
    ["This sentence has multiple long words.", "sentence"],
]

if __name__ == "__main__":
    failures = []

    for test in tests:
        sentence = test[0]
        expected = test[1]
        actual = get_longest_word(sentence)
        success = expected == actual
        print(f"Testing \"{sentence}\" (expecting \"{expected}\")...\"{actual}\" (success: {success}).")
        
        if not success:
            failures.append(sentence)
    
    if len(failures) > 0:
        print("The following inputs failed:")

        for failure in failures:
            print(f"  \"{failure}\".")
    else:
        print("All tests passed!")
