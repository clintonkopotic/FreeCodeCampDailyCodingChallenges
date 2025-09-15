// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-09-10
function arrayDiff(arr1, arr2) {
  if (!Array.isArray(arr1) || !Array.isArray(arr2)) {
    return undefined;
  }

  let result = [];

  for (let i = 0; i < arr1.length; i++) {
    let arr1item = arr1[i];

    if (!arr2.includes(arr1item)) {
      result.push(arr1item);
    }
  }

  for (let i = 0; i < arr2.length; i++) {
    let arr2item = arr2[i];

    if (!arr1.includes(arr2item)) {
      result.push(arr2item);
    }
  }

  return result.sort();
}

const tests = [
  [["apple", "banana"], ["apple", "banana", "cherry"], ["cherry"]],
  [["apple", "banana", "cherry"], ["apple", "banana"], ["cherry"]],
  [["one", "two", "three", "four", "six"], ["one", "three", "eight"], ["eight", "four", "six", "two"]],
  [["two", "four", "five", "eight"], ["one", "two", "three", "four", "seven", "eight"], ["five", "one", "seven", "three"]],
  [["I", "like", "freeCodeCamp"], ["I", "like", "rocks"], ["freeCodeCamp", "rocks"]],
];

let failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 3) {
    continue;
  }

  const arr1 = test[0];
  const arr2 = test[1];
  const expected = test[2];

  if (!Array.isArray(arr1) || !Array.isArray(arr2) || !Array.isArray(expected)) {
    continue;
  }

  const actual = arrayDiff(arr1, arr2);
  const success = areArraysIdentical(expected, actual);
  console.log("Testing [" + arr1 + "], [" + arr2 + "] (expecting [" + expected + "])...[" + actual + "] (success: " + success + ").");

  if (success !== true) {
    failures.push([arr1, arr2]);
  }
}

if (Array.isArray(failures) && failures.length > 0) {
  console.log("The following inputs failed:");

  for (const failure of failures) {
    if (!Array.isArray(failure) || failure.length !== 2) {
      continue;
    }

    console.log("  [" + failure[0] + "], [" + failure[1] + "].");
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
