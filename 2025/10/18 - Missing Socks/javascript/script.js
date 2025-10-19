// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-18
function sockPairs(pairs, cycles) {
  if (!Number.isInteger(pairs) || pairs <= 0 || !Number.isInteger(cycles) || cycles <= 0) {
    return undefined;
  }

  let numberOfSocks = pairs * 2;

  for (let cycle = 1; cycle <= cycles; cycle++) {
    // Every 2 wash cycles, you lose a single sock.
    if (cycle % 2 == 0) {
      numberOfSocks--;
    }

    // Every 3 wash cycles, you find a single missing sock.
    if (cycle % 3 == 0) {
      numberOfSocks++;
    }

    // Every 5 wash cycles, a single sock is worn out and must be thrown away.
    if (cycle % 5 == 0) {
      numberOfSocks--;
    }

    // Every 10 wash cycles, you buy a pair of socks.
    if (cycle % 10 == 0) {
      numberOfSocks += 2;
    }

    // You can never have less than zero total socks.
    if (numberOfSocks < 0) {
      numberOfSocks = 0;
    }
  }

  // Return the number of complete pairs of socks.
  return Math.floor(numberOfSocks / 2);
}

const tests = [
  [2, 5, 1],
  [1, 2, 0],
  [5, 11, 4],
  [6, 25, 3],
  [1, 8, 0],
];
const failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 3) {
    continue;
  }

  const pairs = test[0];
  const cycles = test[1];
  const expected = test[2];
  const actual = sockPairs(pairs, cycles);
  const success = expected === actual;

  console.log(`Testing ${pairs} and ${cycles} (expecting ${expected})...${actual} (success: ${success}).`);

  if (!success) {
    failures.push([pairs, cycles]);
  }
}

if (Array.isArray(failures) && failures.length !== 0) {
  console.log("The following inputs failed:");

  for (const failure of failures) {
    if (!Array.isArray(failure) || failure.length !== 2) {
      continue;
    }

    const pairs = failure[0];
    const cycles = failure[1];

    console.log(`  ${pairs} and ${cycles}.`);
  }
}
else {
  console.log("All tests passed!");
}
