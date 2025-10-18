# https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-07
import math

def findLandingSpot(matrix):
    if not isinstance(matrix, list) or len(matrix) == 0:
        return None
    
    # Traverse the matix first to ensure all elements are numbers between 0 and 9, inclusive and to ensure each array in the array have the same number of columns in them.
    rows = len(matrix)
    columns = 0

    for i in range(rows):
        row = matrix[i]

        if not isinstance(row, list) or len(row) == 0 or (columns != 0 and columns != len(row)):
            return None
        
        if columns == 0:
            columns = len(row)
        
        for j in range(columns):
            value = row[j]

            if not isinstance(value, int) and not isinstance(value, float):
                return None
            elif isinstance(value, int) and (value < 0 or value > 9):
                return None
            elif isinstance(value, float) and (not value.is_integer() or value < 0.0 or value > 9.0):
                return None
    
    # Now traverse the matix to find the landing spot.
    minNeighborTotal = -1
    landingSpot = [-1, -1]

    for i in range(rows):
        for j in range(columns):
            if matrix[i][j] != 0:
                continue

            northNeighborValue = matrix[i - 1][j] if i > 0 else 0
            eastNeighborValue = matrix[i][j + 1] if j < (columns - 1) else 0
            southNeighborValue = matrix[i + 1][j] if i < (rows - 1) else 0
            westNeighborsValue = matrix[i][j - 1] if j > 0 else 0
            neighborTotal = northNeighborValue + eastNeighborValue + southNeighborValue + westNeighborsValue

            if minNeighborTotal < 0 or neighborTotal < minNeighborTotal:
                minNeighborTotal = neighborTotal
                landingSpot = [i, j]

    return landingSpot

tests = tests = [
    [[[1, 0], [2, 0]], [0, 1]],
    [[[9, 0, 3], [7, 0, 4], [8, 0, 5]], [1, 1]],
    [[[1, 2, 1], [0, 0, 2], [3, 0, 0]], [2, 2]],
    [[[9, 6, 0, 8], [7, 1, 1, 0], [3, 0, 3, 9], [8, 6, 0, 9]], [2, 1]],
]

if __name__ == "__main__":
    failures = []

    for test in tests:
        matrix = test[0]
        expected = test[1]
        actual = findLandingSpot(matrix)
        success = expected == actual
        print(f"Testing {matrix} (expecting {expected})...{actual} (success: {success}).")
        
        if not success:
            failures.append(matrix)
    
    if len(failures) > 0:
        print("The following inputs failed:")

        for failure in failures:
            print(f"  {failure}.")
    else:
        print("All tests passed!")
