// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-09-11
function reverseSentence(sentence) {
  if (typeof sentence !== 'string' || sentence.length === 0) {
    return undefined;
  }

  const words = sentence.split(" ");
  let result = "";

  for (let i = (words.length - 1); i >= 0; i--) {
    let word = words[i];

    if (typeof word === 'string') {
      word = word.trim();

      if (word.length > 0) {
        if (i === words.length - 1) {
          result = word;
        }
        else {
          result = result.concat(" ", word);
        }
      }
    }
  }

  return result;
}

const tests = [
  ["world hello", "hello world"],
  ["push commit git", "git commit push"],
  ["npm  install  sudo", "sudo install npm"],
  ["import    default   function  export", "export function default import"],
];

let failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 2) {
    continue;
  }

  const sentence = test[0];
  const expected = test[1];

  if (typeof sentence !== 'string' || sentence.length === 0
    || typeof expected !== 'string' || expected.length === 0) {
    continue;
  }

  const actual = reverseSentence(sentence);
  const success = expected === actual;
  console.log("Testing \"" + sentence + "\" (expecting \"" + expected
    + "\")...\"" + actual + "\" (success: " + success + ").");

  if (success !== true) {
    failures.push(sentence);
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
