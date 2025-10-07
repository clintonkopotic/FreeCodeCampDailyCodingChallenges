// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-08-13
function fibonacciSequence(startSequence, length) {
  if (!Array.isArray(startSequence) || startSequence.length !== 2 || !Number.isInteger(length) || length < 0) {
    return undefined;
  }
  
  const result = []

  for (let i = 0; i < length; i++) {
    if (i < 2) {
      result.push(startSequence[i]);
    }
    else {
      result.push(result[i - 2] + result[i - 1]);
    }
  }

  return result;
}

const tests = [
  [[0, 1], 20, [0, 1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144, 233, 377, 610, 987, 1597, 2584, 4181]],
  [[21, 32], 1, [21]],
  [[0, 1], 0, []],
  [[10, 20], 2, [10, 20]],
  [[123456789, 987654321], 5, [123456789, 987654321, 1111111110, 2098765431, 3209876541]],
];
const failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 3) {
    continue;
  }

  const startSequence = test[0];
  const length = test[1];
  const expected = test[2];
  const actual = fibonacciSequence(startSequence, length);
  const success = areArraysIdentical(expected, actual);

  console.log(`Testing ${valueToString(startSequence)} and ${valueToString(length)} (expecting ${valueToString(expected)})...${valueToString(actual)} (success: ${valueToString(success)}).`);

  if (!success) {
    failures.push([startSequence, length]);
  }
}

if (Array.isArray(failures) && failures.length !== 0) {
  console.log("The following inputs failed:");

  for (const failure of failures) {
    if (!Array.isArray(failure) || failure.length !== 2) {
      continue;
    }

    const startSequence = failure[0];
    const length = failure[1];

    console.log(`  ${valueToString(startSequence)} and ${valueToString(length)}.`);
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
