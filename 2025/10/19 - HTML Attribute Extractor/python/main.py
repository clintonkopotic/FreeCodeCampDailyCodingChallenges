# https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-19
import re

def extract_attributes(element):
    if not isinstance(element, str):
        return None
    
    # Adapted from https://stackoverflow.com/a/317081
    regex = r"([\w|data-]+)=[\"']?((?:.(?![\"']?\s+(?:\S+)=|\s*\/?[>\"']))+.)[\"']?"
    matches = re.finditer(regex, element)
    result = []

    for _, match in enumerate(matches, start=1):
        result.append(f"{match.group(1)}, {match.group(2)}")
    
    return result

tests = tests = [
    ['<span class="red"></span>', ["class, red"]],
    ['<meta charset="UTF-8" />', ["charset, UTF-8"]],
    ["<p>Lorem ipsum dolor sit amet</p>", []],
    ['<input name="email" type="email" required="true" />', ["name, email", "type, email", "required, true"]],
    ['<button id="submit" class="btn btn-primary">Submit</button>', ["id, submit", "class, btn btn-primary"]],
]

if __name__ == "__main__":
    failures = []

    for test in tests:
        element = test[0]
        expected = test[1]
        actual = extract_attributes(element)
        success = expected == actual
        print(f"Testing \'{element}\' (expecting {expected})...{actual} (success: {success}).")
        
        if not success:
            failures.append(element)
    
    if len(failures) > 0:
        print("The following inputs failed:")

        for failure in failures:
            print(f"  \"{failure}\".")
    else:
        print("All tests passed!")
