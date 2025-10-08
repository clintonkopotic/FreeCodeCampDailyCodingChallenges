// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-08-11
function isBalanced(s) {
  if (typeof s !== 'string') {
    return undefined;
  }

  const totalCharacters = s.length;
  const numberOfCharactersPerHalf = Math.floor(totalCharacters / 2);
  const lastIndex1stHalf = numberOfCharactersPerHalf - 1;
  const startIndex2ndHalf = numberOfCharactersPerHalf + (totalCharacters % 2);

  let numberOfVowels = 0;

  for (let i = 0; i < totalCharacters; i++) {
    const isVowel = /[aeiou]/.test(s[i].toLowerCase());

    if (!isVowel) {
      continue;
    }
    
    if (i <= lastIndex1stHalf) {
      numberOfVowels++;
    }
    else if (i >= startIndex2ndHalf) {
      numberOfVowels--;
    }
  }

  return numberOfVowels === 0;
}

const tests = [
  ["racecar", true],
  ["Lorem Ipsum", true],
  ["Kitty Ipsum", false],
  ["string", false],
  [" ", true],
  ["abcdefghijklmnopqrstuvwxyz", false],
  ["123A#b!E&*456-o.U", true],
];
const failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 2) {
    continue;
  }

  const s = test[0];
  const expected = test[1];
  const actual = isBalanced(s);
  const success = expected === actual;

  // eslint-disable-next-line no-useless-escape
  console.log(`Testing \"${s}\" (expecting ${expected})...${actual} (success: ${success}).`);

  if (!success) {
    failures.push(s);
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
