function buildAcronym(str) {
  if (typeof str !== 'string' || str.length === 0) {
    return undefined;
  }

  const wordsToIgnore = ["a", "for", "an", "and", "by", "of"];
  let result = "";
  let words = str.split(" ");

  for (let i = 0; i < words.length; i++) {
    let word = words[i];

    if (typeof word !== 'string') {
      continue;
    }

    word = word.trim();

    if (word.length === 0) {
      continue;
    }

    const firstChar = word.charAt(0);

    if (i > 0 && wordsToIgnore.includes(word.toLowerCase())) {
      continue;
    }

    result = result.concat(firstChar.toUpperCase());
  }

  return result;
}

const tests = [
  ["Search Engine Optimization", "SEO"],
  ["Frequently Asked Questions", "FAQ"],
  ["National Aeronautics and Space Administration", "NASA"],
  ["Federal Bureau of Investigation", "FBI"],
  ["For your information", "FYI"],
  ["By the way", "BTW"],
  ["An unstoppable herd of waddling penguins overtakes the icy mountains and sings happily", "AUHWPOTIMSH"],
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

  const actual = buildAcronym(str);
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
