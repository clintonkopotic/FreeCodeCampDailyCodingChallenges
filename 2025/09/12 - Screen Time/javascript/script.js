// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-09-12
function tooMuchScreenTime(hours) {
  if (!Array.isArray(hours) || hours.length !== 7) {
    return undefined;
  }

  let sumOf7 = 0;
  let sumOf02 = 0;
  let sumOf13 = 0;
  let sumOf24 = 0;
  let sumOf35 = 0;
  let sumOf46 = 0;

  for (let i = 0; i < hours.length; i++) {
    let dayHours = hours[i];

    if (!Number.isInteger(dayHours)) {
      return undefined;
    }

    if (dayHours >= 10) {
      return true;
    }

    sumOf7 += dayHours;

    if (i >= 0 && i <= 2) {
      sumOf02 += dayHours;

      if (i === 2 && ((sumOf02 / 3) >= 8)) {
        return true;
      }
    }

    if (i >= 1 && i <= 3) {
      sumOf13 += dayHours;

      if (i === 3 && ((sumOf13 / 3) >= 8)) {
        return true;
      }
    }

    if (i >= 2 && i <= 4) {
      sumOf24 += dayHours;

      if (i === 4 && ((sumOf24 / 3) >= 8)) {
        return true;
      }
    }

    if (i >= 3 && i <= 5) {
      sumOf35 += dayHours;

      if (i === 5 && ((sumOf35 / 3) >= 8)) {
        return true;
      }
    }

    if (i >= 4 && i <= 6) {
      sumOf46 += dayHours;

      if (i === 6 && ((sumOf46 / 3) >= 8)) {
        return true;
      }
    }
  }

  return sumOf7 / 7 >= 6;
}

const tests = [
  [[1, 2, 3, 4, 5, 6, 7], false],
  [[7, 8, 8, 4, 2, 2, 3], false],
  [[5, 6, 6, 6, 6, 6, 6], false],
  [[1, 2, 3, 11, 1, 3, 4], true],
  [[1, 2, 3, 10, 2, 1, 0], true],
  [[3, 3, 5, 8, 8, 9, 4], true],
  [[3, 9, 4, 8, 5, 7, 6], true],
];

let failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 2) {
    continue;
  }

  const hours = test[0];
  const expected = test[1];

  if (!Array.isArray(hours) || hours.length !== 7 || typeof expected !== 'boolean') {
    continue;
  }

  const actual = tooMuchScreenTime(hours);
  const success = expected === actual;
  console.log("Testing [" + hours + "] (expecting " + expected + ")..." + actual + " (success: " + success + ").");

  if (success !== true) {
    failures.push(hours);
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
