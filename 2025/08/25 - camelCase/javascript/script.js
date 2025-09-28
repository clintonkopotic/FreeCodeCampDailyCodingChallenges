// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-08-25
function toCamelCase(s) {
  if (typeof s !== 'string') {
    return undefined;
  }
  
  const words = s.trim().split(/[\s\-\_]/);
  
  if (words.length === 0) {
    return "";
  }

  let result = words[0].toLowerCase();

  for (let i = 1; i < words.length; i++) {
    let word = words[i].toLowerCase();
    
    if (word.length === 0) {
      continue;
    }
    
    word = word[0].toUpperCase() + word.slice(1);
    result += word;
  }

  return result;
}

console.log( toCamelCase("hello world"));

/**
 * camelCase
Given a string, return its camel case version using the following rules:

Words in the string argument are separated by one or more characters from the following set: space ( ), dash (-), or underscore (_). Treat any sequence of these as a word break.
The first word should be all lowercase.
Each subsequent word should start with an uppercase letter, with the rest of it lowercase.
All spaces and separators should be removed.

Tests
Waiting:1. toCamelCase("hello world") should return "helloWorld".
Waiting:2. toCamelCase("HELLO WORLD") should return "helloWorld".
Waiting:3. toCamelCase("secret agent-X") should return "secretAgentX".
Waiting:4. toCamelCase("FREE cODE cAMP") should return "freeCodeCamp".
Waiting:5. toCamelCase("ye old-_-sea  faring_buccaneer_-_with a - peg__leg----and a_parrot_ _named- _squawk") should return "yeOldSeaFaringBuccaneerWithAPegLegAndAParrotNamedSquawk".
 */

const tests = [
  ["hello world", "helloWorld"],
  ["HELLO WORLD", "helloWorld"],
  ["secret agent-X", "secretAgentX"],
  ["FREE cODE cAMP", "freeCodeCamp"],
  ["ye old-_-sea  faring_buccaneer_-_with a - peg__leg----and a_parrot_ _named- _squawk", "yeOldSeaFaringBuccaneerWithAPegLegAndAParrotNamedSquawk"],
];
const failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 2) {
    continue;
  }

  const s = test[0];
  const expected = test[1];
  const actual = toCamelCase(s);
  const success = expected === actual;

  console.log(`Testing \"${s}\" (expecting \"${expected}\")...\"${actual}\" (success: ${success}).`);

  if (!success) {
    failures.push(s);
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