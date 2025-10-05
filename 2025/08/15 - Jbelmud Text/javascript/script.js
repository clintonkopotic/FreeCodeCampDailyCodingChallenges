// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-08-15
function jbelmu(text) {
  if (typeof text !== 'string') {
    return undefined;
  }

  const words = text.split(' ');
  let wordCount = 0;
  let result = "";

  for (const word of words) {
    const trimedWord = word.trim();

    if (trimedWord.length === 0) {
      continue;
    }

    if (wordCount > 0) {
      result += ' ';
    }

    if (trimedWord.length <= 3) {
      result += trimedWord;
    }
    else {
      const middle = trimedWord.slice(1, trimedWord.length - 1);
      const jumbledMiddle = middle.split("").sort().join("");
      const jumbledWord = trimedWord[0] + jumbledMiddle + trimedWord[trimedWord.length - 1];
      result += jumbledWord;
    }

    wordCount++;
  }

  return result;
}

const tests = [
  ["hello world", "hello wlord"],
  ["i love jumbled text", "i love jbelmud text"],
  ["freecodecamp is my favorite place to learn to code", "faccdeeemorp is my faiortve pacle to laern to cdoe"],
  ["the quick brown fox jumps over the lazy dog", "the qciuk borwn fox jmpus oevr the lazy dog"],
];
const failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 2) {
    continue;
  }

  const text = test[0];
  const expected = test[1];
  const actual = jbelmu(text);
  const success = expected === actual;

  // eslint-disable-next-line no-useless-escape
  console.log(`Testing \"${text}\" (expecting \"${expected}\")...\"${actual}\" (success: ${success}).`);

  if (!success) {
    failures.push(text);
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
