// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-27
function sequence(n) {
  if (!Number.isInteger(n) || n <= 0) {
    return undefined;
  }

  let result = "";
  
  for (let i = 1; i <= n; i++) {
    result += i;
  }

  return result;
}

const tests = [
  [5, "12345"],
  [10, "12345678910"],
  [1, "1"],
  [27, "123456789101112131415161718192021222324252627"],
];
const failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 2) {
    continue;
  }

  const n = test[0];
  const expected = test[1];
  const actual = sequence(n);
  const success = expected === actual;

  // eslint-disable-next-line no-useless-escape
  console.log(`Testing ${n} (expecting \"${expected}\")...\"${actual}\" (success: ${success}).`);

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
