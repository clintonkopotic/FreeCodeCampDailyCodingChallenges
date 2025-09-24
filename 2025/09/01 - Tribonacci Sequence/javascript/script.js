function tribonacciSequence(startSequence, length) {
  if (!Array.isArray(startSequence) || startSequence.length !== 3 || !Number.isInteger(length) || length < 0) {
    return undefined;
  }

  let result = [];

  for (let i = 0; i < length; i++) {
    if (i < 3) {
      const number = startSequence[i];

      if (!Number.isFinite(number)) {
        return undefined;
      }

      result.push(number);
    }
    else {
      result.push(result[i - 3] + result[i - 2] + result[i - 1]);
    }
  }

  return result;
}

const tests = [
  [[0, 0, 1], 20, [0, 0, 1, 1, 2, 4, 7, 13, 24, 44, 81, 149, 274, 504, 927, 1_705, 3_136, 5_768, 10_609, 19_513]],
  [[21, 32, 43], 1, [21]],
  [[0, 0, 1], 0, []],
  [[10, 20, 30], 2, [10, 20]],
  [[10, 20, 30], 3, [10, 20, 30]],
  [[123, 456, 789], 8, [123, 456, 789, 1368, 2613, 4770, 8751, 16134]],
];

let failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 3) {
    continue;
  }

  const startSequence = test[0];
  const length = test[1];
  const expected = test[2];

  if (!Array.isArray(startSequence) || startSequence.length !== 3 || !Number.isFinite(length) || length < 0 || !Array.isArray(expected)) {
    continue;
  }

  const actual = tribonacciSequence(startSequence, length);
  const success = areArraysIdentical(expected, actual);

  console.log(`Testing [${startSequence}] and ${length} (expecting [${expected}])...[${actual}] (success: ${success}).`);

  if (typeof success !== 'boolean' || !success) {
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

    if (!Array.isArray(startSequence) || typeof length !== 'number' ) {
      continue;
    }

    console.log(`  [${startSequence}] and ${length}.`);
  }
}
else {
  console.log("All tests passed!");
}

function areArraysIdentical(a, b) {
  if (!Array.isArray(a) || !Array.isArray(b)) {
    return undefined;
  }

  if (a.length !== b.length) {
    return false;
  }

  for (let i = 0; i < a.length; i++) {
    if (a[i] !== b[i]) {
      return false;
    }
  }

  return true;
}
