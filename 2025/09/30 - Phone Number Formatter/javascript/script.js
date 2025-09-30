// https://www.freecodecamp.org/learn/daily-coding-challenge/2025-09-30
function formatNumber(number) {
  if (typeof number !== 'string' || number.length !== 11) {
    return undefined;
  }

  return `+${number[0]} (${number.slice(1, 4)}) ${number.slice(4, 7)}-${number.slice(7, 11)}`;
}

const tests = [
  ["05552340182", "+0 (555) 234-0182"],
  ["15554354792", "+1 (555) 435-4792"],
];
const failures = [];

for (const test of tests) {
  if (!Array.isArray(test) || test.length !== 2) {
    continue;
  }

  const number = test[0];
  const expected = test[1];
  const actual = formatNumber(number);
  const success = expected === actual;

  console.log(`Testing \"${number}\" (expecting \"${expected}\")...\"${actual}\" (success: ${success}).`);

  if (!success) {
    failures.push(number);
  }
}

if (Array.isArray(failures) && failures.length !== 0) {
  console.log("The following inputs failed:");

  for (const failure of failures) {
    console.log(`  \"${failure}\".`)
  }
}
else {
  console.log("All tests passed!");
}
