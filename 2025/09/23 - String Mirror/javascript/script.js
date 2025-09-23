// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-09-23
function isMirror(str1, str2) {
  if (typeof str1 !== 'string' || typeof str2 !== 'string') {
    return undefined;
  }

  const alphabeticalCharacters = /[a-zA-Z]/;

  for (let i = 0, j = (str2.length - 1); (i < str1.length) && (j >= 0); i++, j--) {

    let endOf1 = false;

    while (!alphabeticalCharacters.test(str1[i])) {
      i++;

      if (i >= str1.length) {
        endOf1 = true;

        break;
      }
    }

    let endOf2 = false;

    while (!alphabeticalCharacters.test(str2[j])) {
      j--;

      if (j < 0) {
        endOf2 = true;

        break;
      }
    }

    if (endOf1 === true && endOf2 === true) {
      break;
    }
    else if ((endOf1 === true) || (endOf2 === true) || (str1[i] !== str2[j])) {
      return false;
    }
  }

  return true;
}

const tests =
[
  ["helloworld", "helloworld", false],
  ["Hello World", "dlroW olleH", true],
  ["RaceCar", "raCecaR", true],
  ["RaceCar", "RaceCar", false],
  ["Mirror", "rorrim", false],
  ["Hello World", "dlroW-olleH", true],
  ["Hello World", "!dlroW !olleH", true],
];

let failures = [];

for (const test of tests) {
    if (!Array.isArray(test) && test.length !== 3) {
      continue;
    }

    const str1 = test[0];
    const str2 = test[1];
    const expected = test[2];
    const actual = isMirror(str1, str2);
    const success = expected === actual;

    console.log(`Testing \"${str1}\" and \"${str2}\" (expecting ${expected})....${actual} (success: ${success}).`);

    if (!success) {
      failures.push([str1, str2]);
    }
}

if (Array.isArray(failures) && failures.length !== 0) {
  console.log("The following inputs failed:");

  for (const failure of failures) {
    if (!Array.isArray(failure) && failure.length !== 2) {
      continue;
    }

    const str1 = failure[0];
    const str2 = failure[1];

    console.log(`  \"${str1}\" and \"${str2}\".`);
  }
}
else {
  console.log("All tests passed!");
}
