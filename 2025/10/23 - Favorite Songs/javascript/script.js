// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-23
function favoriteSongs(playlist) {
  if (!Array.isArray(playlist)) {
    return undefined;
  }

  const sortedPlaylist = [...playlist].sort((a, b) => b.plays - a.plays);

  const result = [];

  for (let i in sortedPlaylist) {
    const song = sortedPlaylist[i];

    if (typeof song !== 'object') {
      return undefined;
    }

    result.push(song.title);

    if (result.length >= 2) {
      break;
    }
  }

  return result;
}

const tests = [
  [[{ "title": "Sync or Swim", "plays": 3 }, { "title": "Byte Me", "plays": 1 }, { "title": "Earbud Blues", "plays": 2 }], ["Sync or Swim", "Earbud Blues"]],
  [[{ "title": "Skip Track", "plays": 98 }, { "title": "99 Downloads", "plays": 99 }, { "title": "Clickwheel Love", "plays": 100 }], ["Clickwheel Love", "99 Downloads"]],
  [[{ "title": "Song A", "plays": 42 }, { "title": "Song B", "plays": 99 }, { "title": "Song C", "plays": 75 }], ["Song B", "Song C"]],
];
const failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 2) {
    continue;
  }

  const playlist = test[0];
  const expected = test[1];
  const actual = favoriteSongs(playlist);
  const success = areArraysEqual(expected, actual);

  console.log(`Testing ${JSON.stringify(playlist)} (expecting ${JSON.stringify(expected)})...${JSON.stringify(actual)} (success: ${success}).`);

  if (!success) {
    failures.push(playlist);
  }
}

if (Array.isArray(failures) && failures.length !== 0) {
  console.log("The following inputs failed:");

  for (const failure of failures) {
    console.log(`  ${JSON.stringify(failure)}.`);
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
