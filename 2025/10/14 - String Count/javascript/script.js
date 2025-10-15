// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-14
function count(text, pattern) {
  if (typeof text !== 'string' || typeof pattern !== 'string') {
    return undefined;
  }

  let result = 0;

  for (let i = 0; i < text.length; i++) {
    i = text.indexOf(pattern, i);

    if (!Number.isInteger(i) || i < 0) {
      break;
    }

    result++;
  }

  return result;
}

const tests = [
  ['abcdefg', 'def', 1],
  ['hello', 'world', 0],
  ['mississippi', 'iss', 2],
  ['she sells seashells by the seashore', 'sh', 3],
  ['101010101010101010101', '101', 10],
];
const failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 3) {
    continue;
  }

  const text = test[0];
  const pattern = test[1];
  const expected = test[2];
  const actual = count(text, pattern);
  const success = expected === actual;

  // eslint-disable-next-line no-useless-escape
  console.log(`Testing \"${text}\" and \"${pattern}\" (expecting ${expected})...${actual} (success: ${success}).`);

  if (!success) {
    failures.push([text, pattern]);
  }
}

if (Array.isArray(failures) && failures.length !== 0) {
  console.log("The following inputs failed:");

  for (const failure of failures) {
    if (!Array.isArray(failure) || failure.length !== 2) {
      continue;
    }

    const text = failure[0];
    const pattern = failure[1];

    // eslint-disable-next-line no-useless-escape
    console.log(`  \"${text}\" and \"${pattern}\".`);
  }
}
else {
  console.log("All tests passed!");
}
