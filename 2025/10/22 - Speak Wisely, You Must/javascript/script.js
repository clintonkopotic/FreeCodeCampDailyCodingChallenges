// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-22
function wiseSpeak(sentence) {
  if (typeof sentence !== 'string') {
    return undefined;
  }

  const words = sentence.split(' ');

  if (!Array.isArray(words) || words.length === 0) {
    return "";
  }

  // All given sentences will end with a single punctuation mark. Keep the original punctuation of the sentence and move it to the end of the new sentence.
  const lastWord = words[words.length - 1];
  const sentencePunctuationMark = lastWord[lastWord.length - 1];

  // Find the first occurrence of one of the following words in the sentence: "have", "must", "are", "will", "can".
  const newEndingWords = ["have", "must", "are", "will", "can"];
  const newEndingWordIndex = words.findIndex(word => newEndingWords.includes(word));

  if (newEndingWordIndex < 0 || newEndingWordIndex >= words.length) {
    return undefined;
  }

  const newWordsOrder = [];
  const newFirstWord = words[newEndingWordIndex + 1];
  newWordsOrder.push(`${newFirstWord[0].toUpperCase()}${newFirstWord.slice(1)}`);

  if (newEndingWordIndex + 2 < words.length - 1) {
    words.slice(newEndingWordIndex + 2, words.length - 1).forEach(
      word => newWordsOrder.push(word));
  }

  newWordsOrder.push(`${lastWord.slice(0, lastWord.length - 1)},`);
  newWordsOrder.push(words[0].toLowerCase());

  if (newEndingWordIndex > 1) {
    words.slice(1, newEndingWordIndex).forEach(
      word => newWordsOrder.push(word));
  }

  newWordsOrder.push(`${words[newEndingWordIndex]}${sentencePunctuationMark}`);

  return newWordsOrder.join(' ');
}

const tests = [
  ["You must speak wisely.", "Speak wisely, you must."],
  ["You can do it!", "Do it, you can!"],
  ["Do you think you will complete this?", "Complete this, do you think you will?"],
  ["All your base are belong to us.", "Belong to us, all your base are."],
  ["You have much to learn.", "Much to learn, you have."],
];
const failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 2) {
    continue;
  }

  const sentence = test[0];
  const expected = test[1];
  const actual = wiseSpeak(sentence);
  const success = expected === actual;

  // eslint-disable-next-line no-useless-escape
  console.log(`Testing \"${sentence}\" (expecting \"${expected}\")...\"${actual}\" (success: ${success}).`);

  if (!success) {
    failures.push(sentence);
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
