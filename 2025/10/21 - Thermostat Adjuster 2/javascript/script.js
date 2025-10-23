// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-21
function adjustThermostat(currentF, targetC) {
  if (!Number.isFinite(currentF) || !Number.isFinite(targetC)) {
    return undefined;
  }

  const targetF = targetC * 1.8 + 32;
  const differenceF = parseFloat((currentF - targetF).toFixed(1));

  if (differenceF === 0) {
    return "Hold";
  }
  else if (differenceF < 0) {
    return `Heat: ${(Math.abs(differenceF)).toFixed(1)} degrees Fahrenheit`;
  }
  else if (differenceF > 0) {
    return `Cool: ${differenceF.toFixed(1)} degrees Fahrenheit`;
  }

  return undefined;
}

const tests = [
  [32, 0, "Hold"],
  [70, 25, "Heat: 7.0 degrees Fahrenheit"],
  [72, 18, "Cool: 7.6 degrees Fahrenheit"],
  [212, 100, "Hold"],
  [59, 22, "Heat: 12.6 degrees Fahrenheit"],
];
const failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 3) {
    continue;
  }

  const currentF = test[0];
  const targetC = test[1];
  const expected = test[2];
  const actual = adjustThermostat(currentF, targetC);
  const success = expected === actual;

  // eslint-disable-next-line no-useless-escape
  console.log(`Testing ${currentF} and ${targetC} (expecting \"${expected}\")...\"${actual}\" (success: ${success}).`);

  if (!success) {
    failures.push([currentF, targetC]);
  }
}

if (Array.isArray(failures) && failures.length !== 0) {
  console.log("The following inputs failed:");

  for (const failure of failures) {
    if (!Array.isArray(failure) || failure.length !== 2) {
      continue;
    }

    const currentF = failure[0];
    const targetC = failure[1];
    console.log(`  ${currentF} and ${targetC}.`);
  }
}
else {
  console.log("All tests passed!");
}
