# https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-28
import math

def navigate(commands):
    if (not isinstance(commands, list)) or len(commands) == 0:
        return None
    
    visitPagePrefix = "Visit "
    history = ["Home"]
    currentPageHistoryIndex = 0

    for command in commands:
        if not isinstance(command, str) or len(command) == 0:
            return None
        
        if command.startswith(visitPagePrefix):
            history = history[0:(currentPageHistoryIndex + 1)]
            history.append(command[len(visitPagePrefix):])
            currentPageHistoryIndex += 1
        elif command == "Back":
            if currentPageHistoryIndex > 0:
                currentPageHistoryIndex -= 1
        elif command == "Forward":
            if currentPageHistoryIndex != len(history) - 1:
                currentPageHistoryIndex += 1
        else:
            return None
    
    return history[currentPageHistoryIndex]

tests = tests = [
    [["Visit About Us", "Back", "Forward"], "About Us"],
    [["Forward"], "Home"],
    [["Back"], "Home"],
    [["Visit About Us", "Visit Gallery"], "Gallery"],
    [["Visit About Us", "Visit Gallery", "Back", "Back"], "Home"],
    [["Visit About", "Visit Gallery", "Back", "Visit Contact", "Forward"], "Contact"],
    [["Visit About Us", "Visit Visit Us", "Forward", "Visit Contact Us", "Back"], "Visit Us"],
]

if __name__ == "__main__":
    failures = []

    for test in tests:
        commands = test[0]
        expected = test[1]
        actual = navigate(commands)
        success = expected == actual
        print(f"Testing {commands} (expecting \"{expected}\")...\"{actual}\" (success: {success}).")
        
        if not success:
            failures.append(commands)
    
    if len(failures) > 0:
        print("The following inputs failed:")

        for failure in failures:
            print(f"  {failure}.")
    else:
        print("All tests passed!")
