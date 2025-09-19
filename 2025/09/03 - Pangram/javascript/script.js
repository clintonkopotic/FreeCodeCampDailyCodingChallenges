// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-09-03
function isPangram(sentence, letters) {
  if (typeof sentence !== 'string' || typeof letters !== 'string') {
    return undefined;
  }

  sentence = sentence.toLowerCase();
  letters = letters.toLowerCase();

  for (const character of sentence) {
    if (/[a-zA-Z]/.test(character) && !letters.includes(character.toLowerCase())) {
      return false;
    }
  }

  for (const character of letters) {
    if (/[a-zA-Z]/.test(character) && !sentence.includes(character.toLowerCase())) {
      return false;
    }
  }

  return true;
}

const tests = [
  ["hello", "helo", true],
  ["hello", "hel", false],
  ["hello", "helow", false],
  ["hello world", "helowrd", true],
  ["Hello World!", "helowrd", true],
  ["Hello World!", "heliowrd", false],
  ["freeCodeCamp", "frcdmp", false],
  ["The quick brown fox jumps over the lazy dog.", "abcdefghijklmnopqrstuvwxyz", true],
];

let failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 3) {
    continue;
  }

  const sentence = test[0];
  const letters = test[1];
  const expected = test[2];

  if (typeof sentence !== 'string' || sentence.length === 0 || typeof letters !== 'string' || sentence.length === 0
    || typeof expected !== 'boolean') {
    continue;
  }

  const actual = isPangram(sentence, letters);
  const success = expected === actual;
  console.log(`Testing \"${sentence}\" and \"${letters}\" (expecting ${expected})...${actual} (success: ${success}).`);

  if (success !== true) {
    failures.push([sentence, letters]);
  }
}

if (Array.isArray(failures) && failures.length > 0) {
  console.log("The following inputs failed:");

  for (const failure of failures) {
    if (!Array.isArray(failure) || failure.length !== 2) {
      continue;
    }

    console.log(`  \"${failure[0]}\" and \"${failure[1]}\".`);
  }
}
else {
  console.log("All tests passed!");
}
