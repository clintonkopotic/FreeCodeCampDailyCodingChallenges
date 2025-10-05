// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-04
function classification(temp) {
  if (!Number.isFinite(temp) || temp < 0) {
    return undefined;
  }

  if (temp >= 30_000) {
    return "O";
  }
  else if (temp >= 10_000) {
    return "B";
  }
  else if (temp >= 7_500) {
    return "A";
  }
  else if (temp >= 6_000) {
    return "F";
  }
  else if (temp >= 5_200) {
    return "G";
  }
  else if (temp >= 3_700) {
    return "K";
  }
  else {
    return "M";
  }
}

const tests = [
  [5_778, "G"],
  [2_400, "M"],
  [9_999, "A"],
  [3_700, "K"],
  [3_699, "M"],
  [210_000, "O"],
  [6_000, "F"],
  [11_432, "B"],
];
const failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 2) {
    continue;
  }

  const temp = test[0];
  const expected = test[1];
  const actual = classification(temp);
  const success = expected === actual;

  // eslint-disable-next-line no-useless-escape
  console.log(`Testing ${temp} (expecting \"${expected}\")...\"${actual}\" (success: ${success}).`);

  if (!success) {
    failures.push(temp);
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
