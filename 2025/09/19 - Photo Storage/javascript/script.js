// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-09-19
function numberOfPhotos(photoSizeMb, hardDriveSizeGb) {
  if (typeof photoSizeMb !== 'number' || typeof hardDriveSizeGb !== 'number') {
    return undefined;
  }

  const hardDriveSizeMb = 1_000 * hardDriveSizeGb;
  const numberOfPhotos = Math.floor(hardDriveSizeMb / photoSizeMb);

  return numberOfPhotos;
}

const tests = [
  [1, 1, 1_000],
  [2, 1, 500],
  [4, 256, 64_000],
  [3.5, 750, 214_285],
  [3.5, 5.5, 1_571],
];

let failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length != 3) {
    continue;
  }

  const photoSizeMb = test[0];
  const hardDriveSizeGb = test[1];
  const expected = test[2];

  const actual = numberOfPhotos(photoSizeMb, hardDriveSizeGb);
  const success = expected === actual;

  console.log(`Testing ${photoSizeMb} and ${hardDriveSizeGb} (expecting ${expected})...${actual} (success: ${success}).`);

  if (!success) {
    failures.push([photoSizeMb, hardDriveSizeGb]);
  }
}

if (Array.isArray(failures) && failures.length !== 0) {
  console.log("The following inputs failed:");

  for (const failure of failures) {
    if (!Array.isArray(failure) || failure.length != 2) {
      continue;
    }

    console.log(`   ${photoSizeMb} and ${hardDriveSizeGb}.`);
  }
}
else {
  console.log("All tests past!");
}