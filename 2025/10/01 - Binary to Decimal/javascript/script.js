/* eslint-disable no-useless-escape */
// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-01
function toDecimal(binary) {
  if (typeof binary !== 'string' || binary.length === 0) {
    return undefined;
  }

  let result = 0;

  for (let i = binary.length - 1, j = 0; i >= 0; i--, j++) {
    const digit = binary[i];

    if (digit === "1") {
      result += 2 ** j;
    }
    else if (digit !== "0") {
      return undefined;
    }
  }

  return result;
}

const tests = [
  ["101", 5],
  ["1010", 10],
  ["10010", 18],
  ["1010101", 85],
];
const failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 2) {
    continue;
  }

  const binary = test[0];
  const expected = test[1];
  const actual = toDecimal(binary);
  const success = expected === actual;

  console.log(`Testing \"${binary}\" (expecting ${expected})...${actual} (success: ${success}).`);

  if (!success) {
    failures.push(binary);
  }
}

if (Array.isArray(failures) && failures.length !== 0) {
  console.log("The following inputs failed:");
  
  for (const failure of failures) {
    console.log(`  \"${failure}\".`);
  }
}
else {
  console.log("All tests passed!");
}
