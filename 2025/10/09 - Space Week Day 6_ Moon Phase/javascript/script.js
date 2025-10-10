// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-09
function moonPhase(dateString) {
  if (typeof dateString !== 'string' || dateString.length !== 10) {
    return undefined;
  }

  const date = new Date(dateString);

  if (isNaN(date.valueOf())) {
    return undefined;
  }

  const referenceNewMoon = new Date("2000-01-06");

  if (isNaN(referenceNewMoon.valueOf())) {
    return undefined;
  }

  const oneDayInMs = 1000 * 60 * 60 * 24; // Milliseconds in one day
  const diffInTime = date.getTime() - referenceNewMoon.getTime(); // Difference in milliseconds
  const diffInDays = Math.round(diffInTime / oneDayInMs); // Convert to days
  const dayInLunarCycle = diffInDays % 28 + 1;

  if (dayInLunarCycle >= 1 && dayInLunarCycle <=7) {
    return "New";
  }
  else if (dayInLunarCycle >= 8 && dayInLunarCycle <=14) {
    return "Waxing";
  }
  else if (dayInLunarCycle >= 15 && dayInLunarCycle <=21) {
    return "Full";
  }
  else if (dayInLunarCycle >= 22 && dayInLunarCycle <=28) {
    return "Waning";
  }

  return undefined;
}

const tests = [
  ["2000-01-12", "New"],
  ["2000-01-13", "Waxing"],
  ["2014-10-15", "Full"],
  ["2012-10-21", "Waning"],
  ["2022-12-14", "New"],
];
const failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 2) {
    continue;
  }

  const dateString = test[0];
  const expected = test[1];
  const actual = moonPhase(dateString);
  const success = expected === actual;

  // eslint-disable-next-line no-useless-escape
  console.log(`Testing \"${dateString}\" (expecting \"${expected}\")...\"${actual}\" (success: ${success}).`);

  if (!success) {
    failures.push(dateString);
  }
}

if (Array.isArray(failures) && failures.length !== 0) {
  console.log("The following inputs failed:");

  for (const failure of failures) {
    // eslint-disable-next-line no-useless-escape
    console.log(`  \"${failure}\".`);
  }
}
else {
  console.log("All tests passed!");
}
