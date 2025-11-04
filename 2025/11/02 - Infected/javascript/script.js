// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-11-02
function infected(days) {
  if (!Number.isInteger(days) || days < 0) {
    return undefined;
  }

  let numberOfComputersInfected = 1;

  for (let day = 1; day <= days; day++) {
    numberOfComputersInfected *= 2;

    if (day % 3 === 0) {
      const patched = Math.ceil(numberOfComputersInfected * 0.2);
      numberOfComputersInfected -= patched;
    }
  }

  return numberOfComputersInfected;
}

const tests = [
  [1, 2],
  [3, 6],
  [8, 152],
  [17, 39808],
  [25, 5217638],
];
const failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 2) {
    continue;
  }

  const days = test[0];
  const expected = test[1];
  const actual = infected(days);
  const success = expected === actual;

  console.log(`Testing ${days} (expecting ${expected})...${actual} (success: ${success}).`);

  if (!success) {
    failures.push(days);
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
