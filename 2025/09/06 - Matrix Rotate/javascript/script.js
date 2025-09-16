// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-09-06
function rotate(matrix) {
  const matrixRowsColumns = getMatrixRowsAndColumns(matrix);

  if (matrixRowsColumns === undefined) {
    return undefined;
  }
  
  const [rows, columns] = matrixRowsColumns;
  let rotated = [];

  for (let columnIndex = 0; columnIndex < columns; columnIndex++) {
    let row = [];

    for (let rowIndex = rows - 1; rowIndex >= 0; rowIndex--) {
      row.push(matrix[rowIndex][columnIndex]);
    }

    rotated.push(row);
  }

  return rotated;
}

const tests = [
  [[[1]], [[1]]],
  [[[1, 2], [3, 4]], [[3, 1], [4, 2]]],
  [[[1, 2, 3], [4, 5, 6], [7, 8, 9]], [[7, 4, 1], [8, 5, 2], [9, 6, 3]]],
  [[[0, 1, 0], [1, 0, 1], [0, 0, 0]], [[0, 1, 0], [0, 0, 1], [0, 1, 0]]],
];

let failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 2) {
    continue;
  }

  const matrix = test[0];
  const expected = test[1];

  if (!Array.isArray(matrix) || !Array.isArray(expected)) {
    continue;
  }

  const actual = rotate(matrix);
  const success = areMatricesIdentical(expected, actual);
  console.log(`Testing ${getMatrixAsString(matrix)} (expecting ${getMatrixAsString(expected)})...${getMatrixAsString(actual)} (success: ${success}).`);

  if (success !== true) {
    failures.push(matrix);
  }
}

if (Array.isArray(failures) && failures.length > 0) {
  console.log("The following inputs failed:");

  for (const failure of failures) {
    console.log(`  ${getMatrixAsString(failure)}.`)
  }
}
else {
  console.log("All tests passed!");
}

function areArraysIdentical(arr1, arr2) {
  // First, check if the lengths of the arrays are different.
  // If they are, the arrays cannot be identical.
  if (!Array.isArray(arr1) || !Array.isArray(arr2) || arr1.length !== arr2.length) {
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

function areMatricesIdentical(matrix1, matrix2) {
  const matrix1RowsColumns = getMatrixRowsAndColumns(matrix1);

  if (matrix1RowsColumns === undefined) {
    return false;
  }

  const matrix2RowsColumns = getMatrixRowsAndColumns(matrix2);

  if (matrix2RowsColumns === undefined) {
    return false;
  }

  if (!areArraysIdentical(matrix1RowsColumns, matrix2RowsColumns)) {
    return false;
  }

  const [rows, columns] = matrix1RowsColumns;

  for (let rowIndex = 0; rowIndex < rows; rowIndex++) {
    for (let columnIndex = 0; columnIndex < columns; columnIndex++) {
      if (matrix1[rowIndex][columnIndex] !== matrix2[rowIndex][columnIndex]) {
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
        result += ", ";
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
