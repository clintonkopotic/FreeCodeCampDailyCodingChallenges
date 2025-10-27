// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-26
function format(seconds) {
  if (!Number.isInteger(seconds) || seconds < 0) {
    return undefined;
  }

  const secondsPerHour = 3_600;
  const secondsPerMinute = 60;
  let remaining = seconds;
  let hours = 0;
  let result = "";

  if (remaining >= secondsPerHour) {
    hours = Math.floor(remaining / secondsPerHour);
    remaining = remaining % secondsPerHour;

    if (hours > 0) {
      result += `${hours}:`;
    }
  }

  let minutes = 0;

  if (remaining >= secondsPerMinute) {
    minutes = Math.floor(remaining / secondsPerMinute);
    remaining = remaining % secondsPerMinute;
  }

  result += `${hours > 0 ? String(minutes).padStart(2, '0') : minutes}:${String(remaining).padStart(2, '0')}`;

  return result;
}

const tests = [
  [500, "8:20"],
  [4000, "1:06:40"],
  [1, "0:01"],
  [5555, "1:32:35"],
  [99999, "27:46:39"],
];
const failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 2) {
    continue;
  }

  const seconds = test[0];
  const expected = test[1];
  const actual = format(seconds);
  const success = expected === actual;

  // eslint-disable-next-line no-useless-escape
  console.log(`Testing ${seconds} (expecting \"${expected}\")...\"${actual}\" (success: ${success}).`);

  if (!success) {
    failures.push(seconds);
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
