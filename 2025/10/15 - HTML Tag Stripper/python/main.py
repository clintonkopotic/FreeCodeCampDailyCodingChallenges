# https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-15
import re

def strip_tags(html):
    if not isinstance(html, str):
        return None
    
    return re.sub(r"<.*?>", "", html)

tests = tests = [
    ['<a href="#">Click here</a>', "Click here"],
    ['<p class="center">Hello <b>World</b>!</p>', "Hello World!"],
    ['<img src="cat.jpg" alt="Cat">', ""],
    ['<main id="main"><section class="section">section</section><section class="section">section</section></main>', "sectionsection"],
]

if __name__ == "__main__":
    failures = []

    for test in tests:
        html = test[0]
        expected = test[1]
        actual = strip_tags(html)
        success = expected == actual
        print(f"Testing \"{html}\" (expecting \"{expected}\")...\"{actual}\" (success: {success}).")
        
        if not success:
            failures.append(html)
    
    if len(failures) > 0:
        print("The following inputs failed:")

        for failure in failures:
            print(f"  \"{failure}\".")
    else:
        print("All tests passed!")
