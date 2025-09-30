// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-08-19
function sumOfSquares(n) {
  if (!Number.isInteger(n) || n < 1 || n > 1_000) {
    return undefined;
  }
  
  let result = 0;

  for (let i = 1; i <= n; i++) {
    result += i ** 2;
  }

  return result;
}

const tests = [
  [5, 55],
  [10, 385],
  [25, 5_525],
  [500, 41_791_750],
  [1_000, 333_833_500],
];
const failures =[];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 2) {
    continue;
  }
  
  const n = test[0];
  const expected = test[1];
  const actual = sumOfSquares(n);
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
else {
  console.log("All tests passed!");
}
