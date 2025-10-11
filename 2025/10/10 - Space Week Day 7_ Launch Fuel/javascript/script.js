// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-10
function launchFuel(payload) {
  if (!Number.isFinite(payload) || payload < 0) {
    return undefined;
  }

  // Rockets require 1 kg of fuel per 5 kg of mass they must lift.
  const fuelInKgPerLiftMassInKg = 1 / 5;
  let totalPayloadInKg = payload;
  let fuelToLiftInKg = totalPayloadInKg * fuelInKgPerLiftMassInKg;
  let additionalFuelInKg = fuelToLiftInKg;
  let lastFuelToLiftInKg = fuelToLiftInKg;
  let totalFuelInKg = fuelToLiftInKg;
  totalPayloadInKg += fuelToLiftInKg;

  while (additionalFuelInKg >= 1) {
    fuelToLiftInKg = totalPayloadInKg * fuelInKgPerLiftMassInKg;
    additionalFuelInKg = Math.abs(lastFuelToLiftInKg - fuelToLiftInKg);
    lastFuelToLiftInKg = fuelToLiftInKg;
    totalFuelInKg += additionalFuelInKg;
    totalPayloadInKg += additionalFuelInKg;
  }

  return parseFloat(totalFuelInKg.toFixed(1));
}

const tests = [
  [50, 12.4],
  [500, 124.8],
  [243, 60.7],
  [11000, 2749.8],
  [6214, 1553.4],
];
const failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 2) {
    continue;
  }

  const payload = test[0];
  const expected = test[1];
  const actual = launchFuel(payload);
  const success = expected === actual;

  console.log(`Testing ${payload} (expecting ${expected})...${actual} (success: ${success}).`);

  if (!success) {
    failures.push();
  }
}

if (Array.isArray(failures) && failures.length !== 0) {
  console.log("The following inputs failed:");

  for (const failure of failures) {
    console.log(`  ${failure}.`);
  }
}
else {
  console.log("All tests passed!");
}
