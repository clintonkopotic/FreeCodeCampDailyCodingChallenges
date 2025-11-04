// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-11-03
function countWords(sentence) {
  if (typeof sentence !== 'string') {
    return undefined;
  }

  return sentence.split(' ').length;
}

const tests = [
  ["Hello world", 2],
  ["The quick brown fox jumps over the lazy dog.", 9],
  ["I like coding challenges!", 4],
  ["Complete the challenge in JavaScript and Python.", 7],
  ["The missing semi-colon crashed the entire internet.", 7],
];
const failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 2) {
    continue;
  }

  const sentence = test[0];
  const expected = test[1];
  const actual = countWords(sentence);
  const success = expected === actual;

  // eslint-disable-next-line no-useless-escape
  console.log(`Testing \"${sentence}\" (expecting ${expected})...${actual} (success: ${success}).`);

  if (!success) {
    failures.push(sentence);
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
