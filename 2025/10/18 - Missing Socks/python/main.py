# https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-15
import math

def sock_pairs(pairs, cycles):
    def is_integer(value):
        return isinstance(value, int) or (isinstance(value, float) and value.is_integer())
    
    if not is_integer(pairs) or not is_integer(cycles):
        return None
    
    numberOfSocks = pairs * 2

    for cycle in range(1, cycles + 1):
        # Every 2 wash cycles, you lose a single sock.
        if cycle % 2 == 0:
            numberOfSocks -= 1

        # Every 3 wash cycles, you find a single missing sock.
        if cycle % 3 == 0:
            numberOfSocks += 1

        # Every 5 wash cycles, a single sock is worn out and must be thrown away.
        if cycle % 5 == 0:
            numberOfSocks -= 1

        # Every 10 wash cycles, you buy a pair of socks.
        if cycle % 10 == 0:
            numberOfSocks += 2

        # You can never have less than zero total socks.
        if numberOfSocks < 0:
            numberOfSocks = 0

    # Return the number of complete pairs of socks.
    return math.floor(numberOfSocks / 2)

tests = tests = [
    [2, 5, 1],
    [1, 2, 0],
    [5, 11, 4],
    [6, 25, 3],
    [1, 8, 0],
]

if __name__ == "__main__":
    failures = []

    for test in tests:
        pairs = test[0]
        cycles = test[1]
        expected = test[2]
        actual = sock_pairs(pairs, cycles)
        success = expected == actual
        print(f"Testing {pairs} and {cycles} (expecting {expected})..{actual} (success: {success}).")
        
        if not success:
            failures.append([pairs, cycles])
    
    if len(failures) > 0:
        print("The following inputs failed:")

        for failure in failures:
            pairs = failure[0]
            cycles = failure[1]
            print(f"  {pairs} and {cycles}.")
    else:
        print("All tests passed!")
