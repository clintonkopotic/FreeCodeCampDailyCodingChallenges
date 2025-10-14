// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-10-13
function to12(time) {
  if (typeof time !== 'string' || time.length !== 4) {
    return undefined;
  }

  const hourOfDay = parseInt(time.slice(0, 2));
  const minuteOfHour = parseInt(time.slice(2));

  if (!Number.isInteger(hourOfDay) || hourOfDay < 0 || hourOfDay >= 24 || !Number.isInteger(minuteOfHour) || minuteOfHour < 0 || minuteOfHour >= 60) {
    return undefined;
  }

  const hourOfMeridian = hourOfDay === 0 || hourOfDay === 12 ? 12 : hourOfDay < 12 ? hourOfDay : hourOfDay - 12;
  const meridian = hourOfDay < 12 ? "AM" : "PM";

  return `${hourOfMeridian}:${minuteOfHour.toString().padStart(2, '0')} ${meridian}`;
}

const tests = [
  ["1124", "11:24 AM"],
  ["0900", "9:00 AM"],
  ["1455", "2:55 PM"],
  ["2346", "11:46 PM"],
  ["0030", "12:30 AM"],
];
const failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 2) {
    continue;
  }

  const time = test[0];
  const expected = test[1];
  const actual = to12(time);
  const success = expected === actual;

  // eslint-disable-next-line no-useless-escape
  console.log(`Testing \"${time}\" (expecting \"${expected}\")...\"${actual}\" (success: ${success}).`);

  if (!success) {
    failures.push(time);
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
