// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-09-09
function allUnique(str) {
  if (typeof str !== 'string' || str.length === 0) {
    return undefined;
  }

  let uniqueChars = [];

  for (let i = 0; i < str.length; i++) {
    const character = str[i];

    if (uniqueChars.includes(character)) {
      return false;
    }

    uniqueChars.push(character);
  }

  return true;
}

const tests = [
  ["abc", true],
  ["aA", true],
  ["QwErTy123!@", true],
  ["~!@#$%^&*()_+", true],
  ["hello", false],
  ["freeCodeCamp", false],
  ["!@#*$%^&*()aA", false],
];

let failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 2) {
    continue;
  }

  const str = test[0];
  const expected = test[1];

  if (typeof str !== 'string' || str.length === 0 || typeof expected !== 'boolean') {
    continue;
  }

  const actual = allUnique(str);
  const success = expected === actual;
  console.log("Testing \"" + str + "\" (expecting " + expected + ")..." + actual + " (success: " + success + ").");

  if (success !== true) {
    failures.push(str);
  }
}

if (Array.isArray(failures) && failures.length > 0) {
  console.log("The following inputs failed:");

  for (const failure of failures) {
    console.log("  \"" + failure + "\".");
  }
}
else {
  console.log("All tests passed!");
}
