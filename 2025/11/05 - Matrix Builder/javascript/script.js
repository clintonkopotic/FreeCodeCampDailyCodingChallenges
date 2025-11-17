// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-11-05
function buildMatrix(rows, cols) {
  if (!Number.isInteger(rows) || rows < 0 || !Number.isInteger(cols) || cols < 0) {
    return undefined;
  }

  const matrix = [];
  const row = [];

  for (let j = 0; j < cols; j++) {
    row.push(0);
  }

  for (let i = 0; i < rows; i++) {
    matrix.push(row);
  }

  return matrix;
}

const tests = [
  [2, 3, [[0, 0, 0], [0, 0, 0]]],
  [3, 2, [[0, 0], [0, 0], [0, 0]]],
  [4, 3, [[0, 0, 0], [0, 0, 0], [0, 0, 0], [0, 0, 0]]],
  [9, 1, [[0], [0], [0], [0], [0], [0], [0], [0], [0]]],
];
const failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 3) {
    continue;
  }

  const rows = test[0];
  const cols = test[1];
  const expected = test[2];
  const actual = buildMatrix(rows, cols);
  const success = areMatricesIdentical(expected, actual);

  console.log(`Testing ${rows} and ${cols} (expecting ${getMatrixAsString(expected)})...${getMatrixAsString(actual)} (success: ${success}).`);

  if (!success) {
    failures.push([rows, cols]);
  }
}

if (Array.isArray(failures) && failures.length !== 0) {
  console.log("The following inputs failed:");

  for (const failure of failures) {
    if (!Array.isArray(failure) || failure.length !== 2) {
      continue;
    }

    const rows = failure[0];
    const cols = failure[1];

    console.log(`  ${rows} and ${cols}.`);
  }
}
else {
  console.log("All tests passed!");
}

function areArraysIdentical(arr1, arr2) {
  // First, check if the two parameters are arrays.
  if (!Array.isArray(arr1) || !Array.isArray(arr2)) {
    return undefined;
  }

  // Check if the lengths of the arrays are different.
  // If they are, the arrays cannot be identical.
  if (arr1.length !== arr2.length) {
    return false;
  }

  // Iterate through the elements of the arrays.
  // Compare each element at the same index in both arrays.
  for (let i = 0; i < arr1.length; i++) {
    // If any element at a given index does not match,
    // the arrays are not identical.
    if (arr1[i] !== arr2[i]) {
      return false;
    }
  }

  // If the loop completes without finding any mismatches,
  // the arrays are identical in content and order.
  return true;
}

function areMatricesIdentical(matrixA, matrixB) {
  const matrixARowsAndColumns = getMatrixRowsAndColumns(matrixA);
  const matrixBRowsAndColumns = getMatrixRowsAndColumns(matrixB);

  if (!Array.isArray(matrixARowsAndColumns) || !Array.isArray(matrixBRowsAndColumns) || matrixARowsAndColumns.length !== 2 || matrixBRowsAndColumns.length !== 2) {
    return undefined;
  }

  if (!areArraysIdentical(matrixARowsAndColumns, matrixBRowsAndColumns)) {
    return false;
  }

  const rows = matrixARowsAndColumns[0];
  const columns = matrixARowsAndColumns[1];

  for (let i = 0; i < rows; i++) {
    for (let j = 0; j < columns; j++) {
      if (matrixA[i][j] !== matrixB[i][j]) {
        return false;
      }
    }
  }

  return true;
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
