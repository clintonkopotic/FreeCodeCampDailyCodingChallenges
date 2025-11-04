# https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-31

def spookify(boo):
    if not isinstance(boo, str):
        return None
    
    spookyTilde = boo.replace('_', '~').replace('-', '~')
    spookyCase = ""
    capitalizeLetter = True

    for character in spookyTilde:
        spookyCase += character.upper() if capitalizeLetter else character.lower()

        if character != '~':
            capitalizeLetter = not capitalizeLetter

    return spookyCase

tests = tests = [
    ["hello_world", "HeLlO~wOrLd"],
    ["Spooky_Case", "SpOoKy~CaSe"],
    ["TRICK-or-TREAT", "TrIcK~oR~tReAt"],
    ["c_a-n_d-y_-b-o_w_l", "C~a~N~d~Y~~b~O~w~L"],
    ["thE_hAUntEd-hOUsE-Is-fUll_Of_ghOsts", "ThE~hAuNtEd~HoUsE~iS~fUlL~oF~gHoStS"],
]

if __name__ == "__main__":
    failures = []

    for test in tests:
        boo = test[0]
        expected = test[1]
        actual = spookify(boo)
        success = expected == actual
        print(f"Testing \"{boo}\" (expecting \"{expected}\")...\"{actual}\" (success: {success}).")
        
        if not success:
            failures.append(boo)
    
    if len(failures) > 0:
        print("The following inputs failed:")

        for failure in failures:
            print(f"  \"{failure}\".")
    else:
        print("All tests passed!")
