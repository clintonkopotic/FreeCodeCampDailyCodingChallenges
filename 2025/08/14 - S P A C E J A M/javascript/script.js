// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-08-14
function spaceJam(s) {
  if (typeof s !== 'string') {
    return undefined;
  }

  let insertSpaces = false;
  let result = "";

  for (const character of s) {
    if (character === " ") {
      continue;
    }
    else if (insertSpaces) {
      result += "  ";
    }

    result += character.toUpperCase();
    insertSpaces = true;
  }

  return result;
}

const tests = [
  ["freeCodeCamp", "F  R  E  E  C  O  D  E  C  A  M  P"],
  ["   free   Code   Camp   ", "F  R  E  E  C  O  D  E  C  A  M  P"],
  ["Hello World?!", "H  E  L  L  O  W  O  R  L  D  ?  !"],
  ["C@t$ & D0g$", "C  @  T  $  &  D  0  G  $"],
  ["allyourbase", "A  L  L  Y  O  U  R  B  A  S  E"],
];
const failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 2) {
    continue;
  }

  const s = test[0];
  const expected = test[1];
  const actual = spaceJam(s);
  const success = expected === actual;

  // eslint-disable-next-line no-useless-escape
  console.log(`Testing \"${s}\" (expecting \"${expected}\")...\"${actual}\" (success: ${success}).`);

  if (!success) {
    failures.push(s);
  }
}

if (Array.isArray(failures) && failures.length !== 0) {
  console.log("The following inputs failed:");

  for (const failure of failures) {
    // eslint-disable-next-line no-useless-escape
    console.log(`  \"${failure}\".`)
  }
}
else {
  console.log("All tests passed!");
}
