// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-02
function toBinary(decimal) {
  if (!Number.isInteger(decimal) || decimal < 0) {
    return undefined;
  }

  if (decimal === 0) {
    return "0";
  }

  let value = decimal;
  let remainders = [];

  while (value > 0) {
    remainders.push(value % 2);
    value = Math.floor(value / 2);
  }

  return remainders.reverse().join('');
}

const tests = [
  [5, "101"],
  [12, "1100"],
  [50, "110010"],
  [99, "1100011"],
];
const failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 2) {
    continue;
  }

  const decimal = test[0];
  const expected = test[1];
  const actual = toBinary(decimal);
  const success = expected === actual;

  // eslint-disable-next-line no-useless-escape
  console.log(`Testing ${decimal} (expecting \"${expected}\")...\"${actual}\" (success: ${success}).`);

  if (!success) {
    failures.push(decimal);
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
