// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-09-21
function numberOfFiles(fileSize, fileUnit, driveSizeGb) {
  const bytes = {
    "B": 1,
    "KB": 1_000,
    "MB": 1_000_000,
    "GB": 1_000_000_000,
  }

  if (!Number.isFinite(fileSize) || fileSize <= 0 || typeof fileUnit !== 'string' || fileUnit.length === 0 || !bytes.hasOwnProperty(fileUnit.toUpperCase()) || !Number.isFinite(driveSizeGb) || driveSizeGb < 0) {
    return undefined;
  }

  const driveSizeB = driveSizeGb * bytes["GB"];
  const fileSizeB = fileSize * bytes[fileUnit.toUpperCase()];
  const numberOfFiles = Math.floor(driveSizeB / fileSizeB);

  return numberOfFiles;
}

const tests = [
  [500, "KB", 1, 2_000],
  [50_000, "B", 1, 20_000],
  [4_096, "B", 1.5, 366_210],
  [220.5, "KB", 100, 453_514],
  [4.5, "MB", 750, 166_666],
];

let failures = [];

for (let test of tests) {
  if (!Array.isArray(test) || test.length != 4) {
    continue;
  }

  const fileSize = test[0];
  const fileUnit = test[1];
  const driveSizeGb = test[2];
  const expected = test[3];
  const actual = numberOfFiles(fileSize, fileUnit, driveSizeGb);
  const success = expected === actual;

  console.log (`Testing [${test}]]...${actual} (success: ${success}).`);

  if (!success) {
    failures.push(test);
  }
}

if (Array.isArray(failures) && failures.length !== 0) {
  console.log("The following inputs failed:");

  for (const failure of failures) {
    console.log(`  [${failure}].`);
  }
}
else {
  console.log("All tests passed!");
}
