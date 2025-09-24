// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-09-24
function isPerfectSquare(n) {
  if (!Number.isInteger(n)) {
    return undefined;
  }

  return Number.isInteger(Math.sqrt(n));
}

const tests = [
  [9, true],
  [49, true],
  [1, true],
  [2, false],
  [99, false],
  [-9, false],
  [0, true],
  [25281, true],
];

let failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 2) {
    continue;
  }

  const n = test[0];
  const expected = test[1];
  const actual = isPerfectSquare(n);
  const success = expected === actual;

  console.log(`Testing ${n} (expecting ${expected})...${actual} (success: ${success}).`);

  if (!success) {
    failures.push(n);
  }
}

if (Array.isArray(failures) && failures.length !== 0) {
  console.log("The following inputs failed:");

  for (const failure of failures) {
    console.log(`  ${n}.`);
  }
}
else {
  console.log("All tests passed!");
}
