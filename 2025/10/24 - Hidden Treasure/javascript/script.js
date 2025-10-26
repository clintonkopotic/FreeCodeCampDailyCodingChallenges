// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-24
function dive(map, coordinates) {
  if (!Array.isArray(map) || map.length === 0 || !Array.isArray(coordinates) || coordinates.length !== 2) {
    return undefined;
  }

  const rows = map.length;
  let columns = 0;
  let unfoundPartsCount = 0;
  const cellValues = ['-', 'O', 'X'];

  for (const row of map) {
    if (!Array.isArray(row) || row.length === 0 || (columns !== 0 && columns !== row.length)) {
      return undefined;
    }

    if (columns === 0) {
      columns = row.length;
    }

    for (const cell of row) {
      if (typeof cell !== 'string' || !cellValues.includes(cell)) {
        return undefined;
      }

      if (cell === 'O') {
        unfoundPartsCount++;
      }
    }
  }

  const rowIndex = coordinates[0];
  const columnIndex = coordinates[1];

  if (!Number.isInteger(rowIndex) || rowIndex < 0 || rowIndex >= rows || !Number.isInteger(columnIndex) || columnIndex < 0 || columnIndex >= columns) {
    return undefined;
  }

  const cell = map[rowIndex][columnIndex];

  if (cell === '-') {
    return "Empty";
  }
  else if (cell === 'O') {
    unfoundPartsCount--;
  }

  return unfoundPartsCount === 0 ? "Recovered" : "Found";
}

const tests = [
  [[["-", "X"], ["-", "X"], ["-", "O"]], [2, 1], "Recovered"],
  [[["-", "X"], ["-", "X"], ["-", "O"]], [2, 0], "Empty"],
  [[["-", "X"], ["-", "O"], ["-", "O"]], [1, 1], "Found"],
  [[["-", "-", "-"], ["X", "O", "X"], ["-", "-", "-"]], [1, 2], "Found"],
  [[["-", "-", "-"], ["-", "-", "-"], ["O", "X", "X"]], [2, 0], "Recovered"],
  [[["-", "-", "-"], ["-", "-", "-"], ["O", "X", "X"]], [1, 2], "Empty"],
];
const failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 3) {
    continue;
  }

  const map = test[0];
  const coordinates = test[1];
  const expected = test[2];
  const actual = dive(map, coordinates);
  const success = expected === actual;

  // eslint-disable-next-line no-useless-escape
  console.log(`Testing ${getMatrixAsString(map)} and [${coordinates}] (expecting \"${expected}\")...\"${actual}\" (success: ${success}).`);

  if (!success) {
    failures.push([map, coordinates]);
  }
}

if (Array.isArray(failures) && failures.length !== 0) {
  console.log("The following inputs failed:");

  for (const failure of failures) {
    if (!Array.isArray(failure) || failure.length !== 2) {
      continue;
    }

    const map = failure[0];
    const coordinates = failure[1];

    // eslint-disable-next-line no-useless-escape
    console.log(`  ${getMatrixAsString(map)} and [${coordinates}].`);
  }
}
else {
  console.log("All tests passed!");
}

function getMatrixAsString(matrix) {
  const matrixRowsColumns = getMatrixRowsAndColumns(matrix);

  if (matrixRowsColumns === undefined) {
    return undefined;
  }

  const [rows, columns] = matrixRowsColumns;
  let result = "[";

  for (let rowIndex = 0; rowIndex < rows; rowIndex++) {
    const row = matrix[rowIndex];
    if (rowIndex > 0) {
      result += ", ";
    }

    result += "[";

    for (let columnIndex = 0; columnIndex < columns; columnIndex++) {
      if (columnIndex > 0) {
        result += ",";
      }

      result += row[columnIndex];
    }

    result += "]";
  }

  return result + "]";
}

function getMatrixRowsAndColumns(matrix) {
  if (!Array.isArray(matrix)) {
    return undefined;
  }

  const rows = matrix.length;

  if (rows === 0) {
    return undefined;
  }

  const firstRow = matrix[0];

  if (!Array.isArray(firstRow)) {
    return undefined;
  }

  const columns = firstRow.length;

  if (columns === 0) {
    return undefined;
  }

  for (let rowIndex = 1; rowIndex < rows; rowIndex++) {
    const row = matrix[rowIndex];

    if (!Array.isArray(row) || row.length !== columns) {
      return undefined;
    }
  }

  return [rows, columns];
}
