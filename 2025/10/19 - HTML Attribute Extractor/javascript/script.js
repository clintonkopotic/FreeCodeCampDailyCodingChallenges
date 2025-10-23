// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-19
function extractAttributes(element) {
  if (typeof element !== 'string') {
    return undefined;
  }

  // Adapted from https://stackoverflow.com/a/317081
  const attributeRegex = /([\w|data-]+)=["']?((?:.(?!["']?\s+(?:\S+)=|\s*\/?[>"']))+.)["']?/gm;

  let match;
  const result = [];

  while ((match = attributeRegex.exec(element)) !== null) {
    // This is necessary to avoid infinite loops with zero-width matches
    if (match.index === attributeRegex.lastIndex) {
      attributeRegex.lastIndex++;
    }

    if (match.length === 3) {
      result.push(`${match[1]}, ${match[2]}`);
    }
  }

  return result;
}

const tests = [
  ['<span class="red"></span>', ["class, red"]],
  ['<meta charset="UTF-8" />', ["charset, UTF-8"]],
  ["<p>Lorem ipsum dolor sit amet</p>", []],
  ['<input name="email" type="email" required="true" />', ["name, email", "type, email", "required, true"]],
  ['<button id="submit" class="btn btn-primary">Submit</button>', ["id, submit", "class, btn btn-primary"]],
];
const failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 2) {
    continue;
  }

  const element = test[0];
  const expected = test[1];
  const actual = extractAttributes(element);
  const success = areArraysEqual(expected, actual);

  // eslint-disable-next-line no-useless-escape
  console.log(`Testing \"${element}\" (expecting [${expected}])...[${actual}] (success: ${success}).`);

  if (!success) {
    failures.push(element);
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

function areArraysEqual(arr1, arr2) {
  if (!Array.isArray(arr1) || !Array.isArray(arr2)) {
    return undefined;
  }

  // First, check if the arrays are strictly the same reference.
  if (arr1 === arr2) {
    return true;
  }

  // If either array is null or undefined, they are not equal (unless both are null/undefined, caught by the first check).
  if (arr1 == null || arr2 == null) {
    return false;
  }

  // If the lengths are different, the arrays cannot be equal.
  if (arr1.length !== arr2.length) {
    return false;
  }

  // Iterate through the arrays and compare elements at corresponding indices.
  for (let i = 0; i < arr1.length; i++) {
    if (arr1[i] !== arr2[i]) {
      return false; // Found a differing element, so arrays are not equal.
    }
  }

  // If all checks pass, the arrays are equal.
  return true;
}
