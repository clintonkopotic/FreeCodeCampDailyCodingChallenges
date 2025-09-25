// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-09-25
function secondLargest(arr) {
  if (!Array.isArray(arr) || arr.length < 2) {
    return undefined;
  }

  // Create new array to remove duplicates, sort in descending order and return the 2nd largest.
  const uniqueArraySorted = [...new Set(arr)].sort((a, b) => b - a);

  if (uniqueArraySorted.length < 2) {
    return undefined;
  }

  return uniqueArraySorted[1];
}

const tests = [
  [[1, 2, 3, 4], 3],
  [[20, 139, 94, 67, 31], 94],
  [[2, 3, 4, 6, 6], 4],
  [[10, -17, 55.5, 44, 91, 0], 55.5],
  [[1, 0, -1, 0, 1, 0, -1, 1, 0], 0],
]

let failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length != 2) {
    continue;
  }

  const arr = test[0];
  const expected = test[1];
  const actual = secondLargest(arr);
  const success = expected === actual;

  console.log(`Testing [${arr}] (expecting ${expected})...${actual} (success: ${success}).`);

  if (!success) {
    failures.push(arr);
  }
}

if (Array.isArray(failures) && failures.length !== 0) {
  console.log("The following inputs failed:");

  for (const failure of failures) {
    if (!Array.isArray(failure)) {
      continue;
    }

    console.log(`  [${failure}].`);
  }
}
else {
  console.log("All tests passed!");
}
