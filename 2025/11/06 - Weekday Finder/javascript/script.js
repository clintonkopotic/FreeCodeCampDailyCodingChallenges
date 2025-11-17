// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-11-06
function getWeekday(dateString) {
  if (typeof dateString !== 'string' || dateString.length !== 10) {
    return undefined;
  }

  const daysOfTheWeek = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
  const dayOfTheWeekIndex = new Date(dateString).getUTCDay();

  return daysOfTheWeek[dayOfTheWeekIndex];
}

const tests = [
  ["2025-11-06", "Thursday"],
  ["1999-12-31", "Friday"],
  ["1111-11-11", "Saturday"],
  ["2112-12-21", "Wednesday"],
  ["2345-10-01", "Monday"],
];
const failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 2) {
    continue;
  }

  const dateString = test[0];
  const expected = test[1];
  const actual = getWeekday(dateString);
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
