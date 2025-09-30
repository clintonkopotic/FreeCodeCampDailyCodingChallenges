// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-08-20
function squaresWithThree(n) {
  if (!Number.isInteger(n) || n < 1 || n > 10_000) {
    return undefined;
  }

  let result = 0;

  for (let i = 1; i < n; i++) {
    const squareOfI = i ** 2;

    if (squareOfI.toString().includes("3")) {
      result++;
    }
  }

  return result;
}

const tests = [
  [1, 0],
  [10, 1],
  [100, 19],
  [1_000, 326],
  [10_000, 4531],
];
const failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 2) {
    continue;
  }

  const n = test[0];
  const expected = test[1];
  const actual = squaresWithThree(n);
  const success = expected === actual;

  console.log(`Testing ${n} (expecting ${expected})...${actual} (success: ${success}).`);

  if (!success) {
    failures.push(n);
  }
}

if (Array.isArray(failures) && failures.length !== 0) {
  console.log("The following inputs failed:");

  for (const failure of failures) {
    console.log(`  ${failure}.`);
  }
}
