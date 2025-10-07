// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-06
function sendMessage(route) {
  if (!Array.isArray(route) || route.length === 0 || route.length === 1) {
    return undefined;
  }

  let distanceTraveledInKm = 0;
  // Start at negative one to not count the trip to the first satellite.
  let numberOfSatelitesPassedThrough = -1; 

  for (const value of route) {
    if (!Number.isFinite(value) || value < 0) {
      return undefined;
    }

    distanceTraveledInKm += value;
    numberOfSatelitesPassedThrough++;
  }

  const messageSpeedInKmPerSecond = 300_000;
  const delayOfMessageThroughSatelliteInSeconds = 0.5;
  const timeOfTravelInSeonds = distanceTraveledInKm / messageSpeedInKmPerSecond + delayOfMessageThroughSatelliteInSeconds * numberOfSatelitesPassedThrough;

  return parseFloat(timeOfTravelInSeonds.toFixed(4));
}

const tests = [
  [[300000, 300000], 2.5],
  [[384400, 384400], 3.0627],
  [[54600000, 54600000], 364.5],
  [[1000000, 500000000, 1000000], 1674.3333],
  [[10000, 21339, 50000, 31243, 10000], 2.4086],
  [[802101, 725994, 112808, 3625770, 481239], 21.1597],
];
const failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 2) {
    continue;
  }

  const route = test[0];
  const expected = test[1];
  const actual = sendMessage(route);
  const success = expected === actual;

  console.log(`Testing [${route}] (expecting ${expected})...${actual} (success: ${success}).`);

  if (!success) {
    failures.push(route);
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
