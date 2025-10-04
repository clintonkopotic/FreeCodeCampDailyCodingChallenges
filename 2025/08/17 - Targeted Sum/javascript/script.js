// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-08-17
function findTarget(arr, target) {
  if (!Array.isArray(arr) || arr.length === 0 || !Number.isInteger(target)) {
    return undefined;
  }

  for (let i = 0; i < arr.length; i++) {
    const number0 = arr[i];

    if (!Number.isInteger(number0)) {
      return undefined;
    }

    for (let j = 0; j < arr.length; j++) {
      if (j === i) {
        continue;
      }

      const number1 = arr[j];

      if (!Number.isInteger(number1)) {
        return undefined;
      }

      if (number0 + number1 === target) {
        return [i, j];
      }
    }
  }

  return "Target not found";
}

const tests = [
  [[2, 7, 11, 15], 9, [0, 1]],
  [[3, 2, 4, 5], 6, [1, 2]],
  [[1, 3, 5, 6, 7, 8], 15, [4, 5]],
  [[1, 3, 5, 7], 14, "Target not found"],
];
const failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 3) {
    continue;
  }

  const arr = test[0];
  const target = test[1];
  const expected = test[2];
  const actual = findTarget(arr, target);
  const success = Array.isArray(expected) ? areArraysIdentical(expected, actual) : expected === actual;
  
  console.log(`Testing ${valueToString(arr)} and ${valueToString(target)} (expecting ${valueToString(expected)})...${valueToString(actual)} (success: ${valueToString(success)}).`);

  if (!success) {
    failures.push([arr, target]);
  }
}

if (Array.isArray(failures) && failures.length !== 0) {
  console.log("The following inputs failed:");

  for (const failure of failures) {
    if (!Array.isArray(failure) || failure.length !== 2) {
      continue;
    }

    const arr = failure[0];
    const target = failure[1];

    console.log(`  [${valueToString(arr)}] and ${valueToString(target)}.`);
  }
}
else {
  console.log("All tests passed!");
}

function valueToString(value) {
  if (Array.isArray(value)) {
    return `[${value}]`;
  }
  else if (typeof value === 'string') {
    // eslint-disable-next-line no-useless-escape
    return `\"${value}\"`;
  }
  else {
    return `${value}`;
  }
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
