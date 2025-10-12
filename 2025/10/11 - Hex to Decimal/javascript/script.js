// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-11
function hexToDecimal(hex) {
  if (typeof hex !== 'string' || hex.length === 0) {
    return undefined;
  }

  const digits = "0123456789ABCDEF";
  let result = 0;

  for (let i = hex.length - 1, exponent = 0; i >= 0; i--, exponent++) {
    const digit = hex[i].toUpperCase();
    const value = digits.indexOf(digit);

    if (value < 0) {
      return undefined;
    }

    const digitValue = value * Math.pow(16, exponent);
    result += digitValue;
  }

  return result;
}

const tests = [
  ["A", 10],
  ["15", 21],
  ["2E", 46],
  ["FF", 255],
  ["A3F", 2623],
];
const failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 2) {
    continue;
  }

  const hex = test[0];
  const expected = test[1];
  const actual = hexToDecimal(hex);
  const success = expected === actual;

  // eslint-disable-next-line no-useless-escape
  console.log(`Testing \"${hex}\" (expecting ${expected})...${actual} (success: ${success}).`);

  if (!success) {
    failures.push(hex);
  }
}

if (Array.isArray(failures) && failures.length !== 0) {
  console.log("The following inputs failed:");

  for (const failure of failures) {
    // eslint-disable-next-line no-useless-escape
    console.log(`  \"${failure}\".`);
  }
}
else {
  console.log("All tests passed!");
}
