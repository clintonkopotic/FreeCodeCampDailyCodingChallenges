// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-07
function findLandingSpot(matrix) {
  if (!Array.isArray(matrix) || matrix.length === 0) {
    return undefined;
  }

  // Traverse the matix first to ensure all elements are numbers between 0 and 9, inclusive and to ensure each array in the array have the same number of columns in them.
  const rows = matrix.length;
  let columns = 0;

  for (let i = 0; i < rows; i++) {
    const row = matrix[i];

    if (!Array.isArray(row) || row.length === 0 || (columns !== 0 && columns !== row.length)) {
      return undefined;
    }

    if (columns === 0) {
      columns = row.length;
    }

    for (let j = 0; j < columns; j++) {
      const value = row[j];

      if (!Number.isInteger(value) || value < 0 || value > 9) {
        return undefined;
      }
    }
  }

  // Now traverse the matix to find the landing spot.
  let minNeighborTotal = -1;
  let landingSpot = [-1, -1];

  for (let i = 0; i < rows; i++) {
    for (let j = 0; j < columns; j++) {
      if (matrix[i][j] !== 0) {
        continue;
      }

      const northNeighborValue = i > 0 ? matrix[i - 1][j] : 0;
      const eastNeighborValue = j < (columns - 1) ? matrix[i][j + 1] : 0;
      const southNeighborValue = i < (rows - 1) ? matrix[i + 1][j] : 0;
      const westNeighborsValue = j > 0 ? matrix[i][j - 1] : 0;
      const neighborTotal = northNeighborValue + eastNeighborValue + southNeighborValue + westNeighborsValue;

      if (minNeighborTotal < 0 || neighborTotal < minNeighborTotal) {
        minNeighborTotal = neighborTotal;
        landingSpot = [i, j];
      }
    }
  }

  return landingSpot;
}

const tests = [
  [[[1, 0], [2, 0]], [0, 1]],
  [[[9, 0, 3], [7, 0, 4], [8, 0, 5]], [1, 1]],
  [[[1, 2, 1], [0, 0, 2], [3, 0, 0]], [2, 2]],
  [[[9, 6, 0, 8], [7, 1, 1, 0], [3, 0, 3, 9], [8, 6, 0, 9]], [2, 1]],
];
const failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 2) {
    continue;
  }

  const matrix = test[0];
  const expected = test[1];
  const actual = findLandingSpot(matrix);
  const success = expected === actual;

  console.log(`Testing ${getMatrixAsString(matrix)}] (expecting [${expected}])...[${actual}] (success: ${success}).`);

  if (!success) {
    failures.push();
  }
}

if (Array.isArray(failures) && failures.length !== 0) {
  console.log("The following inputs failed:");

  for (const failure of failures) {
    console.log(`  ${getMatrixAsString(failure)}.`);
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
