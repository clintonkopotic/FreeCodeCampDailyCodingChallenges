// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-09-22
function digitsOrLetters(str) {
  if (typeof str !== 'string') {
    return undefined;
  }

  let numberOfDigits = 0;
  let numberOfLetters = 0;

  for (const character of str) {
    if (/[0-9]/.test(character)) {
      numberOfDigits++;
    }
    else if (/[a-zA-Z]/.test(character)) {
      numberOfLetters++;
    }
  }

  if (numberOfDigits > numberOfLetters) {
    return "digits";
  }
  else if (numberOfLetters > numberOfDigits) {
    return "letters";
  }

  return "tie";
}

const tests = [
  ["abc123", "tie"],
  ["a1b2c3d", "letters"],
  ["1a2b3c4", "digits"],
  ["abc123!@#DEF", "letters"],
  ["H3110 W0R1D", "digits"],
  ["P455W0RD", "tie"],
];

let failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 2) {
    continue;
  }

  const str = test[0];
  const expected = test[1];
  const actual = digitsOrLetters(str);
  const success = expected === actual;

  console.log(`Testing \"${str}\" (expecting: \"${expected}\")...\"${actual}\" (success: ${success}).`);

  if (!success) {
    failures.push(str);
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
