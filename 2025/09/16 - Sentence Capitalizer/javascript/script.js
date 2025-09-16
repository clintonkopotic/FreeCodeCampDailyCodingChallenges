// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-09-16
function capitalize(paragraph) {
  if (typeof paragraph !== 'string') {
    return undefined;
  }

  let paragraphCapitalized = "";
  let awaitingStartOfSentence = true;

  for (let i = 0; i < paragraph.length; i++) {
    const character = paragraph[i];

    if (typeof character !== 'string') {
      continue;
    }
    else if (character === "." || character === "?" || character === "!") {
      paragraphCapitalized += character;
      awaitingStartOfSentence = true;
    }
    else if (character === " ") {
      paragraphCapitalized += character;
    }
    else if (awaitingStartOfSentence) {
      paragraphCapitalized += character.toUpperCase();
      awaitingStartOfSentence = false;
    }
    else {
      paragraphCapitalized += character;
    }
  }

  return paragraphCapitalized;
}

const tests = [
  ["this is a simple sentence.", "This is a simple sentence."],
  ["hello world. how are you?", "Hello world. How are you?"],
  ["i did today's coding challenge... it was fun!!", "I did today's coding challenge... It was fun!!"],
  ["crazy!!!strange???unconventional...sentences.", "Crazy!!!Strange???Unconventional...Sentences."],
  ["there's a space before this period . why is there a space before that period ?", "There's a space before this period . Why is there a space before that period ?"],
];

let failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 2) {
    continue;
  }

  const paragraph = test[0];
  const expected = test[1];

  if (typeof paragraph !== 'string' || paragraph.length === 0
    || typeof expected !== 'string' || expected.length === 0) {
    continue;
  }

  const actual = capitalize(paragraph);
  const success = expected === actual;
  console.log("Testing \"" + paragraph + "\" (expecting \"" + expected
    + "\")...\"" + actual + "\" (success: " + success + ").");

  if (success !== true) {
    failures.push(paragraph);
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
