// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-09-17
function generateSlug(str) {
  if (typeof str !== 'string') {
    return undefined;
  }
  
  str = str.trim();
  let lastCharacterWasASpace = false;
  let result = "";

  for (let i = 0; i < str.length; i++) {
    const character = str[i];

    if (typeof character === 'string') {
      if (character === " ") {
        if (!lastCharacterWasASpace) {
          result += "%20";
          lastCharacterWasASpace = true;
        }
      }
      else if (/[0-9]/.test(character)) {
        result += character;
        lastCharacterWasASpace = false;
      }
      else if (/[a-zA-Z]/.test(character)) {
        result += character.toLowerCase();
        lastCharacterWasASpace = false;
      }
    }
  }

  return result;
}

const tests = [
  ["helloWorld", "helloworld"],
  ["hello world!", "hello%20world"],
  [" hello-world ", "helloworld"],
  ["hello  world", "hello%20world"],
  ["  ?H^3-1*1]0! W[0%R#1]D  ", "h3110%20w0r1d"],
];

let failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 2) {
    continue;
  }

  const str = test[0];
  const expected = test[1];

  if (typeof str !== 'string' || str.length === 0
    || typeof expected !== 'string' || expected.length === 0) {
    continue;
  }

  const actual = generateSlug(str);
  const success = expected === actual;
  console.log("Testing \"" + str + "\" (expecting \"" + expected
    + "\")...\"" + actual + "\" (success: " + success + ").");

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
