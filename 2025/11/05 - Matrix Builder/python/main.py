# https://www.freecodecamp.org/learn/daily-coding-challenge/2025-11-05
import math

def buildMatrix(rows, cols):
    if not isinstance(rows, int) or rows < 0 or not isinstance(cols, int) or cols < 0:
        return None
    
    matrix = []
    row = []

    for j in range(cols):
        row.append(0)
    
    for i in range(rows):
        matrix.append(row)
    
    return matrix

tests = tests = [
    [2, 3, [[0, 0, 0], [0, 0, 0]]],
    [3, 2, [[0, 0], [0, 0], [0, 0]]],
    [4, 3, [[0, 0, 0], [0, 0, 0], [0, 0, 0], [0, 0, 0]]],
    [9, 1, [[0], [0], [0], [0], [0], [0], [0], [0], [0]]],
]

if __name__ == "__main__":
    failures = []

    for test in tests:
        rows = test[0]
        cols = test[1]
        expected = test[2]
        actual = buildMatrix(rows, cols)
        success = expected == actual
        print(f"Testing {rows} and {cols} (expecting {expected})...{actual} (success: {success}).")
        
        if not success:
            failures.append([rows, cols])
    
    if len(failures) > 0:
        print("The following inputs failed:")

        for failure in failures:
            rows = failure[0]
            cols = failure[1]
            print(f"  {rows} and {cols}.")
    else:
        print("All tests passed!")
