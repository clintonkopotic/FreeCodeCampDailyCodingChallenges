// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-08
function goldilocksZone(mass) {
  if (!Number.isFinite(mass) || mass <= 0) {
    return undefined;
  }

  const luminosityOfStar = Math.pow(mass, 3.5);
  const squareRootOfLuminosityOfStar = Math.sqrt(luminosityOfStar);
  const startOfZone = 0.95 * squareRootOfLuminosityOfStar;
  const endOfZone = 1.37 * squareRootOfLuminosityOfStar;

  return [parseFloat(startOfZone.toFixed(2)), parseFloat(endOfZone.toFixed(2))];
}

const tests = [
  [1, [0.95, 1.37]],
  [0.5, [0.28, 0.41]],
  [6, [21.85, 31.51]],
  [3.7, [9.38, 13.52]],
  [20, [179.69, 259.13]],
];
const failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 2) {
    continue;
  }

  const mass = test[0];
  const expected = test[1];
  const actual = goldilocksZone(mass);
  const success = areArraysIdentical(expected, actual);

  console.log(`Testing ${mass} (expecting [${expected}])...[${actual}] (success: ${success}).`);

  if (!success) {
    failures.push();
  }
}

if (Array.isArray(failures) && failures.length !== 0) {
  console.log("The following inputs failed:");

  for (const failure of failures) {
    console.log(`  ${failure}.`);
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
