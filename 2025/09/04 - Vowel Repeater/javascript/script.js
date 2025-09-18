// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-09-04
function repeatVowels(str) {
  if (typeof str !== 'string') {
    return undefined;
  }

  let vowelCount = 0;
  let result = "";

  for (const character of str) {
    result += character;

    if (/[aAeEiIoOuU]/.test(character)) {
      vowelCount++;

      if (vowelCount > 1) {
        result += character.toLowerCase().repeat(vowelCount - 1);
      }
    }
    else {
    }
  }

  return result;
}

const tests = [
  ["hello world", "helloo wooorld"],
  ["freeCodeCamp", "freeeCooodeeeeCaaaaamp"],
  ["AEIOU", "AEeIiiOoooUuuuu"],
  ["I like eating ice cream in Iceland", "I liikeee eeeeaaaaatiiiiiing iiiiiiiceeeeeeee creeeeeeeeeaaaaaaaaaam iiiiiiiiiiin Iiiiiiiiiiiiceeeeeeeeeeeeelaaaaaaaaaaaaaand"],
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

  const actual = repeatVowels(str);
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
