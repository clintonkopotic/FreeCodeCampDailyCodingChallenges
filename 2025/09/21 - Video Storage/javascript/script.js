// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-09-21
function numberOfVideos(videoSize, videoUnit, driveSize, driveUnit) {
  const allowedVideoUnits = ["KB", "MB", "GB"];
  const allowedDriveUnits = ["GB", "TB"]
  const bytes = {
    "B": 1,
    "KB": 1_000,
    "MB": 1_000_000,
    "GB": 1_000_000_000,
    "TB": 1_000_000_000_000,
  }

  if (!Number.isFinite(videoSize) || videoSize <= 0 || typeof videoUnit !== 'string' || videoUnit.length === 0 || !bytes.hasOwnProperty(videoUnit.toUpperCase()) || !Number.isFinite(driveSize) || driveSize <= 0 || typeof driveUnit !== 'string' || driveUnit.length === 0 || !bytes.hasOwnProperty(driveUnit.toUpperCase())) {
    return undefined;
  }

  if (!allowedVideoUnits.includes(videoUnit)) {
    return "Invalid video unit";
  }

  if (!allowedDriveUnits.includes(driveUnit)) {
    return "Invalid drive unit";
  }

  const driveSizeB = driveSize * bytes[driveUnit.toUpperCase()];
  const videoSizeB = videoSize * bytes[videoUnit.toUpperCase()];
  const numberOfVideos = Math.floor(driveSizeB / videoSizeB);

  return numberOfVideos;

}

const tests = [
  [500, "MB", 100, "GB", 200],
  [2_000, "B", 1, "TB", "Invalid video unit"],
  [2_000, "MB", 100_000, "MB", "Invalid drive unit"],
  [500_000, "KB", 2, "TB", 4_000],
  [1.5, "GB", 2.2, "TB", 1_466],
];

let failures = [];

for (let test of tests) {
  if (!Array.isArray(test) || test.length != 5) {
    continue;
  }

  const videoSize = test[0];
  const videoUnit = test[1];
  const driveSize = test[2];
  const driveUnit = test[3];
  const expected = test[4];
  const actual = numberOfVideos(videoSize, videoUnit, driveSize, driveUnit);
  const success = expected === actual;

  console.log(`Testing [${test}]]...${actual} (success: ${success}).`);

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
