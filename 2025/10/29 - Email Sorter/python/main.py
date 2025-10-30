# https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-29

def sort(emails):
    if (not isinstance(emails, list)):
        return None
    
    def parseEmail(email):
        if not isinstance(email, str) or len(email) <= 0:
            return None
        
        emailParts = email.lower().split('@')

        if not isinstance(emailParts, list) or len(emailParts) != 2:
            return None
        
        return [emailParts[1], emailParts[0]]
    
    return sorted(emails, key=lambda email: ' '.join(parseEmail(email)))

tests = tests = [
    [["jill@mail.com", "john@example.com", "jane@example.com"], ["jane@example.com", "john@example.com", "jill@mail.com"]],
    [["bob@mail.com", "alice@zoo.com", "carol@mail.com"], ["bob@mail.com", "carol@mail.com", "alice@zoo.com"]],
    [["user@z.com", "user@y.com", "user@x.com"], ["user@x.com", "user@y.com", "user@z.com"]],
    [["sam@MAIL.com", "amy@mail.COM", "bob@Mail.com"], ["amy@mail.COM", "bob@Mail.com", "sam@MAIL.com"]],
    [["simon@beta.com", "sammy@alpha.com", "Sarah@Alpha.com", "SAM@ALPHA.com", "Simone@Beta.com", "sara@alpha.com"], ["SAM@ALPHA.com", "sammy@alpha.com", "sara@alpha.com", "Sarah@Alpha.com", "simon@beta.com", "Simone@Beta.com"]],
]

if __name__ == "__main__":
    failures = []

    for test in tests:
        emails = test[0]
        expected = test[1]
        actual = sort(emails)
        success = expected == actual
        print(f"Testing {emails} (expecting {expected})...{actual} (success: {success}).")
        
        if not success:
            failures.append(emails)
    
    if len(failures) > 0:
        print("The following inputs failed:")

        for failure in failures:
            print(f"  {failure}.")
    else:
        print("All tests passed!")
