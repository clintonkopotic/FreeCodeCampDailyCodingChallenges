// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-08-21
function milePace(miles, duration) {
  if (typeof miles !== 'number' || !Number.isFinite(miles) || miles < 0 || typeof duration !== 'string' || duration.length === 0) {
    return undefined;
  }

  const split = duration.split(':');

  if (!Array.isArray(split) || split.length !== 2) {
    return undefined;
  }

  let minutes = parseFloat(split[0]);
  let seconds = parseFloat(split[1]);
  const totalSeconds = (minutes * 60) + seconds;

  if (totalSeconds === 0) {
    return undefined;
  }

  let milePaceInSeconds = totalSeconds / miles;
  minutes = Math.floor(milePaceInSeconds / 60);

  if (minutes > 0) {
    milePaceInSeconds = milePaceInSeconds - (minutes * 60);
  }

  return minutes.toString().padStart(2, '0') + ':' + Math.round(milePaceInSeconds).toString().padStart(2, '0');
}

const tests = [
  [3, "24:00", "08:00"],
  [1, "06:45", "06:45"],
  [2, "07:00", "03:30"],
  [26.2, "120:35", "04:36"],
];
const failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 3) {
    continue;
  }

  const miles = test[0];
  const duration = test[1];
  const expected = test[2];
  const actual = milePace(miles, duration);
  const success = expected === actual;

  console.log(`Testing ${miles} and \"${duration}\" (expecting \"${expected}\")...\"${actual}\" (success: ${success}).`);

  if (!success) {
    failures.push([miles, duration]);
  }
}

if (Array.isArray(failures) && failures.length !== 0) {
  console.log("The following inputs failed:");

  for (const failure of failures) {
    if (!Array.isArray(failure) || failure.length !== 2) {
      continue;
    }

    const miles = failure[0];
    const duration = failure[1];

    console.log(`  ${miles} and \"${duration}\".`);
  }
}
else {
  console.log("All tests passed!");
}
