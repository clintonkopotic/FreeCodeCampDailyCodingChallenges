// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-09-26
function speeding(speeds, limit) {
  if (!Array.isArray(speeds) || speeds.length == 0 || !Number.isFinite(limit) || limit < 0) {
    return undefined;
  }

  let numberSpeeding = 0;
  let averageSpeedOver = 0;

  for (const speed of speeds) {
    if (Number.isFinite(speed) && speed > limit ) {
      averageSpeedOver += speed - limit;
      numberSpeeding++;
    }
  }

  if (numberSpeeding !== 0) {
    averageSpeedOver /= numberSpeeding;
  }

  return [numberSpeeding, averageSpeedOver];
}

const tests = [
  [[50, 60, 55], 60, [0, 0]],
  [[58, 50, 60, 55], 55, [2, 4]],
  [[61, 81, 74, 88, 65, 71, 68], 70, [4, 8.5]],
  [[100, 105, 95, 102], 100, [2, 3.5]],
  [[40, 45, 44, 50, 112, 39], 55, [1, 57]],
];
const failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 3) {
    continue;
  }

  const speeds = test[0];
  const limit = test[1];
  const expected = test[2];
  const actual = speeding(speeds, limit);
  const success = areArraysIdentical(expected, actual);

  console.log(`Testing [${speeds}] and ${limit} (expecting [${expected}])...[${actual}] (success: ${success}).`);

  if (!success) {
    failures.push([speeds, limit]);
  }
}

if (Array.isArray(failures) && failures.length !== 0) {
  console.log("The following inputs failed:");

  for (const failure of failures) {
    if (!Array.isArray(failure) || failure.length !== 2) {
      continue;
    }

    const speeds = failure[0];
    const limit = failure[1];

    console.log(`  [${speeds}] and ${limit}.`);
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
