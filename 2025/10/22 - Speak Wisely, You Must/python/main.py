# https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-22

def wise_speak(sentence):
    if not isinstance(sentence, str):
        return None
    
    words = sentence.split(' ')

    if not isinstance(words, list) or len(words) == 0:
        return ""
    
    # All given sentences will end with a single punctuation mark. Keep the original punctuation of the sentence and move it to the end of the new sentence.
    lastWord = words[len(words) - 1]
    sentencePunctuationMark = lastWord[len(lastWord) - 1]

    # Find the first occurrence of one of the following words in the sentence: "have", "must", "are", "will", "can".
    newEndingWords = set(["have", "must", "are", "will", "can"])
    newEndingWordIndex = -1

    for i, s in enumerate(words):
        if s in newEndingWords:
            newEndingWordIndex = i
    
    if newEndingWordIndex < 0 or newEndingWordIndex > len(words):
        return None
    
    newWordsOrder = []
    newFirstWord = words[newEndingWordIndex + 1]
    newWordsOrder.append(f"{newFirstWord.capitalize()}")

    if newEndingWordIndex + 2 < len(words) - 1:
        newWordsOrder.extend(words[newEndingWordIndex + 2:len(words) - 1])
    
    newWordsOrder.append(f"{lastWord[0:len(lastWord) - 1]},")
    newWordsOrder.append(words[0].lower())

    if newEndingWordIndex > 1:
        newWordsOrder.extend(words[1:newEndingWordIndex])
    
    newWordsOrder.append(f"{words[newEndingWordIndex]}{sentencePunctuationMark}")
    
    return ' '.join(newWordsOrder)

tests = tests = [
    ["You must speak wisely.", "Speak wisely, you must."],
    ["You can do it!", "Do it, you can!"],
    ["Do you think you will complete this?", "Complete this, do you think you will?"],
    ["All your base are belong to us.", "Belong to us, all your base are."],
    ["You have much to learn.", "Much to learn, you have."],
]

if __name__ == "__main__":
    failures = []

    for test in tests:
        sentence = test[0]
        expected = test[1]
        actual = wise_speak(sentence)
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
