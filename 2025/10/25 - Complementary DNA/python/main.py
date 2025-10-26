# https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-25

def complementary_DNA(strand):
    if not isinstance(strand, str):
        return None
    
    result = ""

    for letter in strand:
        if not isinstance(letter, str) or len(letter) != 1:
            return None
        
        if letter == 'A':
            result += 'T'
        elif letter == 'T':
            result += 'A'
        elif letter == 'C':
            result += 'G'
        elif letter == 'G':
            result += 'C'
        else:
            return None

    return result

tests = tests = [
    ["ACGT", "TGCA"],
    ["ATGCGTACGTTAGC", "TACGCATGCAATCG"],
    ["GGCTTACGATCGAAG", "CCGAATGCTAGCTTC"],
    ["GATCTAGCTAGGCTAGCTAG", "CTAGATCGATCCGATCGATC"],
]

if __name__ == "__main__":
    failures = []

    for test in tests:
        strand = test[0]
        expected = test[1]
        actual = complementary_DNA(strand)
        success = expected == actual
        print(f"Testing \"{strand}\" (expecting \"{expected}\")...\"{actual}\" (success: {success}).")
        
        if not success:
            failures.append(strand)
    
    if len(failures) > 0:
        print("The following inputs failed:")

        for failure in failures:
            print(f"  \"{failure}\".")
    else:
        print("All tests passed!")
