// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-08-18
function factorial(n) {
  if (!Number.isInteger(n) || n < 0 || n > 20) {
    return undefined;
  }
  
  if (n === 0) {
    return 1;
  }

  return n * factorial(n -1);
}

const tests = [
  [0, 1],
  [5, 120],
  [20, 2432902008176640000],
]
const failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 2) {
    continue;
  }

  const n = test[0];
  const expected = test[1];
  const actual = factorial(n);
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
