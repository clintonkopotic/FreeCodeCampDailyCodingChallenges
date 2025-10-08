// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-08-12
function isValidNumber(n, base) {
  if (typeof n !== 'string' || n.length === 0 || !Number.isFinite(base) || base < 2 || base > 36) {
    return undefined;
  }

  const baseCharcters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".slice(0, base);

  for (const character of n) {
    if (!baseCharcters.includes(character.toUpperCase())) {
      return false;
    }
  }

  return true;
}

const tests = [
  ["10101", 2, true],
  ["10201", 2, false],
  ["76543210", 8, true],
  ["9876543210", 8, false],
  ["9876543210", 10, true],
  ["ABC", 10, false],
  ["ABC", 16, true],
  ["Z", 36, true],
  ["ABC", 20, true],
  ["4B4BA9", 16, true],
  ["5G3F8F", 16, false],
  ["5G3F8F", 17, true],
  ["abc", 10, false],
  ["abc", 16, true],
  ["AbC", 16, true],
  ["z", 36, true],
];
const failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 3) {
    continue;
  }

  const n = test[0];
  const base = test[1];
  const expected = test[2];
  const actual = isValidNumber(n, base);
  const success = expected === actual;

  // eslint-disable-next-line no-useless-escape
  console.log(`Testing \"${n}\" and ${base} (expecting ${expected})...${actual} (success: ${success}).`);

  if (!success) {
    failures.push([n, base]);
  }
}

if (Array.isArray(failures) && failures.length !== 0) {
  console.log("The following inputs failed:");

  for (const failure of failures) {
    if (!Array.isArray(failure) || failure.length !== 2) {
      continue;
    }

    const n = failure[0];
    const base = failure[1];

    // eslint-disable-next-line no-useless-escape
    console.log(`  \"${n}\" and ${base}.`);
  }
}
else {
  console.log("All tests passed!");
}
