# https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-24
import math

def dive(map, coordinates):
    if not isinstance(map, list) or len(map) == 0 or not isinstance(coordinates, list) or len(coordinates) != 2:
        return None
    
    # Traverse the map first to ensure all elements are members of cell_values and to ensure each list in the list have the same number of columns in them.
    rows = len(map)
    columns = 0
    unfoundPartsCount = 0
    cell_values = set(['-', 'O', 'X'])

    for i in range(rows):
        row = map[i]

        if not isinstance(row, list) or len(row) == 0 or (columns != 0 and columns != len(row)):
            return None
        
        if columns == 0:
            columns = len(row)
        
        for j in range(columns):
            cell = row[j]

            if not isinstance(cell, str) or cell not in cell_values:
                return None
            
            if cell == 'O':
                unfoundPartsCount += 1
    
    row_index = coordinates[0]
    column_index = coordinates[1]

    if not isinstance(row_index, int) or row_index < 0 or row_index >= rows or not isinstance(column_index, int) or column_index < 0 or column_index >= columns:
        return None
    
    cell = map[row_index][column_index]

    if cell == '-':
        return "Empty"
    elif cell == 'O':
        unfoundPartsCount -= 1
    
    return "Recovered" if unfoundPartsCount == 0 else "Found"

tests = tests = [
    [[["-", "X"], ["-", "X"], ["-", "O"]], [2, 1], "Recovered"],
    [[["-", "X"], ["-", "X"], ["-", "O"]], [2, 0], "Empty"],
    [[["-", "X"], ["-", "O"], ["-", "O"]], [1, 1], "Found"],
    [[["-", "-", "-"], ["X", "O", "X"], ["-", "-", "-"]], [1, 2], "Found"],
    [[["-", "-", "-"], ["-", "-", "-"], ["O", "X", "X"]], [2, 0], "Recovered"],
    [[["-", "-", "-"], ["-", "-", "-"], ["O", "X", "X"]], [1, 2], "Empty"],
]

if __name__ == "__main__":
    failures = []

    for test in tests:
        map = test[0]
        coordinates = test[1]
        expected = test[2]
        actual = dive(map, coordinates)
        success = expected == actual
        print(f"Testing {map} and {coordinates} (expecting \"{expected}\")...\"{actual}\" (success: {success}).")
        
        if not success:
            failures.append(map)
    
    if len(failures) > 0:
        print("The following inputs failed:")

        for failure in failures:
            map = failure[0]
            coordinates = failure[1]
            print(f"  {map} and {coordinates}.")
    else:
        print("All tests passed!")
