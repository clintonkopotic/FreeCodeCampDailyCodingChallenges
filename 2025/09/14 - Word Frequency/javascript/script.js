// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-09-14
function getWords(paragraph) {
  if (typeof paragraph !== 'string' || paragraph.length === 0) {
    return undefined;
  }

  const words = paragraph.split(" ");
  const wordFrequency = new Map();

  for (let i = 0; i < words.length; i++) {
    // Look at each word and ensure it is a string of length more than zero.
    let word = words[i];

    if (typeof word !== 'string' || word.length === 0) {
      continue;
    }

    // Change the case of the word to lowercase for easier comparison.
    word = word.toLowerCase();
    let lastIndex = word.length - 1;

    // Remove any punctuation.
    if (word[lastIndex] === "," || word[lastIndex] === "." || word[lastIndex] === "!") {
      word = word.substring(0, lastIndex);
    }

    // Keep track of the number of times this word has occured so far in the paragraph.
    let wordCount = 0;

    if (wordFrequency.has(word)) {
      wordCount = wordFrequency.get(word);
    }

    wordFrequency.set(word, wordCount + 1);
  }

  // Convert Map to an array of [key, value] pairs
  const entriesArray = [...wordFrequency.entries()];

  // Sort the array by value in decending order
  entriesArray.sort((a, b) => b[1] - a[1]);

  // Create a new Map from the sorted array
  const sortedMap = new Map(entriesArray);
  const mostWordsSize = sortedMap.size >= 3 ? 3 : sortedMap.size;
  let mostWords = [];

  for (const word of sortedMap.keys()) {
    mostWords.push(word);

    if (mostWords.length >= mostWordsSize) {
      return mostWords;
    }
  }

  return [];
}

const tests = [
  ["Coding in Python is fun because coding Python allows for coding in Python easily while coding", ["coding", "python", "in"]],
  ["I like coding. I like testing. I love debugging!", ["i", "like", "coding"]],
  ["Debug, test, deploy. Debug, debug, test, deploy. Debug, test, test, deploy!", ["debug", "test", "deploy"]],
];

let failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 2) {
    continue;
  }

  const paragraph = test[0];
  const expected = test[1];

  if (typeof paragraph !== 'string' || paragraph.length === 0 || !Array.isArray(expected)) {
    continue;
  }

  const actual = getWords(paragraph);
  const success = areArraysIdentical(expected, actual);
  console.log("Testing \"" + paragraph + "\" (expecting [" + expected + "])...[" + actual + "] (success: " + success + ").");

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

function areArraysIdentical(arr1, arr2) {
  // First, check if the lengths of the arrays are different.
  // If they are, the arrays cannot be identical.
  if (!Array.isArray(arr1) || !Array.isArray(arr2) || arr1.length !== arr2.length) {
    return false;
  }

  // Iterate through the elements of the arrays.
  // Compare each element at the same index in both arrays.
  for (let i = 0; i < arr1.length; i++) {
    // If any element at a given index does not match,
    // the arrays are not identical.
    if (arr1[i] !== arr2[i]) {
      return false;
    }
  }

  // If the loop completes without finding any mismatches,
  // the arrays are identical in content and order.
  return true;
}
