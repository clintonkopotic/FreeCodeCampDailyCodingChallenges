// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-11-04
function imageSearch(images, term) {
  if (!Array.isArray(images) || typeof term !== 'string') {
    return undefined;
  }

  const lowerCaseTerm = term.toLowerCase();
  let foundImages = [];

  for (const image of images) {
    if (typeof image !== 'string') {
      return undefined;
    }

    if (image.toLowerCase().includes(lowerCaseTerm)) {
      foundImages.push(image);
    }
  }

  return foundImages;
}

const tests = [
  [["dog.png", "cat.jpg", "parrot.jpeg"], "dog", ["dog.png"]],
  [["Sunset.jpg", "Beach.png", "sunflower.jpeg"], "sun", ["Sunset.jpg", "sunflower.jpeg"]],
  [["Moon.png", "sun.jpeg", "stars.png"], "PNG", ["Moon.png", "stars.png"]],
  [["cat.jpg", "dogToy.jpeg", "kitty-cat.png", "catNip.jpeg", "franken_cat.gif"], "Cat", ["cat.jpg", "kitty-cat.png", "catNip.jpeg", "franken_cat.gif"]],
];
const failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 3) {
    continue;
  }

  const images = test[0];
  const term = test[1];
  const expected = test[2];
  const actual = imageSearch(images, term);
  const success = areArraysEqual(expected, actual);

  // eslint-disable-next-line no-useless-escape
  console.log(`Testing [${images}] and \"${term}\" (expecting [${expected}])...[${actual}] (success: ${success}).`);

  if (!success) {
    failures.push([images, term]);
  }
}

if (Array.isArray(failures) && failures.length !== 0) {
  console.log("The following inputs failed:");

  for (const failure of failures) {
    if (!Array.isArray(failure) || failure.length !== 2) {
      continue;
    }

    const images = failure[0];
    const term = failure[1];

    // eslint-disable-next-line no-useless-escape
    console.log(`  [${images}] and \"${term}\".`);
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
