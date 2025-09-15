// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-09-15
function adjustThermostat(temp, target) {
  if (typeof temp !== 'number' || typeof target !== 'number') {
    return undefined;
  }

  let adjust = "hold";

  if (temp < target) {
    adjust = "heat";
  }
  else if (temp > target) {
    adjust = "cool"
  }

  return adjust;
}

const tests = [
  [[68, 72], "heat"],
  [[75, 72], "cool"],
  [[72, 72], "hold"],
  [[-20.5, -10.1], "heat"],
  [[100, 99.9], "cool"],
  [[0.0, 0.0], "hold"],
];

let failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 2) {
    continue;
  }

  const settings = test[0];
  const expected = test[1];

  if (!Array.isArray(settings) || settings.length !== 2 || typeof expected !== 'string') {
    continue;
  }

  const temp = settings[0];
  const target = settings[1];

  if (typeof temp !== 'number' || typeof target !== 'number') {
    continue;
  }

  const actual = adjustThermostat(temp, target);
  const success = expected === actual;
  console.log("Testing [" + settings + "] (expecting " + expected + ")..." + actual + " (success: " + success + ").");

  if (success !== true) {
    failures.push(settings);
  }
}

if (Array.isArray(failures) && failures.length > 0) {
  console.log("The following inputs failed:");

  for (const failure of failures) {
    console.log("  [" + failure + "].");
  }
}
else {
  console.log("All tests passed!");
}
