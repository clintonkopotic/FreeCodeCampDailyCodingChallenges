// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-09-29
function getLongestWord(sentence) {
  if (typeof sentence !== 'string') {
    return undefined;
  }

  const words = sentence.split(' ');
  let result = "";

  for (let word of words) {
    word = word.trim();

    if (word.endsWith('.')) {
      word = word.slice(0, word.length - 1);
    }

    if (word.length > result.length) {
      result = word;
    }
  }

  return result;
}

const tests = [
  ["coding is fun", "coding"],
  ["Coding challenges are fun and educational.", "educational"],
  ["This sentence has multiple long words.", "sentence"],
];
const failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 2) {
    continue;
  }

  const sentence = test[0];
  const expected = test[1];
  const actual = getLongestWord(sentence);
  const success = expected === actual;

  console.log(`Testing \"${sentence}\" (expecting \"${expected}\")...\"${actual}\" (success: ${success}).`);

  if (!success) {
    failures.push(sentence);
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
