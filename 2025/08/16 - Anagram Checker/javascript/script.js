// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-08-16
function areAnagrams(str1, str2) {
  if (typeof str1 !== 'string' || typeof str2 !== 'string') {
    return undefined;
  }

  const str1characters = [];

  for (const str1character of str1) {
    if (str1character !== " ") {
      str1characters.push(str1character.toLowerCase());
    }
  }

  for (const str2character of str2) {
    if (str2character === " ") {
      continue;
    }

    var indexInStr1Characters = str1characters.indexOf(str2character.toLowerCase());

    if (indexInStr1Characters < 0) {
      return false;
    }

    str1characters.splice(indexInStr1Characters, 1);
  }

  return str1characters.length === 0;
}

const tests = [
  ["listen", "silent", true],
  ["School master", "The classroom", true],
  ["A gentleman", "Elegant man", true],
  ["Hello", "World", false],
  ["apple", "banana", false],
  ["cat", "dog", false],
];
const failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 3) {
    continue;
  }

  const str1 = test[0];
  const str2 = test[1];
  const expected = test[2];
  const actual = areAnagrams(str1, str2);
  const success = expected === actual;

  // eslint-disable-next-line no-useless-escape
  console.log(`Testing \"${str1}\" and \"${str2}\" (expecting ${expected})...${actual} (success: ${success}).`);

  if (!success) {
    failures.push([str1, str2]);
  }
}

if (Array.isArray(failures) && failures.length !== 0) {
  console.log("The following inputs failed:");

  for (const failure of failures) {
    if (!Array.isArray(failure) || failure.length !== 2) {
      continue;
    }

    const str1 = failure[0];
    const str2 = failure[1];

    // eslint-disable-next-line no-useless-escape
    console.log(`  \"${str1}\" and \"${str2}\".`);
  }
}
else {
  console.log("All tests passed!");
}
