// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-11-01
function verify(message, key, signature) {
  if (typeof message !== 'string' || typeof key !== 'string' || !Number.isInteger(signature) || signature < 0) {
    return undefined;
  }

  const calculatedSignature = calculateSignature(message) + calculateSignature(key);

  return signature === calculatedSignature;

  function calculateSignature(string) {
    if (typeof string !== 'string') {
      return undefined;
    }

    const letters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
    let signature = 0;

    for (const letter of string) {
      if (typeof letter !== 'string') {
        return undefined;
      }

      const index = letters.indexOf(letter);

      if (index >= 0) {
        signature += index + 1;
      }
    }

    return signature;
  }
}

const tests = [
  ["foo", "bar", 57, true],
  ["foo", "bar", 54, false],
  ["freeCodeCamp", "Rocks", 238, true],
  ["Is this valid?", "No", 210, false],
  ["Is this valid?", "Yes", 233, true],
  ["Check out the freeCodeCamp podcast,", "in the mobile app", 514, true],
];
const failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 4) {
    continue;
  }

  const message = test[0];
  const key = test[1];
  const signature = test[2];
  const expected = test[3];
  const actual = verify(message, key, signature);
  const success = expected === actual;

  // eslint-disable-next-line no-useless-escape
  console.log(`Testing \"${message}\", \"${key}\" and ${signature} (expecting ${expected})...${actual} (success: ${success}).`);

  if (!success) {
    failures.push([message, key, signature]);
  }
}

if (Array.isArray(failures) && failures.length !== 0) {
  console.log("The following inputs failed:");

  for (const failure of failures) {
    if (!Array.isArray(failure) || failure.length !== 3) {
      continue;
    }

    const message = failure[0];
    const key = failure[1];
    const signature = failure[2];

    // eslint-disable-next-line no-useless-escape
    console.log(`  \"${message}\", \"${key}\" and ${signature}.`);
  }
}
else {
  console.log("All tests passed!");
}
