// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-09-13
function findMissingNumbers(arr) {
  if (!Array.isArray(arr)) {
    return undefined;
  }

  if (arr.length === 0) {
    return [];
  }

  // Find the largest number in arr.
  let n = 0;

  for (let i = 0; i < arr.length; i++) {
    let number = arr[i];

    if (Number.isInteger(number) && number > n) {
      n = number;
    }
  }

  // Now count from 1 to n-1 and keep track any numbers no included in arr.
  let missingNumbers = [];

  for (let i = 1; i < n; i++) {
    if (!arr.includes(i)) {
      missingNumbers.push(i);
    }
  }

  return missingNumbers;
}

const tests = [
  [[1, 3, 5],  [2, 4]],
  [[1, 2, 3, 4, 5], []],
  [[1, 10], [2, 3, 4, 5, 6, 7, 8, 9]],
  [[10, 1, 10, 1, 10, 1], [2, 3, 4, 5, 6, 7, 8, 9]],
  [[3, 1, 4, 1, 5, 9], [2, 6, 7, 8]],
  [[1, 2, 3, 4, 5, 7, 8, 9, 10, 12, 6, 8, 9, 3, 2, 10, 7, 4], [11]],
];

let failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 2) {
    continue;
  }

  const arr = test[0];
  const expected = test[1];

  if (!Array.isArray(arr) || !Array.isArray(expected)) {
    continue;
  }

  const actual = findMissingNumbers(arr);
  const success = areArraysIdentical(expected, actual);
  console.log("Testing [" + arr + "] (expecting [" + expected + "])...[" + actual + "] (success: " + success + ").");

  if (success !== true) {
    failures.push(arr);
  }
}

if (Array.isArray(failures) && failures.length > 0) {
  console.log("The following inputs failed:");

  for (const failure of failures) {
    console.log("  [" + failure + "].");
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
