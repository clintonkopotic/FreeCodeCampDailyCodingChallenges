# https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-30
import math

def nth_prime(n):
    if (not isinstance(n, int) and not isinstance(n, float)) or (isinstance(n, float) and not n.is_integer()) or n <= 0:
        return None

    def is_prime(n):
        if (not isinstance(n, int) and not isinstance(n, float)) or (isinstance(n, float) and not n.is_integer()) or n <= 0:
            return None
        
        # 2 is the only even prime number.
        if n == 2:
            return True
        
        # Even numbers greater than 2 are not prime.
        if n % 2 == 0:
            return False
        
        # Check for divisibility by odd numbers from 3 up to the square root of num.
        # We only need to check up to the square root because if a number has a divisor
        # greater than its square root, it must also have a divisor smaller than its square root.
        limit = math.sqrt(n)
        i = 3
        
        while i <= limit:
            if n % i == 0:
                return False
            
            i += 2

        return True
    
    prime = 2

    for i in range(1, n):
        number = prime + 1

        while not is_prime(number):
            number += 1
        
        prime = number

    return prime

tests = tests = [
    [5, 11],
    [10, 29],
    [16, 53],
    [99, 523],
    [1000, 7919],
]

if __name__ == "__main__":
    failures = []

    for test in tests:
        n = test[0]
        expected = test[1]
        actual = nth_prime(n)
        success = expected == actual
        print(f"Testing {n} (expecting {expected})...{actual} (success: {success}).")
        
        if not success:
            failures.append(n)
    
    if len(failures) > 0:
        print("The following inputs failed:")

        for failure in failures:
            print(f"  {failure}.")
    else:
        print("All tests passed!")
