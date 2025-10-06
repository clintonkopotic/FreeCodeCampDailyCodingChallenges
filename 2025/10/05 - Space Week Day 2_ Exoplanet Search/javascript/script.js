// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-05
function hasExoplanet(readings) {
  if (typeof readings !== 'string') {
    return undefined;
  }

  const values = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
  let totalNumberOfReadings = 0;
  let sumOfReadings = 0;
  const readingValues = [];

  for (const readingCharacter of readings) {
    const value = values.indexOf(readingCharacter);

    if (value < 0) {
      return undefined;
    }

    totalNumberOfReadings++;
    sumOfReadings += value;
    readingValues.push(value);
  }

  if (totalNumberOfReadings === 0) {
    return undefined;
  }
  
  const averageReading = sumOfReadings / totalNumberOfReadings;

  if (!Number.isFinite(averageReading)) {
    return undefined;
  }

  const exoplanetMaxThresholdReading = 0.8 * averageReading;

  for (const readingValue of readingValues) {
    if (readingValue <= exoplanetMaxThresholdReading) {
      return true;
    }
  }

  return false;
}

const tests = [
  ["665544554", false],
  ["FGFFCFFGG", true],
  ["MONOPLONOMONPLNOMPNOMP", false],
  ["FREECODECAMP", true],
  ["9AB98AB9BC98A", false],
  ["ZXXWYZXYWYXZEGZXWYZXYGEE", true],
];
const failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 2) {
    continue;
  }

  const readings = test[0];
  const expected = test[1];
  const actual = hasExoplanet(readings);
  const success = expected === actual;

  // eslint-disable-next-line no-useless-escape
  console.log(`Testing \"${readings}\" (expecting ${expected})...${actual} (success: ${success}).`);

  if (!success) {
    failures.push(readings);
  }
}

if (Array.isArray(failures) && failures.length !== 0) {
  console.log("The following inputs failed:");

  for (const failure of failures) {
    // eslint-disable-next-line no-useless-escape
    console.log(`  \"${failure}.\"`)
  }
}
else {
  console.log("All tests passed!");
}
